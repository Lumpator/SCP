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
            SuspendLayout();
            // 
            // btnSelectLocalFile
            // 
            btnSelectLocalFile.Location = new Point(12, 79);
            btnSelectLocalFile.Name = "btnSelectLocalFile";
            btnSelectLocalFile.Size = new Size(159, 23);
            btnSelectLocalFile.TabIndex = 0;
            btnSelectLocalFile.Text = "Select Local File";
            btnSelectLocalFile.UseVisualStyleBackColor = true;
            btnSelectLocalFile.Click += btnSelectLocalFile_Click;
            // 
            // txtLocalFilePath
            // 
            txtLocalFilePath.Location = new Point(12, 108);
            txtLocalFilePath.Name = "txtLocalFilePath";
            txtLocalFilePath.Size = new Size(328, 23);
            txtLocalFilePath.TabIndex = 1;
            // 
            // btnSelectRemoteDirectory
            // 
            btnSelectRemoteDirectory.Location = new Point(462, 79);
            btnSelectRemoteDirectory.Name = "btnSelectRemoteDirectory";
            btnSelectRemoteDirectory.Size = new Size(159, 23);
            btnSelectRemoteDirectory.TabIndex = 2;
            btnSelectRemoteDirectory.Text = "Select Remote Directory";
            btnSelectRemoteDirectory.UseVisualStyleBackColor = true;
            btnSelectRemoteDirectory.Click += btnSelectRemoteDirectory_Click;
            // 
            // treeViewRemoteDirectories
            // 
            treeViewRemoteDirectories.Location = new Point(462, 137);
            treeViewRemoteDirectories.Name = "treeViewRemoteDirectories";
            treeViewRemoteDirectories.Size = new Size(328, 197);
            treeViewRemoteDirectories.TabIndex = 3;
            treeViewRemoteDirectories.BeforeExpand += treeViewRemoteDirectories_BeforeExpand;
            treeViewRemoteDirectories.AfterSelect += treeViewRemoteDirectories_AfterSelect;
            // 
            // btnTransferFile
            // 
            btnTransferFile.Location = new Point(346, 356);
            btnTransferFile.Name = "btnTransferFile";
            btnTransferFile.Size = new Size(110, 23);
            btnTransferFile.TabIndex = 4;
            btnTransferFile.Text = "Transfer File ->";
            btnTransferFile.UseVisualStyleBackColor = true;
            btnTransferFile.Click += btnTransferFile_Click;
            // 
            // txtRemoteDirectoryPath
            // 
            txtRemoteDirectoryPath.Location = new Point(462, 108);
            txtRemoteDirectoryPath.Name = "txtRemoteDirectoryPath";
            txtRemoteDirectoryPath.Size = new Size(328, 23);
            txtRemoteDirectoryPath.TabIndex = 5;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(346, 385);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(110, 23);
            progressBar.TabIndex = 6;
            // 
            // lblFileSize
            // 
            lblFileSize.AutoSize = true;
            lblFileSize.Location = new Point(346, 411);
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
            comboBoxMode.Location = new Point(346, 12);
            comboBoxMode.Name = "comboBoxMode";
            comboBoxMode.Size = new Size(110, 23);
            comboBoxMode.TabIndex = 8;
            comboBoxMode.SelectedIndexChanged += comboBoxMode_SelectedIndexChanged;
            // 
            // SCPTransferForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(881, 506);
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
        }
    }
