using PauseSystem;
using System;
using System.Collections;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

namespace WallsSystem
{
    public class MoveableWall : Wall, IPauseable
    {
        private const float DistanceEpsilon = 0.1f;

        [SerializeField]
        private float _movementSpeed;
        [SerializeField]
        private WallType _wallType;
        [SerializeField]
        private float _wallWidth;
        [SerializeField]
        private LayerMask _wallLayer;

        private Coroutine _movementCoroutine;

        private Vector3 _localForwardSideCenterPoint;
        private Vector3 _localBackwardSideCenterPoint;

        private bool _isMoving;
        private bool _isPaused;
        private Vector3 _currentMovingDirection;

        public event Action<MoveableWall> ReachedDesiredPosition;

        [field: SerializeField]
        public Transform InitialPosition { get; private set; }
        public WallType WallType => _wallType;

        private Vector3 WorldForwardSideCenterPoint => transform.position + _localForwardSideCenterPoint;
        private Vector3 WorldBackwardSideCenterPoint => transform.position + _localBackwardSideCenterPoint;

        public void StopMovingImidiatly()
        {
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
            }
        }

        public void SetPositionTo(Vector3 position)
        {
            transform.position = position;
        }

        public void MoveTo(Vector3 position)
        {
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
            }

            var movementDirection = (position - transform.position).normalized;
            _currentMovingDirection = movementDirection;

            _movementCoroutine = StartCoroutine(MoveToRoutine(position));
        }

        public void Move(MoveDirection direction)
        {
            Vector3 movementDiretion = _wallType == WallType.Horizontal ? Vector3.right : Vector3.forward;
            movementDiretion *= (int)direction;
            
            _currentMovingDirection = movementDiretion;

            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
            }

            var position = CalculatePositionMoveTo(direction);
            _movementCoroutine = StartCoroutine(MoveToRoutine(position));
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
            var hits = Physics.RaycastAll(ray);

            foreach (var hit in hits)
            {
                if (hit.transform.TryGetComponent(out Wall wall) && wall != this)
                {
                    var distance = Vector3.Distance(hit.point, transform.position);

                    if (distance - DistanceEpsilon <= _wallWidth / 2)
                    {
                        return false;
                    }
                }
            }

            var centerOfBox = transform.position + movementDiretion * _wallWidth / 2;

            var boxSize = GetSizeOfOverlapBox();
            var colliders = Physics.OverlapBox(centerOfBox, boxSize);

            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out Wall wall) && wall != this)
                {
                    return false;
                }
            }

            return true;
        }

        private Vector3 GetSizeOfOverlapBox()
        {
            if (WallType == WallType.Horizontal)
            {
                return new Vector3(0.45f, 0.45f, 0.15f);
            }
            else if (WallType == WallType.Vertical)
            {
                return new Vector3(0.15f, 0.45f, 0.45f);
            }
            else
            {
                return new Vector3(0.45f, 0.45f, 0.45f);
            }
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Unpause()
        {
            _isPaused = false;
        }

        private Vector3 CalculatePositionMoveTo(MoveDirection direction)
        {
            Vector3 movementDirection = _wallType == WallType.Horizontal ? Vector3.right : Vector3.forward;
            movementDirection *= (int)direction;

            if (direction == MoveDirection.Forward)
            {
                var ray = new Ray(WorldForwardSideCenterPoint, movementDirection);

                if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, _wallLayer) &&
                    hit.collider.gameObject.TryGetComponent(out Wall wall))
                {
                    var desiredPosition = new Vector3(hit.point.x - _wallWidth / 2 * movementDirection.x,
                        WorldForwardSideCenterPoint.y, hit.point.z - _wallWidth / 2 * movementDirection.z);

                    return desiredPosition;
                }
            }
            else if (direction == MoveDirection.Backward)
            {
                var ray = new Ray(WorldBackwardSideCenterPoint, movementDirection);

                if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, _wallLayer) &&
                    hit.collider.gameObject.TryGetComponent(out Wall wall))
                {
                    var desiredPosition = new Vector3(hit.point.x - _wallWidth / 2 * movementDirection.x,
                       WorldBackwardSideCenterPoint.y, hit.point.z - _wallWidth / 2 * movementDirection.z);

                    return desiredPosition;
                }
            }
            else
            {
                throw new Exception("Can not find point to move when move direction is none");
            }

            throw new Exception("There is no object to move to");
        }

        private IEnumerator MoveToRoutine(Vector3 position)
        {
            _isMoving = true;

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

            _isMoving = false;
            ReachedDesiredPosition?.Invoke(this);
        }

        private void StopMoving()
        {
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
                _isMoving = false;
            }
        }

        private void Awake()
        {
            if (_wallType == WallType.Horizontal)
            {
                _localForwardSideCenterPoint = Vector3.right * _wallWidth / 2;
                _localBackwardSideCenterPoint = Vector3.left * _wallWidth / 2;
            }
            else if (_wallType == WallType.Vertical)
            {
                _localForwardSideCenterPoint = Vector3.forward * _wallWidth / 2;
                _localBackwardSideCenterPoint = Vector3.back * _wallWidth / 2;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IInteracteable interacteable))
            {
                interacteable.Interact();
            }
        }

    }
}
