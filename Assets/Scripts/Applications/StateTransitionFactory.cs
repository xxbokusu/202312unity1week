using System.Collections.Generic;
using System.Threading;
using unity1week202312.Common;

namespace unity1week202312.State
{
    public class StateTransitionFactory
    {
        CancellationToken _token;
        SceneTransitionFactory _sceneTransitionFactory;

        public StateTransitionFactory(
            CancellationToken token,
            SceneTransitionFactory sceneTransitionFactory
        ) {
            _token = token;
            _sceneTransitionFactory = sceneTransitionFactory;
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
                _sceneTransitionFactory.Create(SceneName.Initialize);
            }
            Debug.Util.Log($"Create StateTransition: {currentState}");
            return new StateTransition(_token);
        }
    }
}