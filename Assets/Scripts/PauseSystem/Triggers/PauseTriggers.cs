using GameUI.Buttons;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PauseSystem
{
    public class PauseTriggers : MonoBehaviour ,IPauseTrigger
    {
        [SerializeField]
        private List<PauseButton> _pauseButtons;

        public event Action GamePaused;

        private void Awake()
        {
            foreach (var button in _pauseButtons)
            {
                button.GamePaused += TriggerPause;
            }
        }

        private void TriggerPause()
        {
            GamePaused?.Invoke();
        }
    }
}
