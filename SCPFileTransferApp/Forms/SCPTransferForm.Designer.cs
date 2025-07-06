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
            btnSelectLocalFile = new System.Windows.Forms.Button();
            txtLocalFilePath = new System.Windows.Forms.TextBox();
            btnSelectRemoteDirectory = new System.Windows.Forms.Button();
            treeViewRemoteDirectories = new System.Windows.Forms.TreeView();
            btnTransferFile = new System.Windows.Forms.Button();
            txtRemoteDirectoryPath = new System.Windows.Forms.TextBox();
            progressBar = new System.Windows.Forms.ProgressBar();
            lblFileSize = new System.Windows.Forms.Label();
            comboBoxMode = new System.Windows.Forms.ComboBox();
            comboBoxHosts = new System.Windows.Forms.ComboBox();
            lblTransferMode = new System.Windows.Forms.Label();
            lblSelectHost = new System.Windows.Forms.Label();
            pictureBoxPingStatus = new System.Windows.Forms.PictureBox();
            pictureBoxSSHStatus = new System.Windows.Forms.PictureBox();
            lblPingStatus = new System.Windows.Forms.Label();
            lblSSHStatus = new System.Windows.Forms.Label();
            btnSshConsole = new System.Windows.Forms.Button();
            panelDragDrop = new System.Windows.Forms.Panel();
            lblDragDrop = new System.Windows.Forms.Label();
            listView1 = new System.Windows.Forms.ListView();
            lblListOfVms = new System.Windows.Forms.Label();
            comboBoxPipelines = new System.Windows.Forms.ComboBox();
            textBox1 = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            lblVmDetails = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            listViewStatus = new System.Windows.Forms.ListView();
            Status = new System.Windows.Forms.ColumnHeader();
            btnJenkinsDownload = new System.Windows.Forms.Button();
            label8 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            textBox5 = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            label4 = new System.Windows.Forms.Label();
            textBox4 = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            textBox3 = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPingStatus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSSHStatus).BeginInit();
            panelDragDrop.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnSelectLocalFile
            // 
            btnSelectLocalFile.Location = new System.Drawing.Point(332, 388);
            btnSelectLocalFile.Name = "btnSelectLocalFile";
            btnSelectLocalFile.Size = new System.Drawing.Size(159, 23);
            btnSelectLocalFile.TabIndex = 0;
            btnSelectLocalFile.Text = "Browse Local Files";
            btnSelectLocalFile.UseVisualStyleBackColor = true;
            btnSelectLocalFile.Click += btnSelectLocalFile_Click;
            // 
            // txtLocalFilePath
            // 
            txtLocalFilePath.Location = new System.Drawing.Point(332, 417);
            txtLocalFilePath.Name = "txtLocalFilePath";
            txtLocalFilePath.Size = new System.Drawing.Size(407, 23);
            txtLocalFilePath.TabIndex = 1;
            // 
            // btnSelectRemoteDirectory
            // 
            btnSelectRemoteDirectory.Location = new System.Drawing.Point(332, 64);
            btnSelectRemoteDirectory.Name = "btnSelectRemoteDirectory";
            btnSelectRemoteDirectory.Size = new System.Drawing.Size(159, 23);
            btnSelectRemoteDirectory.TabIndex = 2;
            btnSelectRemoteDirectory.Text = "Browse Remote Directories";
            btnSelectRemoteDirectory.UseVisualStyleBackColor = true;
            btnSelectRemoteDirectory.Click += btnSelectRemoteDirectory_Click;
            // 
            // treeViewRemoteDirectories
            // 
            treeViewRemoteDirectories.Location = new System.Drawing.Point(332, 123);
            treeViewRemoteDirectories.Name = "treeViewRemoteDirectories";
            treeViewRemoteDirectories.Size = new System.Drawing.Size(407, 259);
            treeViewRemoteDirectories.TabIndex = 3;
            treeViewRemoteDirectories.BeforeExpand += treeViewRemoteDirectories_BeforeExpand;
            treeViewRemoteDirectories.AfterSelect += treeViewRemoteDirectories_AfterSelect;
            // 
            // btnTransferFile
            // 
            btnTransferFile.BackColor = System.Drawing.Color.LightGreen;
            btnTransferFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTransferFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
            btnTransferFile.Location = new System.Drawing.Point(334, 560);
            btnTransferFile.Name = "btnTransferFile";
            btnTransferFile.Size = new System.Drawing.Size(159, 23);
            btnTransferFile.TabIndex = 4;
            btnTransferFile.Text = "Start File Transfer ->";
            btnTransferFile.UseVisualStyleBackColor = false;
            btnTransferFile.Click += btnTransferFile_Click;
            // 
            // txtRemoteDirectoryPath
            // 
            txtRemoteDirectoryPath.Location = new System.Drawing.Point(332, 94);
            txtRemoteDirectoryPath.Name = "txtRemoteDirectoryPath";
            txtRemoteDirectoryPath.Size = new System.Drawing.Size(407, 23);
            txtRemoteDirectoryPath.TabIndex = 5;
            // 
            // progressBar
            // 
            progressBar.Location = new System.Drawing.Point(498, 560);
            progressBar.Name = "progressBar";
            progressBar.Size = new System.Drawing.Size(156, 22);
            progressBar.TabIndex = 6;
            // 
            // lblFileSize
            // 
            lblFileSize.AutoSize = true;
            lblFileSize.Location = new System.Drawing.Point(336, 586);
            lblFileSize.Name = "lblFileSize";
            lblFileSize.Size = new System.Drawing.Size(48, 15);
            lblFileSize.TabIndex = 7;
            lblFileSize.Text = "File Size";
            // 
            // comboBoxMode
            // 
            comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxMode.FormattingEnabled = true;
            comboBoxMode.Items.AddRange(new object[] { "Transfer to", "Transfer from" });
            comboBoxMode.Location = new System.Drawing.Point(332, 35);
            comboBoxMode.Name = "comboBoxMode";
            comboBoxMode.Size = new System.Drawing.Size(159, 23);
            comboBoxMode.TabIndex = 8;
            comboBoxMode.SelectedIndexChanged += comboBoxMode_SelectedIndexChanged;
            // 
            // comboBoxHosts
            // 
            comboBoxHosts.FormattingEnabled = true;
            comboBoxHosts.Location = new System.Drawing.Point(721, 29);
            comboBoxHosts.Name = "comboBoxHosts";
            comboBoxHosts.Size = new System.Drawing.Size(156, 23);
            comboBoxHosts.TabIndex = 9;
            comboBoxHosts.SelectedIndexChanged += comboBoxHosts_SelectedIndexChanged;
            // 
            // lblTransferMode
            // 
            lblTransferMode.AutoSize = true;
            lblTransferMode.Location = new System.Drawing.Point(334, 17);
            lblTransferMode.Name = "lblTransferMode";
            lblTransferMode.Size = new System.Drawing.Size(83, 15);
            lblTransferMode.TabIndex = 10;
            lblTransferMode.Text = "Transfer Mode";
            // 
            // lblSelectHost
            // 
            lblSelectHost.AutoSize = true;
            lblSelectHost.Location = new System.Drawing.Point(721, 9);
            lblSelectHost.Name = "lblSelectHost";
            lblSelectHost.Size = new System.Drawing.Size(110, 15);
            lblSelectHost.TabIndex = 11;
            lblSelectHost.Text = "Select Remote Host";
            // 
            // pictureBoxPingStatus
            // 
            pictureBoxPingStatus.Image = global::SCPFileTransferApp.Properties.Resources.RedCircle;
            pictureBoxPingStatus.Location = new System.Drawing.Point(516, 35);
            pictureBoxPingStatus.Name = "pictureBoxPingStatus";
            pictureBoxPingStatus.Size = new System.Drawing.Size(25, 25);
            pictureBoxPingStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxPingStatus.TabIndex = 12;
            pictureBoxPingStatus.TabStop = false;
            // 
            // pictureBoxSSHStatus
            // 
            pictureBoxSSHStatus.Image = global::SCPFileTransferApp.Properties.Resources.RedCircle;
            pictureBoxSSHStatus.Location = new System.Drawing.Point(578, 35);
            pictureBoxSSHStatus.Name = "pictureBoxSSHStatus";
            pictureBoxSSHStatus.Size = new System.Drawing.Size(25, 25);
            pictureBoxSSHStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxSSHStatus.TabIndex = 13;
            pictureBoxSSHStatus.TabStop = false;
            // 
            // lblPingStatus
            // 
            lblPingStatus.AutoSize = true;
            lblPingStatus.Location = new System.Drawing.Point(516, 17);
            lblPingStatus.Name = "lblPingStatus";
            lblPingStatus.Size = new System.Drawing.Size(31, 15);
            lblPingStatus.TabIndex = 14;
            lblPingStatus.Text = "Ping";
            // 
            // lblSSHStatus
            // 
            lblSSHStatus.AutoSize = true;
            lblSSHStatus.Location = new System.Drawing.Point(578, 17);
            lblSSHStatus.Name = "lblSSHStatus";
            lblSSHStatus.Size = new System.Drawing.Size(28, 15);
            lblSSHStatus.TabIndex = 15;
            lblSSHStatus.Text = "SSH";
            // 
            // btnSshConsole
            // 
            btnSshConsole.Location = new System.Drawing.Point(638, 35);
            btnSshConsole.Name = "btnSshConsole";
            btnSshConsole.Size = new System.Drawing.Size(101, 23);
            btnSshConsole.TabIndex = 16;
            btnSshConsole.Text = "SSH Console";
            btnSshConsole.UseVisualStyleBackColor = true;
            btnSshConsole.Click += btnSshConsole_Click;
            // 
            // panelDragDrop
            // 
            panelDragDrop.AllowDrop = true;
            panelDragDrop.BackColor = System.Drawing.SystemColors.ControlLight;
            panelDragDrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelDragDrop.Controls.Add(lblDragDrop);
            panelDragDrop.Location = new System.Drawing.Point(332, 446);
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
            // listView1
            // 
            listView1.Location = new System.Drawing.Point(35, 70);
            listView1.Name = "listView1";
            listView1.Size = new System.Drawing.Size(311, 600);
            listView1.TabIndex = 18;
            listView1.UseCompatibleStateImageBehavior = false;
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
            // comboBoxPipelines
            // 
            comboBoxPipelines.FormattingEnabled = true;
            comboBoxPipelines.Location = new System.Drawing.Point(6, 435);
            comboBoxPipelines.Name = "comboBoxPipelines";
            comboBoxPipelines.Size = new System.Drawing.Size(285, 23);
            comboBoxPipelines.TabIndex = 20;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(6, 35);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(120, 23);
            textBox1.TabIndex = 21;
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
            // groupBox1
            // 
            groupBox1.Controls.Add(listViewStatus);
            groupBox1.Controls.Add(btnJenkinsDownload);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(textBox5);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(textBox4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(treeViewRemoteDirectories);
            groupBox1.Controls.Add(btnSelectLocalFile);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtLocalFilePath);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(btnSelectRemoteDirectory);
            groupBox1.Controls.Add(comboBoxPipelines);
            groupBox1.Controls.Add(btnTransferFile);
            groupBox1.Controls.Add(txtRemoteDirectoryPath);
            groupBox1.Controls.Add(progressBar);
            groupBox1.Controls.Add(panelDragDrop);
            groupBox1.Controls.Add(lblFileSize);
            groupBox1.Controls.Add(btnSshConsole);
            groupBox1.Controls.Add(comboBoxMode);
            groupBox1.Controls.Add(lblSSHStatus);
            groupBox1.Controls.Add(lblTransferMode);
            groupBox1.Controls.Add(lblPingStatus);
            groupBox1.Controls.Add(pictureBoxPingStatus);
            groupBox1.Controls.Add(pictureBoxSSHStatus);
            groupBox1.Location = new System.Drawing.Point(375, 60);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(756, 610);
            groupBox1.TabIndex = 24;
            groupBox1.TabStop = false;
            // 
            // listViewStatus
            // 
            listViewStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { Status });
            listViewStatus.Location = new System.Drawing.Point(6, 493);
            listViewStatus.Name = "listViewStatus";
            listViewStatus.Size = new System.Drawing.Size(285, 108);
            listViewStatus.TabIndex = 37;
            listViewStatus.UseCompatibleStateImageBehavior = false;
            listViewStatus.View = System.Windows.Forms.View.List;
            // 
            // Status
            // 
            Status.Name = "Status";
            Status.Text = "";
            Status.Width = 285;
            // 
            // btnJenkinsDownload
            // 
            btnJenkinsDownload.Location = new System.Drawing.Point(6, 464);
            btnJenkinsDownload.Name = "btnJenkinsDownload";
            btnJenkinsDownload.Size = new System.Drawing.Size(75, 23);
            btnJenkinsDownload.TabIndex = 36;
            btnJenkinsDownload.Text = "Download";
            btnJenkinsDownload.UseVisualStyleBackColor = true;
            btnJenkinsDownload.Click += btnJenkinsDownload_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(9, 417);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(90, 15);
            label8.TabIndex = 35;
            label8.Text = "Jenkins pipeline";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(9, 332);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(282, 23);
            button1.TabIndex = 34;
            button1.Text = "Reload information";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            textBox5.Location = new System.Drawing.Point(9, 303);
            textBox5.Name = "textBox5";
            textBox5.Size = new System.Drawing.Size(282, 23);
            textBox5.TabIndex = 33;
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
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            label6.Location = new System.Drawing.Point(6, 378);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(212, 32);
            label6.TabIndex = 31;
            label6.Text = "Download artifacts";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 123);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(97, 15);
            label5.TabIndex = 30;
            label5.Text = "Installed versions";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(6, 141);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new System.Drawing.Size(285, 141);
            dataGridView1.TabIndex = 29;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(171, 68);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(21, 15);
            label4.TabIndex = 28;
            label4.Text = "Os";
            // 
            // textBox4
            // 
            textBox4.Location = new System.Drawing.Point(171, 86);
            textBox4.Name = "textBox4";
            textBox4.Size = new System.Drawing.Size(120, 23);
            textBox4.TabIndex = 27;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 68);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(62, 15);
            label3.TabIndex = 26;
            label3.Text = "IP Address";
            // 
            // textBox3
            // 
            textBox3.Location = new System.Drawing.Point(6, 86);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(120, 23);
            textBox3.TabIndex = 25;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(171, 17);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(32, 15);
            label2.TabIndex = 24;
            label2.Text = "Type";
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(171, 35);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(120, 23);
            textBox2.TabIndex = 23;
            // 
            // SCPTransferForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1147, 682);
            Controls.Add(groupBox1);
            Controls.Add(lblVmDetails);
            Controls.Add(lblListOfVms);
            Controls.Add(listView1);
            Controls.Add(lblSelectHost);
            Controls.Add(comboBoxHosts);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
            Text = "SCP File Transfer";
            ((System.ComponentModel.ISupportInitialize)pictureBoxPingStatus).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSSHStatus).EndInit();
            panelDragDrop.ResumeLayout(false);
            panelDragDrop.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ColumnHeader Status;

        private System.Windows.Forms.ListView listViewStatus;

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
        private System.Windows.Forms.ComboBox comboBoxHosts;
        private Label lblTransferMode;
        private System.Windows.Forms.Label lblSelectHost;
        private PictureBox pictureBoxPingStatus;
        private PictureBox pictureBoxSSHStatus;
        private Label lblPingStatus;
        private Label lblSSHStatus;
        private Button btnSshConsole;
        private Panel panelDragDrop;
        private Label lblDragDrop;
        private System.Windows.Forms.ListView listView1;
        private Label lblListOfVms;
        private System.Windows.Forms.ComboBox comboBoxPipelines;
        private TextBox textBox1;
        private Label label1;
        private Label lblVmDetails;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private Label label2;
        private TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnJenkinsDownload;
        private System.Windows.Forms.Label label8;
    }
    }
