using VContainer;
using VContainer.Unity;
using unity1week202312.State;
using Cysharp.Threading.Tasks;

namespace unity1week202312.Common {
    public class RootLifeTimeScope : LifetimeScope {
        protected override void Configure(IContainerBuilder builder) {
            builder.RegisterEntryPoint<GameInitializer>();

            StateTransitionFactory stateTransitionFactory = new(this.GetCancellationTokenOnDestroy());
            ScenePresenter scenePresenter = new(this.GetCancellationTokenOnDestroy());

            builder.RegisterInstance(stateTransitionFactory).As<StateTransitionFactory>();
            builder.RegisterInstance(scenePresenter).As<ScenePresenter>();
            
        }
    }

}