using System.Threading;
using Core;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using ShipsWar.Game.Features.InputFeature.Components;
using ShipsWar.Game.Features.PlayerFeature.Components;
using ShipsWar.Game.Features.TransformFeature.Components;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game.Features.PlayerFeature.Systems
{
    public class PlayerShootingSystem : IAsyncStartable, ITickable
    {
        [Inject] private World _world;
        [Inject] private Config _config;
        [Inject] private Assets _assets;
        [Inject] private AssetProvider _assetProvider;
        [Inject] private IObjectResolver _objectResolver;
        
        private Stash<InputShooting> _inputStash;
        private Stash<Bullet> _bulletStash;
        private Stash<GameObjectRef> _gameObjectRef;
        private Stash<BulletSpeed> _bulletSpeed;
        
        private Filter _inputFilter;
        private Filter _playerFilter;

        private GameObject _bulletPrefab;
        private float _timer;
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _inputStash = _world.GetStash<InputShooting>();
            _bulletStash = _world.GetStash<Bullet>();
            _gameObjectRef = _world.GetStash<GameObjectRef>();
            _bulletSpeed = _world.GetStash<BulletSpeed>();

            _inputFilter = _world.Filter.With<InputShooting>().Build();
            _playerFilter = _world.Filter.With<Player>().With<GameObjectRef>().Build();
            
            _bulletPrefab = await _assetProvider.LoadAssetAsync<GameObject>(_assets.Bullet);
        }
        
        public void Tick()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                return;
            }
            
            foreach (var inputEntity in _inputFilter)
            {
                foreach (var playerEntity in _playerFilter)
                {
                    ref var shooting = ref _inputStash.Get(inputEntity);
                    if (!shooting.Active)
                    {
                        continue;
                    }
                    
                    var playerPosition = _gameObjectRef.Get(playerEntity).GameObject.transform.position;
                    var bulletInstance = _objectResolver.Instantiate(_bulletPrefab, playerPosition, Quaternion.identity);
                        
                    var bulletEntity = _world.CreateEntity();
                    _bulletStash.Add(bulletEntity);
                    _gameObjectRef.Set(bulletEntity, new GameObjectRef { GameObject = bulletInstance });
                    _bulletSpeed.Set(bulletEntity, new BulletSpeed { Speed = _config.BulletSpeed });

                    _timer = _config.ReloadTime;
                }
            }
        }
    }
}