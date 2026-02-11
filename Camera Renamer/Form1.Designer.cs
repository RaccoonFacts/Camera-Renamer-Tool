namespace Camera_Renamer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.camListComboBox1 = new System.Windows.Forms.ComboBox();
            this.camSelBtn = new System.Windows.Forms.Button();
            this.selCamLbl1 = new System.Windows.Forms.Label();
            this.crntFriendlyNameLbl1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LogFriendlyNameBox = new System.Windows.Forms.TextBox();
            this.DIPBox = new System.Windows.Forms.TextBox();
            this.regPathLog1 = new System.Windows.Forms.TextBox();
            this.regBoxLog2 = new System.Windows.Forms.TextBox();
            this.verifyRegBtn = new System.Windows.Forms.Button();
            this.backupBtn = new System.Windows.Forms.Button();
            this.newNameBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.refBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // camListComboBox1
            // 
            this.camListComboBox1.FormattingEnabled = true;
            this.camListComboBox1.Location = new System.Drawing.Point(12, 25);
            this.camListComboBox1.Name = "camListComboBox1";
            this.camListComboBox1.Size = new System.Drawing.Size(168, 21);
            this.camListComboBox1.TabIndex = 0;
            // 
            // camSelBtn
            // 
            this.camSelBtn.Location = new System.Drawing.Point(204, 25);
            this.camSelBtn.Name = "camSelBtn";
            this.camSelBtn.Size = new System.Drawing.Size(94, 23);
            this.camSelBtn.TabIndex = 1;
            this.camSelBtn.Text = "Select Camera";
            this.camSelBtn.UseVisualStyleBackColor = true;
            this.camSelBtn.Click += new System.EventHandler(this.camSelBtn_Click);
            // 
            // selCamLbl1
            // 
            this.selCamLbl1.AutoSize = true;
            this.selCamLbl1.Location = new System.Drawing.Point(9, 9);
            this.selCamLbl1.Name = "selCamLbl1";
            this.selCamLbl1.Size = new System.Drawing.Size(79, 13);
            this.selCamLbl1.TabIndex = 2;
            this.selCamLbl1.Text = "Select Camera:";
            // 
            // crntFriendlyNameLbl1
            // 
            this.crntFriendlyNameLbl1.AutoSize = true;
            this.crntFriendlyNameLbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crntFriendlyNameLbl1.Location = new System.Drawing.Point(12, 66);
            this.crntFriendlyNameLbl1.Name = "crntFriendlyNameLbl1";
            this.crntFriendlyNameLbl1.Size = new System.Drawing.Size(134, 13);
            this.crntFriendlyNameLbl1.TabIndex = 3;
            this.crntFriendlyNameLbl1.Text = "Original FriendlyName:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Current Device Instance Path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(337, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Regex Path for 65e8773d-8f56-11d0-a3b9-00a0c9223196:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 257);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(334, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Regex Path for e5323777-f976-4f5b-9b55-b94699c46e44:";
            // 
            // LogFriendlyNameBox
            // 
            this.LogFriendlyNameBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LogFriendlyNameBox.Location = new System.Drawing.Point(15, 82);
            this.LogFriendlyNameBox.Name = "LogFriendlyNameBox";
            this.LogFriendlyNameBox.ReadOnly = true;
            this.LogFriendlyNameBox.Size = new System.Drawing.Size(151, 20);
            this.LogFriendlyNameBox.TabIndex = 11;
            this.LogFriendlyNameBox.WordWrap = false;
            // 
            // DIPBox
            // 
            this.DIPBox.Location = new System.Drawing.Point(15, 144);
            this.DIPBox.Multiline = true;
            this.DIPBox.Name = "DIPBox";
            this.DIPBox.ReadOnly = true;
            this.DIPBox.Size = new System.Drawing.Size(151, 25);
            this.DIPBox.TabIndex = 12;
            this.DIPBox.WordWrap = false;
            // 
            // regPathLog1
            // 
            this.regPathLog1.Location = new System.Drawing.Point(15, 214);
            this.regPathLog1.Multiline = true;
            this.regPathLog1.Name = "regPathLog1";
            this.regPathLog1.ReadOnly = true;
            this.regPathLog1.Size = new System.Drawing.Size(411, 25);
            this.regPathLog1.TabIndex = 13;
            this.regPathLog1.WordWrap = false;
            // 
            // regBoxLog2
            // 
            this.regBoxLog2.Location = new System.Drawing.Point(15, 283);
            this.regBoxLog2.Multiline = true;
            this.regBoxLog2.Name = "regBoxLog2";
            this.regBoxLog2.ReadOnly = true;
            this.regBoxLog2.Size = new System.Drawing.Size(411, 25);
            this.regBoxLog2.TabIndex = 14;
            this.regBoxLog2.WordWrap = false;
            // 
            // verifyRegBtn
            // 
            this.verifyRegBtn.Location = new System.Drawing.Point(15, 325);
            this.verifyRegBtn.Name = "verifyRegBtn";
            this.verifyRegBtn.Size = new System.Drawing.Size(92, 38);
            this.verifyRegBtn.TabIndex = 15;
            this.verifyRegBtn.Text = "Verify Regex Paths";
            this.verifyRegBtn.UseVisualStyleBackColor = true;
            this.verifyRegBtn.Click += new System.EventHandler(this.verifyRegBtn_Click);
            // 
            // backupBtn
            // 
            this.backupBtn.Location = new System.Drawing.Point(175, 325);
            this.backupBtn.Name = "backupBtn";
            this.backupBtn.Size = new System.Drawing.Size(92, 38);
            this.backupBtn.TabIndex = 16;
            this.backupBtn.Text = "Backup Regex Files";
            this.backupBtn.UseVisualStyleBackColor = true;
            this.backupBtn.Click += new System.EventHandler(this.backupBtn_Click);
            // 
            // newNameBox1
            // 
            this.newNameBox1.Location = new System.Drawing.Point(235, 82);
            this.newNameBox1.Name = "newNameBox1";
            this.newNameBox1.Size = new System.Drawing.Size(182, 20);
            this.newNameBox1.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(232, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "New Name:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(334, 325);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 38);
            this.button1.TabIndex = 19;
            this.button1.Text = "Change Camera\'s Name";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // refBtn
            // 
            this.refBtn.Location = new System.Drawing.Point(304, 25);
            this.refBtn.Name = "refBtn";
            this.refBtn.Size = new System.Drawing.Size(90, 23);
            this.refBtn.TabIndex = 20;
            this.refBtn.Text = "Refresh";
            this.refBtn.UseVisualStyleBackColor = true;
            this.refBtn.Click += new System.EventHandler(this.refBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 396);
            this.Controls.Add(this.refBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.newNameBox1);
            this.Controls.Add(this.backupBtn);
            this.Controls.Add(this.verifyRegBtn);
            this.Controls.Add(this.regBoxLog2);
            this.Controls.Add(this.regPathLog1);
            this.Controls.Add(this.DIPBox);
            this.Controls.Add(this.LogFriendlyNameBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.crntFriendlyNameLbl1);
            this.Controls.Add(this.selCamLbl1);
            this.Controls.Add(this.camSelBtn);
            this.Controls.Add(this.camListComboBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Camera Renamer Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox camListComboBox1;
        private System.Windows.Forms.Button camSelBtn;
        private System.Windows.Forms.Label selCamLbl1;
        private System.Windows.Forms.Label crntFriendlyNameLbl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox LogFriendlyNameBox;
        private System.Windows.Forms.TextBox DIPBox;
        private System.Windows.Forms.TextBox regPathLog1;
        private System.Windows.Forms.TextBox regBoxLog2;
        private System.Windows.Forms.Button verifyRegBtn;
        private System.Windows.Forms.Button backupBtn;
        private System.Windows.Forms.TextBox newNameBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button refBtn;
    }
}

