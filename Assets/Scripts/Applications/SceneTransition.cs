using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

namespace unity1week202312.Common {
    public class SceneTransition : IDisposable {
        SceneName _currentScene;
        /**
         * 現在シーンからの遷移をタスク登録する
         */
        public SceneTransition(SceneName currentScene, CancellationToken token) {
            // シーン遷移構造を登録する
            _currentScene = currentScene;
            RegiseterTransitions(currentScene, token);
        }

        private void RegiseterTransitions(SceneName currentScene, CancellationToken token) {
            ScenePath[] sceneTransitions = GetSceneTransitions(currentScene);
            foreach (ScenePath sceneTransition in sceneTransitions) {
                LoadSceneAsync(sceneTransition.To, token).Forget();
            }
        }

        /**
         * シーン遷移構造一覧
         * Title > Main : 遷移ボタンのクリック
         * Main > Title : Result表示完了後にクリック
         */
        private struct ScenePath {
            public SceneName From { get; }
            public SceneName To { get; }
            public ScenePath(SceneName from, SceneName to) {
                From = from;
                To = to;
            }
        }

        /**
        * シーン遷移パターン一覧
        */
        private readonly ScenePath[] _sceneTransitions = new ScenePath[] {
            new ScenePath(SceneName.Initialize, SceneName.Title),
            new ScenePath(SceneName.Title, SceneName.Main),
            new ScenePath(SceneName.Main, SceneName.Title),
        };

        /**
         * 現在のScenesに対応するシーン遷移構造群を取得する
         */
        ScenePath[] GetSceneTransitions(SceneName targetScene) {
            ScenePath[] result = _sceneTransitions.Where(x => x.From == targetScene).ToArray();
            if (result.Length > 0) {
                return result;
            }
            throw new System.Exception($"シーン遷移構造が見つかりませんでした。scenes={targetScene}");
        }

        //非同期的に次のシーンを読み込んで遷移する. 読み込み開始でフェードアウト, 読み込み完了でフェードイン
        private async UniTaskVoid LoadSceneAsync(SceneName targetScene, CancellationToken token) {
            // クリック待ち
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: token);
            var progress = SceneManager.LoadSceneAsync(targetScene.ToString(), LoadSceneMode.Additive);

            await UniTask.WaitUntil(() => progress.isDone, cancellationToken: token);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(targetScene.ToString()));
            await SceneManager.UnloadSceneAsync(_currentScene.ToString());

            _currentScene = targetScene;
            RegiseterTransitions(targetScene, token);
        }

        public void Dispose() {
            // fadeCanvas.Dispose();
        }
    }
}