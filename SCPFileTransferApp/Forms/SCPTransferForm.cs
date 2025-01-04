using System;
using System.IO;
using System.Windows.Forms;
using SCPFileTransferApp.Services;
using System.Diagnostics;
using SCPFileTransferApp.Models;
using Newtonsoft.Json;

namespace SCPFileTransferApp
    {
    public partial class SCPTransferForm : Form
        {
        private string? localFilePath;
        private string? remoteDirectoryPath;
        private TransferMode transferMode;
        private List<HostInfo> hosts;


        private SftpService sftpService;

        public SCPTransferForm()
            {
            InitializeComponent();
            LoadHosts();
            ToggleHostUIElements(false);
            comboBoxMode.SelectedIndex = 0; // 0 = Transfer to, 1 = Transfer from          
            }

       

        private void comboBoxHosts_SelectedIndexChanged(object sender, EventArgs e)
            {
            if (comboBoxHosts.SelectedIndex >= 0)
                {
                var selectedHost = hosts [comboBoxHosts.SelectedIndex];
                sftpService = new SftpService(selectedHost);
                txtRemoteDirectoryPath.Clear();
                treeViewRemoteDirectories.Nodes.Clear();
                ToggleHostUIElements(true);
                }
            else
                {
                ToggleHostUIElements(false);
                }
            }

        private void btnSelectLocalFile_Click(object sender, EventArgs e)
            {
            if (transferMode == TransferMode.TransferTo)
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
            else if (transferMode == TransferMode.TransferFrom)
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

                if (transferMode == TransferMode.TransferTo)
                    {
                    LoadRemoteDirectories("/");
                    }
                else if (transferMode == TransferMode.TransferFrom)
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
                    node.Nodes.Add("Loading...");
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
                    node.Nodes.Add("Loading...");
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

            if (transferMode == TransferMode.TransferTo)
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
            else if (transferMode == TransferMode.TransferFrom)
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
                if (transferMode == TransferMode.TransferTo)
                    {
                    await sftpService.UploadFileAsync(localFilePath, remoteDirectoryPath, progress =>
                    {
                        UpdateProgressBar(progress);
                    });

                    MessageBox.Show("File transferred successfully.");
                    }
                else if (transferMode == TransferMode.TransferFrom)
                    {
                    await sftpService.DownloadFileAsync(remoteDirectoryPath, localFilePath, progress =>
                    {
                        UpdateProgressBar(progress);
                                
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

        private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
            {
            if (comboBoxMode.SelectedIndex == 0)
                {
                transferMode = TransferMode.TransferTo;          
                }
            else if (comboBoxMode.SelectedIndex == 1)
                {
                transferMode = TransferMode.TransferFrom;
                }
            UpdateTransferModeUI();
            }


        public enum TransferMode
            {
            TransferTo,
            TransferFrom
            }

        private void UpdateTransferModeUI()
            {
            if (transferMode == TransferMode.TransferTo)
                {
                lblFileSize.Text = "File Size:";
                btnTransferFile.Text = "Transfer ->";
                btnSelectLocalFile.Text = "Browse Local Files";
                btnSelectRemoteDirectory.Text = "Browse Remote Directories";
                }
            else if (transferMode == TransferMode.TransferFrom)
                {
                lblFileSize.Text = "File Size:";
                btnTransferFile.Text = "Transfer <-";
                btnSelectLocalFile.Text = "Browse Local Directories";
                btnSelectRemoteDirectory.Text = "Browse Remote Files";
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
        private void UpdateProgressBar(double progress)
            {
            this.Invoke((MethodInvoker)delegate
                {
                    progressBar.Value = (int)progress;
                    });
            }

        private void LoadHosts()
            {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hosts.json");
            if (File.Exists(jsonFilePath))
                {
                string json = File.ReadAllText(jsonFilePath);
                hosts = JsonConvert.DeserializeObject<List<HostInfo>>(json);
                comboBoxHosts.Items.Clear();
                foreach (var host in hosts)
                    {
                    comboBoxHosts.Items.Add(host.Name);
                    }
                }
            else
                {
                MessageBox.Show("hosts.json file not found.");
                }
            }
        private void ToggleHostUIElements(bool enabled)
            {
            if (enabled)
                {
                txtRemoteDirectoryPath.Enabled = true;
                treeViewRemoteDirectories.Enabled = true;
                btnSelectRemoteDirectory.Enabled = true;
                }
            else
                {
                txtRemoteDirectoryPath.Enabled = false;
                treeViewRemoteDirectories.Enabled = false;
                btnSelectRemoteDirectory.Enabled = false;
                }
            }
        }



    }
