using System.Threading;
using Core;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using ShipsWar.Game.Features.BulletsFeature.Components;
using ShipsWar.Game.Features.TransformFeature.Components;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game.Features.BulletsFeature.Systems
{
    public partial class BulletSpawnSystem : IUpdateSystem
    {
        [Inject] private World _world;
        [Inject] private Config _config;
        [Inject] private Assets _assets;
        [Inject] private AssetProvider _assetProvider;
        [Inject] private IObjectResolver _objectResolver;
        
        private Stash<Bullet> _bulletStash;
        private Stash<BulletCreate> _bulletCreate;
        private Stash<GameObjectRef> _gameObjectRef;
        
        [With(typeof(BulletCreate))]
        private Filter _bulletCreateFilter;

        private GameObject _bulletPrefab;
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
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
                
                _bulletCreate.Remove(entity);
            }
        }
    }
}