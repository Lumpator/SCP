using System.Net.NetworkInformation;

namespace SCPFileTransferApp.Services
{
    public class NetworkService
    {
        public async Task<bool> CheckPingAsync(string hostname)
        {
            const int timeout = 1000; // Timeout in milliseconds (1 second)
            try
            {
                return await Task.Run(() =>
                {
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(hostname, timeout);
                    return reply.Status == IPStatus.Success;
                });
            }
            catch
            {
                return false;
            }
        }
    }
}