using System;
using UnityEngine;
using unity1week202312.Common;
using Cysharp.Threading.Tasks;

namespace unity1week202312.State
{
    public class StateView : MonoBehaviour, IStateView
    {
        public static StateView Instance { get; private set; }
        ScenePresenter _scenePresenter;
        TransitionConditionFactory _transitionConditionFactory;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            _transitionConditionFactory = new TransitionConditionFactory(this.GetCancellationTokenOnDestroy());
            _scenePresenter = new ScenePresenter(this.GetCancellationTokenOnDestroy());
            _scenePresenter.SetSceneTransition(SceneName.Initialize);
        }

        public void SetTransitionCondition(SceneName currentScene)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}