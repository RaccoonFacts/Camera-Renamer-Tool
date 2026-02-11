using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectShowLib;
using System.Management;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.Win32;
using System.Security.Principal;
using System.IO;

namespace Camera_Renamer
{
    public partial class Form1 : Form
    {

        string firstGuid = "65e8773d-8f56-11d0-a3b9-00a0c9223196";
        string secondGuid = "e5323777-f976-4f5b-9b55-b94699c46e44";
        string orgFriendlyName;
        //how i am not using this
        string DeviceInstancePath;


        string regPath1;
        string regPath2;


        public class CameraDeviceInfo
        {
            public string DirectShowName { get; set; }     // What combo box shows
            public string DeviceID { get; set; }           // The real stable identifier (\\?\...)
            public string FriendlyName { get; set; }       // Current friendly name from WMI
            public string OriginalFriendlyName { get; set; }
            public string RegPath1 { get; set; }
            public string RegPath2 { get; set; }

            public override string ToString() => DirectShowName; // for combo box
        }





        // Store the device instance path separately to use for matching
        private Dictionary<string, string> deviceNameToInstancePath = new Dictionary<string, string>();


        private DsDevice[] videoDevices;
        public Form1()
        {
            InitializeComponent();
            LoadVideoDevices();
        }
        //camListComboBox1
        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void LoadVideoDevices()
        {
            // Clear existing items
            camListComboBox1.Items.Clear();
            deviceNameToInstancePath.Clear();

            // Get all video input devices
            videoDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            // Add each device name to the ComboBox and map to instance path
            foreach (DsDevice device in videoDevices)
            {
                camListComboBox1.Items.Add(device.Name);

                // Try to get the instance path immediately and store it
                string instancePath = GetDeviceInstancePathByDirectShowName(device.Name);
                if (!string.IsNullOrEmpty(instancePath))
                {
                    deviceNameToInstancePath[device.Name] = instancePath;
                }
            }

            // Optionally select the first device
            if (camListComboBox1.Items.Count > 0)
            {
                camListComboBox1.SelectedIndex = 0;
            }

        }

        // New method to get instance path and cache it
        private string GetDeviceInstancePathByDirectShowName(string deviceName)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                    "SELECT * FROM Win32_PnPEntity WHERE PNPClass='Camera' OR PNPClass='Image'"
                );

                // Try exact match first
                foreach (ManagementObject device in searcher.Get())
                {
                    string name = device["Name"]?.ToString();
                    if (name != null && name.Equals(deviceName, StringComparison.OrdinalIgnoreCase))
                    {
                        return device["DeviceID"]?.ToString();
                    }
                }

                // Then try partial matches
                foreach (ManagementObject device in searcher.Get())
                {
                    string name = device["Name"]?.ToString();
                    if (name != null && (name.Contains(deviceName) || deviceName.Contains(name)))
                    {
                        return device["DeviceID"]?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Don't show error here, just return null
            }

            return null;
        }


        private void camSelBtn_Click(object sender, EventArgs e)
        {
            if (camListComboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a video device first.");
                return;
            }

            DsDevice selectedDevice = videoDevices[camListComboBox1.SelectedIndex];
            string selectedName = selectedDevice.Name;

            // Try to use cached instance path first
            string devicePath = null;
            if (deviceNameToInstancePath.ContainsKey(selectedName))
            {
                devicePath = deviceNameToInstancePath[selectedName];
            }
            else
            {
                // Fall back to searching
                devicePath = GetDeviceInstancePath(selectedName);
            }

            // Check if device path is null before proceeding
            if (string.IsNullOrEmpty(devicePath))
            {
                MessageBox.Show("Could not find device instance path. The device may have been renamed or disconnected.\n\nPlease click 'Refresh' to reload the device list.",
                    "Device Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string friendlyName = GetDeviceFriendlyName(selectedName);
            string firstDeviceLoc = GetGuidPath(devicePath, firstGuid);
            string secondDeviceLoc = GetGuidPath(devicePath, secondGuid);

            orgFriendlyName = friendlyName;
            DeviceInstancePath = devicePath;
            regPath1 = firstDeviceLoc;
            regPath2 = secondDeviceLoc;

            LogFriendlyNameBox.Text = friendlyName ?? "Friendly name not found.";
            DIPBox.Text = devicePath ?? "Device instance path not found.";
            regPathLog1.Text = firstDeviceLoc ?? "Could not construct registry path.";
            regBoxLog2.Text = secondDeviceLoc ?? "Could not construct registry path.";
        }



        //Get the Device Instance Path using WMI
        //Returns deviceID as string
        private string GetDeviceInstancePath(string deviceName)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                    "SELECT * FROM Win32_PnPEntity WHERE PNPClass='Camera' OR PNPClass='Image'"
                );

                // First try exact match
                foreach (ManagementObject device in searcher.Get())
                {
                    string name = device["Name"]?.ToString();
                    if (name != null && name.Equals(deviceName, StringComparison.OrdinalIgnoreCase))
                    {
                        return device["DeviceID"]?.ToString();
                    }
                }

                // Then try contains match
                foreach (ManagementObject device in searcher.Get())
                {
                    string name = device["Name"]?.ToString();
                    if (name != null && name.Contains(deviceName))
                    {
                        return device["DeviceID"]?.ToString();
                    }
                }

                // Finally try reverse contains (deviceName contains name)
                foreach (ManagementObject device in searcher.Get())
                {
                    string name = device["Name"]?.ToString();
                    if (name != null && deviceName.Contains(name))
                    {
                        return device["DeviceID"]?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            return null;
        }


        //gets friendly name of device
        //Returns friendly name as string
        private string GetDeviceFriendlyName(string deviceName)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                    "SELECT * FROM Win32_PnPEntity WHERE PNPClass='Camera' OR PNPClass='Image'"
                );

                foreach (ManagementObject device in searcher.Get())
                {
                    string name = device["Name"]?.ToString();

                    if (name != null && (name.Equals(deviceName, StringComparison.OrdinalIgnoreCase)
                        || name.Contains(deviceName)
                        || deviceName.Contains(name)))
                    {
                        // The "Caption" or "Name" property is typically the friendly name
                        string friendlyName = device["Caption"]?.ToString() ?? device["Name"]?.ToString();
                        return friendlyName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            return null;
        }




        private string GetGuidPath(string deviceInstancePath, string guid)
        {
            try
            {
                // Check if deviceInstancePath is null or empty
                if (string.IsNullOrEmpty(deviceInstancePath))
                {
                    MessageBox.Show("Device instance path is null. Please select a device first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                string formattedPath = "##?#" + deviceInstancePath.Replace("\\", "#");

                if (!guid.StartsWith("{"))
                {
                    guid = "{" + guid + "}";
                }

                string registryPath = $@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\DeviceClasses\{guid}\{formattedPath}#{guid}\#GLOBAL\Device Parameters";

                return registryPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error constructing registry path: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void verifyRegBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(regPath1) || string.IsNullOrEmpty(regPath2))
            {
                MessageBox.Show("Please select a device first.", "No Device Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool path1Exists = VerifyRegistryPath(regPath1);
            bool path2Exists = VerifyRegistryPath(regPath2);
            if (path1Exists) { label2.Text = "✓ Path 1 exists"; label2.ForeColor = Color.Green; } else { label2.Text = "✗ Path 1 not found"; label2.ForeColor = Color.Red; }
            if (path2Exists) { label3.Text = "✓ Path 2 exists"; label3.ForeColor = Color.Green; } else { label3.Text = "✗ Path 1 not found"; label3.ForeColor = Color.Red; }
            string result = "Registry Path Verification:\n\n";
            result += $"Path 1 ({firstGuid}):\n";
            result += path1Exists ? "✓ EXISTS\n" : "✗ NOT FOUND\n";
            result += $"{regPath1}\n\n";

            result += $"Path 2 ({secondGuid}):\n";
            result += path2Exists ? "✓ EXISTS\n" : "✗ NOT FOUND\n";
            result += $"{regPath2}";

            MessageBoxIcon icon = (path1Exists && path2Exists) ? MessageBoxIcon.Information : MessageBoxIcon.Warning;
            MessageBox.Show(result, "Registry Verification", MessageBoxButtons.OK, icon);
        }

        private bool VerifyRegistryPath(string fullRegistryPath)
        {
            try
            {
                // Remove "HKEY_LOCAL_MACHINE\" prefix to get the subkey path
                string subKeyPath = fullRegistryPath.Replace(@"HKEY_LOCAL_MACHINE\", "");

                // Try to open the registry key (read-only)
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(subKeyPath, writable: false))
                {
                    return key != null;
                }
            }
            catch (Exception ex)
            {
                // Key doesn't exist or access denied
                return false;
            }
        }




        private bool ExportRegistryKey(string fullRegistryPath, string outputFile)
        {
            try
            {
                string subKeyPath = fullRegistryPath.Replace(@"HKEY_LOCAL_MACHINE\", @"HKEY_LOCAL_MACHINE\");

                var processInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "reg.exe",
                    Arguments = $"export \"{subKeyPath}\" \"{outputFile}\" /y",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (var process = System.Diagnostics.Process.Start(processInfo))
                {
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
            catch
            {
                return false;
            }
        }

        private void backupBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(regPath1) || string.IsNullOrEmpty(regPath2))
            {
                MessageBox.Show("Please select a device first.");
                return;
            }

            // Create backup folder
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string backupFolder = Path.Combine(documentsPath, "CameraRenamer_Backups");
            Directory.CreateDirectory(backupFolder);

            // Create device-specific folder with timestamp
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string safeDeviceName = string.Join("_", orgFriendlyName.Split(Path.GetInvalidFileNameChars()));
            string deviceFolder = Path.Combine(backupFolder, $"{safeDeviceName}_{timestamp}");
            Directory.CreateDirectory(deviceFolder);

            // Export using reg.exe (Windows built-in tool)
            bool success1 = ExportRegistryKey(regPath1, Path.Combine(deviceFolder, "backup_path1.reg"));
            bool success2 = ExportRegistryKey(regPath2, Path.Combine(deviceFolder, "backup_path2.reg"));

            // Backup the Device Manager registry path
            string deviceMgrPath = $@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\{DeviceInstancePath}";
            bool success3 = ExportRegistryKey(deviceMgrPath, Path.Combine(deviceFolder, "backup_device_manager.reg"));

            if (success1 && success2 && success3)
            {
                MessageBox.Show($"Backup created successfully!\n\nAll 3 registry locations backed up.\n\n{deviceFolder}", "Success");
                System.Diagnostics.Process.Start("explorer.exe", deviceFolder);
            }
            else
            {
                MessageBox.Show($"Backup completed with errors.\nPath 1: {(success1 ? "✓" : "✗")}\nPath 2: {(success2 ? "✓" : "✗")}\nDevice Manager: {(success3 ? "✓" : "✗")}", "Warning");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrEmpty(regPath1) || string.IsNullOrEmpty(regPath2))
            {
                MessageBox.Show("Please select a device first.", "No Device Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(newNameBox1.Text))
            {
                MessageBox.Show("Please enter a new name.", "No Name Entered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check admin rights
            if (!IsAdministrator())
            {
                MessageBox.Show("This operation requires administrator privileges.\n\nPlease run the application as Administrator.",
                    "Admin Rights Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string newName = newNameBox1.Text.Trim();

            // Confirm with user
            DialogResult confirmResult = MessageBox.Show(
                $"Change camera name from:\n\"{orgFriendlyName}\"\n\nTo:\n\"{newName}\"\n\nThis will modify the registry. Continue?",
                "Confirm Name Change",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult != DialogResult.Yes)
                return;


            // Update Device Manager name this is important because of bullshit names like "USB2.0 HD UVC WebCam" that show up instead of the friendly name, and also because it seems to be the only way to reliably update the name that shows in the "Connected Cameras" section of Windows Settings
            bool successDevMgr = SetDeviceManagerFriendlyName(DeviceInstancePath, newName);
            // Update both registry paths
            bool success1 = SetRegistryFriendlyName(regPath1, newName);
            bool success2 = SetRegistryFriendlyName(regPath2, newName);

            // Show results
            if (success1 && success2 && successDevMgr)
            {
                MessageBox.Show($"Successfully updated FriendlyName in both registry locations!\n\nNew name: {newName}\n\nPlease reconnect the device or restart the application to see changes.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // DON'T reload devices immediately - the device needs to be reconnected first
                // LoadVideoDevices();
            }
            else if (success1 || success2 )

            {
                MessageBox.Show($"Partial success:\n\nPath 1: {(success1 ? "✓ Updated" : "✗ Failed")}\nPath 2: {(success2 ? "✓ Updated" : "✗ Failed")}\n\nSome registry keys may not exist for this device.",
                    "Partial Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Failed to update FriendlyName in both locations.\n\nThe registry keys may not exist or are inaccessible.",
                    "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool SetRegistryFriendlyName(string fullRegistryPath, string friendlyName)
        {
            try
            {
                // Remove HKEY_LOCAL_MACHINE prefix
                string subKeyPath = fullRegistryPath.Replace(@"HKEY_LOCAL_MACHINE\", "");

                // Open registry key with write access
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(subKeyPath, writable: true))
                {
                    if (key == null)
                    {
                        // Key doesn't exist, can't create it in this location
                        return false;
                    }

                    // Set or update the FriendlyName value
                    key.SetValue("FriendlyName", friendlyName, RegistryValueKind.String);

                    return true;
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access denied. Please run as administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating registry: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool IsAdministrator()
        {
            try
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                return false;
            }
        }

        private void refBtn_Click(object sender, EventArgs e)
        {
            LoadVideoDevices();
            MessageBox.Show("Device list refreshed!", "Refresh Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private bool SetDeviceManagerFriendlyName(string deviceInstancePath, string friendlyName)
        {
            try
            {
                string subKeyPath = $@"SYSTEM\CurrentControlSet\Enum\{deviceInstancePath}";

                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(subKeyPath, writable: true))
                {
                    if (key == null)
                    {
                        return false;
                    }

                    key.SetValue("FriendlyName", friendlyName, RegistryValueKind.String);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating Device Manager name: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}

