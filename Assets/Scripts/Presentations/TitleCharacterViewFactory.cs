using unity1week202312.Common;
using UnityEngine;

namespace unity1week202312.Title
{
    public class TitleCharacterViewFactory: IObjectViewFactory
    {
        private readonly ObjectViewPrefabData _objectViewPrefabData;
        public TitleCharacterViewFactory(ObjectViewPrefabData objectViewPrefabData)
        {
            _objectViewPrefabData = objectViewPrefabData;
        }
        public IObjectView Create() { 
            var objectView = Object.Instantiate(_objectViewPrefabData.TitleCharacterPrefab);
            objectView.Initialize();
            return objectView;
        }
    }
}