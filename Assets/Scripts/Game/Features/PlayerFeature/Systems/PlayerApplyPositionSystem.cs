using System.Threading;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using ShipsWar.Game.Features.PlayerFeature.Components;
using ShipsWar.Game.Features.TransformFeature.Components;
using UnityEngine;
using VContainer;

namespace ShipsWar.Game.Features.PlayerFeature.Systems
{
    public partial class PlayerApplyPositionSystem : IUpdateSystem
    {
        [Inject] private World _world;
        
        private Stash<GameObjectRef> _gameObjectRefStash;
        private Stash<PlayerPosition> _playerPosition;
        
        [With(typeof(PlayerPosition), typeof(GameObjectRef))]
        private Filter _playerFilter;
        
        public async UniTask StartAsync(CancellationToken cancellation) { }

        public void Tick()
        {
            foreach (var playerEntity in _playerFilter)
            {
                ref var gameObject = ref _gameObjectRefStash.Get(playerEntity);
                ref var playerPosition = ref _playerPosition.Get(playerEntity);
                gameObject.GameObject.transform.position = new Vector3(playerPosition.Position, 0, 0);
            }
        }
    }
}