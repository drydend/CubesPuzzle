using GameUI.Buttons;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameUI
{
    public class UIMenu : MonoBehaviour
    {
        private const string OpenAnimationTrigger = "Open trigger";
        private const string CloseAnimationTrigger = "Close trigger";

        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private List<InteractableUIButton> _buttons;

        public virtual void Open()
        {
            ActivateButtons();
            gameObject.SetActive(true);
            _animator.SetTrigger(OpenAnimationTrigger);
        }

        private void ActivateButtons()
        {
            foreach (var item in _buttons)
            {
                item.SetActive(true);
            }
        }

        public virtual void Close()
        {
            _animator.SetTrigger(CloseAnimationTrigger);
        }

        public void DisableInteractables()
        {

        }

        public void DisableObject()
        {
            gameObject.SetActive(false);
        }
    }
}
