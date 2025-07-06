using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SCPFileTransferApp.Services
{
    public class PipelineRepository
    {
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pipelines.json");

        public Dictionary<string, string> Pipelines { get; private set; } = new();

        public PipelineRepository()
        {
            if (File.Exists(filePath))
                Pipelines = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(filePath));
        }
    }
}