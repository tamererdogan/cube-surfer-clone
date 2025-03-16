using Enums;
using Signals.Core;
using Signals.Player;
using Signals.UI.Button;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Inject]
        private SignalBus _signalBus;

        private GameState _gameState = GameState.PreGame;

        private void StartGame(object signal)
        {
            ChangeState(GameState.Running);
            Time.timeScale = 1f;
        }

        private void FailGame(PlayerFailedSignal signal)
        {
            ChangeState(GameState.Failed);
            Time.timeScale = 0f;
        }

        private void FinishGame(PlayerFinishedSignal signal)
        {
            ChangeState(GameState.Finished);
            Time.timeScale = 0f;
        }

        private void ChangeState(GameState state)
        {
            _gameState = state;
            _signalBus.Fire(new GameStateChangedSignal(_gameState));
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<PlayButtonClickedSignal>(StartGame);
            _signalBus.Subscribe<ReplayButtonClickedSignal>(StartGame);
            _signalBus.Subscribe<NextLevelButtonClickedSignal>(StartGame);
            _signalBus.Subscribe<PlayerFailedSignal>(FailGame);
            _signalBus.Subscribe<PlayerFinishedSignal>(FinishGame);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<PlayButtonClickedSignal>(StartGame);
            _signalBus.Unsubscribe<ReplayButtonClickedSignal>(StartGame);
            _signalBus.Unsubscribe<NextLevelButtonClickedSignal>(StartGame);
            _signalBus.Unsubscribe<PlayerFailedSignal>(FailGame);
            _signalBus.Unsubscribe<PlayerFinishedSignal>(FinishGame);
        }
    }
}