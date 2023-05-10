using System.Collections.Generic;
using UnityEngine;
using Wall;

namespace LevelSystem
{
    public class LevelPreset : MonoBehaviour
    {
        [SerializeField]
        private List<MoveableWall> _walls;

        public List<MoveableWall> Walls => _walls;
    }
}