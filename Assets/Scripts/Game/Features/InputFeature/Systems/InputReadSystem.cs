using System.Threading;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using ShipsWar.Game.Features.InputFeature.Components;
using UnityEngine;
using VContainer;

namespace ShipsWar.Game.Features.InputFeature.Systems
{
    public partial class InputReadSystem : IUpdateSystem
    {
        [Inject] private World _world;
        
        private Stash<InputMoveDirection> _inputStash;
        private Stash<InputShooting> _inputShooting;
        
        [With(typeof(InputMoveDirection), typeof(InputShooting))]
        private Filter _filter;
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            var inputEntity = _world.CreateEntity();
            _inputStash.Add(inputEntity);
            _inputShooting.Add(inputEntity);
        }
        
        public void Tick()
        {
            foreach (var entity in _filter)
            {
                ref var moveInput = ref _inputStash.Get(entity);
                var rightPressed = Input.GetKey(KeyCode.D);
                var leftPressed = Input.GetKey(KeyCode.A);
                var moveDirection = (rightPressed ? 1 : 0) + (leftPressed ? -1 : 0);
                moveInput.direction = moveDirection;
                
                ref var inputShooting = ref _inputShooting.Get(entity);
                inputShooting.Active = Input.GetKey(KeyCode.Space);
            }
        }
    }
}