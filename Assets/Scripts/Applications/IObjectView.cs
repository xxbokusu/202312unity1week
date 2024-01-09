using System;

namespace unity1week202312.MainGame {
    public interface IObjectView: IDisposable {
        void SetPosition(float x, float y);
        void SetRotation(float angle);
    }
}