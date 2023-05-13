namespace LevelSystem
{
    public interface ILevelCompleteChecker
    {   
        void SetLevel(LevelPreset level);
        bool CheckWin();
    }
}