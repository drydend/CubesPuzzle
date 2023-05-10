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

        public LevelPreset Preset => _levelPreset;
        public int LevelNumber => _levelNumber;
        public Vector3 CameraPosition => _cameraPosition;
        public Vector3 CameraRotation => _cameraRotation;
    }
}
