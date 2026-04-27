using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using TaskManager.Api.Models;

namespace TaskManager.Api.Services
{
    public class FileService
    {
        private static string _filePath = "tasks.json";

        public static void SaveToFile(List<MyTask> tasks)
        {
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true }); //
            File.WriteAllText(_filePath, json);
        }

        public static List<MyTask> LoadFromFile()
        {
            if (!File.Exists(_filePath))
                return new List<MyTask>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<MyTask>>(json) ?? new List<MyTask>();
        }


    }
}
