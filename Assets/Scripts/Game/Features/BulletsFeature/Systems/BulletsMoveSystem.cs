using System.Threading;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using ShipsWar.Game.Features.BulletsFeature.Components;
using ShipsWar.Game.Features.TransformFeature.Components;
using UnityEngine;
using VContainer;

namespace ShipsWar.Game.Features.BulletsFeature.Systems
{
    public partial class BulletsMoveSystem : IUpdateSystem
    {
        [Inject] private World _world;
        
        private Stash<GameObjectRef> _gameObjectRefStash;
        private Stash<BulletSpeed> _bulletSpeed;
        
        [With(typeof(Bullet), typeof(GameObjectRef), typeof(BulletSpeed))]
        private Filter _bulletFilter;
        
        public async UniTask StartAsync(CancellationToken cancellation) { }

        public void Tick()
        {
            foreach (var entity in _bulletFilter)
            {
                ref var go = ref _gameObjectRefStash.Get(entity);
                ref var speed = ref _bulletSpeed.Get(entity);
                
                go.GameObject.transform.position += speed.Speed * Time.deltaTime * Vector3.forward;
            }
        }
    }
}