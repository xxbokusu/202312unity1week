using System;
using unity1week202312.Common;
using VContainer.Unity;

namespace unity1week202312.State {
    public class GameInitializer : IInitializable
    {
        StateTransitionFactory _factory;

        GameState _currentGameState;

        public GameInitializer(
            StateTransitionFactory factory
        ) {
            _factory = factory;
            _currentGameState = GameState.Initializing;
        }

        public async void Initialize()
        {
            while (true)
            {
                StateTransition transition = _factory.Create(_currentGameState);
                await transition.Execute();
                Debug.Util.Log($"StateTransition: {_currentGameState} -> {_factory._statePathDic[_currentGameState]}");
                _currentGameState = _factory._statePathDic[_currentGameState];
            }
        }
    }
}