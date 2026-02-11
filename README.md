# Camera Renamer Tool

A Free Windows desktop application that allows you to rename your webcams and cameras for OBS, Streamlabs, Discord, Twitch, and more. It also changes the name for the Device Manager by modifying Windows registry entries. This will remain free. 

## 🎯 Purpose

Windows often assigns generic names to USB cameras like "USB2.0 HD UVC WebCam" or shows manufacturer names instead of meaningful identifiers. This tool lets you give your cameras custom, friendly names that appear in:
- Device Manager
- Windows Settings → Cameras
- DirectShow applications
- Any software that enumerates video devices

## ✨ Features

- **View All Connected Cameras** - Lists all video input devices detected by DirectShow
- **Registry Path Discovery** - Automatically finds the correct registry locations for each camera
- **Safe Registry Backup** - Creates timestamped backups before making any changes
- **Registry Path Verification** - Validates that registry paths exist before modification
- **Multi-Location Update** - Updates camera names in all three critical registry locations:
  - Device Manager registry path
  - DirectShow GUID path 1
  - DirectShow GUID path 2
- **Administrator Check** - Ensures the app has proper permissions before making changes

## 🖥️ System Requirements

- **OS**: Windows 7 or later
- **Framework**: .NET Framework 4.5+
- **Privileges**: Must be run as Administrator to modify registry
- **Dependencies**: DirectShowLib (included via NuGet)

## 📦 Installation

### Option 1: Download Release
1. Download the latest release from the [Releases](../../releases) page
2. Extract the ZIP file
3. Right-click `CameraRenamer.exe` → **Run as Administrator**

### Option 2: Build from Source
1. Clone this repository:
```bash
   git clone https://github.com/yourusername/camera-renamer.git
```
2. Open `Camera_Renamer.sln` in Visual Studio
3. Restore NuGet packages (DirectShowLib should auto-restore)
4. Build the solution (F6)
5. Run as Administrator

## 🚀 Usage

### Basic Workflow

1. **Launch as Administrator**
   - Right-click the executable → "Run as Administrator"

2. **Select Your Camera**
   - Choose a camera from the dropdown list
   - Click "Select Device"

3. **Verify Information**
   - Review the friendly name, device instance path, and registry paths
   - Click "Verify Registry Paths" to confirm they exist

4. **Create Backup** (Recommended)
   - Click "Backup Registry"
   - Backups are saved to `Documents\CameraRenamer_Backups\`

5. **Rename Camera**
   - Enter your desired camera name
   - Click "Update Name"
   - Confirm the action

6. **Apply Changes**
   - Reconnect your camera (unplug and replug USB)
   - Or restart your computer
   - Or click "Refresh" to reload the device list

### Important Notes

⚠️ **Administrator Rights Required**: Registry modifications require elevated privileges.

⚠️ **Changes Require Reconnection**: Windows caches device names. You must reconnect the camera or restart for changes to take effect.

✅ **Always Backup First**: Use the backup feature before making changes.

## 🗂️ Registry Locations Modified

The application updates the camera's FriendlyName in three registry locations:

1. **Device Manager Path**:
```
   HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\{DeviceInstancePath}
```

2. **DirectShow GUID Path 1**:
```
   HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\DeviceClasses\
   {65e8773d-8f56-11d0-a3b9-00a0c9223196}\...
```

3. **DirectShow GUID Path 2**:
```
   HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\DeviceClasses\
   {e5323777-f976-4f5b-9b55-b94699c46e44}\...
```

## 🛡️ Safety Features

- **Read-only verification** before modifications
- **Automatic registry backups** with timestamps
- **User confirmation dialogs** for destructive actions
- **Validation checks** for device selection and name input
- **Error handling** with informative messages

## 📸 Screenshots

<img width="460" height="422" alt="image" src="https://github.com/user-attachments/assets/f1d7e2cd-3c0d-4e20-87ae-5a4ca45e3e9a" />
<img width="933" height="501" alt="image" src="https://github.com/user-attachments/assets/fd4b4023-1f67-4757-a34a-93e4e581dda0" />
<img width="466" height="431" alt="image" src="https://github.com/user-attachments/assets/905acf9c-7148-449b-b626-16b91145dd69" />


## 🔧 Troubleshooting

### "Could not find device instance path"
- Click the "Refresh" button to reload the device list
- Ensure the camera is properly connected
- Try unplugging and replugging the camera

### "Access denied" or "Admin Rights Required"
- Close the application
- Right-click the executable → "Run as Administrator"

### Changes not showing after rename
- Disconnect and reconnect the USB camera
- Restart the computer
- Check Device Manager manually to verify the change

### Backup failed
- Ensure you have write permissions to your Documents folder
- Check available disk space

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## ⚖️ License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## ⚠️ Disclaimer

This software modifies Windows registry entries. While it includes safety features and backup functionality, **use at your own risk**. Always create backups before making changes. The authors are not responsible for any system damage or data loss.

## 🙏 Acknowledgments

- Uses [DirectShowLib](https://github.com/pauldotknopf/DirectShow.NET) for camera enumeration
- Built with Windows Forms and .NET Framework

## 📧 Support

If you encounter issues or have questions:
- Open an [Issue](../../issues)
- Check existing issues for solutions
- Provide detailed information about your camera model and Windows version

---

**Made with ❤️ for everyone tired of generic camera names**
