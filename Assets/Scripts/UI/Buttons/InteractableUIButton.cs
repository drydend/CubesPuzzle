using UnityEngine.UI;

namespace GameUI.Buttons
{
    public class InteractableUIButton : Button, IInteracteableUI
    {
        public void SetActive(bool value)
        {
            interactable = value;
        }
    }
}
