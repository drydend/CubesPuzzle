using System.Collections.Generic;

namespace SavingSystem
{
    public interface ILevelSaveDataLoader
    {
        LevelsSaveData LoadLevelSaveData();
        void SaveLevelsData(LevelsSaveData data);
    }
}
