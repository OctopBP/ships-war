using System.Threading;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using ShipsWar.Game.Features.BulletsFeature.Components;
using ShipsWar.Game.Features.EnemiesFeature.Components;
using ShipsWar.Game.Features.TransformFeature.Components;
using UnityEngine;
using VContainer;

namespace ShipsWar.Game.Features.EnemiesFeature.Systems
{
    public partial class EnemiesShootSystem : IUpdateSystem
    {
        [Inject] private Config _config;
        [Inject] private World _world;

        private Stash<Cooldown> _cooldown;
        private Stash<GameObjectRef> _gameObjectRef;
        private Stash<BulletCreate> _bulletCreateStash;
        private Stash<BulletSpeed> _bulletSpeed;
        
        [With(typeof(Enemy), typeof(GameObjectRef), typeof(Cooldown))]
        private Filter _filter;
        
        public async UniTask StartAsync(CancellationToken cancellation) { }

        public void Tick()
        {
            foreach (var entity in _filter)
            {
                ref var cooldown = ref _cooldown.Get(entity);

                if (cooldown.Time > 0)
                {
                    cooldown.Time -= Time.deltaTime;
                    continue;
                }

                cooldown.Time = _config.EnemiesReloadTime;

                var enemyPosition = _gameObjectRef.Get(entity).GameObject.transform.position;

                for (var i = 0; i < 3; i++)
                {
                    var position = enemyPosition + (i - 1) * Vector3.right * 0.75f;
                    var bulletEntity = _world.CreateEntity();
                    _bulletCreateStash.Set(bulletEntity, new BulletCreate { SpawnPosition = position });
                    _bulletSpeed.Set(bulletEntity, new BulletSpeed { Speed = -_config.EnemiesBulletSpeed });
                }
            }
        }
    }
}