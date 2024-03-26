using Scellecs.Morpeh;
using ShipsWar.Game.Features.PlayerFeature.Components;
using ShipsWar.Game.Features.TransformFeature.Components;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game.Features.PlayerFeature.Systems
{
    public class MoveBulletsSystem : IStartable, ITickable
    {
        [Inject] private World _world;
        
        private Stash<GameObjectRef> _gameObjectRefStash;
        private Stash<BulletSpeed> _bulletSpeed;
        
        private Filter _bulletFilter;
        
        public void Start()
        {
            _bulletFilter = _world.Filter.With<Bullet>().With<GameObjectRef>().With<BulletSpeed>().Build();
            
            _gameObjectRefStash = _world.GetStash<GameObjectRef>();
            _bulletSpeed = _world.GetStash<BulletSpeed>();
        }
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