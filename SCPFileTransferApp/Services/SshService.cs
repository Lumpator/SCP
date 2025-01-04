using SCPFileTransferApp.Models;
using System;
using System.Diagnostics;

namespace SCPFileTransferApp.Services
    {
    public class SshService
        {
        private readonly HostInfo hostInfo;

        public SshService(HostInfo hostInfo)
            {
            this.hostInfo = hostInfo;
            }

        public void OpenSSHConnection()
            {
            try
                {
                // Construct the SSH command
                string sshCommand = $"ssh {hostInfo.Username}@{hostInfo.Host}";

                // Start a new process to open the command line with the SSH command
                Process.Start(new ProcessStartInfo
                    {
                    FileName = "cmd.exe",
                    Arguments = $"/c start {sshCommand}",
                    RedirectStandardInput = false,
                    RedirectStandardOutput = false,
                    CreateNoWindow = false,
                    UseShellExecute = true
                    });
                }
            catch (Exception ex)
                {
                throw new InvalidOperationException("Failed to open SSH connection", ex);
                }
            }
        }
    }