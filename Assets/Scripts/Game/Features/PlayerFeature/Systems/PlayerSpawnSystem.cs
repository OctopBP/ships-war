using System.Threading;
using Core;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using ShipsWar.Game.Features.InputFeature.Components;
using ShipsWar.Game.Features.PlayerFeature.Components;
using ShipsWar.Game.Features.TransformFeature.Components;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game.Features.PlayerFeature.Systems
{
    public class PlayerSpawnSystem : IAsyncStartable
    {
        [Inject] private AssetProvider _assetProvider;
        [Inject] private Config _config;
        [Inject] private Assets _assets;
        [Inject] private IObjectResolver _container;
        
        [Inject] private World _world;
        
        private Stash<GameObjectRef> _gameObjectRefStash;
        private Stash<Player> _playerStash;
        private Stash<PlayerPosition> _playerPositionStash;
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            var playerPrefab = await _assetProvider.LoadAssetAsync<GameObject>(_assets.Player);
            var instance = _container.Instantiate(playerPrefab);
            
            _playerStash = _world.GetStash<Player>();
            _playerPositionStash = _world.GetStash<PlayerPosition>();
            _gameObjectRefStash = _world.GetStash<GameObjectRef>();
            
            var playerEntity = _world.CreateEntity();
            _playerStash.Add(playerEntity);
            _playerPositionStash.Set(playerEntity, new PlayerPosition { Position = 0 });
            _gameObjectRefStash.Set(playerEntity, new GameObjectRef { GameObject = instance });
        }
    }
}