using System;
using UnityEngine;

namespace LevelSystem
{
    [CreateAssetMenu(menuName = "Level Config")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public LevelPreset Preset { get; private set; }
        [field: SerializeField] public int LevelNumber { get; private set; }
        [field: SerializeField] public float InitialCameraSize { get; private set; }
        [field: SerializeField] public float CameraSize { get; private set; }
        [field : SerializeField] public bool IsUnlocked { get; private set; }
        
        public void InitializeWithData(LevelSaveData levelSaveDate)
        {
            IsUnlocked = levelSaveDate.IsUnlocked;
        }

        public void InitializeWithDefaults()
        {

        }

        public void Unlock()
        {
            IsUnlocked = true;
        }
    }
}
