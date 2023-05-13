using System;
using System.Collections.Generic;

namespace PauseSystem
{
    public class LevelPauser
    {
        private List<IPauseable> _pauseables = new List<IPauseable>();

        public void AddPauseable(List<IPauseable> pauseables)
        {
            _pauseables.Add(pauseables);
        }

        public void PauseGame()
        {
            foreach (var pauseable in _pauseables)
            {
                pauseable.Pause();
            }
        }

        public void UnpauseGame()
        {
            foreach (var pauseable in _pauseables)
            {
                pauseable.Unpause();
            }
        }

        public void ResetPauseables()
        {
            _pauseables.Clear();
        }
    }
}
