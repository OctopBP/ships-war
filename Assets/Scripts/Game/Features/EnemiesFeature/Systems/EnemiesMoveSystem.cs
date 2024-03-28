using System.Threading;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using ShipsWar.Game.Features.EnemiesFeature.Components;
using ShipsWar.Game.Features.TransformFeature.Components;
using UnityEngine;
using VContainer;

namespace ShipsWar.Game.Features.EnemiesFeature.Systems
{
    public partial class EnemiesMoveSystem : IUpdateSystem
    {
        [Inject] private Config _config;
        [Inject] private World _world;

        private const float ChangeDirectionTimer = 1f;

        private Stash<GameObjectRef> _gameObjectRef;
        private Stash<MoveDirection> _moveDirection;
        
        [With(typeof(Enemy), typeof(GameObjectRef))]
        private Filter _filter;

        private float _timer;
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _timer = ChangeDirectionTimer * 0.5f;
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
                
                _timer = ChangeDirectionTimer;
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