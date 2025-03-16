using Signals.Initialize;
using Signals.Level;
using Signals.Request;
using Signals.Score;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [Inject]
        private SignalBus _signalBus;

        private int _score;
        private int _previousScore;

        private void Awake()
        {
            _score = _previousScore = PlayerPrefs.GetInt("score", 0);
        }

        private void Start()
        {
            NotifyScoreChanged();
        }

        private void IncreaseScore(ScoreCollectedSignal signal)
        {
            _score++;
            NotifyScoreChanged(null);
        }

        private void RestorePrevScore()
        {
            _score = _previousScore;
        }

        private void SaveScore(LevelChangedSignal signal)
        {
            _previousScore = _score;
            PlayerPrefs.SetInt("score", _score);
        }

        private void NotifyScoreChanged(object  signal = null)
        {
            _signalBus.Fire(new ScoreUpdatedSignal(_score));
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<ScoreCollectedSignal>(IncreaseScore);
            _signalBus.Subscribe<LevelChangedSignal>(SaveScore);
            _signalBus.Subscribe<LevelReplayedSignal>(RestorePrevScore);
            _signalBus.Subscribe<RequestScoreSignal>(NotifyScoreChanged);
            _signalBus.Subscribe<NextLevelPanelInitializedSignal>(NotifyScoreChanged);
            _signalBus.Subscribe<ProgressPanelInitializedSignal>(NotifyScoreChanged);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<ScoreCollectedSignal>(IncreaseScore);
            _signalBus.Unsubscribe<LevelChangedSignal>(SaveScore);
            _signalBus.Unsubscribe<LevelReplayedSignal>(RestorePrevScore);
            _signalBus.Unsubscribe<RequestScoreSignal>(NotifyScoreChanged);
            _signalBus.Unsubscribe<NextLevelPanelInitializedSignal>(NotifyScoreChanged);
            _signalBus.Unsubscribe<ProgressPanelInitializedSignal>(NotifyScoreChanged);
        }
    }
}