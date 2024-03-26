using Scellecs.Morpeh;
using ShipsWar.Game.Features.EnemiesFeature.Components;
using ShipsWar.Game.Features.TransformFeature.Components;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game.Features.EnemiesFeature.Systems
{
    public class EnemiesMoveSystem : IStartable, ITickable
    {
        [Inject] private Config _config;
        [Inject] private World _world;
        
        const float CHANGE_DIRECTION_TIMER = 1f;

        private Stash<GameObjectRef> _gameObjectRef;
        private Stash<MoveDirection> _moveDirection;
        
        private Filter _filter;

        private float _timer;
        
        public void Start()
        {
            _gameObjectRef = _world.GetStash<GameObjectRef>();
            _moveDirection = _world.GetStash<MoveDirection>();
            
            _filter = _world.Filter.With<Enemy>().With<GameObjectRef>().Build();
            
            _timer = CHANGE_DIRECTION_TIMER * 0.5f;
        }

        public void Tick()
        {
            if (_timer <= 0)
            {
                foreach (var entity in _filter)
                {
                    ref var moveDirection = ref _moveDirection.Get(entity);
                    moveDirection.Right = !moveDirection.Right;
                }
                
                _timer = CHANGE_DIRECTION_TIMER;
            }
            
            _timer -= Time.deltaTime;
            
            foreach (var entity in _filter)
            {
                ref var moveDirection = ref _moveDirection.Get(entity);
                ref var go = ref _gameObjectRef.Get(entity);
                go.GameObject.transform.position +=
                    Time.deltaTime * _config.EnemiesSideMove * (moveDirection.Right ? Vector3.right : Vector3.left) 
                    + Time.deltaTime * _config.EnemiesSpeed * Vector3.back;
            }
        }
    }
}