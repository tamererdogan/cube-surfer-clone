namespace Signals.Level
{
    public class LevelChangedSignal
    {
        public readonly int LevelIndex;

        public LevelChangedSignal(int levelIndex)
        {
            LevelIndex = levelIndex;
        }
    }
}