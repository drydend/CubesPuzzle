using UnityEngine;

namespace LevelSystem
{
    [CreateAssetMenu(menuName = "Level Config")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField]
        private LevelPreset _levelPreset;

    }
}
