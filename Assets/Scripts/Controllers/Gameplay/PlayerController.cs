using Components;
using Enums;
using ScriptableObjects;
using Signals.Core;
using Signals.Cube;
using Signals.Level;
using Signals.Player;
using UnityEngine;
using Zenject;

namespace Controllers.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        [Inject]
        private SignalBus _signalBus;
        [Inject]
        private DiContainer _container;

        private PlayerMovementData _playerMovementData;
        private GameObject _prefab;
        private bool _isRunning;
        private int _cubeCount;

        private const float VerticalOffset = 1f;

        private void Awake()
        {
            _playerMovementData = Resources.Load<PlayerMovementData>("Data/ScriptableObjects/PlayerMovementData");
            _prefab = Resources.Load<GameObject>("Prefabs/GameObjects/Cube");
        }

        private void FixedUpdate()
        {
            if (!_isRunning) return;
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z + (_playerMovementData.verticalSpeed * Time.deltaTime)
            );
        }

        private void HorizontalMove(InputTakenSignal signal)
        {
            if (!_isRunning) return;
            var movementX = signal.MovementValue * _playerMovementData.horizontalSpeed * Time.deltaTime;
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x + movementX, _playerMovementData.minX, _playerMovementData.maxX),
                transform.position.y,
                transform.position.z
            );
        }

        private void CheckIsRunning(GameStateChangedSignal signal)
        {
            _isRunning = signal.State == GameState.Running;
        }

        private void AddCubes(int count)
        {
            for (var i = 0; i < count; i++)
                _container.InstantiatePrefab(_prefab, transform).transform.localPosition = (_cubeCount + i) * VerticalOffset * Vector3.up;

            _cubeCount += count;
        }

        private void AddCubes(AddCubeSignal signal)
        {
            AddCubes(signal.CubeCount);
        }

        private void DecreaseCubeCount(CubeRemovedSignal signal)
        {
            _cubeCount--;
            if (_cubeCount <= 0) _signalBus.Fire(new PlayerFailedSignal());
        }

        private void ResetPlayer(LevelLoadedSignal signal)
        {
            transform.position = new Vector3(0, 1f, 1f);
            var cubes = gameObject.GetComponentsInChildren<Cube>();
            foreach (var cube in cubes)
                Destroy(cube.gameObject);
            _cubeCount = 0;
            AddCubes(1);
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<GameStateChangedSignal>(CheckIsRunning);
            _signalBus.Subscribe<InputTakenSignal>(HorizontalMove);
            _signalBus.Subscribe<CubeRemovedSignal>(DecreaseCubeCount);
            _signalBus.Subscribe<AddCubeSignal>(AddCubes);
            _signalBus.Subscribe<LevelLoadedSignal>(ResetPlayer);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<GameStateChangedSignal>(CheckIsRunning);
            _signalBus.Unsubscribe<InputTakenSignal>(HorizontalMove);
            _signalBus.Unsubscribe<CubeRemovedSignal>(DecreaseCubeCount);
            _signalBus.Unsubscribe<AddCubeSignal>(AddCubes);
            _signalBus.Unsubscribe<LevelLoadedSignal>(ResetPlayer);
        }
    }
}