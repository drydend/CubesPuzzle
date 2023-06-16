using System;

namespace Tutorial
{
    public class TutorialCompleteTrigger
    {
        public event Action Compleated;

        public void OnTutorialCompleated()
        {
            Compleated?.Invoke();
        }
    }
}
