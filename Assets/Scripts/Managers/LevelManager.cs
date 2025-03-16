using System.Collections.Generic;
using System.Linq;
using Signals.Initialize;
using Signals.Level;
using Signals.UI.Button;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [Inject]
        private SignalBus _signalBus;
        [Inject]
        private DiContainer _container;

        private GameObject _levelHolder;
        private int _levelIndex;
        private List<GameObject> _levels;

        private const string LevelHolderName = "LevelHolder";

        private void Awake()
        {
            _levelIndex = PlayerPrefs.GetInt("level", 0);
        }

        private void Start()
        {
            _levels = LoadLevelData();
            LoadLevel();
        }

        private void ReplayLevel(ReplayButtonClickedSignal signal)
        {
            _signalBus.Fire(new LevelReplayedSignal());
            LoadLevel();
        }

        private void NextLevel(NextLevelButtonClickedSignal signal)
        {
            _levelIndex++;
            PlayerPrefs.SetInt("level", _levelIndex);
            LoadLevel();
            NotifyLevelChanged();
        }

        private void LoadLevel()
        {
            Destroy(_levelHolder);
            _levelHolder = new GameObject(LevelHolderName);
            _container.InstantiatePrefab(_levels[GetLevelIndex()], _levelHolder.transform);
            NotifyLevelLoaded();
        }

        private List<GameObject> LoadLevelData()
        {
            return Resources.LoadAll<GameObject>("Prefabs/Level").ToList();
        }

        private int GetLevelIndex()
        {
            return _levelIndex > _levels.Count - 1 ? _levelIndex % _levels.Count : _levelIndex;
        }

        private void NotifyLevelChanged()
        {
            _signalBus.Fire(new LevelChangedSignal(_levelIndex));
        }

        private void NotifyLevelLoaded()
        {
            _signalBus.Fire(new LevelLoadedSignal());
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<NextLevelButtonClickedSignal>(NextLevel);
            _signalBus.Subscribe<ReplayButtonClickedSignal>(ReplayLevel);
            _signalBus.Subscribe<ProgressPanelInitializedSignal>(NotifyLevelChanged);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<NextLevelButtonClickedSignal>(NextLevel);
            _signalBus.Unsubscribe<ReplayButtonClickedSignal>(ReplayLevel);
            _signalBus.Unsubscribe<ProgressPanelInitializedSignal>(NotifyLevelChanged);
        }
    }
}