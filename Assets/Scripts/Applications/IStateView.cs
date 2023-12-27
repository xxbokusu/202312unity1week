using System;
using unity1week202312.Common;

namespace unity1week202312.State {
    public interface IStateView: IDisposable {
        void SetTransitionCondition(SceneName currentScene);
    }
}