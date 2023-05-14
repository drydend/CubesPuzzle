using TMPro;
using UnityEngine;

namespace GameUI
{
    public class LevelCounter : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _target;

        public void SetLevel(int levelNumber)
        {
            _target.text = "Level " + levelNumber.ToString();
        }
    }
}