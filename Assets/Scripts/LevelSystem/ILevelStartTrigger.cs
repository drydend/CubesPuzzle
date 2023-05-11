using UnityEngine;
using System;

namespace LevelSystem
{
    public interface ILevelStartTrigger
    {
        event Action<Vector2> LevelStarted;
    }
}