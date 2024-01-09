using System;
using unity1week202312.Common;
using unity1week202312.MainGame;
using VContainer.Unity;

namespace unity1week202312.State {
    public class GameInitializer : IInitializable
    {
        StateTransitionFactory _stateTransitionFactory;

        GameState _currentGameState;

        public GameInitializer(
            StateTransitionFactory stateTransitionFactory
        ) {
            _stateTransitionFactory = stateTransitionFactory;            
        }

        public void Initialize()
        {
            _stateTransitionFactory.WatchStateTransition(_stateTransitionFactory.Create());
        }
    }
}