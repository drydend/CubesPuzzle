using System;
using System.Collections.Generic;

namespace SavingSystem
{
    [Serializable]
    public class LevelsSaveData
    {
        public List<LevelSaveData> SaveData = new List<LevelSaveData>();
        public int LastCompletedLevel;
        public bool IsTutorialCompleted;

        public LevelsSaveData(List<LevelSaveData> levelsSaveData, int lastUnlockedLevel = 1, bool isTutorialCompleated = false)
        {
            SaveData = levelsSaveData;
            LastCompletedLevel = lastUnlockedLevel;
            IsTutorialCompleted = isTutorialCompleated;
        }

        public LevelsSaveData() 
        {
            var levelSaveData = new LevelSaveData(LastCompletedLevel, true);
            SaveData.Add(levelSaveData);

            LastCompletedLevel = 0;
            IsTutorialCompleted = false;
        }
    }
}