using System;
using Scellecs.Morpeh;
using ShipsWar.Game.Features.InputFeature.Components;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game.Features.InputFeature.Systems
{
    public class InputReadSystem : IStartable, ITickable, IDisposable
    {
        [Inject] private World _world;
        
        private Stash<InputMoveDirection> _inputStash;
        private Stash<InputShooting> _inputShooting;
        private Filter _filter;
        
        public void Start()
        {
            _inputStash = _world.GetStash<InputMoveDirection>();
            _inputShooting = _world.GetStash<InputShooting>();
            
            var inputEntity = _world.CreateEntity();
            _inputStash.Add(inputEntity);
            _inputShooting.Add(inputEntity);
            
            _filter = _world.Filter.With<InputMoveDirection>().With<InputShooting>().Build();
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

        public void Dispose()
        {
            _world.Dispose();
            _inputStash.Dispose();
        }
    }
}