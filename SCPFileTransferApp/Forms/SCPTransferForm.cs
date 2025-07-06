using Newtonsoft.Json;
using SCPFileTransferApp.Models;
using SCPFileTransferApp.Services;
using static SCPFileTransferApp.Models.Enums;

namespace SCPFileTransferApp.Forms
{
    public partial class ScpTransferForm : Form
    {
        private string? localFilePath;
        private string? remoteDirectoryPath;
        private TransferMode transferMode;
        private List<HostInfo>? hosts;
        private SftpService sftpService;
        private SshService sshService;
        private NetworkService networkService;
        private HostUiElements hostUiElements;
        private PipelineRepository pipelineRepository;
        private TransferModeUiElements transferModeUiElements;
        private DisabledDuringTransferElements disabledDuringTransferElements;
        private InstalledVersionsRepository installedVersionsRepository;


        public ScpTransferForm()
        {
            InitializeComponent();
            networkService = new NetworkService();
            hosts = ScpTransferFormHelpers.LoadHosts();
            PopulateVmListView(hosts);
            pipelineRepository = new PipelineRepository();
            PopulatePipelinesComboBox();
            installedVersionsRepository = new InstalledVersionsRepository();

            hostUiElements = new HostUiElements
            {
                BtnSshConsole = btnSshConsole,
                TxtRemoteDirectoryPath = txtRemoteDirectoryPath,
                TreeViewRemoteDirectories = treeViewRemoteDirectories,
                BtnSelectRemoteDirectory = btnSelectRemoteDirectory,
                BtnJenkinsDownload = btnJenkinsDownload,
                BtnReloadVmInformation = btnReloadVmInformation
            };

            transferModeUiElements = new TransferModeUiElements
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
            
            ScpTransferFormHelpers.ToggleHostUiElements(false, hostUiElements);
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
                await CheckPingAndSshAsync(selectedHost);
            }
            else
            {
                ScpTransferFormHelpers.ToggleHostUiElements(false, hostUiElements);
                ScpTransferFormHelpers.UpdateStatusIcons(false, false, pictureBoxPingStatus, pictureBoxSSHStatus);
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

            int rowIndex = AddStatusRow($"Downloading {selectedPipeline} to {vmName}...", Color.LightYellow);

            bool result = await ScpTransferFormHelpers.DownloadPipelineToRemoteAsync(
                sshService, url, remoteDirectoryPath, progress => { /* Optionally handle progress */ });

            if (result)
            {
                UpdateStatusRow(rowIndex, $"Downloaded {selectedPipeline} to {vmName}", Color.LightGreen);
            }
            else
            {
                UpdateStatusRow(rowIndex, $"Error: {selectedPipeline} do {vmName}", Color.Red);
                MessageBox.Show("Download error.");
            }
        }

        private void btnSelectLocalFile_Click(object sender, EventArgs e)
        {
            if (transferMode == TransferMode.TransferTo)
            {
                var selectedFile = ScpTransferFormHelpers.SelectLocalFileAndGetPath();
                if (!string.IsNullOrEmpty(selectedFile))
                {
                    localFilePath = selectedFile;
                    txtLocalFilePath.Text = localFilePath;
                    ScpTransferFormHelpers.UpdateFileSizeLabel(lblFileSize, localFilePath);
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
                    ScpTransferFormHelpers.LoadRemoteDirectories(sftpService, treeViewRemoteDirectories, "/");
                }
                else if (transferMode == TransferMode.TransferFrom)
                {
                    ScpTransferFormHelpers.LoadRemoteFiles(sftpService, treeViewRemoteDirectories, "/");
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
                if (selectedNode.Nodes.Count > 0 || ScpTransferFormHelpers.IsDirectory(sftpService, selectedNode))
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
                if (selectedNode.Nodes.Count == 0 && !ScpTransferFormHelpers.IsDirectory(sftpService, selectedNode))
                {
                    remoteDirectoryPath = selectedPath;
                    txtRemoteDirectoryPath.Text = remoteDirectoryPath;

                    var fileAttributes = sftpService.GetFileAttributes(remoteDirectoryPath);
                    ScpTransferFormHelpers.UpdateFileSizeLabel(lblFileSize, localFilePath, fileAttributes.Size);
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

            ScpTransferFormHelpers.ToggleDisabledDuringTransferUiElements(false, disabledDuringTransferElements);
            try
            {
                if (transferMode == TransferMode.TransferTo)
                {
                    await sftpService.UploadFileAsync(localFilePath, remoteDirectoryPath,
                        progress => { ScpTransferFormHelpers.UpdateProgressBar(this, progressBar, progress); });

                    MessageBox.Show("File transferred successfully.");
                }
                else if (transferMode == TransferMode.TransferFrom)
                {
                    await sftpService.DownloadFileAsync(remoteDirectoryPath, localFilePath,
                        progress => { ScpTransferFormHelpers.UpdateProgressBar(this, progressBar, progress); });
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
                ScpTransferFormHelpers.ToggleDisabledDuringTransferUiElements(true, disabledDuringTransferElements);
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

            ScpTransferFormHelpers.UpdateTransferModeUi(transferMode, transferModeUiElements);
        }

        private void btnSshConsole_Click(object sender, EventArgs e)
        {
            try
            {
                sshService.OpenSshConnection();
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
                        ScpTransferFormHelpers.UpdateFileSizeLabel(lblFileSize, localFilePath);
                    }
                    else
                    {
                        MessageBox.Show("Drag and drop is only supported in 'Transfer to' mode.");
                    }
                }
            }
        }

        private async Task CheckPingAndSshAsync(HostInfo selectedHost)
        {
            ScpTransferFormHelpers.UpdateStatusIcons(loading: true, pictureBoxPingStatus, pictureBoxSSHStatus);

            bool canPing = await networkService.CheckPingAsync(selectedHost.Host);
            bool canSsh = await SftpService.CheckSshConnectionAsync(selectedHost);

            ScpTransferFormHelpers.UpdateStatusIcons(canPing, canSsh, pictureBoxPingStatus, pictureBoxSSHStatus);

            if (canPing && canSsh)
            {
                sshService = new SshService(selectedHost);
                sftpService = new SftpService(selectedHost);
                ScpTransferFormHelpers.ToggleHostUiElements(true, hostUiElements);
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
                Text = "Updating Data...",
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
                var selectedHost = listViewVmList.SelectedItems.Count > 0 ? (HostInfo)listViewVmList.SelectedItems[0].Tag : null;
                var (vmName, vmOs, vmIp) = await ScpTransferFormHelpers.GetVmInfoAsync(sshService, selectedHost);
                txtVmName.Text = vmName;
                txtVmIp.Text = vmIp;
                txtVmOs.Text = vmOs;

                var installedAppVersions = await ScpTransferFormHelpers.GetInstalledAppVersionsAsync(sshService, apps);

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
