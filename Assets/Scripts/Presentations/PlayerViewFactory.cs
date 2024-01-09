using unity1week202312.Common;
using UnityEngine;

namespace unity1week202312.MainGame
{
    public class PlayerViewFactory : IPlayerViewFactory
    {
        private readonly ObjectViewPrefabData _objectViewPrefabData;
        public PlayerViewFactory(ObjectViewPrefabData objectViewPrefabData)
        {
            _objectViewPrefabData = objectViewPrefabData;
        }

        public IObjectView Create()
        {
            var objectView = Object.Instantiate(_objectViewPrefabData.PlayerPrefab);
            objectView.Initialize();
            return objectView;
        }
    }
}