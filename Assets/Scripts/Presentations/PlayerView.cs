using UnityEngine;
using unity1week202312.Common;

namespace unity1week202312.MainGame
{
    public sealed class PlayerView : MonoBehaviour, IObjectView
    {
        public enum Direction
        {
            Right,
            Left,
        }

        public void SetPosition(float x, float y)
        {
            transform.position = new Vector3(x, y, 0);
        }


        private Direction _direction = Direction.Left;

        /**
         * <summary>
         * 右左の指定だけを受けて、徐々に回転する
         * </summary>
         */
        public void SetRotation(Direction direction)
        {
            if (_direction == direction) return;

            _direction = direction;
        }
        public void SetRotation(float angle)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        void Update()
        {
            if (_direction == Direction.Right)
            {
                // 既に右を向いているなら何もしない
                if (transform.rotation.z >= 180) return;
                SetRotation(transform.rotation.z + 1);
            }
            else
            {
                // 既に左を向いているなら何もしない
                if (transform.rotation.z <= 0) return;
                SetRotation(transform.rotation.z - 1);
            }
        }

        public void Dispose()
        {
            if (gameObject == null) return;

            Destroy(gameObject);
        }

        public void Initialize()
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}