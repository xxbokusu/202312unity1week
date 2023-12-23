using System;
using unity1week202312.Common;
using VContainer.Unity;

namespace unity1week202312.State {
    public class GameInitializer : IInitializable
    {
        ScenePresenter _scenePresenter;
        StateTransitionFactory _factory;

        public GameInitializer(
            StateTransitionFactory factory,
            ScenePresenter scenePresenter
        ) {
            _factory = factory;
            _scenePresenter = scenePresenter;
        }

        public void Initialize()
        {
            _scenePresenter.SetSceneTransition(SceneName.Initialize);
        }
    }
}