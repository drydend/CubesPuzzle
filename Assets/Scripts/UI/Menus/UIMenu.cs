using UnityEngine;

namespace GameUI
{
    public class UIMenu : MonoBehaviour
    {
        private const string OpenAnimationTrigger = "Open trigger";
        private const string CloseAnimationTrigger = "Close trigger";

        [SerializeField]
        private Animator _animator;

        public virtual void Open()
        {   
            gameObject.SetActive(true);
            _animator.SetTrigger(OpenAnimationTrigger);
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
