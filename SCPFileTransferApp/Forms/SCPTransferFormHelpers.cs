using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using SCPFileTransferApp.Models;
using SCPFileTransferApp.Services;

namespace SCPFileTransferApp.Helpers
    {
    public static class SCPTransferFormHelpers
        {
        public static List<HostInfo> LoadHosts()
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
        }

    
    }
