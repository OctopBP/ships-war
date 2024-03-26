using UnityAttributes;
using UnityEngine;

namespace ShipsWar.Game
{
    [CreateAssetMenu(fileName = "Config", menuName = "Game/Config", order = 1)]
    public partial class Config : ScriptableObject
    {
        [SerializeField, PublicAccessor] private float _sideMoveSpeed;
        [SerializeField, PublicAccessor] private float _reloadTime;
        [SerializeField, PublicAccessor] private float _bulletSpeed;
        
        [Header("Enemies")]
        [SerializeField, PublicAccessor] private float _spawnRate;
        [SerializeField, PublicAccessor] private float _enemiesSideMove;
        [SerializeField, PublicAccessor] private float _enemiesSpeed;
        [SerializeField, PublicAccessor] private float _enemiesReloadTime;
        [SerializeField, PublicAccessor] private float _enemiesStartHealth;
        [SerializeField, PublicAccessor] private float _enemiesBulletSpeed;
    }
}