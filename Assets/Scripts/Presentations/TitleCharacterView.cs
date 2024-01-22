using UnityEngine;
using unity1week202312.Common;

namespace unity1week202312.Title {
    public sealed class TitleCharacterView : MonoBehaviour, IObjectView {
        public void Dispose() {
            if (gameObject == null) return;

            Destroy(gameObject);
        }

        public void Initialize() {
        }

        public void SetPosition(float x, float y) {
            transform.position = new UnityEngine.Vector3(x, y, 0);
        }

        public void SetRotation(float angle) {
            transform.rotation = UnityEngine.Quaternion.Euler(0, 0, angle);
        }
    }
}