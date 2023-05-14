using UnityEngine;
using Zenject;

namespace GameUI.Buttons
{
    [RequireComponent(typeof(InteractableUIButton))]
    public class CloseUIMenuButton : MonoBehaviour
    {
        [SerializeField]
        private UIMenu _menuToClose;

        private UIMenusHolder _UIMenusHolder;
        private InteractableUIButton _button;

        [Inject]
        public void Construct(UIMenusHolder UIMenusHolder)
        {
            _UIMenusHolder = UIMenusHolder;
            _button = GetComponent<InteractableUIButton>();

            _button.onClick.AddListener(CloseMenu);
        }

        private void CloseMenu()
        {
            _UIMenusHolder.CloseMenu(_menuToClose);
        }
    }
}
