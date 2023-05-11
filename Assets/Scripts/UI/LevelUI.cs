using LevelSystem;
using System;
using UnityEngine;

namespace GameUI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField]
        private UIMenu _levelStartUI;
        [SerializeField]
        private UIMenu _gameRuningUI;
        [SerializeField]
        private UIMenu _levelCompleteUI;
        [SerializeField]
        private UIMenu _gamePauseUI;
        [SerializeField]
        private ScreenFade _screenFade;

        public UIMenu LevelStartUI => _levelStartUI;
        public UIMenu GameRuningUI => _gameRuningUI;
        public UIMenu LevelCompleteUI => _levelCompleteUI;
        public UIMenu GamePausedUI => _gamePauseUI;
        public ScreenFade ScreenFade => _screenFade;

        public void UpdateUI(LevelConfig levelConfig)
        {

        }
    }
}
