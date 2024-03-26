using System.Threading;
using Core;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using ShipsWar.Game.Features.BulletsFeature.Components;
using ShipsWar.Game.Features.InputFeature.Components;
using ShipsWar.Game.Features.PlayerFeature.Components;
using ShipsWar.Game.Features.TransformFeature.Components;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game.Features.BulletsFeature.Systems
{
    public class BulletSpawnSystem : IAsyncStartable, ITickable
    {
        [Inject] private World _world;
        [Inject] private Config _config;
        [Inject] private Assets _assets;
        [Inject] private AssetProvider _assetProvider;
        [Inject] private IObjectResolver _objectResolver;
        
        private Stash<Bullet> _bulletStash;
        private Stash<BulletCreate> _bulletCreate;
        private Stash<GameObjectRef> _gameObjectRef;
        private Stash<BulletSpeed> _bulletSpeed;
        
        private Filter _bulletCreateFilter;

        private GameObject _bulletPrefab;
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _bulletStash = _world.GetStash<Bullet>();
            _bulletCreate = _world.GetStash<BulletCreate>();
            _gameObjectRef = _world.GetStash<GameObjectRef>();
            _bulletSpeed = _world.GetStash<BulletSpeed>();

            _bulletCreateFilter = _world.Filter.With<BulletCreate>().Build();
            
            _bulletPrefab = await _assetProvider.LoadAssetAsync<GameObject>(_assets.Bullet);
        }
        public void Tick()
        {
            foreach (var entity in _bulletCreateFilter)
            {
                ref var bulletCreate = ref _bulletCreate.Get(entity);
                
                var bulletInstance = _objectResolver.Instantiate(
                    _bulletPrefab, bulletCreate.SpawnPosition, Quaternion.identity);

                _bulletStash.Add(entity);
                _gameObjectRef.Set(entity, new GameObjectRef { GameObject = bulletInstance });
                _bulletSpeed.Set(entity, new BulletSpeed { Speed = _config.BulletSpeed });
                
                _bulletCreate.Remove(entity);
            }
        }
    }
}