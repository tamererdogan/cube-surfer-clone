using Abstracts;
using Signals.UI.Button;
using Zenject;

namespace Controllers.UI
{
    public class ReplayPanelController : AbstractButtonEventController
    {
        [Inject]
        private SignalBus _signalBus;

        protected override void OnClick()
        {
           _signalBus.Fire(new ReplayButtonClickedSignal());
        }
    }
}
