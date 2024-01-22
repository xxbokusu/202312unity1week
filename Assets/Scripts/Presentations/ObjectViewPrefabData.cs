using unity1week202312.MainGame;
using unity1week202312.Title;
using UnityEngine;

namespace unity1week202312.Common
{
    [CreateAssetMenu(fileName = "ObjectViewPrefabData", menuName = "ScriptableObjects/ObjectViewPrefabData")]
    public class ObjectViewPrefabData : ScriptableObject
    {
        [SerializeField] private PlayerView _playerPrefab = default;
        [SerializeField] private TitleCharacterView _titleCharacterPrefab = default;
        public PlayerView PlayerPrefab => _playerPrefab;
        public TitleCharacterView TitleCharacterPrefab => _titleCharacterPrefab;
    }
}