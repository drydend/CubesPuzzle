using System;
using System.Collections.Generic;

namespace SavingSystem
{
    [Serializable]
    public class LevelsSaveData
    {
        public List<LevelSaveData> SaveData = new List<LevelSaveData>();
        public int LastCompleatedLevel;
        public bool IsTutorialCompleated;

        public LevelsSaveData(List<LevelSaveData> levelsSaveData, int lastUnlockedLevel = 1, bool isTutorialCompleated = false)
        {
            SaveData = levelsSaveData;
            LastCompleatedLevel = lastUnlockedLevel;
            IsTutorialCompleated = isTutorialCompleated;
        }

        public LevelsSaveData() 
        {
            var levelSaveData = new LevelSaveData(LastCompleatedLevel, true);
            SaveData.Add(levelSaveData);

            LastCompleatedLevel = 0;
            IsTutorialCompleated = false;
        }
    }
}