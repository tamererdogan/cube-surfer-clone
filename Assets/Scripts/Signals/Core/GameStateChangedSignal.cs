using Enums;

namespace Signals.Core
{
    public class GameStateChangedSignal
    {
        public readonly GameState State;

        public GameStateChangedSignal(GameState state)
        {
            State = state;
        }
    }
}