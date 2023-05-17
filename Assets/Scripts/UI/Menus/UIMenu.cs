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
            ActivateInteractables();
            gameObject.SetActive(true);
            _animator.SetTrigger(OpenAnimationTrigger);
        }


        public virtual void Close()
        {
            _animator.SetTrigger(CloseAnimationTrigger);
        }


        public void DisableObject()
        {
            gameObject.SetActive(false);
        }

        public virtual void ActivateInteractables()
        {
            foreach (var item in _buttons)
            {
                item.SetActive(true);
            }
        }


        public virtual void DisableInteractables()
        {

        }
    }
}
