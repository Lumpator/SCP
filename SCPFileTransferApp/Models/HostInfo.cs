using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPFileTransferApp.Models
    {
    public class HostInfo
        {
        public required string Name { get; set; }
        public required string Host { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        }
    }
