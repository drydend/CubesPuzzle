namespace LevelSystem
{
    internal class LevelWinChecker : ILevelWinChecker
    {
        private LevelPreset _levelPreset;

        public LevelWinChecker(LevelPreset levelPreset)
        {
            _levelPreset = levelPreset;
        }

        public bool CheckWin()
        {
            return false;
        }
    }
}