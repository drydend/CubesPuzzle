using System;
using UnityEngine;

namespace LevelSystem
{
    [CreateAssetMenu(menuName = "Level Config")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField]
        private LevelPreset _levelPreset;
        [SerializeField]
        private int _levelNumber = 0;
        [SerializeField]
        private Vector3 _cameraPosition;
        [SerializeField]
        private Vector3 _cameraRotation;
        [SerializeField]
        private Vector3 _initialCameraPosition;
        [SerializeField]
        private bool _isUnlocked = false;

        public LevelPreset Preset => _levelPreset;
        public int LevelNumber => _levelNumber;
        public Vector3 CameraPosition => _cameraPosition;
        public Vector3 CameraRotation => _cameraRotation;
        public Vector3 InitialCameraPosition => _initialCameraPosition;
        public bool IsUnlocked => _isUnlocked;

        public void InitializeWithData(LevelSaveData levelSaveDate)
        {
            _isUnlocked = levelSaveDate.IsUnlocked;
        }

        public void InitializeWithDefaults()
        {

        }

        public void Unlock()
        {
            _isUnlocked = true;
        }
    }
}
