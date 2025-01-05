using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPFileTransferApp.Models
    {
    public class DisabledDuringTransferElements
        {
        public required TextBox TxtRemoteDirectoryPath { get; set; }
        public required TextBox TxtLocalDirectoryPath { get; set; }
        public required TreeView TreeViewRemoteDirectories { get; set; }
        public required Button BtnSelectRemoteDirectory { get; set; }
        public required Button BtnSelectLocalFile { get; set; }
        public required Button BtnTransferFile { get; set; }
        public required ComboBox ComboBoxHosts { get; set; }
        }
    }
