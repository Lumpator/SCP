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
        
            public void DownloadFileOnRemote(string url, string remoteDirectory)
            {
                using (var client = new Renci.SshNet.SshClient(hostInfo.Host, hostInfo.Username, hostInfo.Password))
                {
                    client.Connect();
                    string fileName = System.IO.Path.GetFileName(url);
                    if (remoteDirectory.StartsWith("/") && remoteDirectory.Length > 2 && char.IsLetter(remoteDirectory[1]) && remoteDirectory[2] == ':')
                    {
                        remoteDirectory = remoteDirectory.Substring(1);
                    }
                    string command = $"cd {remoteDirectory} && curl -o {fileName} {url}";
                    var result = client.RunCommand(command);
                    if (!string.IsNullOrEmpty(result.Error))
                        throw new Exception(result.Error);
                    client.Disconnect();
                }
            }
            
            public async Task DownloadFileOnRemoteAsync(string url, string remoteDirectory, Action<double> progressCallback)
            {
                await Task.Run(() =>
                {
                    using (var client = new Renci.SshNet.SshClient(hostInfo.Host, hostInfo.Username, hostInfo.Password))
                    {
                        client.Connect();
                        string fileName = System.IO.Path.GetFileName(url);
                        if (remoteDirectory.StartsWith("/") && remoteDirectory.Length > 2 && char.IsLetter(remoteDirectory[1]) && remoteDirectory[2] == ':')
                        {
                            remoteDirectory = remoteDirectory.Substring(1);
                        }
                        // Spustí stahování na pozadí, progressCallback můžeš volat např. po částech, pokud budeš chtít detailní progress
                        string command = $"cd {remoteDirectory} && curl -# -o {fileName} {url}";
                        var result = client.RunCommand(command);
                        // Progress lze získat složitěji, např. přes parsování výstupu curl, zde pouze voláme 100% po dokončení
                        progressCallback?.Invoke(100);
                        if (result.ExitStatus != 0)
                            throw new Exception(result.Error);
                        client.Disconnect();
                    }
                });
            }
        }
    }