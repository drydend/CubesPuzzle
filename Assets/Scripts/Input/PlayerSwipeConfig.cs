using UnityEngine;

namespace Input
{
    [CreateAssetMenu(menuName ="Swipe config")]
    public class PlayerSwipeConfig : ScriptableObject
    {
        [SerializeField]
        private float _swipeMaxTime;

        public float SwipeMaxTime => _swipeMaxTime;
    }
}