using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPFileTransferApp.Models
{
    public class HostUiElements
    {
        public required Button BtnSshConsole { get; set; }
        public required TextBox TxtRemoteDirectoryPath { get; set; }
        public required TreeView TreeViewRemoteDirectories { get; set; }
        public required Button BtnSelectRemoteDirectory { get; set; }
        public required Button BtnJenkinsDownload { get; set; }
        public required Button BtnReloadVmInformation { get; set; }
    }
}