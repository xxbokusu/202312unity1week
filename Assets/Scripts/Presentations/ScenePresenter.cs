using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace unity1week202312.Common {
    public class ScenePresenter : MonoBehaviour {
        public static ScenePresenter Instance { get; private set; }

        SceneTransition _sceneTransition;
        void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }

            // シーン遷移構造を登録する
            _sceneTransition = new SceneTransition(SceneName.Initialize, this.GetCancellationTokenOnDestroy());
        }
    }
}
