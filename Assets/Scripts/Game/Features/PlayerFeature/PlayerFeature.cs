using System.Threading;
using Cysharp.Threading.Tasks;
using ShipsWar.Game.Features.PlayerFeature.Systems;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game.Features.PlayerFeature
{
    public class PlayerFeature : IAsyncStartable, ITickable
    {
        [Inject] private IObjectResolver _objectResolver;
        
        private readonly PlayerSpawnSystem _playerSpawnSystem = new PlayerSpawnSystem();
        private readonly PlayerMoveSystem _playerMoveSystem = new PlayerMoveSystem();
        private readonly PlayerShootingSystem _playerShootingSystem = new PlayerShootingSystem();
        private readonly PlayerApplyPositionSystem _playerApplyPositionSystem = new PlayerApplyPositionSystem();
        private readonly MoveBulletsSystem _moveBulletsSystem = new MoveBulletsSystem();
        
        public void Inject(IObjectResolver objectResolver)
        { 
            objectResolver.Inject(_playerSpawnSystem);
            objectResolver.Inject(_playerMoveSystem);
            objectResolver.Inject(_playerShootingSystem);
            objectResolver.Inject(_playerApplyPositionSystem);
            objectResolver.Inject(_moveBulletsSystem);
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await _playerSpawnSystem.StartAsync(cancellation);
            _playerMoveSystem.Start();
            await _playerShootingSystem.StartAsync(cancellation);
            _playerApplyPositionSystem.Start();
            _moveBulletsSystem.Start();
        }

        public void Tick()
        {
            _playerMoveSystem.Tick();
            _playerShootingSystem.Tick();
            _playerApplyPositionSystem.Tick();
            _moveBulletsSystem.Tick();
        }
    }
}