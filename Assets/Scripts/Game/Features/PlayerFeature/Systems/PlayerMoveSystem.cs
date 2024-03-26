using System;
using Scellecs.Morpeh;
using ShipsWar.Game.Features.InputFeature.Components;
using ShipsWar.Game.Features.PlayerFeature.Components;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game.Features.PlayerFeature.Systems
{
    public class PlayerMoveSystem : IStartable, ITickable, IDisposable
    {
        [Inject] private World _world;
        [Inject] private Config _config;
        
        private Stash<InputMoveDirection> _inputStash;
        private Stash<PlayerPosition> _playerPosition;
        
        private Filter _inputFilter;
        private Filter _playerFilter;
        
        public void Start()
        {
            _inputFilter = _world.Filter.With<InputMoveDirection>().Build();
            _playerFilter = _world.Filter.With<Player>().With<PlayerPosition>().Build();
            
            _inputStash = _world.GetStash<InputMoveDirection>();
            _playerPosition = _world.GetStash<PlayerPosition>();
        }

        public void Tick()
        {
            foreach (var inputEntity in _inputFilter)
            {
                foreach (var playerEntity in _playerFilter)
                {
                    var deltaTime = Time.deltaTime;
                    ref var input = ref _inputStash.Get(inputEntity);
                    ref var player = ref _playerPosition.Get(playerEntity);
                    player.Position += input.direction * deltaTime * _config.SideMoveSpeed;
                }
            }
        }

        public void Dispose()
        {
            _world.Dispose();
            _inputStash.Dispose();
            _playerPosition.Dispose();
        }
    }
}