using GameUI;
using LevelSystem;
using UnityEngine;

namespace Assets.Scripts.UI.Menus
{
    public class GameStartUI : UIMenu
    {
        [SerializeField]
        private LevelCounter _levelCounter;

        public void UpdateUI(LevelConfig levelConfig)
        {
            _levelCounter.SetLevel(levelConfig.LevelNumber);
        }
    }
}
