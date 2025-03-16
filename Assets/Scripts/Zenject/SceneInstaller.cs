using Signals.Core;
using Signals.Cube;
using Signals.Initialize;
using Signals.Level;
using Signals.Player;
using Signals.Request;
using Signals.Score;
using Signals.UI.Button;

namespace Zenject
{
    public class SceneInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            //SignalBus install
            SignalBusInstaller.Install(Container);

            //Core Signals
            Container.DeclareSignal<GameStateChangedSignal>();
            Container.DeclareSignal<InputTakenSignal>();
            
            //Request Signals
            Container.DeclareSignal<RequestScoreSignal>();
            
            //Level Signals
            Container.DeclareSignal<LevelLoadedSignal>();
            Container.DeclareSignal<LevelReplayedSignal>();
            Container.DeclareSignal<LevelChangedSignal>();

            //Cube Signals
            Container.DeclareSignal<AddCubeSignal>();
            Container.DeclareSignal<CubeRemovedSignal>();

            //Score Signals
            Container.DeclareSignal<ScoreUpdatedSignal>();
            Container.DeclareSignal<ScoreCollectedSignal>();

            //Player Signals
            Container.DeclareSignal<PlayerFailedSignal>();
            Container.DeclareSignal<PlayerFinishedSignal>();

            //UI / Button Signals
            Container.DeclareSignal<PlayButtonClickedSignal>();
            Container.DeclareSignal<ReplayButtonClickedSignal>();
            Container.DeclareSignal<NextLevelButtonClickedSignal>();

            //Initialize Signals
            Container.DeclareSignal<NextLevelPanelInitializedSignal>();
            Container.DeclareSignal<ProgressPanelInitializedSignal>();
        }
    }
}