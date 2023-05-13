using UnityEngine;
using UnityEngine.EventSystems;

namespace Input
{
    public class GameInputField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool _playerTouched;
        private int _numberOfTouches;
        public bool IsFingerOnField()
        {
            return _playerTouched;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _playerTouched = true;
            _numberOfTouches++;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _numberOfTouches--;
            if (_numberOfTouches == 0)
            {
                _playerTouched = false;
            }
        }
    }
}