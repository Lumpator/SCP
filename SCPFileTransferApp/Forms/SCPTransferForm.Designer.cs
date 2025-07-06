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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SCPTransferForm));
            listViewVmList = new System.Windows.Forms.ListView();
            Vms = new System.Windows.Forms.ColumnHeader();
            lblListOfVms = new System.Windows.Forms.Label();
            lblVmDetails = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            pictureBoxSSHStatus = new System.Windows.Forms.PictureBox();
            pictureBoxPingStatus = new System.Windows.Forms.PictureBox();
            lblPingStatus = new System.Windows.Forms.Label();
            lblTransferMode = new System.Windows.Forms.Label();
            lblSSHStatus = new System.Windows.Forms.Label();
            comboBoxMode = new System.Windows.Forms.ComboBox();
            btnSshConsole = new System.Windows.Forms.Button();
            txtRemoteDirectoryPath = new System.Windows.Forms.TextBox();
            comboBoxPipelines = new System.Windows.Forms.ComboBox();
            btnSelectRemoteDirectory = new System.Windows.Forms.Button();
            txtVmName = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            treeViewRemoteDirectories = new System.Windows.Forms.TreeView();
            txtVmIp = new System.Windows.Forms.TextBox();
            txtLocalFilePath = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            btnTransferFile = new System.Windows.Forms.Button();
            txtVmOs = new System.Windows.Forms.TextBox();
            progressBar = new System.Windows.Forms.ProgressBar();
            label4 = new System.Windows.Forms.Label();
            panelDragDrop = new System.Windows.Forms.Panel();
            lblDragDrop = new System.Windows.Forms.Label();
            dgvInstalledVersions = new System.Windows.Forms.DataGridView();
            lblFileSize = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            btnSelectLocalFile = new System.Windows.Forms.Button();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            txtLastVmInstalledVersionReload = new System.Windows.Forms.TextBox();
            btnReloadVmInformation = new System.Windows.Forms.Button();
            label8 = new System.Windows.Forms.Label();
            btnJenkinsDownload = new System.Windows.Forms.Button();
            listViewStatus = new System.Windows.Forms.ListView();
            Status = new System.Windows.Forms.ColumnHeader();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSSHStatus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPingStatus).BeginInit();
            panelDragDrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInstalledVersions).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // listViewVmList
            // 
            listViewVmList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { Vms });
            listViewVmList.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            listViewVmList.FullRowSelect = true;
            listViewVmList.GridLines = true;
            listViewVmList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewVmList.Location = new System.Drawing.Point(35, 70);
            listViewVmList.MultiSelect = false;
            listViewVmList.Name = "listViewVmList";
            listViewVmList.Size = new System.Drawing.Size(310, 600);
            listViewVmList.TabIndex = 100;
            listViewVmList.UseCompatibleStateImageBehavior = false;
            listViewVmList.View = System.Windows.Forms.View.Details;
            listViewVmList.SelectedIndexChanged += listViewVmList_SelectedIndexChanged;
            // 
            // Vms
            // 
            Vms.Name = "Vms";
            Vms.Text = "";
            Vms.Width = 300;
            // 
            // lblListOfVms
            // 
            lblListOfVms.AutoSize = true;
            lblListOfVms.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            lblListOfVms.Location = new System.Drawing.Point(35, 25);
            lblListOfVms.Name = "lblListOfVms";
            lblListOfVms.Size = new System.Drawing.Size(132, 32);
            lblListOfVms.TabIndex = 19;
            lblListOfVms.Text = "List of VMs";
            // 
            // lblVmDetails
            // 
            lblVmDetails.AutoSize = true;
            lblVmDetails.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            lblVmDetails.Location = new System.Drawing.Point(375, 25);
            lblVmDetails.Name = "lblVmDetails";
            lblVmDetails.Size = new System.Drawing.Size(130, 32);
            lblVmDetails.TabIndex = 23;
            lblVmDetails.Text = "VM Details";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            label9.Location = new System.Drawing.Point(897, 25);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(142, 32);
            label9.TabIndex = 101;
            label9.Text = "File Transfer";
            // 
            // pictureBoxSSHStatus
            // 
            pictureBoxSSHStatus.Location = new System.Drawing.Point(252, 34);
            pictureBoxSSHStatus.Name = "pictureBoxSSHStatus";
            pictureBoxSSHStatus.Size = new System.Drawing.Size(25, 25);
            pictureBoxSSHStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxSSHStatus.TabIndex = 13;
            pictureBoxSSHStatus.TabStop = false;
            // 
            // pictureBoxPingStatus
            // 
            pictureBoxPingStatus.Location = new System.Drawing.Point(190, 34);
            pictureBoxPingStatus.Name = "pictureBoxPingStatus";
            pictureBoxPingStatus.Size = new System.Drawing.Size(25, 25);
            pictureBoxPingStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxPingStatus.TabIndex = 12;
            pictureBoxPingStatus.TabStop = false;
            // 
            // lblPingStatus
            // 
            lblPingStatus.AutoSize = true;
            lblPingStatus.Location = new System.Drawing.Point(190, 16);
            lblPingStatus.Name = "lblPingStatus";
            lblPingStatus.Size = new System.Drawing.Size(31, 15);
            lblPingStatus.TabIndex = 14;
            lblPingStatus.Text = "Ping";
            // 
            // lblTransferMode
            // 
            lblTransferMode.AutoSize = true;
            lblTransferMode.Location = new System.Drawing.Point(6, 16);
            lblTransferMode.Name = "lblTransferMode";
            lblTransferMode.Size = new System.Drawing.Size(83, 15);
            lblTransferMode.TabIndex = 10;
            lblTransferMode.Text = "Transfer Mode";
            // 
            // lblSSHStatus
            // 
            lblSSHStatus.AutoSize = true;
            lblSSHStatus.Location = new System.Drawing.Point(252, 16);
            lblSSHStatus.Name = "lblSSHStatus";
            lblSSHStatus.Size = new System.Drawing.Size(28, 15);
            lblSSHStatus.TabIndex = 15;
            lblSSHStatus.Text = "SSH";
            // 
            // comboBoxMode
            // 
            comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxMode.FormattingEnabled = true;
            comboBoxMode.Items.AddRange(new object[] { "Transfer to", "Transfer from" });
            comboBoxMode.Location = new System.Drawing.Point(6, 34);
            comboBoxMode.Name = "comboBoxMode";
            comboBoxMode.Size = new System.Drawing.Size(159, 23);
            comboBoxMode.TabIndex = 8;
            comboBoxMode.SelectedIndexChanged += comboBoxMode_SelectedIndexChanged;
            // 
            // btnSshConsole
            // 
            btnSshConsole.Location = new System.Drawing.Point(312, 34);
            btnSshConsole.Name = "btnSshConsole";
            btnSshConsole.Size = new System.Drawing.Size(101, 23);
            btnSshConsole.TabIndex = 16;
            btnSshConsole.Text = "SSH Console";
            btnSshConsole.UseVisualStyleBackColor = true;
            btnSshConsole.Click += btnSshConsole_Click;
            // 
            // txtRemoteDirectoryPath
            // 
            txtRemoteDirectoryPath.Location = new System.Drawing.Point(6, 92);
            txtRemoteDirectoryPath.Name = "txtRemoteDirectoryPath";
            txtRemoteDirectoryPath.Size = new System.Drawing.Size(407, 23);
            txtRemoteDirectoryPath.TabIndex = 5;
            // 
            // comboBoxPipelines
            // 
            comboBoxPipelines.FormattingEnabled = true;
            comboBoxPipelines.Location = new System.Drawing.Point(9, 406);
            comboBoxPipelines.Name = "comboBoxPipelines";
            comboBoxPipelines.Size = new System.Drawing.Size(282, 23);
            comboBoxPipelines.TabIndex = 20;
            // 
            // btnSelectRemoteDirectory
            // 
            btnSelectRemoteDirectory.Location = new System.Drawing.Point(6, 63);
            btnSelectRemoteDirectory.Name = "btnSelectRemoteDirectory";
            btnSelectRemoteDirectory.Size = new System.Drawing.Size(159, 23);
            btnSelectRemoteDirectory.TabIndex = 2;
            btnSelectRemoteDirectory.Text = "Browse Remote Directories";
            btnSelectRemoteDirectory.UseVisualStyleBackColor = true;
            btnSelectRemoteDirectory.Click += btnSelectRemoteDirectory_Click;
            // 
            // txtVmName
            // 
            txtVmName.Location = new System.Drawing.Point(6, 35);
            txtVmName.Name = "txtVmName";
            txtVmName.ReadOnly = true;
            txtVmName.Size = new System.Drawing.Size(120, 23);
            txtVmName.TabIndex = 21;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 17);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(39, 15);
            label1.TabIndex = 22;
            label1.Text = "Name";
            // 
            // treeViewRemoteDirectories
            // 
            treeViewRemoteDirectories.Location = new System.Drawing.Point(6, 129);
            treeViewRemoteDirectories.Name = "treeViewRemoteDirectories";
            treeViewRemoteDirectories.Size = new System.Drawing.Size(407, 259);
            treeViewRemoteDirectories.TabIndex = 3;
            treeViewRemoteDirectories.BeforeExpand += treeViewRemoteDirectories_BeforeExpand;
            treeViewRemoteDirectories.AfterSelect += treeViewRemoteDirectories_AfterSelect;
            // 
            // txtVmIp
            // 
            txtVmIp.Location = new System.Drawing.Point(132, 35);
            txtVmIp.Name = "txtVmIp";
            txtVmIp.ReadOnly = true;
            txtVmIp.Size = new System.Drawing.Size(120, 23);
            txtVmIp.TabIndex = 25;
            // 
            // txtLocalFilePath
            // 
            txtLocalFilePath.Location = new System.Drawing.Point(6, 423);
            txtLocalFilePath.Name = "txtLocalFilePath";
            txtLocalFilePath.Size = new System.Drawing.Size(407, 23);
            txtLocalFilePath.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(132, 19);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(62, 15);
            label3.TabIndex = 26;
            label3.Text = "IP Address";
            // 
            // btnTransferFile
            // 
            btnTransferFile.BackColor = System.Drawing.Color.LightGreen;
            btnTransferFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTransferFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
            btnTransferFile.Location = new System.Drawing.Point(8, 566);
            btnTransferFile.Name = "btnTransferFile";
            btnTransferFile.Size = new System.Drawing.Size(159, 23);
            btnTransferFile.TabIndex = 4;
            btnTransferFile.Text = "Start File Transfer ->";
            btnTransferFile.UseVisualStyleBackColor = false;
            btnTransferFile.Click += btnTransferFile_Click;
            // 
            // txtVmOs
            // 
            txtVmOs.Location = new System.Drawing.Point(259, 36);
            txtVmOs.Name = "txtVmOs";
            txtVmOs.ReadOnly = true;
            txtVmOs.Size = new System.Drawing.Size(256, 23);
            txtVmOs.TabIndex = 27;
            // 
            // progressBar
            // 
            progressBar.Location = new System.Drawing.Point(172, 566);
            progressBar.Name = "progressBar";
            progressBar.Size = new System.Drawing.Size(156, 22);
            progressBar.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(259, 16);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(21, 15);
            label4.TabIndex = 28;
            label4.Text = "Os";
            // 
            // panelDragDrop
            // 
            panelDragDrop.AllowDrop = true;
            panelDragDrop.BackColor = System.Drawing.SystemColors.ControlLight;
            panelDragDrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelDragDrop.Controls.Add(lblDragDrop);
            panelDragDrop.Location = new System.Drawing.Point(6, 452);
            panelDragDrop.Name = "panelDragDrop";
            panelDragDrop.Size = new System.Drawing.Size(407, 108);
            panelDragDrop.TabIndex = 17;
            panelDragDrop.DragDrop += panelDragDrop_DragDrop;
            panelDragDrop.DragEnter += panelDragDrop_DragEnter;
            // 
            // lblDragDrop
            // 
            lblDragDrop.AutoSize = true;
            lblDragDrop.Location = new System.Drawing.Point(144, 35);
            lblDragDrop.Name = "lblDragDrop";
            lblDragDrop.Size = new System.Drawing.Size(129, 15);
            lblDragDrop.TabIndex = 0;
            lblDragDrop.Text = "Drag and Drop file here\r\n";
            // 
            // dgvInstalledVersions
            // 
            dgvInstalledVersions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInstalledVersions.Location = new System.Drawing.Point(6, 86);
            dgvInstalledVersions.Name = "dgvInstalledVersions";
            dgvInstalledVersions.Size = new System.Drawing.Size(509, 196);
            dgvInstalledVersions.TabIndex = 29;
            // 
            // lblFileSize
            // 
            lblFileSize.AutoSize = true;
            lblFileSize.Location = new System.Drawing.Point(8, 592);
            lblFileSize.Name = "lblFileSize";
            lblFileSize.Size = new System.Drawing.Size(48, 15);
            lblFileSize.TabIndex = 7;
            lblFileSize.Text = "File Size";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 68);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(97, 15);
            label5.TabIndex = 30;
            label5.Text = "Installed versions";
            // 
            // btnSelectLocalFile
            // 
            btnSelectLocalFile.Location = new System.Drawing.Point(6, 394);
            btnSelectLocalFile.Name = "btnSelectLocalFile";
            btnSelectLocalFile.Size = new System.Drawing.Size(159, 23);
            btnSelectLocalFile.TabIndex = 0;
            btnSelectLocalFile.Text = "Browse Local Files";
            btnSelectLocalFile.UseVisualStyleBackColor = true;
            btnSelectLocalFile.Click += btnSelectLocalFile_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            label6.Location = new System.Drawing.Point(9, 350);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(212, 32);
            label6.TabIndex = 31;
            label6.Text = "Download artifacts";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(6, 285);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(64, 15);
            label7.TabIndex = 32;
            label7.Text = "Last reload";
            // 
            // txtLastVmInstalledVersionReload
            // 
            txtLastVmInstalledVersionReload.Location = new System.Drawing.Point(9, 303);
            txtLastVmInstalledVersionReload.Name = "txtLastVmInstalledVersionReload";
            txtLastVmInstalledVersionReload.ReadOnly = true;
            txtLastVmInstalledVersionReload.Size = new System.Drawing.Size(282, 23);
            txtLastVmInstalledVersionReload.TabIndex = 33;
            // 
            // btnReloadVmInformation
            // 
            btnReloadVmInformation.Location = new System.Drawing.Point(440, 302);
            btnReloadVmInformation.Name = "btnReloadVmInformation";
            btnReloadVmInformation.Size = new System.Drawing.Size(75, 23);
            btnReloadVmInformation.TabIndex = 34;
            btnReloadVmInformation.Text = "Reload information";
            btnReloadVmInformation.UseVisualStyleBackColor = true;
            btnReloadVmInformation.Click += btnReloadVmInformation_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(13, 388);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(90, 15);
            label8.TabIndex = 35;
            label8.Text = "Jenkins pipeline";
            // 
            // btnJenkinsDownload
            // 
            btnJenkinsDownload.Location = new System.Drawing.Point(440, 405);
            btnJenkinsDownload.Name = "btnJenkinsDownload";
            btnJenkinsDownload.Size = new System.Drawing.Size(75, 23);
            btnJenkinsDownload.TabIndex = 36;
            btnJenkinsDownload.Text = "Download";
            btnJenkinsDownload.UseVisualStyleBackColor = true;
            btnJenkinsDownload.Click += btnJenkinsDownload_Click;
            // 
            // listViewStatus
            // 
            listViewStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { Status });
            listViewStatus.Location = new System.Drawing.Point(6, 435);
            listViewStatus.Name = "listViewStatus";
            listViewStatus.Size = new System.Drawing.Size(509, 169);
            listViewStatus.TabIndex = 37;
            listViewStatus.UseCompatibleStateImageBehavior = false;
            listViewStatus.View = System.Windows.Forms.View.List;
            // 
            // Status
            // 
            Status.Name = "Status";
            Status.Text = "";
            Status.Width = 370;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listViewStatus);
            groupBox1.Controls.Add(btnJenkinsDownload);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(btnReloadVmInformation);
            groupBox1.Controls.Add(txtLastVmInstalledVersionReload);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(dgvInstalledVersions);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtVmOs);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtVmIp);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtVmName);
            groupBox1.Controls.Add(comboBoxPipelines);
            groupBox1.Location = new System.Drawing.Point(375, 60);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(521, 616);
            groupBox1.TabIndex = 24;
            groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(treeViewRemoteDirectories);
            groupBox2.Controls.Add(txtLocalFilePath);
            groupBox2.Controls.Add(btnSelectRemoteDirectory);
            groupBox2.Controls.Add(btnTransferFile);
            groupBox2.Controls.Add(btnSshConsole);
            groupBox2.Controls.Add(progressBar);
            groupBox2.Controls.Add(txtRemoteDirectoryPath);
            groupBox2.Controls.Add(lblSSHStatus);
            groupBox2.Controls.Add(lblTransferMode);
            groupBox2.Controls.Add(lblFileSize);
            groupBox2.Controls.Add(btnSelectLocalFile);
            groupBox2.Controls.Add(panelDragDrop);
            groupBox2.Controls.Add(comboBoxMode);
            groupBox2.Controls.Add(lblPingStatus);
            groupBox2.Controls.Add(pictureBoxSSHStatus);
            groupBox2.Controls.Add(pictureBoxPingStatus);
            groupBox2.Location = new System.Drawing.Point(902, 60);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(431, 616);
            groupBox2.TabIndex = 102;
            groupBox2.TabStop = false;
            // 
            // SCPTransferForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1345, 690);
            Controls.Add(groupBox2);
            Controls.Add(label9);
            Controls.Add(groupBox1);
            Controls.Add(lblVmDetails);
            Controls.Add(lblListOfVms);
            Controls.Add(listViewVmList);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
            Text = "SCP File Transfer";
            ((System.ComponentModel.ISupportInitialize)pictureBoxSSHStatus).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPingStatus).EndInit();
            panelDragDrop.ResumeLayout(false);
            panelDragDrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInstalledVersions).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.GroupBox groupBox2;

        private System.Windows.Forms.Label label9;

        private System.Windows.Forms.ColumnHeader Vms;

        private System.Windows.Forms.ColumnHeader Status;

        private System.Windows.Forms.ListView listViewStatus;

        #endregion

        private System.Windows.Forms.Button btnSelectLocalFile;
        private System.Windows.Forms.TextBox txtLocalFilePath;
        private System.Windows.Forms.Button btnSelectRemoteDirectory;
        private System.Windows.Forms.TreeView treeViewRemoteDirectories;
        private System.Windows.Forms.Button btnTransferFile;
        private System.Windows.Forms.TextBox txtRemoteDirectoryPath;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblFileSize;
        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.Windows.Forms.Label lblTransferMode;
        private System.Windows.Forms.PictureBox pictureBoxPingStatus;
        private System.Windows.Forms.PictureBox pictureBoxSSHStatus;
        private System.Windows.Forms.Label lblPingStatus;
        private System.Windows.Forms.Label lblSSHStatus;
        private System.Windows.Forms.Button btnSshConsole;
        private System.Windows.Forms.Panel panelDragDrop;
        private System.Windows.Forms.Label lblDragDrop;
        private System.Windows.Forms.ListView listViewVmList;
        private Label lblListOfVms;
        private System.Windows.Forms.ComboBox comboBoxPipelines;
        private System.Windows.Forms.TextBox txtVmName;
        private System.Windows.Forms.Label label1;
        private Label lblVmDetails;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvInstalledVersions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtVmOs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVmIp;
        private System.Windows.Forms.Button btnReloadVmInformation;
        private System.Windows.Forms.TextBox txtLastVmInstalledVersionReload;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnJenkinsDownload;
        private System.Windows.Forms.Label label8;
    }
    }
