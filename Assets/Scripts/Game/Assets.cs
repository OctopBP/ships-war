using UnityAttributes;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ShipsWar.Game
{
    [CreateAssetMenu(fileName = "Assets", menuName = "Game/Assets", order = 1)]
    public partial class Assets : ScriptableObject
    {
        [SerializeField, PublicAccessor] private AssetReference _player;
        [SerializeField, PublicAccessor] private AssetReference _bullet;
    }
}