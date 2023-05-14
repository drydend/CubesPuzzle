using GameUI.Buttons;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace PauseSystem
{
    public class UnpauseButton : InteractableUIButton, IUnpauseTrigger
    {
        public event Action GameUnpaused;

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(UnpauseGame);
        }

        private void UnpauseGame()
        {
            GameUnpaused?.Invoke();
        }
    }
}