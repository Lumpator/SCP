using System;
using System.IO;
using System.Windows.Forms;
using SCPFileTransferApp.Services;
using System.Diagnostics;

namespace SCPFileTransferApp
    {
    public partial class SCPTransferForm : Form
        {
        private string localFilePath;
        private string remoteDirectoryPath;
        private SftpService sftpService;

        public SCPTransferForm()
            {
            InitializeComponent();
            sftpService = new SftpService();
            comboBoxMode.SelectedIndex = 0;
            }


        private void btnSelectLocalFile_Click(object sender, EventArgs e)
            {
            if (comboBoxMode.SelectedItem.ToString() == "Transfer to")
                {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                        localFilePath = openFileDialog.FileName;
                        txtLocalFilePath.Text = localFilePath;

                        UpdateFileSizeLabel();
                        }
                    }
                }
            else if (comboBoxMode.SelectedItem.ToString() == "Transfer from")
                {
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                    {
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                        localFilePath = folderBrowserDialog.SelectedPath;
                        txtLocalFilePath.Text = localFilePath;
                        }
                    }
                }
            }

        private void btnSelectRemoteDirectory_Click(object sender, EventArgs e)
            {
            try
                {
                sftpService.Disconnect();
                sftpService.Connect();
                if (comboBoxMode.SelectedItem.ToString() == "Transfer to")
                    {
                    LoadRemoteDirectories("/");
                    }
                else if (comboBoxMode.SelectedItem.ToString() == "Transfer from")
                    {
                    LoadRemoteFiles("/");
                    }
                }
            catch (Exception ex)
                {
                MessageBox.Show("Error: " + ex.Message);
                }
            }

        private void LoadRemoteDirectories(string path)
            {
            treeViewRemoteDirectories.Nodes.Clear();
            var rootDirectory = sftpService.ListDirectory(path);
            foreach (var item in rootDirectory)
                {
                if (item.IsDirectory && item.Name != "." && item.Name != "..")
                    {
                    var node = new TreeNode(item.Name);
                    node.Tag = item.FullName;
                    node.Nodes.Add("Loading..."); // Placeholder for lazy loading
                    treeViewRemoteDirectories.Nodes.Add(node);
                    }
                }
            }
        private void LoadRemoteFiles(string path)
            {
            treeViewRemoteDirectories.Nodes.Clear();
            var rootDirectory = sftpService.ListDirectory(path);
            foreach (var item in rootDirectory)
                {
                var node = new TreeNode(item.Name);
                node.Tag = item.FullName;
                if (item.IsDirectory)
                    {
                    node.Nodes.Add("Loading..."); // Placeholder for lazy loading
                    }
                treeViewRemoteDirectories.Nodes.Add(node);
                }
            }

        private void treeViewRemoteDirectories_BeforeExpand(object sender, TreeViewCancelEventArgs e)
            {
            if (e.Node.Nodes [0].Text == "Loading...")
                {
                e.Node.Nodes.Clear();
                var subItems = sftpService.ListDirectory(e.Node.Tag.ToString());
                foreach (var item in subItems)
                    {
                    var node = new TreeNode(item.Name);
                    node.Tag = item.FullName;
                    if (item.IsDirectory && item.Name != "." && item.Name != "..")
                        {
                        node.Nodes.Add("Loading...");
                        }
                    e.Node.Nodes.Add(node);
                    }
                }
            }

        private void treeViewRemoteDirectories_AfterSelect(object sender, TreeViewEventArgs e)
            {
            var selectedNode = e.Node;
            var selectedPath = selectedNode.Tag.ToString();

            if (comboBoxMode.SelectedItem.ToString() == "Transfer to")
                {
                if (selectedNode.Nodes.Count > 0) // It's a directory
                    {
                    remoteDirectoryPath = selectedPath;
                    txtRemoteDirectoryPath.Text = remoteDirectoryPath;
                    }
                else
                    {
                    MessageBox.Show("Please select a directory.");
                    treeViewRemoteDirectories.SelectedNode = null;
                    }
                }
            else if (comboBoxMode.SelectedItem.ToString() == "Transfer from")
                {
                if (selectedNode.Nodes.Count == 0) // It's a file
                    {
                    remoteDirectoryPath = selectedPath;
                    txtRemoteDirectoryPath.Text = remoteDirectoryPath;

                    // Get the file size from the remote file attributes
                    var fileAttributes = sftpService.GetFileAttributes(remoteDirectoryPath);
                    UpdateFileSizeLabel(fileAttributes.Size);
                    }
                else
                    {
                    MessageBox.Show("Please select a file.");
                    treeViewRemoteDirectories.SelectedNode = null;
                    }
                }
            }

        private async void btnTransferFile_Click(object sender, EventArgs e)
            {
            localFilePath = txtLocalFilePath.Text;
            remoteDirectoryPath = txtRemoteDirectoryPath.Text; // Convert backslashes to forward slashes for SFTP


            if (string.IsNullOrEmpty(localFilePath) || string.IsNullOrEmpty(remoteDirectoryPath))
                {
                MessageBox.Show("Please select both local file and remote directory.");
                return;
                }

            try
                {
                if (comboBoxMode.SelectedItem.ToString() == "Transfer to")
                    {
                    await sftpService.UploadFileAsync(localFilePath, remoteDirectoryPath, progress =>
                    {
                        this.Invoke((MethodInvoker)delegate
                            {
                                progressBar.Value = (int)progress;
                                });
                    });

                    MessageBox.Show("File transferred successfully.");
                    }
                else if (comboBoxMode.SelectedItem.ToString() == "Transfer from")
                    {
                    await sftpService.DownloadFileAsync(remoteDirectoryPath, localFilePath, progress =>
                    {
                        this.Invoke((MethodInvoker)delegate
                            {
                                progressBar.Value = (int)progress;
                                });
                    });

                    MessageBox.Show("File downloaded successfully.");
                    }

                progressBar.Value = 0;
                }
            catch (Exception ex)
                {
                MessageBox.Show("Error: " + ex.Message);
                }
            }
        private void UpdateFileSizeLabel(long? fileSize = null)
            {
            if (fileSize.HasValue)
                {
                lblFileSize.Text = $"File Size: {fileSize.Value / 1024.0 / 1024.0:F2} MB";
                }
            else if (!string.IsNullOrEmpty(localFilePath))
                {
                FileInfo fileInfo = new FileInfo(localFilePath);
                lblFileSize.Text = $"File Size: {fileInfo.Length / 1024.0 / 1024.0:F2} MB";
                }
            }

        private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
            {
            if (comboBoxMode.SelectedItem.ToString() == "Transfer to")
                {
                lblFileSize.Text = "File Size:";
                btnTransferFile.Text = "Transfer ->";
                btnSelectLocalFile.Text = "Select Local File";
                btnSelectRemoteDirectory.Text = "Select Remote Directory";
                }
            else if (comboBoxMode.SelectedItem.ToString() == "Transfer from")
                {
                lblFileSize.Text = "File Size:";
                btnTransferFile.Text = "Transfer <-";
                btnSelectLocalFile.Text = "Select Local Folder";
                btnSelectRemoteDirectory.Text = "Select Remote File";
                }
            }
        }

    }
