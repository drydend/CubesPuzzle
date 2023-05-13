using System.Collections.Generic;
using UnityEngine;
using WallsSystem;

namespace LevelSystem
{
    public class LevelPreset : MonoBehaviour
    {
        [SerializeField]
        private List<CompleteTrigger> _completeTriggers;

        [SerializeField]
        private List<MoveableWall> _walls;

        public List<MoveableWall> Walls => _walls;
        public List<CompleteTrigger> CompleteTriggers => _completeTriggers;
    }
}