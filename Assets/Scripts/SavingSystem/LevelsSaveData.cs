using System;
using System.Collections.Generic;

namespace SavingSystem
{
    [Serializable]
    public class LevelsSaveData
    {
        public List<LevelSaveData> SaveData = new List<LevelSaveData>();
        public int LastCompleatedLevel;

        public LevelsSaveData(List<LevelSaveData> levelsSaveData, int lastUnlockedLevel = 1)
        {
            SaveData = levelsSaveData;
            LastCompleatedLevel = lastUnlockedLevel;
        }

        public LevelsSaveData() 
        {
            var levelSaveData = new LevelSaveData(LastCompleatedLevel, true);
            SaveData.Add(levelSaveData);

            LastCompleatedLevel = 0;
        }
    }
}