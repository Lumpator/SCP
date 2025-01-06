using SCPFileTransferApp.Helpers;
using SCPFileTransferApp.Models;
using SCPFileTransferApp.Services;
using static SCPFileTransferApp.Models.Enums;

namespace SCPFileTransferApp
    {
    public partial class SCPTransferForm : Form
        {
        private string? localFilePath;
        private string? remoteDirectoryPath;
        private TransferMode transferMode;
        private List<HostInfo>? hosts;
        private SftpService sftpService;
        private SshService sshService;
        private NetworkService networkService;
        private HostUIElements hostUIElements;
        private TransferModeUIElements transferModeUIElements;
        private DisabledDuringTransferElements disabledDuringTransferElements;

        public SCPTransferForm()
            {
            InitializeComponent();
            networkService = new NetworkService();
            hosts = SCPTransferFormHelpers.LoadHosts();
            PopulateHostsComboBox(hosts);

            hostUIElements = new HostUIElements
                {
                BtnSshConsole = btnSshConsole,
                TxtRemoteDirectoryPath = txtRemoteDirectoryPath,
                TreeViewRemoteDirectories = treeViewRemoteDirectories,
                BtnSelectRemoteDirectory = btnSelectRemoteDirectory
                };

            transferModeUIElements = new TransferModeUIElements
                {
                PanelDragDrop = panelDragDrop,
                LblFileSize = lblFileSize,
                BtnTransferFile = btnTransferFile,
                BtnSelectLocalFile = btnSelectLocalFile,
                BtnSelectRemoteDirectory = btnSelectRemoteDirectory
                };

            disabledDuringTransferElements = new DisabledDuringTransferElements
                {
                TxtRemoteDirectoryPath = txtRemoteDirectoryPath,
                TxtLocalDirectoryPath = txtLocalFilePath,
                TreeViewRemoteDirectories = treeViewRemoteDirectories,
                BtnSelectRemoteDirectory = btnSelectRemoteDirectory,
                BtnSelectLocalFile = btnSelectLocalFile,
                BtnTransferFile = btnTransferFile,
                ComboBoxHosts = comboBoxHosts
                };

            SCPTransferFormHelpers.ToggleHostUIElements(false, hostUIElements);
            comboBoxMode.SelectedIndex = 0; // 0 = Transfer to, 1 = Transfer from          
            }


        private async void comboBoxHosts_SelectedIndexChanged(object sender, EventArgs e)
            {
            if (comboBoxHosts.SelectedIndex >= 0)
                {
                SCPTransferFormHelpers.ToggleHostUIElements(false, hostUIElements);
                comboBoxHosts.Enabled = false;
                txtRemoteDirectoryPath.Clear();
                treeViewRemoteDirectories.Nodes.Clear();
                var selectedHost = hosts[comboBoxHosts.SelectedIndex];
                await CheckPingAndSSHAsync(selectedHost);
                }
            else
                {
                SCPTransferFormHelpers.ToggleHostUIElements(false, hostUIElements);
                SCPTransferFormHelpers.UpdateStatusIcons(false, false, pictureBoxPingStatus, pictureBoxSSHStatus);
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
                        SCPTransferFormHelpers.UpdateFileSizeLabel(lblFileSize, localFilePath);
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
                    SCPTransferFormHelpers.LoadRemoteDirectories(sftpService, treeViewRemoteDirectories, "/");
                    }
                else if (transferMode == TransferMode.TransferFrom)
                    {
                    SCPTransferFormHelpers.LoadRemoteFiles(sftpService, treeViewRemoteDirectories, "/");
                    }
                }
            catch (Exception ex)
                {
                MessageBox.Show("Error: " + ex.Message);
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
                if (selectedNode.Nodes.Count > 0 || SCPTransferFormHelpers.IsDirectory(sftpService, selectedNode))
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
                if (selectedNode.Nodes.Count == 0 && !SCPTransferFormHelpers.IsDirectory(sftpService, selectedNode))
                    {
                    remoteDirectoryPath = selectedPath;
                    txtRemoteDirectoryPath.Text = remoteDirectoryPath;

                    var fileAttributes = sftpService.GetFileAttributes(remoteDirectoryPath);
                    SCPTransferFormHelpers.UpdateFileSizeLabel(lblFileSize, localFilePath, fileAttributes.Size);
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
            remoteDirectoryPath = txtRemoteDirectoryPath.Text;

            if (string.IsNullOrEmpty(localFilePath) || string.IsNullOrEmpty(remoteDirectoryPath))
                {
                MessageBox.Show("Please select both local and remote paths.");
                return;
                }
            SCPTransferFormHelpers.ToggleDisabledDuringTransferUIElements(false, disabledDuringTransferElements);
            try
                {
                if (transferMode == TransferMode.TransferTo)
                    {
                    await sftpService.UploadFileAsync(localFilePath, remoteDirectoryPath, progress =>
                    {
                        SCPTransferFormHelpers.UpdateProgressBar(this, progressBar, progress);
                    });

                    MessageBox.Show("File transferred successfully.");
                    }
                else if (transferMode == TransferMode.TransferFrom)
                    {
                    await sftpService.DownloadFileAsync(remoteDirectoryPath, localFilePath, progress =>
                    {
                        SCPTransferFormHelpers.UpdateProgressBar(this, progressBar, progress);

                    });
                    MessageBox.Show("File transferred successfully.");
                    }       
                }
            catch (Exception ex)
                {
                MessageBox.Show("Error: " + ex.Message);
                }
            finally
                {
                progressBar.Value = 0;
                SCPTransferFormHelpers.ToggleDisabledDuringTransferUIElements(true, disabledDuringTransferElements);
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
            SCPTransferFormHelpers.UpdateTransferModeUI(transferMode, transferModeUIElements);
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
                    if (transferMode == TransferMode.TransferTo)
                        {
                        localFilePath = filePath;
                        SCPTransferFormHelpers.UpdateFileSizeLabel(lblFileSize, localFilePath);
                        }
                    else
                        {
                        MessageBox.Show("Drag and drop is only supported in 'Transfer to' mode.");
                        }
                    }
                }
            }

        private async Task CheckPingAndSSHAsync(HostInfo selectedHost)
            {
            SCPTransferFormHelpers.UpdateStatusIcons(loading: true, pictureBoxPingStatus, pictureBoxSSHStatus);

            bool canPing = await networkService.CheckPingAsync(selectedHost.Host);
            bool canSSH = await SftpService.CheckSSHConnectionAsync(selectedHost);

            SCPTransferFormHelpers.UpdateStatusIcons(canPing, canSSH, pictureBoxPingStatus, pictureBoxSSHStatus);

            if (canPing && canSSH)
                {
                sshService = new SshService(selectedHost);
                sftpService = new SftpService(selectedHost);
                SCPTransferFormHelpers.ToggleHostUIElements(true, hostUIElements);
                }
            }

        private void PopulateHostsComboBox(List<HostInfo>? hosts)
            {
            comboBoxHosts.Items.Clear();
            if (hosts != null)
                {
                foreach (var host in hosts)
                    {
                    comboBoxHosts.Items.Add(host.Name);
                    }
                }
            }
        }

    }
