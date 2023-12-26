using System;
using System.Collections.Generic;
using System.Threading;
using unity1week202312.Common;
using Cysharp.Threading.Tasks;

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

        private readonly Dictionary<GameState, StateTransition> _stateTransitionDic = new()
        {
            { GameState.Initializing, new StateTransition(CancellationToken.None, TransitionCondition.WaitClick) },
            { GameState.TitleShowing, new StateTransition(CancellationToken.None, TransitionCondition.WaitClick) },
            { GameState.MainLoading, new StateTransition(CancellationToken.None, TransitionCondition.WaitClick) },
            { GameState.MainPlaying, new StateTransition(CancellationToken.None, TransitionCondition.WaitClick) },
            { GameState.ResultShowing, new StateTransition(CancellationToken.None, TransitionCondition.WaitClick) },
        };

        public StateTransition Create(GameState currentState)
        {
            if (currentState == GameState.Initializing) {
                _sceneTransitionFactory.Create(SceneName.Initialize);
            }
            Debug.Util.Log($"Create StateTransition: {currentState}");
            return _stateTransitionDic[currentState];
        }
    }
}