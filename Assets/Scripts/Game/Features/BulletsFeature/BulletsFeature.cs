using System.Threading;
using Cysharp.Threading.Tasks;
using ShipsWar.Game.Features.BulletsFeature.Systems;
using ShipsWar.Game.Features.PlayerFeature.Systems;
using VContainer;

namespace ShipsWar.Game.Features.BulletsFeature
{
    public class BulletsFeature
    {
        [Inject] private IObjectResolver _objectResolver;
        
        private readonly BulletSpawnSystem _bulletSpawnSystem = new BulletSpawnSystem();
        private readonly BulletsMoveSystem _bulletsMoveSystem = new BulletsMoveSystem();
        
        public void Inject(IObjectResolver objectResolver)
        { 
            objectResolver.Inject(_bulletSpawnSystem);
            objectResolver.Inject(_bulletsMoveSystem);
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await _bulletSpawnSystem.StartAsync(cancellation);
            _bulletsMoveSystem.Start();
        }

        public void Tick()
        {
            _bulletSpawnSystem.Tick();
            _bulletsMoveSystem.Tick();
        }
    }
}