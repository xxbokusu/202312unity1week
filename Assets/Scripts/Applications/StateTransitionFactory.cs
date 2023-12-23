using System.Collections.Generic;
using System.Threading;
using unity1week202312.Common;

namespace unity1week202312.State
{
    public class StateTransitionFactory
    {
        CancellationToken _token;
        ScenePresenter _scenePresenter;

        public StateTransitionFactory(
            CancellationToken token,
            ScenePresenter scenePresenter
        ) {
            _token = token;
            _scenePresenter = scenePresenter;
        }

        private readonly Dictionary<GameState, StateTransition> _stateTransitions = new()
        {
            { GameState.Initializing, new StateTransition(CancellationToken.None) },
            { GameState.TitleShowing, new StateTransition(CancellationToken.None) },
            { GameState.MainLoading, new StateTransition(CancellationToken.None) },
            { GameState.MainPlaying, new StateTransition(CancellationToken.None) },
            { GameState.ResultShowing, new StateTransition(CancellationToken.None) },
        };

        public StateTransition Create(GameState currentState)
        {
            if (currentState == GameState.Initializing) {
                _scenePresenter.SetSceneTransition(SceneName.Initialize);
            }
            Debug.Util.Log($"Create StateTransition: {currentState}");
            return new StateTransition(_token);
        }
    }
}