using Assets.Scripts.UI.Menus;
using GameUI.Menus;
using Input;
using LevelSystem;
using UnityEngine;
using Zenject;

namespace GameUI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField]
        private GameStartUI _levelStartUI;
        [SerializeField]
        private GameRuningUIMenu _gameRuningUI;
        [SerializeField]
        private UIMenu _levelCompleteUI;
        [SerializeField]
        private UIMenu _gamePauseUI;
        [SerializeField]
        private ScreenFade _screenFade;
        [SerializeField]
        private LevelChoseMenu _levelChoseMenu;

        public UIMenu LevelStartUI => _levelStartUI;
        public UIMenu GameRuningUI => _gameRuningUI;
        public UIMenu LevelCompleteUI => _levelCompleteUI;
        public UIMenu GamePausedUI => _gamePauseUI;
        public ScreenFade ScreenFade => _screenFade;

        private void Awake()
        {
            _levelChoseMenu.gameObject.SetActive(false);
        }

        public void UpdateChoseMenu(LevelsConfigs configs)
        {
            _levelChoseMenu.UpdateUI(configs);
        }

        public void UpdateUI(LevelConfig levelConfig)
        {
            _levelStartUI.UpdateUI(levelConfig);
            _gameRuningUI.UpdateUI(levelConfig);
        }

        public void ResetUI()
        {

        }
    }
}
