using Newtonsoft.Json;
using SCPFileTransferApp.Models;

namespace SCPFileTransferApp.Services
{
    public class InstalledVersionsRepository
    {
        private readonly string filePath;

        public InstalledVersionsRepository(string filePath = null)
        {
            this.filePath = filePath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "installed_versions.json");
        }

        public List<InstalledVersionsInfo> LoadAll()
        {
            if (!File.Exists(filePath))
                return new List<InstalledVersionsInfo>();
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<InstalledVersionsInfo>>(json) ??
                   new List<InstalledVersionsInfo>();
        }

        public InstalledVersionsInfo GetLatestForHost(string host)
        {
            var all = LoadAll();
            return all.FirstOrDefault(x => x.Host.Equals(host, StringComparison.OrdinalIgnoreCase));
        }

        public void SaveForHost(InstalledVersionsInfo info)
        {
            var all = LoadAll();
            var existing = all.FirstOrDefault(x => x.Host.Equals(info.Host, StringComparison.OrdinalIgnoreCase));
            if (existing != null)
            {
                all.Remove(existing);
            }

            all.Add(info);
            var json = JsonConvert.SerializeObject(all, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}