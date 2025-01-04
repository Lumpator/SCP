namespace SCPFileTransferApp
    {
    partial class SCPTransferForm
        {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
            {
            btnSelectLocalFile = new Button();
            txtLocalFilePath = new TextBox();
            btnSelectRemoteDirectory = new Button();
            treeViewRemoteDirectories = new TreeView();
            btnTransferFile = new Button();
            txtRemoteDirectoryPath = new TextBox();
            progressBar = new ProgressBar();
            lblFileSize = new Label();
            comboBoxMode = new ComboBox();
            comboBoxHosts = new ComboBox();
            lblTransferMode = new Label();
            lblSelectHost = new Label();
            pictureBoxPingStatus = new PictureBox();
            pictureBoxSSHStatus = new PictureBox();
            lblPingStatus = new Label();
            lblSSHStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPingStatus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSSHStatus).BeginInit();
            SuspendLayout();
            // 
            // btnSelectLocalFile
            // 
            btnSelectLocalFile.Location = new Point(12, 117);
            btnSelectLocalFile.Name = "btnSelectLocalFile";
            btnSelectLocalFile.Size = new Size(159, 23);
            btnSelectLocalFile.TabIndex = 0;
            btnSelectLocalFile.Text = "Browse Local Files";
            btnSelectLocalFile.UseVisualStyleBackColor = true;
            btnSelectLocalFile.Click += btnSelectLocalFile_Click;
            // 
            // txtLocalFilePath
            // 
            txtLocalFilePath.Location = new Point(12, 146);
            txtLocalFilePath.Name = "txtLocalFilePath";
            txtLocalFilePath.Size = new Size(407, 23);
            txtLocalFilePath.TabIndex = 1;
            // 
            // btnSelectRemoteDirectory
            // 
            btnSelectRemoteDirectory.Location = new Point(462, 117);
            btnSelectRemoteDirectory.Name = "btnSelectRemoteDirectory";
            btnSelectRemoteDirectory.Size = new Size(159, 23);
            btnSelectRemoteDirectory.TabIndex = 2;
            btnSelectRemoteDirectory.Text = "Browse Remote Directories";
            btnSelectRemoteDirectory.UseVisualStyleBackColor = true;
            btnSelectRemoteDirectory.Click += btnSelectRemoteDirectory_Click;
            // 
            // treeViewRemoteDirectories
            // 
            treeViewRemoteDirectories.Location = new Point(462, 175);
            treeViewRemoteDirectories.Name = "treeViewRemoteDirectories";
            treeViewRemoteDirectories.Size = new Size(407, 253);
            treeViewRemoteDirectories.TabIndex = 3;
            treeViewRemoteDirectories.BeforeExpand += treeViewRemoteDirectories_BeforeExpand;
            treeViewRemoteDirectories.AfterSelect += treeViewRemoteDirectories_AfterSelect;
            // 
            // btnTransferFile
            // 
            btnTransferFile.Location = new Point(346, 427);
            btnTransferFile.Name = "btnTransferFile";
            btnTransferFile.Size = new Size(110, 23);
            btnTransferFile.TabIndex = 4;
            btnTransferFile.Text = "Transfer File ->";
            btnTransferFile.UseVisualStyleBackColor = true;
            btnTransferFile.Click += btnTransferFile_Click;
            // 
            // txtRemoteDirectoryPath
            // 
            txtRemoteDirectoryPath.Location = new Point(462, 146);
            txtRemoteDirectoryPath.Name = "txtRemoteDirectoryPath";
            txtRemoteDirectoryPath.Size = new Size(407, 23);
            txtRemoteDirectoryPath.TabIndex = 5;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(346, 456);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(110, 23);
            progressBar.TabIndex = 6;
            // 
            // lblFileSize
            // 
            lblFileSize.AutoSize = true;
            lblFileSize.Location = new Point(346, 482);
            lblFileSize.Name = "lblFileSize";
            lblFileSize.Size = new Size(48, 15);
            lblFileSize.TabIndex = 7;
            lblFileSize.Text = "File Size";
            // 
            // comboBoxMode
            // 
            comboBoxMode.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMode.FormattingEnabled = true;
            comboBoxMode.Items.AddRange(new object [] { "Transfer to", "Transfer from" });
            comboBoxMode.Location = new Point(346, 24);
            comboBoxMode.Name = "comboBoxMode";
            comboBoxMode.Size = new Size(110, 23);
            comboBoxMode.TabIndex = 8;
            comboBoxMode.SelectedIndexChanged += comboBoxMode_SelectedIndexChanged;
            // 
            // comboBoxHosts
            // 
            comboBoxHosts.FormattingEnabled = true;
            comboBoxHosts.Location = new Point(465, 88);
            comboBoxHosts.Name = "comboBoxHosts";
            comboBoxHosts.Size = new Size(156, 23);
            comboBoxHosts.TabIndex = 9;
            comboBoxHosts.SelectedIndexChanged += comboBoxHosts_SelectedIndexChanged;
            // 
            // lblTransferMode
            // 
            lblTransferMode.AutoSize = true;
            lblTransferMode.Location = new Point(361, 6);
            lblTransferMode.Name = "lblTransferMode";
            lblTransferMode.Size = new Size(82, 15);
            lblTransferMode.TabIndex = 10;
            lblTransferMode.Text = "Transfer Mode";
            // 
            // lblSelectHost
            // 
            lblSelectHost.AutoSize = true;
            lblSelectHost.Location = new Point(465, 70);
            lblSelectHost.Name = "lblSelectHost";
            lblSelectHost.Size = new Size(110, 15);
            lblSelectHost.TabIndex = 11;
            lblSelectHost.Text = "Select Remote Host";
            // 
            // pictureBoxPingStatus
            // 
            pictureBoxPingStatus.Image = Properties.Resources.RedCircle;
            pictureBoxPingStatus.Location = new Point(664, 88);
            pictureBoxPingStatus.Name = "pictureBoxPingStatus";
            pictureBoxPingStatus.Size = new Size(25, 25);
            pictureBoxPingStatus.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPingStatus.TabIndex = 12;
            pictureBoxPingStatus.TabStop = false;
            // 
            // pictureBoxSSHStatus
            // 
            pictureBoxSSHStatus.Image = Properties.Resources.RedCircle;
            pictureBoxSSHStatus.Location = new Point(723, 88);
            pictureBoxSSHStatus.Name = "pictureBoxSSHStatus";
            pictureBoxSSHStatus.Size = new Size(25, 25);
            pictureBoxSSHStatus.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxSSHStatus.TabIndex = 13;
            pictureBoxSSHStatus.TabStop = false;
            // 
            // lblPingStatus
            // 
            lblPingStatus.AutoSize = true;
            lblPingStatus.Location = new Point(664, 70);
            lblPingStatus.Name = "lblPingStatus";
            lblPingStatus.Size = new Size(31, 15);
            lblPingStatus.TabIndex = 14;
            lblPingStatus.Text = "Ping";
            // 
            // lblSSHStatus
            // 
            lblSSHStatus.AutoSize = true;
            lblSSHStatus.Location = new Point(723, 70);
            lblSSHStatus.Name = "lblSSHStatus";
            lblSSHStatus.Size = new Size(28, 15);
            lblSSHStatus.TabIndex = 15;
            lblSSHStatus.Text = "SSH";
            // 
            // SCPTransferForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(881, 506);
            Controls.Add(lblSSHStatus);
            Controls.Add(lblPingStatus);
            Controls.Add(pictureBoxSSHStatus);
            Controls.Add(pictureBoxPingStatus);
            Controls.Add(lblSelectHost);
            Controls.Add(lblTransferMode);
            Controls.Add(comboBoxHosts);
            Controls.Add(comboBoxMode);
            Controls.Add(lblFileSize);
            Controls.Add(progressBar);
            Controls.Add(txtRemoteDirectoryPath);
            Controls.Add(btnTransferFile);
            Controls.Add(treeViewRemoteDirectories);
            Controls.Add(btnSelectRemoteDirectory);
            Controls.Add(txtLocalFilePath);
            Controls.Add(btnSelectLocalFile);
            Name = "SCPTransferForm";
            Text = "SCP File Transfer";
            ((System.ComponentModel.ISupportInitialize)pictureBoxPingStatus).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSSHStatus).EndInit();
            ResumeLayout(false);
            PerformLayout();
            }

        #endregion

        private Button btnSelectLocalFile;
        private TextBox txtLocalFilePath;
        private Button btnSelectRemoteDirectory;
        private TreeView treeViewRemoteDirectories;
        private Button btnTransferFile;
        private TextBox txtRemoteDirectoryPath;
        private ProgressBar progressBar;
        private Label lblFileSize;
        private ComboBox comboBoxMode;
        private ComboBox comboBoxHosts;
        private Label lblTransferMode;
        private Label lblSelectHost;
        private PictureBox pictureBoxPingStatus;
        private PictureBox pictureBoxSSHStatus;
        private Label lblPingStatus;
        private Label lblSSHStatus;
        }
    }
