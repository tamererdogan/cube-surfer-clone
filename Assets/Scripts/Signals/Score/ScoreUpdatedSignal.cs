namespace Signals.Score
{
    public class ScoreUpdatedSignal
    {
        public readonly int Score;

        public ScoreUpdatedSignal(int score)
        {
            Score = score;
        }
    }
}