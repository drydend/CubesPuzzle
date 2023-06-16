using System;
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
        private List<MainCube> _mainCubes;
        public List<MainCube> MainCubes
        {
            get
            {
                CacheMainCubes();

                return _mainCubes;
            }
        }

        public List<MoveableWall> Walls => _walls;
        public List<CompleteTrigger> CompleteTriggers => _completeTriggers;

        public void ResetLevel()
        {
            foreach (var wall in _walls)
            {
                wall.SetPositionTo(wall.InitialPosition.position);
            }
        }

        private void CacheMainCubes()
        {
            if (_mainCubes == null)
            {
                _mainCubes = new List<MainCube>();

                foreach (var wall in _walls)
                {
                    if (wall.GetType() == typeof(MainCube))
                    {
                        _mainCubes.Add((MainCube)wall);
                    }
                }
            }
        }
    }
}