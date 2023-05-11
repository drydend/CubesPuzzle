using PauseSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PauseSystem
{
    public class UnpauseTriggers : MonoBehaviour, IUnpauseTrigger
    {
        [SerializeField]
        private List<UnpauseButton> _pauseButtons;

        public event Action GameUnpaused;

        private void Awake()
        {
            foreach (var button in _pauseButtons)
            {
                button.GameUnpaused += TriggerUnpause;
            }
        }

        private void TriggerUnpause()
        {
            GameUnpaused?.Invoke();
        }
    }
}
