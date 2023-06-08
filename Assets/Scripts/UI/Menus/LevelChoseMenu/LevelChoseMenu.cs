using Input;
using LevelSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using Zenject;

namespace GameUI.Menus
{
    public class LevelChoseMenu : UIMenu
    {
        private const float InputThreshold = 0.8f;

        [SerializeField]
        private float _swipeTime;

        [SerializeField]
        private float _menuPartYPosition;

        [SerializeField]
        private int _numberOfLevelPerList;
        [SerializeField]
        private LevelChoseMenuSlot _slotPrefab;
        [SerializeField]
        private LevelChoseMenuPart _partPrefab;
        [SerializeField]
        private RectTransform _menuPartsParent;
        [SerializeField]
        private PointsMenu _pointsMenu;

        private Vector2[] _menusAnchorDesiredPositions;
        private int[] _menusAnchorRelativePositions;

        private List<LevelChoseMenuSlot> _menuSlots = new List<LevelChoseMenuSlot>();
        private List<LevelChoseMenuPart> _menuParts = new List<LevelChoseMenuPart>();

        private Game _game;
        private PlayerInput _input;

        private int _openedMenuPartIndex = 0;

        [Inject]
        public void Construct(Game game, LevelsConfigs levelsConfigs, PlayerInput input)
        {
            _game = game;
            _input = input;
            var numberOfParts = Mathf.CeilToInt((float)levelsConfigs.Configs.Count / _numberOfLevelPerList);
            _menusAnchorDesiredPositions = new Vector2[numberOfParts];
            _menusAnchorRelativePositions = new int[numberOfParts];
            var configs = levelsConfigs.Configs.OrderBy(config => config.LevelNumber).ToList();

            for (int i = 0; i < Mathf.CeilToInt((float)levelsConfigs.Configs.Count / _numberOfLevelPerList); i++)
            {
                _menusAnchorRelativePositions[i] = i;
                var menuPart = InitializeMenuPart(i + 1, configs);
                _menuParts.Add(menuPart);
            }

            _pointsMenu.Initialize(Mathf.CeilToInt((float)levelsConfigs.Configs.Count / _numberOfLevelPerList), 0);
        }

        public override void Open()
        {
            base.Open();
            _input.Swiped += OnPlayerSwiped;

            foreach (var menuPart in _menuParts)
            {
                menuPart.SetActiveInteractors(true);
            }
        }

        public override void Close()
        {
            base.Close();
            _input.Swiped -= OnPlayerSwiped;

            foreach (var menuPart in _menuParts)
            {
                menuPart.SetActiveInteractors(false);
            }
        }

        public void OnClosed()
        {
            for (int i = 0; i < _menuParts.Count; i++)
            {
                _menuParts[i].StopMoving();
                _menuParts[i].SetAnchorePosition(_menusAnchorDesiredPositions[i]);
            }
        }

        public void UpdateUI(LevelsConfigs configs)
        {
            for (int i = 0; i < configs.Configs.Count; i++)
            {
                _menuSlots[i].UpdateUI(configs.Configs[i]);
            }
        }

        public override void ActivateInteractables()
        {
            base.ActivateInteractables();
            foreach (var menuPart in _menuParts)
            {
                menuPart.SetActiveInteractors(true);
            }
        }

        public override void DisableInteractables()
        {
            base.DisableInteractables();

            foreach (var menuPart in _menuParts)
            {
                menuPart.SetActiveInteractors(false);
            }
        }

        private void OnPlayerSwiped(Vector2 direction)
        {
            if (_menuParts.Count == 1)
            {
                return;
            }

            var moveDirection = MoveDirection.Forward;

            if (Vector2.Dot(Vector2.right, direction) > InputThreshold)
            {
                moveDirection = MoveDirection.Forward;
            }
            else if (Vector2.Dot(Vector2.left, direction) > InputThreshold)
            {
                moveDirection = MoveDirection.Backward;
            }
            else
            {
                return;
            }

            SwipeMenus(moveDirection);
        }

        private void SwipeMenus(MoveDirection moveDirection)
        {
            if (_menusAnchorRelativePositions[0] == 0 && moveDirection == MoveDirection.Forward)
            {
                return;
            }
            else if (_menusAnchorRelativePositions[_menusAnchorRelativePositions.Length - 1] == 0
                && moveDirection == MoveDirection.Backward)
            {
                return;
            }

            for (int i = 0; i < _menusAnchorRelativePositions.Length; i++)
            {
                _menusAnchorRelativePositions[i] += (int)moveDirection;
            }

            for (int i = 0; i < _menuParts.Count; i++)
            {
                _menusAnchorDesiredPositions[i] = CreateMenuAchorPositionForMenu(_menusAnchorRelativePositions[i]);

                _menuParts[i].MoveTo(_swipeTime, _menusAnchorDesiredPositions[i]);
            }

            _pointsMenu.TryMoveTo(moveDirection, _swipeTime);
        }

        private LevelChoseMenuPart InitializeMenuPart(int menuNumber, List<LevelConfig> levelsConfigs)
        {
            int lastLevelIndex = (menuNumber * _numberOfLevelPerList) < levelsConfigs.Count ?
              menuNumber * _numberOfLevelPerList : levelsConfigs.Count;

            var position = CreateMenuAchorPositionForMenu(menuNumber - 1);
            _menusAnchorDesiredPositions[menuNumber - 1] = position;
            var menuPart = Instantiate(_partPrefab, _menuPartsParent);
            menuPart.SetAnchorePosition(position);


            for (int i = _numberOfLevelPerList * (menuNumber - 1); i < lastLevelIndex; i++)
            {
                var slot = Instantiate(_slotPrefab);
                slot.Initialize(levelsConfigs[i], _game);
                menuPart.AddSlot(slot);
                _menuSlots.Add(slot);
            }

            return menuPart;
        }

        private Vector2 CreateMenuAchorPositionForMenu(int menuIndex)
        {
            Vector2 anchorPosition;

            if (menuIndex == 0)
            {
                anchorPosition = Vector2.zero;
                anchorPosition.y = _menuPartYPosition;
            }
            else
            {
                int sign = Utilities.GetSign(menuIndex);
                anchorPosition = new Vector2(Screen.width * 2 * sign + _partPrefab.Width / 2 * (menuIndex),
                    _menuPartYPosition);
            }

            return anchorPosition;
        }
    }
}
