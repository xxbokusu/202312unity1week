using System;
using System.Threading;

namespace unity1week202312.Common {
    public class ScenePresenter: IDisposable {
        private SceneName _currentScene;
        public SceneName CurrentScene => _currentScene;

        CancellationToken _token;
        SceneTransition _sceneTransition;
        public ScenePresenter(CancellationToken token) {
            _token = token;
        }
        public void SetSceneTransition(SceneName currentScene) {
            _sceneTransition = new SceneTransition(currentScene, _token);
            _currentScene = currentScene;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
