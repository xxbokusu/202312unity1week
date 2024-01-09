using unity1week202312.MainGame;
using UnityEngine;

namespace unity1week202312.Common
{
    [CreateAssetMenu(fileName = "ObjectViewPrefabData", menuName = "ScriptableObjects/ObjectViewPrefabData")]
    public class ObjectViewPrefabData : ScriptableObject
    {
        [SerializeField] private PlayerView _playerPrefab = default;
        public PlayerView PlayerPrefab => _playerPrefab;
    }
}