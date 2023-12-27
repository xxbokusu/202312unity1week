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
            
        }

        public void Initialize()
        {
            _factory.WatchStateTransition(_factory.Create());
        }
    }
}