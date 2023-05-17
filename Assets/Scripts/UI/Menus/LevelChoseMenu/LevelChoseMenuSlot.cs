using GameUI.Buttons;
using LevelSystem;
using TMPro;
using UnityEngine;

namespace GameUI.Menus
{
    [RequireComponent(typeof(InteractableUIButton))]
    public class LevelChoseMenuSlot : MonoBehaviour
    {   
        [SerializeField]
        private RectTransform _unlockedStateObject;
        [SerializeField]
        private RectTransform _lockedStateObject;
        [SerializeField]
        private TMP_Text _levelNumberLabel;

        private bool _isUnlocked;
        private int _levelNumber;
        
        private InteractableUIButton _button;
        private Game _game;

        public void Initialize(LevelConfig config, Game game)
        {
            _game = game;
            _button = GetComponent<InteractableUIButton>();

            _levelNumber = config.LevelNumber;
            _isUnlocked = config.IsUnlocked;

            _levelNumberLabel.text = _levelNumber.ToString();
            _button.onClick.AddListener(StartLevel);

            UpdateUI(config);
        }

        public void SetActive(bool value)
        {
            _button.SetActive(value);
        }

        public void UpdateUI(LevelConfig config)
        {
            _isUnlocked = config.IsUnlocked;
            _levelNumberLabel.text = config.LevelNumber.ToString();

            _unlockedStateObject.gameObject.SetActive(config.IsUnlocked);
            _lockedStateObject.gameObject.SetActive(!config.IsUnlocked);
        }

        private void StartLevel()
        {
            if (_isUnlocked)
            {
                _game.LoadLevel(_levelNumber);
            }
        }
    }
}