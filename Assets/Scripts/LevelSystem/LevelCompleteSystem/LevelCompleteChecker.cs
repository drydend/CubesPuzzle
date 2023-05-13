using System.Collections.Generic;

namespace LevelSystem
{
    public class LevelCompleteChecker : ILevelCompleteChecker
    {
        private List<CompleteTrigger> _completeTriggers;

        public bool CheckWin()
        {
            foreach (var trigger in _completeTriggers) 
            {
                if(!trigger.IsTriggered)
                {
                    return false;
                }
            }

            return true;
        }

        public void SetLevel(LevelPreset level)
        {
            _completeTriggers = level.CompleteTriggers;
        }
    }
}