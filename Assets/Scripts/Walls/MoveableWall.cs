using System;
using System.Collections;
using UnityEngine;

namespace Wall
{
    public class MoveableWall : Wall
    {
        [SerializeField]
        private Transform _initialPosition;
        [SerializeField]
        private float _movementSpeed;
        [SerializeField]
        private WallType _wallType;

        private Coroutine _movementCoroutine;

        public event Action<MoveableWall> StopedMoving;
        public event Action<MoveableWall> ReachedDesiredPosition;

        public WallType WallType => _wallType;

        public void MoveTo(Vector3 position)
        {
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
            }

            _movementCoroutine = StartCoroutine(MoveToRoutine(position));
        }

        public void Move(MoveDirection direction)
        {
            Vector3 movementDiretion = _wallType == WallType.Horizontal ? Vector3.right : Vector3.forward;
            movementDiretion *= (int)direction;

            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
            }

            _movementCoroutine = StartCoroutine(MoveRoutine(movementDiretion));
        }

        private IEnumerator MoveToRoutine(Vector3 position)
        {
            while (transform.position != position)
            {
                transform.position = Vector3.MoveTowards(transform.position, position,
                    _movementSpeed * Time.deltaTime);
                yield return null;
            }

            ReachedDesiredPosition?.Invoke(this);
        }

        private IEnumerator MoveRoutine(Vector3 direction)
        {
            while (true)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction,
                    _movementSpeed * Time.deltaTime);
                yield return null;
            }
        }

        private void StopMoving()
        {
            StopCoroutine(_movementCoroutine);

            StopedMoving?.Invoke(this);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Wall wall))
            {
                StopMoving();
            }

            if (collision.gameObject.TryGetComponent(out IInteracteable interacteable))
            {
                interacteable.Interact();
            }
        }
    }
}
