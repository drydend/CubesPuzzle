using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUI.Menus
{
    public class LevelChoseMenuPart : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _rectTransform;

        private List<LevelChoseMenuSlot> _slots = new List<LevelChoseMenuSlot>();

        private Coroutine _movementCoroutine;
        public float Width => _rectTransform.sizeDelta.x;

        public void AddSlot(LevelChoseMenuSlot slots)
        {
            _slots.Add(slots);
            slots.transform.SetParent(_rectTransform);
        }

        public void SetActiveInteractors(bool value)
        {
            foreach (var item in _slots)
            {
                item.SetActive(value);
            }
        }

        public void SetAnchorePosition(Vector2 position)
        {
            _rectTransform.anchoredPosition = position;
        }

        public void StopMoving()
        {
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
            }
        }

        public void MoveTo(float time, Vector2 anchorPosition)
        {
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
            }

            _movementCoroutine = StartCoroutine(MoveToRoutine(time, anchorPosition));
        }

        private IEnumerator MoveToRoutine(float time, Vector2 anchorPosition)
        {
            var animationTimeElapsed = 0f;

            while (animationTimeElapsed != 1)
            {
                _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition,
                    anchorPosition, animationTimeElapsed);

                animationTimeElapsed = Mathf.Clamp(animationTimeElapsed + Time.deltaTime / time, 0, 1);
                yield return null;
            }
        }
    }
}