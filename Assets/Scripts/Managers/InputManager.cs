using Enums;
using Signals.Core;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        [Inject]
        private SignalBus _signalBus;

        private bool _isRunning;

        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
        private void Update()
        {
            if (!_isRunning) return;
            if (Input.touchCount == 0) return;
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
                _signalBus.Fire(new InputTakenSignal(touch.deltaPosition.x));
        }

        private void CheckIsRunning(GameStateChangedSignal signal)
        {
            _isRunning = signal.State == GameState.Running;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<GameStateChangedSignal>(CheckIsRunning);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<GameStateChangedSignal>(CheckIsRunning);
        }
    }
}