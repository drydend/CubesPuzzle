using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelSaveData
{
    public int LevelNumber { get; private set; }
    public bool IsUnlocked { get; private set; }

    public LevelSaveData(int levelNumber, bool isUnlocked)
    {
        LevelNumber = levelNumber;
        IsUnlocked = isUnlocked;
    }
}
