using System.Threading;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using ShipsWar.Game.Features.InputFeature.Components;
using ShipsWar.Game.Features.PlayerFeature.Components;
using UnityEngine;
using VContainer;

namespace ShipsWar.Game.Features.PlayerFeature.Systems
{
    public partial class PlayerMoveSystem : IUpdateSystem
    {
        [Inject] private World _world;
        [Inject] private Config _config;
        
        private Stash<InputMoveDirection> _inputStash;
        private Stash<PlayerPosition> _playerPosition;
        
        [With(typeof(InputMoveDirection))] private Filter _inputFilter;
        [With(typeof(Player), typeof(PlayerPosition))] private Filter _playerFilter;
        
        public async UniTask StartAsync(CancellationToken cancellation) { }

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
    }
}