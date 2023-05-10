using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace Input
{
    public class PlayerInput
    {
        private PlayerSwipeAction _playerSwipeActions;
        private Camera _camera;
        private PlayerSwipeConfig _playerSwipeConfig;

        private float _touchStartTime;
        private float _touchEndTime;

        private Vector2 _touchStartPosition;
        private Vector2 _touchEndPosition;

        public event Action<Vector2> Swiped;

        public PlayerInput(Camera camera, PlayerSwipeConfig config)
        {
            _playerSwipeActions = new PlayerSwipeAction();
            _camera = camera;
            _playerSwipeConfig = config;
        }

        public void Initialize()
        {   
            _playerSwipeActions.Enable();

            _playerSwipeActions.SwipeMap.PlayerContact.started += ctx => OnStarterPressing(ctx);
            _playerSwipeActions.SwipeMap.PlayerContact.canceled += ctx => OnEndedPressing(ctx);
        }

        private void OnEndedPressing(InputAction.CallbackContext ctx)
        {
            _touchEndPosition = _playerSwipeActions.SwipeMap.ContactPosition.ReadValue<Vector2>();
            _touchEndTime = (float)ctx.time;

            CheckSwipe();
        }

        private void CheckSwipe()
        {
            if (_touchEndTime - _touchStartTime > _playerSwipeConfig.SwipeMaxTime)
            {
                return;
            }

            var swipeDirection = CalculateSwipeDirection();
            Swiped?.Invoke(swipeDirection);
        }

        private Vector2 CalculateSwipeDirection()
        {
            return (_touchEndPosition - _touchStartPosition).normalized;
        }

        private void OnStarterPressing(InputAction.CallbackContext ctx)
        {
            _touchStartPosition = _playerSwipeActions.SwipeMap.ContactPosition.ReadValue<Vector2>();
            _touchStartTime = (float)ctx.startTime;
        }
    }
}
