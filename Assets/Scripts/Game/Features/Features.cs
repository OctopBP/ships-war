using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game.Features
{
    public class Features : IAsyncStartable, ITickable
    {
        private readonly InputFeature.InputFeature _inputFeature = new InputFeature.InputFeature();
        private readonly PlayerFeature.PlayerFeature _playerFeature = new PlayerFeature.PlayerFeature();
        private readonly EnemiesFeature.EnemiesFeature _enemiesFeature = new EnemiesFeature.EnemiesFeature();
        private readonly BulletsFeature.BulletsFeature _bulletsFeature = new BulletsFeature.BulletsFeature();
        
        public void Inject(IObjectResolver objectResolver)
        {
            _inputFeature.Inject(objectResolver);
            _playerFeature.Inject(objectResolver);
            _enemiesFeature.Inject(objectResolver);
            _bulletsFeature.Inject(objectResolver);
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _inputFeature.Start();
            await _playerFeature.StartAsync(cancellation);
            await _enemiesFeature.StartAsync(cancellation);
            await _bulletsFeature.StartAsync(cancellation);
        }

        public void Tick()
        {
            _inputFeature.Tick();
            _playerFeature.Tick();
            _enemiesFeature.Tick();
            _bulletsFeature.Tick();
        }
    }
}