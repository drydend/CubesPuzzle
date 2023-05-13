using System.Collections.Generic;

namespace GameUI
{
    public class UIMenusHolder
    {
        public Stack<UIMenu> _openedMenus = new Stack<UIMenu>(); 

        public void CloseAllMenus()
        {
            for (int i = 0; i < _openedMenus.Count; i++)
            {
                _openedMenus.Pop().Close();
            }
        }

        public void OpenMenu(UIMenu menu)
        {
            HideCurrentMenu();
            menu.Open();
            _openedMenus.Push(menu);
        }

        public void CloseCurrentMenu()
        {
            if (_openedMenus.Count == 0)
            {
                return;
            }

            var menu = _openedMenus.Pop();
            menu.Close();

            if(_openedMenus.Count != 0) 
            {
                _openedMenus.Peek().Open();
            }
        }

        private void HideCurrentMenu()
        {
            if (_openedMenus.Count == 0)
            {
                return;
            }

            var currentMenu = _openedMenus.Peek();
            currentMenu.Close();
        }
    }
}
