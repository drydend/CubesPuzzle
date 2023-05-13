using System;
using UnityEngine;
using UnityEngine.UI;

namespace PauseSystem
{
    [RequireComponent(typeof(Button), typeof(Animator))]
    public class PauseButton : MonoBehaviour, IPauseTrigger
    {
        private const string CloseAnimationTrigger = "Close trigger";
        private const string OpenAnimationTrigger = "Open trigger";

        private Animator _animator;
        private Button _button;

        public event Action GamePaused;

        public void Close()
        {
            _animator.SetTrigger(CloseAnimationTrigger);
        }

        public void Open()
        {
            _animator.SetTrigger(OpenAnimationTrigger);
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(PauseGame);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(PauseGame);
        }

        private void PauseGame()
        {
            GamePaused?.Invoke();
        }
    }
}