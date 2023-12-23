using VContainer;
using VContainer.Unity;
using unity1week202312.State;
using Cysharp.Threading.Tasks;

namespace unity1week202312.Common {
    public class RootLifeTimeScope : LifetimeScope {
        protected override void Configure(IContainerBuilder builder) {
            builder.RegisterEntryPoint<GameInitializer>();

            ScenePresenter scenePresenter = new(this.GetCancellationTokenOnDestroy());
            StateTransitionFactory stateTransitionFactory = new(this.GetCancellationTokenOnDestroy(), scenePresenter);

            builder.RegisterInstance(scenePresenter).As<ScenePresenter>();
            builder.RegisterInstance(stateTransitionFactory).As<StateTransitionFactory>();

        }
    }

}