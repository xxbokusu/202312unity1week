using System;
using System.Threading;

namespace unity1week202312.Common {
    public class SceneTransitionFactory {
        CancellationToken _token;
        public SceneTransitionFactory(CancellationToken token) {
            _token = token;
        }

        public SceneTransition Create(SceneName currentScene) {
            return new SceneTransition(currentScene, _token);
        }
    }
}
