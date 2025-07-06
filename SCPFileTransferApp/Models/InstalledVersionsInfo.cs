using System;
using System.Collections.Generic;

namespace SCPFileTransferApp.Models
{
    public class InstalledAppVersion
    {
        public string DisplayName { get; set; }
        public string Version { get; set; }
    }

    public class InstalledVersionsInfo
    {
        public string Host { get; set; }
        public DateTime LastChecked { get; set; }
        public List<InstalledAppVersion> Apps { get; set; }
        public string VmName { get; set; }
        public string VmIp { get; set; }
        public string VmOs { get; set; }
    }
}
