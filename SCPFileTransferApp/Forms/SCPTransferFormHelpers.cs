using Newtonsoft.Json;
using SCPFileTransferApp.Models;
using SCPFileTransferApp.Services;
using static SCPFileTransferApp.Models.Enums;

namespace SCPFileTransferApp.Helpers
    {
    public static class SCPTransferFormHelpers
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
        public static void UpdateStatusIcons(bool canPing, bool canSSH, PictureBox pingStatus, PictureBox SSHstatus)
            {
            pingStatus.Image = canPing ? Properties.Resources.GreenCircle : Properties.Resources.RedCircle;
            SSHstatus.Image = canSSH ? Properties.Resources.GreenCircle : Properties.Resources.RedCircle;
            }
        public static void UpdateStatusIcons(bool loading, PictureBox pingStatus, PictureBox SSHstatus)
            {
            var loadingImage = Properties.Resources.LoadingCircle;
            pingStatus.Image = loading ? loadingImage : Properties.Resources.RedCircle;
            SSHstatus.Image = loading ? loadingImage : Properties.Resources.RedCircle;
            }

        public static void ToggleHostUIElements(bool enabled, HostUIElements hostUIElements)
            {
            hostUIElements.BtnSshConsole.Enabled = enabled;
            hostUIElements.TxtRemoteDirectoryPath.Enabled = enabled;
            hostUIElements.TreeViewRemoteDirectories.Enabled = enabled;
            hostUIElements.BtnSelectRemoteDirectory.Enabled = enabled;
            }
        public static void ToggleDisabledDuringTransferUIElements(bool enabled, DisabledDuringTransferElements UIElements)
            {
            UIElements.TxtRemoteDirectoryPath.Enabled = enabled;
            UIElements.TxtLocalDirectoryPath.Enabled = enabled;
            UIElements.TreeViewRemoteDirectories.Enabled = enabled;
            UIElements.BtnSelectRemoteDirectory.Enabled = enabled;
            UIElements.BtnSelectLocalFile.Enabled = enabled;
            UIElements.BtnTransferFile.Enabled = enabled;
            UIElements.ComboBoxHosts.Enabled = enabled;
            }
        public static void UpdateTransferModeUI(TransferMode transferMode, TransferModeUIElements uiElements)
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
            form.Invoke((MethodInvoker)delegate
                {
                    progressBar.Value = (int)progress;
                    });
            }

        public static bool IsDirectory(SftpService sftpService, TreeNode node)
            {
            var path = node.Tag.ToString();
            var attributes = sftpService.GetFileAttributes(path);
            return attributes.IsDirectory;
            }
        }
    }
