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

namespace ShipsWar.Game.Features.PlayerFeature.Systems
{
    public partial class PlayerShootingSystem : IUpdateSystem
    {
        [Inject] private World _world;
        [Inject] private Config _config;
        [Inject] private Assets _assets;
        [Inject] private AssetProvider _assetProvider;
        [Inject] private IObjectResolver _objectResolver;
        
        private Stash<InputShooting> _inputStash;
        private Stash<BulletCreate> _bulletCreateStash;
        private Stash<GameObjectRef> _gameObjectRef;
        private Stash<BulletSpeed> _bulletSpeed;
        
        [With(typeof(InputShooting))] private Filter _inputFilter;
        [With(typeof(Player), typeof(GameObjectRef))] private Filter _playerFilter;

        private GameObject _bulletPrefab;
        private float _timer;
        
        public async UniTask StartAsync(CancellationToken cancellation) { }
        
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
                        
                    var bulletEntity = _world.CreateEntity();
                    _bulletCreateStash.Set(bulletEntity, new BulletCreate { SpawnPosition = playerPosition });
                    _bulletSpeed.Set(bulletEntity, new BulletSpeed { Speed = _config.BulletSpeed });

                    _timer = _config.ReloadTime;
                }
            }
        }
    }
}