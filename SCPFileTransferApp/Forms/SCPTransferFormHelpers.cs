using Newtonsoft.Json;
using SCPFileTransferApp.Models;
using SCPFileTransferApp.Services;
using static SCPFileTransferApp.Models.Enums;

namespace SCPFileTransferApp.Forms
{
    public static class ScpTransferFormHelpers
    {
        public static List<HostInfo>? LoadHosts()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hosts.json");
            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);
                var hosts = JsonConvert.DeserializeObject<List<HostInfo>>(json);
                return hosts;
            }
            else
            {
                MessageBox.Show("hosts.json file not found.");
                return null;
            }
        }

        public static void LoadRemoteDirectories(SftpService sftpService, TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectory = sftpService.ListDirectory(path);
            foreach (var item in rootDirectory)
            {
                if (item.IsDirectory && item.Name != "." && item.Name != "..")
                {
                    var node = new TreeNode(item.Name);
                    node.Tag = item.FullName;
                    node.Nodes.Add("Loading...");
                    treeView.Nodes.Add(node);
                }
            }
        }

        public static void LoadRemoteFiles(SftpService sftpService, TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectory = sftpService.ListDirectory(path);
            foreach (var item in rootDirectory)
            {
                var node = new TreeNode(item.Name);
                node.Tag = item.FullName;
                if (item.IsDirectory)
                {
                    node.Nodes.Add("Loading...");
                }

                treeView.Nodes.Add(node);
            }
        }

        public static void UpdateStatusIcons(bool canPing, bool canSsh, PictureBox pingStatus, PictureBox ssHstatus)
        {
            pingStatus.Image = canPing ? Properties.Resources.GreenCircle : Properties.Resources.RedCircle;
            ssHstatus.Image = canSsh ? Properties.Resources.GreenCircle : Properties.Resources.RedCircle;
        }

        public static void UpdateStatusIcons(bool loading, PictureBox pingStatus, PictureBox ssHstatus)
        {
            var loadingImage = Properties.Resources.LoadingCircle;
            pingStatus.Image = loading ? loadingImage : Properties.Resources.RedCircle;
            ssHstatus.Image = loading ? loadingImage : Properties.Resources.RedCircle;
        }

        public static void ToggleHostUiElements(bool enabled, HostUiElements hostUiElements)
        {
            hostUiElements.BtnSshConsole.Enabled = enabled;
            hostUiElements.TxtRemoteDirectoryPath.Enabled = enabled;
            hostUiElements.TreeViewRemoteDirectories.Enabled = enabled;
            hostUiElements.BtnSelectRemoteDirectory.Enabled = enabled;
            hostUiElements.BtnJenkinsDownload.Enabled = enabled;
            hostUiElements.BtnReloadVmInformation.Enabled = enabled;
        }

        public static void ToggleDisabledDuringTransferUiElements(bool enabled,
            DisabledDuringTransferElements uiElements)
        {
            uiElements.TxtRemoteDirectoryPath.Enabled = enabled;
            uiElements.TxtLocalDirectoryPath.Enabled = enabled;
            uiElements.TreeViewRemoteDirectories.Enabled = enabled;
            uiElements.BtnSelectRemoteDirectory.Enabled = enabled;
            uiElements.BtnSelectLocalFile.Enabled = enabled;
            uiElements.BtnTransferFile.Enabled = enabled;
            uiElements.HostsListView.Enabled = enabled;
        }

        public static void UpdateTransferModeUi(TransferMode transferMode, TransferModeUiElements uiElements)
        {
            if (transferMode == TransferMode.TransferTo)
            {
                uiElements.PanelDragDrop.Enabled = true;
                uiElements.LblFileSize.Text = "File Size:";
                uiElements.BtnTransferFile.Text = "Start File Transfer ->";
                uiElements.BtnSelectLocalFile.Text = "Browse Local Files";
                uiElements.BtnSelectRemoteDirectory.Text = "Browse Remote Directories";
            }
            else if (transferMode == TransferMode.TransferFrom)
            {
                uiElements.PanelDragDrop.Enabled = false;
                uiElements.LblFileSize.Text = "File Size:";
                uiElements.BtnTransferFile.Text = "Start File Transfer <-";
                uiElements.BtnSelectLocalFile.Text = "Browse Local Directories";
                uiElements.BtnSelectRemoteDirectory.Text = "Browse Remote Files";
            }
        }

        public static void UpdateFileSizeLabel(Label lblFileSize, string localFilePath, long? fileSize = null)
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

        public static void UpdateProgressBar(Form form, ProgressBar progressBar, double progress)
        {
            form.Invoke((MethodInvoker)delegate { progressBar.Value = (int)progress; });
        }

        public static bool IsDirectory(SftpService sftpService, TreeNode node)
        {
            var path = node.Tag.ToString();
            var attributes = sftpService.GetFileAttributes(path);
            return attributes.IsDirectory;
        }

        public static async Task<bool> DownloadPipelineToRemoteAsync(SshService sshService, string url,
            string remoteDirectoryPath, Action<double> onProgress)
        {
            try
            {
                await sshService.DownloadFileOnRemoteAsync(url, remoteDirectoryPath,
                    progress => { onProgress?.Invoke(progress); });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string? SelectLocalFileAndGetPath()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }

            return null;
        }

        public static async Task<(string VmName, string VmOs, string VmIp)> GetVmInfoAsync(SshService sshService,
            HostInfo? selectedHost)
        {
            string vmName = string.Empty;
            string vmOs = string.Empty;
            string vmIp = selectedHost?.Host ?? string.Empty;
            try
            {
                vmName = await Task.Run(() => sshService.RunCommand("hostname"));
            }
            catch
            {
                vmName = "Error";
            }

            try
            {
                vmOs = await Task.Run(() =>
                {
                    var output = sshService.RunCommand("wmic os get Caption /value");
                    var line = output.Split('\n').FirstOrDefault(l =>
                        l.Trim().StartsWith("Caption=", StringComparison.OrdinalIgnoreCase));
                    return line != null ? line.Replace("Caption=", "").Trim() : output.Trim();
                });
            }
            catch
            {
                vmOs = "Error";
            }

            return (vmName.Trim(), vmOs.Trim(), vmIp);
        }

        public static async Task<List<InstalledAppVersion>> GetInstalledAppVersionsAsync(SshService sshService,
            List<AppInfo> apps)
        {
            var installedAppVersions = new List<InstalledAppVersion>();
            foreach (var app in apps)
            {
                string version = "Not found";
                try
                {
                    string output = null;
                    string psScript =
                        $@"$results = @(); " +
                        $@"$results += Get-ItemProperty 'HKLM:\\Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\*' -ErrorAction SilentlyContinue; " +
                        $@"$results += Get-ItemProperty 'HKLM:\\Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\*' -ErrorAction SilentlyContinue; " +
                        $@"$results += Get-ItemProperty 'HKCU:\\Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\*' -ErrorAction SilentlyContinue; " +
                        $@"$results | Where-Object {{$_.DisplayName -like '*{app.WingetName}*'}} | Select-Object -ExpandProperty DisplayVersion | Out-String";
                    output = await Task.Run(() => sshService.RunCommand($"powershell -Command \"{psScript}\""));
                    if (!string.IsNullOrWhiteSpace(output))
                    {
                        version = output.Trim().Split('\n').FirstOrDefault()?.Trim() ?? "Not found";
                    }
                }
                catch
                {
                    MessageBox.Show("Error retrieving version for " + app.DisplayName);
                }

                installedAppVersions.Add(new InstalledAppVersion { DisplayName = app.DisplayName, Version = version });
            }

            return installedAppVersions;
        }
    }
}