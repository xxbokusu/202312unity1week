using UnityEngine;
using unity1week202312.Common;

namespace unity1week202312.Title {
    public sealed class TitleCharacterView : MonoBehaviour, IObjectView {
        public void Dispose() {
            if (gameObject == null) return;

            Destroy(gameObject);
        }

        public void Initialize() {
            transform.position = new Vector3(0, 0, 0);
        }

        public void SetPosition(float x, float y) {
            transform.position = new Vector3(x, y, 0);
        }

        public void SetRotation(float angle) {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        void Update() {
            // 矢印キー/wasdキーで移動
            var dx = Input.GetAxisRaw("Horizontal");
            var dy = Input.GetAxisRaw("Vertical");
            // 移動方向に応じて向きを変える
            if (dx != 0 || dy != 0) {
                var rad = Mathf.Atan2(dy, dx);
                var degree = rad * Mathf.Rad2Deg;
                SetRotation(degree);
            }
            transform.position += new Vector3(dx, dy, 0) * 0.1f;
        }
    }
}