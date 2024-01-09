using System.Collections.Generic;
using System.Threading;
using unity1week202312.Common;
using unity1week202312.MainGame;

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

        private GameState _currentGameState;

        public StateTransitionFactory(
            CancellationToken token,
            SceneTransitionFactory sceneTransitionFactory,
            PlayerViewFactory playerViewFactory
        ) {
            _token = token;
            _stateTransitionDic = new() {
                { 
                    GameState.Initializing, new StateTransition(
                        CancellationToken.None,
                        TransitionCondition.WaitClick, 
                        TransitionFunction.LoadTitleScene, 
                        sceneTransitionFactory,
                        playerViewFactory
                    ) 
                },
                { 
                    GameState.TitleShowing, new StateTransition(
                        CancellationToken.None, 
                        TransitionCondition.WaitClick, 
                        TransitionFunction.LoadMainScene, 
                        sceneTransitionFactory,
                        playerViewFactory
                    )
                },
                { 
                    GameState.MainLoading, new StateTransition(
                        CancellationToken.None, 
                        TransitionCondition.WaitClick, 
                        TransitionFunction.StartMainGame, 
                        sceneTransitionFactory,
                        playerViewFactory
                    )
                },
                { 
                    GameState.MainPlaying, new StateTransition(
                        CancellationToken.None, 
                        TransitionCondition.WaitClick, 
                        TransitionFunction.None, 
                        sceneTransitionFactory,
                        playerViewFactory
                    ) 
                },
                { 
                    GameState.ResultShowing, new StateTransition(
                        CancellationToken.None, 
                        TransitionCondition.WaitClick, 
                        TransitionFunction.BackTitleScene, 
                        sceneTransitionFactory,
                        playerViewFactory
                    ) 
                },
            };

            _currentGameState = GameState.Initializing;
        }

        public async void WatchStateTransition(StateTransition transition)
        {
            await transition.Execute();
            _currentGameState = _statePathDic[_currentGameState];
            WatchStateTransition(Create());
        }


        public StateTransition Create()
        {
            Debug.Util.Log($"Create StateTransition: {_currentGameState}");
            return _stateTransitionDic[_currentGameState];
        }
    }
}