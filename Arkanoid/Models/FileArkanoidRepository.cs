using System;
using System.IO;
using System.Text.Json;

namespace Arkanoid.Models
{
    internal class ArkanoidFileRepository : IArkanoidRepository
    {
        public void LoadGame(ArkanoidModel activeModel)
        {
            if (File.Exists(path: Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "data.json")))
            {
                string fileName = "data.json";
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string path = Path.Combine(docPath, fileName);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                string jsonString = File.ReadAllText(path);
                ArkanoidModel arkanoidModel = JsonSerializer.Deserialize<ArkanoidModel>(jsonString, options)!;

                activeModel.Blocks.Clear();
                activeModel.Blocks.AddRange(arkanoidModel.Blocks);
                activeModel.BallModel = arkanoidModel.BallModel;
                activeModel.SliderModel = arkanoidModel.SliderModel;
                activeModel.ScoreModel = arkanoidModel.ScoreModel;
                activeModel.Lifes = arkanoidModel.Lifes;
            }

        }

        public void SaveGame(ArkanoidModel arkanoidModel)
        {
            arkanoidModel.ResetElements();

            string fileName = "data.json";
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string path = Path.Combine(docPath, fileName);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(arkanoidModel, options);
            File.WriteAllText(path, jsonString);
        }
    }
}
