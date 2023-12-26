using System;
using System.Collections.Generic;
using System.Threading;
using unity1week202312.Common;
using Cysharp.Threading.Tasks;

namespace unity1week202312.State
{
    public class StateTransitionFactory
    {
        public Dictionary<GameState, GameState> _statePathDic = new() {
            { GameState.Initializing, GameState.TitleShowing },
            { GameState.TitleShowing, GameState.MainLoading },
            { GameState.MainLoading, GameState.MainPlaying },
            { GameState.MainPlaying, GameState.ResultShowing },
            { GameState.ResultShowing, GameState.TitleShowing },
        };

        private CancellationToken _token;
        private readonly Dictionary<GameState, StateTransition> _stateTransitionDic;

        public StateTransitionFactory(
            CancellationToken token,
            SceneTransitionFactory sceneTransitionFactory
        ) {
            _token = token;
            _stateTransitionDic = new() {
                { GameState.Initializing, new StateTransition(CancellationToken.None, TransitionCondition.WaitClick, TransitionFunction.LoadTitleScene, sceneTransitionFactory) },
                { GameState.TitleShowing, new StateTransition(CancellationToken.None, TransitionCondition.WaitClick, TransitionFunction.LoadMainScene, sceneTransitionFactory) },
                { GameState.MainLoading, new StateTransition(CancellationToken.None, TransitionCondition.WaitClick, TransitionFunction.None, sceneTransitionFactory) },
                { GameState.MainPlaying, new StateTransition(CancellationToken.None, TransitionCondition.WaitClick, TransitionFunction.None, sceneTransitionFactory) },
                { GameState.ResultShowing, new StateTransition(CancellationToken.None, TransitionCondition.WaitClick, TransitionFunction.BackTitleScene, sceneTransitionFactory) },
            };
        }

        public StateTransition Create(GameState currentState)
        {
            Debug.Util.Log($"Create StateTransition: {currentState}");
            return _stateTransitionDic[currentState];
        }
    }
}