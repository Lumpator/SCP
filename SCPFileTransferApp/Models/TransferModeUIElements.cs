using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPFileTransferApp.Models
    {
    public class TransferModeUIElements
        {
        public required Panel PanelDragDrop { get; set; }
        public required Label LblFileSize { get; set; }
        public required Button BtnTransferFile { get; set; }
        public required Button BtnSelectLocalFile { get; set; }
        public required Button BtnSelectRemoteDirectory { get; set; }
        }
    }
