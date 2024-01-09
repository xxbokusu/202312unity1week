using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using unity1week202312.Common;
using unity1week202312.MainGame;

namespace unity1week202312.State {
    public enum TransitionCondition {
        WaitClick,
        WaitTime
    }

    public enum TransitionFunction {
        LoadTitleScene,
        LoadMainScene,
        BackTitleScene,
        StartMainGame,
        None
    }
    public class StateTransition : IDisposable {
        private CancellationToken _token;
        private TransitionCondition _conditionKey;
        private TransitionFunction _functionKey;

        private Dictionary<TransitionCondition, Func<UniTask>> _conditionDic;
        private Dictionary<TransitionFunction, Func<UniTask>> _functionDic;
        private SceneTransitionFactory _sceneTransitionFactory;
        private PlayerViewFactory _playerViewFactory;

        /**
        * SceneTransitionFactoryから生成. 指定された条件を満たすと指定された処理を実行する
        */
        public StateTransition(
            CancellationToken token,
            TransitionCondition conditionKey,
            TransitionFunction funcionKey,
            SceneTransitionFactory sceneTransitionFactory,
            PlayerViewFactory playerViewFactory
        ) {
            _token = token;
            _conditionDic = new() {
                { TransitionCondition.WaitClick, () => WaitClick() },
                { TransitionCondition.WaitTime, () => WaitTime(3) }
            };
            _functionDic = new() {
                { TransitionFunction.LoadTitleScene, () => LoadSceneFrom(SceneName.Initialize) },
                { TransitionFunction.LoadMainScene, () => LoadSceneFrom(SceneName.Title) },
                { TransitionFunction.BackTitleScene, () => LoadSceneFrom(SceneName.Main)},
                { TransitionFunction.StartMainGame, () => InitializeMainGame() },
                { TransitionFunction.None, () => UniTask.CompletedTask }
            };
            _conditionKey = conditionKey;
            _functionKey = funcionKey;
            _sceneTransitionFactory = sceneTransitionFactory;
            _playerViewFactory = playerViewFactory;
        }

        public async UniTask Execute() {
            await _conditionDic[_conditionKey]();
            await _functionDic[_functionKey]();
        }

        private async UniTask WaitClick() {
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);
        }

        private async UniTask WaitTime(float time) {
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: _token);
        }

        private UniTask LoadSceneFrom(SceneName fromScene)
        {
            SceneTransition transition = _sceneTransitionFactory.Create(fromScene);
            transition.RegiseterTransitions();
            return UniTask.CompletedTask;
        }

        private UniTask InitializeMainGame() {
            _playerViewFactory.Create();
            return UniTask.CompletedTask;
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}