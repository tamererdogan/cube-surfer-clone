namespace Zenject
{
    public class SceneInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            //SignalBus install
            SignalBusInstaller.Install(Container);

            //Signal declaration
            //Container.DeclareSignal<PlaceHolderSignal>();
        }
    }
}