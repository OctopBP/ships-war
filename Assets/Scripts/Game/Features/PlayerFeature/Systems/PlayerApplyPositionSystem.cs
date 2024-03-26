using Scellecs.Morpeh;
using ShipsWar.Game.Features.InputFeature.Components;
using ShipsWar.Game.Features.PlayerFeature.Components;
using ShipsWar.Game.Features.TransformFeature.Components;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game.Features.PlayerFeature.Systems
{
    public class PlayerApplyPositionSystem : IStartable, ITickable
    {
        [Inject] private World _world;
        
        private Stash<GameObjectRef> _gameObjectRefStash;
        private Stash<PlayerPosition> _playerPosition;
        
        private Filter _playerFilter;
        
        public void Start()
        {
            _playerFilter = _world.Filter.With<PlayerPosition>().With<GameObjectRef>().Build();
            
            _playerPosition = _world.GetStash<PlayerPosition>();
            _gameObjectRefStash = _world.GetStash<GameObjectRef>();
        }

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