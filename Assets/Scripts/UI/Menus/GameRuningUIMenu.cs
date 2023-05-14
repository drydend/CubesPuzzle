using LevelSystem;
using TMPro;
using UnityEngine;

namespace GameUI
{
    public class GameRuningUIMenu : UIMenu
    {
        [SerializeField]
        private LevelCounter _levelCounter;

        public void UpdateUI(LevelConfig levelConfig)
        {
            _levelCounter.SetLevel(levelConfig.LevelNumber);
        }
    }
}
