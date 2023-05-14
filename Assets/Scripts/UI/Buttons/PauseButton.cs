using PauseSystem;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI.Buttons
{
    [RequireComponent(typeof(Button), typeof(Animator))]
    public class PauseButton : InteractableUIButton, IPauseTrigger
    {
        public event Action GamePaused;

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(PauseGame);
        }

        private void PauseGame()
        {
            GamePaused?.Invoke();
        }
    }
}