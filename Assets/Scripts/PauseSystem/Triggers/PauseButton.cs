using System;
using UnityEngine;
using UnityEngine.UI;

namespace PauseSystem
{
    [RequireComponent(typeof(Button))]
    public class PauseButton : MonoBehaviour, IPauseTrigger
    {
        private Button _button;

        public event Action GamePaused;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(PauseGame);
        }

        private void PauseGame()
        {
            GamePaused?.Invoke();
        }
    }
}