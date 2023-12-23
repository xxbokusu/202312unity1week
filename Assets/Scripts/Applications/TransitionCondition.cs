using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;

namespace unity1week202312.State {
    public class TransitionCondition : IDisposable {
        CancellationToken _token;
        public TransitionCondition(CancellationToken token) {
            _token = token;
        }

        public async UniTaskVoid WaitClick() {
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}