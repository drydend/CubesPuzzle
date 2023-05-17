using UnityEngine;
using System.IO;
using MicroJson;
using TinyJson;

namespace SavingSystem
{
    public class JsonSaveDataLoader : ILevelSaveDataLoader
    {
        private string SavePath => Application.dataPath + @"\LevelSaveData.txt";

        public void SaveLevelsData(LevelsSaveData levelSaveData)
        {
            var serializedData = new JsonSerializer().Serialize(levelSaveData);

            File.WriteAllText(SavePath, serializedData);
        }

        public LevelsSaveData LoadLevelSaveData()
        {
            if (File.Exists(SavePath))
            {
                var serializedData = File.ReadAllText(SavePath);
                var parsedObject = JSONParser.FromJson<LevelsSaveData>(serializedData);
                return parsedObject;
            }
            else
            {
                return new LevelsSaveData();
            }
        }
    }
}
