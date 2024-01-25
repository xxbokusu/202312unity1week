using System.Collections.Generic;
using System.Threading;
using unity1week202312.Common;
using unity1week202312.MainGame;
using unity1week202312.Title;

namespace unity1week202312.State
{
    public class StateTransitionFactory
    {
        public Dictionary<GameState, GameState> _statePathDic = new() {
            { GameState.Initializing, GameState.TitleLoading },
            { GameState.TitleLoading, GameState.TitleShowing },
            { GameState.TitleShowing, GameState.MainLoading },
            { GameState.MainLoading, GameState.MainPlaying },
            { GameState.MainPlaying, GameState.ResultShowing },
            { GameState.ResultShowing, GameState.TitleLoading },
        };

        private CancellationToken _token;
        private readonly Dictionary<GameState, StateTransition> _stateTransitionDic;

        private GameState _currentGameState;

        public StateTransitionFactory(
            CancellationToken token,
            SceneTransitionFactory sceneTransitionFactory,
            TitleCharacterViewFactory titleViewFactory,
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
                        titleViewFactory,
                        playerViewFactory
                    ) 
                },
                { 
                    GameState.TitleLoading, new StateTransition(
                        CancellationToken.None, 
                        TransitionCondition.WaitClick, 
                        TransitionFunction.ShowTitleScene, 
                        sceneTransitionFactory,
                        titleViewFactory,
                        playerViewFactory
                    ) 
                },
                { 
                    GameState.TitleShowing, new StateTransition(
                        CancellationToken.None, 
                        TransitionCondition.WaitClick, 
                        TransitionFunction.LoadMainScene, 
                        sceneTransitionFactory,
                        titleViewFactory,
                        playerViewFactory
                    )
                },
                { 
                    GameState.MainLoading, new StateTransition(
                        CancellationToken.None, 
                        TransitionCondition.WaitClick, 
                        TransitionFunction.StartMainGame, 
                        sceneTransitionFactory,
                        titleViewFactory,
                        playerViewFactory
                    )
                },
                { 
                    GameState.MainPlaying, new StateTransition(
                        CancellationToken.None, 
                        TransitionCondition.WaitClick, 
                        TransitionFunction.None, 
                        sceneTransitionFactory,
                        titleViewFactory,
                        playerViewFactory
                    ) 
                },
                { 
                    GameState.ResultShowing, new StateTransition(
                        CancellationToken.None, 
                        TransitionCondition.WaitClick, 
                        TransitionFunction.BackTitleScene, 
                        sceneTransitionFactory,
                        titleViewFactory,
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