using System.Collections;
using UnityEngine;

namespace GameUI
{
    public class PointsMenuPoint : MonoBehaviour
    {
        private Coroutine _movementCoroutine;
        private Vector2 _desiredAnchorPosition;

        [field: SerializeField]
        public RectTransform RectTransform { get; private set; }

        public void Initialize(Vector2 startDesiredPosition)
        {
            _desiredAnchorPosition = startDesiredPosition;
        }

        public void MoveTo(Vector2 anchoredPosition, float time)
        {
            if(_movementCoroutine != null) 
            {
                StopCoroutine(_movementCoroutine);
            }

            _desiredAnchorPosition = anchoredPosition;
            _movementCoroutine = StartCoroutine(MoveToRoutine(anchoredPosition, time));
        }

        private void OnDisable()
        {
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
            }

            RectTransform.anchoredPosition = _desiredAnchorPosition;
        }

        private IEnumerator MoveToRoutine(Vector2 anchoredPosition, float time)
        {
            var animationTimeElapsed = 0f;
            var initialAnchoredPosition = RectTransform.anchoredPosition;

            while (animationTimeElapsed != 1)
            {
                var newPosition = Vector2.Lerp(initialAnchoredPosition, anchoredPosition, animationTimeElapsed);
                RectTransform.anchoredPosition = newPosition;

                animationTimeElapsed += Mathf.Clamp(animationTimeElapsed + Time.deltaTime / time, 0f, 1f);
                yield return null;
            }
        }
    }
}