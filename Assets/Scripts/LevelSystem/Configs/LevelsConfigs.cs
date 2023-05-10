using System.Collections.Generic;
using UnityEngine;

namespace LevelSystem
{
    [CreateAssetMenu(menuName = "Levels Configs")]
    public class LevelsConfigs : ScriptableObject
    {
        [SerializeField]
        private List<LevelConfig> _configs;

        public List<LevelConfig> Configs => _configs;  
    }
}
