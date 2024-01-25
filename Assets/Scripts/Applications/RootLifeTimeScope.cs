using VContainer;
using VContainer.Unity;
using unity1week202312.State;
using Cysharp.Threading.Tasks;
using unity1week202312.MainGame;
using unity1week202312.Title;
using UnityEngine;

namespace unity1week202312.Common {
    public class RootLifeTimeScope : LifetimeScope {
        [SerializeField] private ObjectViewPrefabData _playerViewPrefabData;
        protected override void Configure(IContainerBuilder builder) {
            builder.RegisterEntryPoint<GameInitializer>();

            SceneTransitionFactory scenePresenter = new(this.GetCancellationTokenOnDestroy());
            PlayerViewFactory playerViewFactory = new(_playerViewPrefabData);
            TitleCharacterViewFactory titleCharacterViewFactory = new(_playerViewPrefabData);
            StateTransitionFactory stateTransitionFactory = new(this.GetCancellationTokenOnDestroy(), scenePresenter, titleCharacterViewFactory, playerViewFactory);

            builder.RegisterInstance(scenePresenter).As<SceneTransitionFactory>();
            builder.RegisterInstance(stateTransitionFactory).As<StateTransitionFactory>();

            builder.RegisterInstance(_playerViewPrefabData).As<ObjectViewPrefabData>();
            builder.RegisterInstance(playerViewFactory).As<IObjectViewFactory>();
            builder.RegisterInstance(titleCharacterViewFactory).As<IObjectViewFactory>();
        }
    }

}