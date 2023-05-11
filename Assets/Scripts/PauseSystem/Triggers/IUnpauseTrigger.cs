using System;

namespace PauseSystem
{
    public interface IUnpauseTrigger
    {
        event Action GameUnpaused;
    }
}
