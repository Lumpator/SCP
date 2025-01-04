using System;
using System.IO;
using System.Windows.Forms;
using SCPFileTransferApp.Services;
using System.Diagnostics;
using SCPFileTransferApp.Models;
using Newtonsoft.Json;
using System.Net.NetworkInformation;

namespace SCPFileTransferApp
    {
    public partial class SCPTransferForm : Form
        {
        private string? localFilePath;
        private string? remoteDirectoryPath;
        private TransferMode transferMode;
        private List<HostInfo> hosts;
        private SftpService sftpService;
        private SshService sshService;

        public SCPTransferForm()
            {
            InitializeComponent();
            LoadHosts();
            ToggleHostUIElements(false);
            comboBoxMode.SelectedIndex = 0; // 0 = Transfer to, 1 = Transfer from          
            }



        private async void comboBoxHosts_SelectedIndexChanged(object sender, EventArgs e)
            {
            if (comboBoxHosts.SelectedIndex >= 0)
                {
                ToggleHostUIElements(false);
                comboBoxHosts.Enabled = false;
                var selectedHost = hosts [comboBoxHosts.SelectedIndex];
                txtRemoteDirectoryPath.Clear();
                treeViewRemoteDirectories.Nodes.Clear();

                // Show spinning circle GIF
                pictureBoxPingStatus.Image = Properties.Resources.LoadingCircle;
                pictureBoxSSHStatus.Image = Properties.Resources.LoadingCircle;

                // Perform ping and SSH checks
                bool canPing = await CheckPingAsync(selectedHost.Host);
                bool canSSH = await CheckSSHConnectionAsync(selectedHost);

                // Update UI based on checks
                UpdateStatusIcons(canPing, canSSH);
                if (canPing && canSSH)
                    {
                    sshService = new SshService(selectedHost);
                    sftpService = new SftpService(selectedHost);
                    ToggleHostUIElements(true);
                    }
                }
            else
                {
                ToggleHostUIElements(false);
                UpdateStatusIcons(false, false);
                }
            comboBoxHosts.Enabled = true;
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
                panelDragDrop.Enabled = true;
                lblFileSize.Text = "File Size:";
                btnTransferFile.Text = "Transfer ->";
                btnSelectLocalFile.Text = "Browse Local Files";
                btnSelectRemoteDirectory.Text = "Browse Remote Directories";
                }
            else if (transferMode == TransferMode.TransferFrom)
                {
                panelDragDrop.Enabled = false;
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
                btnSshConsole.Enabled = true;
                txtRemoteDirectoryPath.Enabled = true;
                treeViewRemoteDirectories.Enabled = true;
                btnSelectRemoteDirectory.Enabled = true;
                }
            else
                {
                btnSshConsole.Enabled = false;
                txtRemoteDirectoryPath.Enabled = false;
                treeViewRemoteDirectories.Enabled = false;
                btnSelectRemoteDirectory.Enabled = false;
                }
            }
        private async Task<bool> CheckPingAsync(string hostname)
            {
            const int timeout = 1000; // Timeout in milliseconds (1 second)
            try
                {
                return await Task.Run(() =>
                {
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(hostname, timeout);
                    return reply.Status == IPStatus.Success;
                });
                }
            catch
                {
                return false;
                }
            }

        private async Task<bool> CheckSSHConnectionAsync(HostInfo host)
            {
            try
                {
                return await Task.Run(() =>
                {
                    var client = new SftpService(host);
                    if (client.Connect())
                        {
                        client.Disconnect();
                        return true;
                        }
                    return false;
                });
                }
            catch
                {
                return false;
                }
            }

        private void UpdateStatusIcons(bool canPing, bool canSSH)
            {
            pictureBoxPingStatus.Image = canPing ? Properties.Resources.GreenCircle : Properties.Resources.RedCircle;
            pictureBoxSSHStatus.Image = canSSH ? Properties.Resources.GreenCircle : Properties.Resources.RedCircle;
            }

        private void button1_Click(object sender, EventArgs e)
            {

            }

        private void btnSshConsole_Click(object sender, EventArgs e)
            {
            try
                {
                sshService.OpenSSHConnection();
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Failed to open SSH connection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        private void panelDragDrop_DragEnter(object sender, DragEventArgs e)
            {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                e.Effect = DragDropEffects.Copy;
                }
            else
                {
                e.Effect = DragDropEffects.None;
                }
            }

        private void panelDragDrop_DragDrop(object sender, DragEventArgs e)
            {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                string [] files = (string [])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                    {
                    string filePath = files [0];
                    txtLocalFilePath.Text = filePath;

                    // Check if "Transfer to" mode is selected
                    if (transferMode == TransferMode.TransferTo)
                        {
                        localFilePath = filePath;
                        UpdateFileSizeLabel();
                        }
                    else
                        {
                        MessageBox.Show("Drag and drop is only supported in 'Transfer to' mode.");
                        }
                    }
                }
            }
        }

    }
