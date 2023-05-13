using PauseSystem;
using System;
using System.Collections;
using UnityEngine;

namespace WallsSystem
{
    public class MoveableWall : Wall, IPauseable
    {
        [SerializeField]
        private Transform _initialPosition;
        [SerializeField]
        private float _movementSpeed;
        [SerializeField]
        private WallType _wallType;

        private Coroutine _movementCoroutine;
        private Wall _lastTouchedWall;

        private bool _isPaused;

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

        public bool CanMoveInDirection(MoveDirection direction)
        {
            Vector3 movementDiretion = _wallType == WallType.Horizontal ? Vector3.right : Vector3.forward;
            movementDiretion *= (int)direction;

            return CanMoveInDirection(movementDiretion);
        }

        public bool CanMoveInDirection(Vector3 movementDiretion)
        {
            var ray = new Ray(transform.position, movementDiretion);
            var raycasts = Physics.RaycastAll(ray);

            if (raycasts.Length == 0)
            {
                return true;
            }

            if (raycasts[0].collider.gameObject.TryGetComponent(out Wall wall) && wall == this)
            {
                if (raycasts[1].collider.gameObject.TryGetComponent(out Wall secondWall) && secondWall == _lastTouchedWall)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (raycasts[0].collider.gameObject.TryGetComponent(out Wall secondWall) && secondWall == _lastTouchedWall)
            {
                return false;
            }

            return true;
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Unpause()
        {
            _isPaused = false;
        }


        private IEnumerator MoveToRoutine(Vector3 position)
        {
            while (transform.position != position)
            {
                transform.position = Vector3.MoveTowards(transform.position, position,
                    _movementSpeed * Time.deltaTime);

                while (_isPaused)
                {
                    yield return null;
                }

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

                while (_isPaused)
                {
                    yield return null;
                }

                yield return null;
            }
        }

        private void StopMoving()
        {
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
            }

            StopedMoving?.Invoke(this);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Wall wall))
            {
                _lastTouchedWall = wall;
                StopMoving();
            }

            if (collision.gameObject.TryGetComponent(out IInteracteable interacteable))
            {
                interacteable.Interact();
            }
        }
    }
}
