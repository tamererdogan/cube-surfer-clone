using Abstracts;
using Signals.Initialize;
using Signals.Score;
using Signals.UI.Button;
using TMPro;
using UnityEngine;
using Zenject;

namespace Controllers.UI
{
    public class NextLevelPanelController : AbstractButtonEventController
    {
        [Inject]
        private SignalBus _signalBus;
        [SerializeField]
        private TMP_Text scoreText;

        protected override void OnClick()
        {
           _signalBus.Fire(new NextLevelButtonClickedSignal());
        }

        private void UpdateScoreUI(ScoreUpdatedSignal signal)
        {
            scoreText.text = "Score: " + signal.Score;
        }

        private new void OnEnable()
        {
            base.OnEnable();
            _signalBus.Subscribe<ScoreUpdatedSignal>(UpdateScoreUI);
            _signalBus.Fire(new NextLevelPanelInitializedSignal());
        }

        private new void OnDisable()
        {
            base.OnDisable();
            _signalBus.Unsubscribe<ScoreUpdatedSignal>(UpdateScoreUI);
        }
    }
}
