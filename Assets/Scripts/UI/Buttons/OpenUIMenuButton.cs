using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace GameUI.Buttons
{
    [RequireComponent(typeof(InteractableUIButton))]
    public class OpenUIMenuButton : MonoBehaviour
    {
        [SerializeField]
        private UIMenu _menuToOpen;

        private UIMenusHolder _UIMenusHolder;
        private InteractableUIButton _button;

        [Inject]
        public void Construct(UIMenusHolder UIMenusHolder)
        {
            _UIMenusHolder = UIMenusHolder;
            _button = GetComponent<InteractableUIButton>();
            _button.onClick.AddListener(OpenMenu);
        }

        private void OpenMenu()
        {
            _UIMenusHolder.OpenMenu(_menuToOpen);
        }
    }
}
