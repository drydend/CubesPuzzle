using System;

namespace PauseSystem
{
    public interface IPauseTrigger
    {
        event Action GamePaused;
    }
}