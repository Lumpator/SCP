namespace SCPFileTransferApp.Models
{
    public class TransferModeUiElements
    {
        public required Panel PanelDragDrop { get; set; }
        public required Label LblFileSize { get; set; }
        public required Button BtnTransferFile { get; set; }
        public required Button BtnSelectLocalFile { get; set; }
        public required Button BtnSelectRemoteDirectory { get; set; }
        public required ListView HostsListView { get; set; }
    }
}