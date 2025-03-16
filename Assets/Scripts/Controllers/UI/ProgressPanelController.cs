using Signals.Initialize;
using Signals.Level;
using Signals.Request;
using Signals.Score;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers.UI
{
    public class ProgressPanelController : MonoBehaviour
    {
        [Inject]
        private SignalBus _signalBus;
        [SerializeField]
        private TMP_Text scoreText;
        [SerializeField]
        private TMP_Text levelText;
        [SerializeField]
        private Slider slider;

        private void UpdateScoreUI(ScoreUpdatedSignal signal)
        {
            scoreText.text = signal.Score.ToString();
        }

        private void ResetUI()
        {
            _signalBus.Fire(new RequestScoreSignal());
        }

        private void UpdateProgressUI(LevelChangedSignal signal)
        {
            slider.value = 0;
            levelText.text = signal.LevelIndex.ToString();
        }

        // private void IncreaseProgressUI(Transform other)
        // {
        //     slider.value++;
        // }

        // private void SetSliderMaxValue(Level currentLevel)
        // {
        //     slider.maxValue = currentLevel.indices.Length;
        // }

        private void OnEnable()
        {
            _signalBus.Subscribe<ScoreUpdatedSignal>(UpdateScoreUI);
            _signalBus.Subscribe<LevelChangedSignal>(UpdateProgressUI);
            _signalBus.Subscribe<LevelReplayedSignal>(ResetUI);

            _signalBus.Fire(new ProgressPanelInitializedSignal());
            // LevelManager.OnLevelLoaded += SetSliderMaxValue;
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<ScoreUpdatedSignal>(UpdateScoreUI);
            _signalBus.Unsubscribe<LevelChangedSignal>(UpdateProgressUI);
            _signalBus.Unsubscribe<LevelReplayedSignal>(ResetUI);
        }
    }
}
