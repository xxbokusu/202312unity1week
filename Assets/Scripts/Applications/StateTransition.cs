using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace unity1week202312.State {
    public enum TransitionCondition {
        WaitClick,
        WaitTime
    }
    public class StateTransition : IDisposable {
        CancellationToken _token;

        private Dictionary<TransitionCondition, Func<UniTaskVoid>> _conditionDic;

        /**
        * SceneTransitionFactoryから生成. 指定された条件を満たすと指定された処理を実行する
        */
        public StateTransition(CancellationToken token, TransitionCondition key) {
            _token = token;
            _conditionDic = new() {
                { TransitionCondition.WaitClick, () => WaitClick() },
                { TransitionCondition.WaitTime, () => WaitTime(3) }
            };
        }

        private async UniTaskVoid WaitClick() {
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);
        }

        private async UniTaskVoid WaitTime(float time) {
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: _token);
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}