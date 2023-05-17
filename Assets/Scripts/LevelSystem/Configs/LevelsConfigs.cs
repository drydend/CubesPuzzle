using System.Collections.Generic;
using UnityEngine;
using SavingSystem;

namespace LevelSystem
{
    [CreateAssetMenu(menuName = "Levels Configs")]
    public class LevelsConfigs : ScriptableObject
    {
        [SerializeField]
        private List<LevelConfig> _configs;

        public List<LevelConfig> Configs => _configs;

        public void InitializeWithSaveData(LevelsSaveData saveData)
        {
            for (int i = 0; i < _configs.Count; i++)
            {
                if(i < saveData.SaveData.Count)
                {
                    _configs[i].InitializeWithData(saveData.SaveData[i]);
                }
                else
                {
                    _configs[i].InitializeWithDefaults();
                }
            }
        }
    }
}
