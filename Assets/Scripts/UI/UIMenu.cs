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
            _animator.SetTrigger(OpenAnimationTrigger);
        }
        public virtual void Close()
        {
            _animator.SetTrigger(CloseAnimationTrigger);
        }
    }
}
