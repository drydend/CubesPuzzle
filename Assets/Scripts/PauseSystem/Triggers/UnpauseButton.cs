using System;
using UnityEngine;
using UnityEngine.UI;

namespace PauseSystem
{
    [RequireComponent(typeof(Button))]
    public class UnpauseButton : MonoBehaviour, IUnpauseTrigger
    {
        private Button _button;

        public event Action GameUnpaused;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(UnpauseGame);
        }

        private void UnpauseGame()
        {
            GameUnpaused?.Invoke();
        }
    }
}