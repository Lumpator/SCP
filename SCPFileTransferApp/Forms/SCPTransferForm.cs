using SCPFileTransferApp.Helpers;
using SCPFileTransferApp.Models;
using SCPFileTransferApp.Services;
using static SCPFileTransferApp.Models.Enums;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

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
        private PipelineRepository pipelineRepository;
        private TransferModeUIElements transferModeUIElements;
        private DisabledDuringTransferElements disabledDuringTransferElements;
        private InstalledVersionsRepository installedVersionsRepository;


        public SCPTransferForm()
        {
            InitializeComponent();
            networkService = new NetworkService();
            hosts = SCPTransferFormHelpers.LoadHosts();
            PopulateVmListView(hosts);
            pipelineRepository = new PipelineRepository();
            PopulatePipelinesComboBox();
            installedVersionsRepository = new InstalledVersionsRepository();

            hostUIElements = new HostUIElements
            {
                BtnSshConsole = btnSshConsole,
                TxtRemoteDirectoryPath = txtRemoteDirectoryPath,
                TreeViewRemoteDirectories = treeViewRemoteDirectories,
                BtnSelectRemoteDirectory = btnSelectRemoteDirectory,
                BtnJenkinsDownload = btnJenkinsDownload,
                BtnReloadVmInformation = btnReloadVmInformation
            };

            transferModeUIElements = new TransferModeUIElements
            {
                PanelDragDrop = panelDragDrop,
                LblFileSize = lblFileSize,
                BtnTransferFile = btnTransferFile,
                BtnSelectLocalFile = btnSelectLocalFile,
                BtnSelectRemoteDirectory = btnSelectRemoteDirectory,
                HostsListView = listViewVmList
            };

            disabledDuringTransferElements = new DisabledDuringTransferElements
            {
                TxtRemoteDirectoryPath = txtRemoteDirectoryPath,
                TxtLocalDirectoryPath = txtLocalFilePath,
                TreeViewRemoteDirectories = treeViewRemoteDirectories,
                BtnSelectRemoteDirectory = btnSelectRemoteDirectory,
                BtnSelectLocalFile = btnSelectLocalFile,
                BtnTransferFile = btnTransferFile,
                HostsListView = listViewVmList
            };
            
            SCPTransferFormHelpers.ToggleHostUIElements(false, hostUIElements);
            comboBoxMode.SelectedIndex = 0; // 0 = Transfer to, 1 = Transfer from          
        }


        private void ShowInstalledVersionsForHost(string host)
        {
            var info = installedVersionsRepository.GetLatestForHost(host);
            if (info != null)
            {
                txtLastVmInstalledVersionReload.Text = info.LastChecked.ToString("yyyy-MM-dd HH:mm:ss");
                dgvInstalledVersions.DataSource = null;
                dgvInstalledVersions.AutoGenerateColumns = true;
                dgvInstalledVersions.DataSource = info.Apps;
                dgvInstalledVersions.AutoResizeColumns();
                dgvInstalledVersions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                // Zobrazit informace o VM
                txtVmName.Text = info.VmName ?? string.Empty;
                txtVmIp.Text = info.VmIp ?? string.Empty;
                txtVmOs.Text = info.VmOs ?? string.Empty;
            }
            else
            {
                txtLastVmInstalledVersionReload.Text = "N/A";
                dgvInstalledVersions.DataSource = null;
                txtVmName.Text = string.Empty;
                txtVmIp.Text = string.Empty;
                txtVmOs.Text = string.Empty;
            }
        }

        private async void listViewVmList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewVmList.SelectedItems.Count > 0)
            {
                listViewVmList.Enabled = false;
                txtRemoteDirectoryPath.Clear();
                treeViewRemoteDirectories.Nodes.Clear();
                remoteDirectoryPath = null;
                var selectedHost = (HostInfo)listViewVmList.SelectedItems[0].Tag;
                ShowInstalledVersionsForHost(selectedHost.Name);
                await CheckPingAndSSHAsync(selectedHost);
            }
            else
            {
                SCPTransferFormHelpers.ToggleHostUIElements(false, hostUIElements);
                SCPTransferFormHelpers.UpdateStatusIcons(false, false, pictureBoxPingStatus, pictureBoxSSHStatus);
                txtLastVmInstalledVersionReload.Text = "";
                dgvInstalledVersions.DataSource = null;
            }

            listViewVmList.Enabled = true; // Opětovné povolení výběru
        }

        private async void btnJenkinsDownload_Click(object sender, EventArgs e)
        {
            if (comboBoxPipelines.SelectedIndex < 0)
            {
                MessageBox.Show("Select a pipeline.");
                return;
            }

            if (string.IsNullOrEmpty(remoteDirectoryPath))
            {
                MessageBox.Show("Select a target directory on the VM.");
                return;
            }

            if (sshService == null)
            {
                MessageBox.Show("No host selected.");
                return;
            }

            string selectedPipeline = comboBoxPipelines.SelectedItem.ToString();
            string url = pipelineRepository.Pipelines[selectedPipeline];
            string vmName = hosts[listViewVmList.SelectedIndices[0]].Name;

            // Přidání řádku do ListView
            int rowIndex = AddStatusRow($"Downloading {selectedPipeline} to {vmName}...", Color.LightYellow);

            try
            {
                await sshService.DownloadFileOnRemoteAsync(url, remoteDirectoryPath, progress =>
                {
                    /* případně progress */
                });
                UpdateStatusRow(rowIndex, $"Downloaded {selectedPipeline} to {vmName}", Color.LightGreen);
            }
            catch (Exception ex)
            {
                UpdateStatusRow(rowIndex, $"Error: {selectedPipeline} do {vmName}", Color.Red);
                MessageBox.Show("Download error: " + ex.Message);
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
            if (e.Node.Nodes[0].Text == "Loading...")
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
                    await sftpService.UploadFileAsync(localFilePath, remoteDirectoryPath,
                        progress => { SCPTransferFormHelpers.UpdateProgressBar(this, progressBar, progress); });

                    MessageBox.Show("File transferred successfully.");
                }
                else if (transferMode == TransferMode.TransferFrom)
                {
                    await sftpService.DownloadFileAsync(remoteDirectoryPath, localFilePath,
                        progress => { SCPTransferFormHelpers.UpdateProgressBar(this, progressBar, progress); });
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
                MessageBox.Show($"Failed to open SSH connection: {ex.Message}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    string filePath = files[0];
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

        private void PopulateVmListView(List<HostInfo>? hosts)
        {
            listViewVmList.Items.Clear();
            if (hosts != null)
            {
                foreach (var host in hosts)
                {
                    var item = new ListViewItem(host.Name);
                    item.Tag = host;
                    listViewVmList.Items.Add(item);
                }
            }
        }

        private void PopulatePipelinesComboBox()
        {
            comboBoxPipelines.Items.Clear();
            if (pipelineRepository.Pipelines != null)
            {
                foreach (var pipeline in pipelineRepository.Pipelines.Keys)
                {
                    comboBoxPipelines.Items.Add(pipeline);
                }
            }
        }

        private int AddStatusRow(string text, Color? backColor = null)
        {
            var item = new ListViewItem(text);
            listViewStatus.Items.Add(item);
            if (backColor != null)
                item.BackColor = backColor.Value;
            return listViewStatus.Items.Count - 1;
        }

        private void UpdateStatusRow(int rowIndex, string newText, Color? backColor = null)
        {
            if (rowIndex >= 0 && rowIndex < listViewStatus.Items.Count)
            {
                var item = listViewStatus.Items[rowIndex];
                item.Text = newText;
                if (backColor != null)
                    item.BackColor = backColor.Value;
            }
        }

        private async void btnReloadVmInformation_Click(object sender, EventArgs e)
        {
            // Překrytí DataGridView hláškou
            var overlay = new Label
            {
                Text = "Probíhá aktualizace dat...",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(200, Color.LightGray),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.Black,
                Name = "dgvOverlayLabel"
            };
            dgvInstalledVersions.Controls.Add(overlay);
            overlay.BringToFront();
            dgvInstalledVersions.Enabled = false;

            try
            {
                var appsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "apps.json");
                List<AppInfo> apps = new List<AppInfo>();
                if (File.Exists(appsPath))
                {
                    var json = File.ReadAllText(appsPath);
                    apps = JsonConvert.DeserializeObject<List<AppInfo>>(json);
                }

                if (sshService == null)
                {
                    MessageBox.Show("No host selected.");
                    return;
                }

                // Získání hostname a OS přes SSH
                string vmName = await Task.Run(() => sshService.RunCommand("hostname"));
                string vmOs = await Task.Run(() => sshService.RunCommand("ver"));
                var selectedHost = listViewVmList.SelectedItems.Count > 0 ? (HostInfo)listViewVmList.SelectedItems[0].Tag : null;
                string vmIp = selectedHost?.Host ?? string.Empty;
                txtVmName.Text = vmName.Trim();
                txtVmIp.Text = vmIp;
                txtVmOs.Text = vmOs.Trim();

                var installedAppVersions = new List<InstalledAppVersion>();
                foreach (var app in apps)
                {
                    string version = "Not found";
                    try
                    {
                        string output = await Task.Run(() => sshService.RunCommand($"winget list \"{app.WingetName}\""));
                        if (!string.IsNullOrWhiteSpace(output))
                        {
                            var lines = output.Split('\n');
                            int versionIndex = -1;
                            foreach (var line in lines)
                            {
                                if (line.Trim().StartsWith("Name") && line.Contains("Version"))
                                {
                                    var headerParts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    for (int i = 0; i < headerParts.Length; i++)
                                    {
                                        if (headerParts[i].Equals("Version", StringComparison.OrdinalIgnoreCase))
                                        {
                                            versionIndex = i;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                            foreach (var line in lines)
                            {
                                if (line.Contains(app.WingetName))
                                {
                                    var parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (versionIndex >= 0 && parts.Length > versionIndex)
                                        version = parts[versionIndex];
                                    else if (parts.Length > 0)
                                        version = parts[^1];
                                }
                            }
                        }
                    }
                    catch
                    {
                        version = "Error";
                    }
                    installedAppVersions.Add(new InstalledAppVersion { DisplayName = app.DisplayName, Version = version });
                }

                // Uložení nových hodnot do repository včetně VM info
                if (selectedHost != null)
                {
                    var info = new InstalledVersionsInfo
                    {
                        Host = selectedHost.Name,
                        LastChecked = DateTime.Now,
                        Apps = installedAppVersions,
                        VmName = vmName.Trim(),
                        VmIp = vmIp,
                        VmOs = vmOs.Trim()
                    };
                    installedVersionsRepository.SaveForHost(info);
                    ShowInstalledVersionsForHost(selectedHost.Name);
                }
            }
            finally
            {
                // Odebrání overlaye a povolení DataGridView
                var overlayToRemove = dgvInstalledVersions.Controls["dgvOverlayLabel"];
                if (overlayToRemove != null)
                    dgvInstalledVersions.Controls.Remove(overlayToRemove);
                dgvInstalledVersions.Enabled = true;
            }
        }
        
    }
}
