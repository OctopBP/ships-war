using System.Threading;
using Core;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using ShipsWar.Game.Features.EnemiesFeature.Components;
using ShipsWar.Game.Features.TransformFeature.Components;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game.Features.EnemiesFeature.Systems
{
    public class EnemiesSpawnSystem : IAsyncStartable, ITickable
    {
        [Inject] private IObjectResolver _objectResolver;
        [Inject] private AssetProvider _assetProvider;
        [Inject] private Assets _assets;
        [Inject] private Config _config;
        [Inject] private World _world;

        private Stash<Enemy> _enemy;
        private Stash<GameObjectRef> _gameObjectRef;
        private Stash<Health> _health;
        private Stash<Cooldown> _cooldown;
        private Stash<MoveDirection> _moveDirection;
        
        private float _timer;
        
        private GameObject _prefab;
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _enemy = _world.GetStash<Enemy>();
            _gameObjectRef = _world.GetStash<GameObjectRef>();
            _health = _world.GetStash<Health>();
            _cooldown = _world.GetStash<Cooldown>();
            _moveDirection = _world.GetStash<MoveDirection>();
            _prefab = await _assetProvider.LoadAssetAsync<GameObject>(_assets.Enemy);
        }

        public void Tick()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                return;
            }

            var enemyEntity = _world.CreateEntity();
            
            var instance = _objectResolver.Instantiate(_prefab);
            instance.transform.position = new Vector3(0, 0, 15);
            
            _enemy.Add(enemyEntity);
            _gameObjectRef.Set(enemyEntity, new GameObjectRef { GameObject = instance });
            _health.Set(enemyEntity, new Health { Value = _config.EnemiesStartHealth });
            _cooldown.Set(enemyEntity, new Cooldown { Time = 0 });
            _moveDirection.Set(enemyEntity, new MoveDirection { Right = true });
            
            _timer = _config.SpawnRate;
        }
    }
}