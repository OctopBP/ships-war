using UnityAttributes;
using UnityEngine;

namespace ShipsWar.Game
{
    [CreateAssetMenu(fileName = "Config", menuName = "Game/Config", order = 1)]
    public partial class Config : ScriptableObject
    {
        [SerializeField, PublicAccessor] private float _spawnRate;
        [SerializeField, PublicAccessor] private float _sideMoveSpeed;
        [SerializeField, PublicAccessor] private float _reloadTime;
        [SerializeField, PublicAccessor] private float _bulletSpeed;
    }
}