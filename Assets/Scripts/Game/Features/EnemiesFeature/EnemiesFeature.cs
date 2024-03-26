using System.Threading;
using Cysharp.Threading.Tasks;
using ShipsWar.Game.Features.EnemiesFeature.Systems;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game.Features.EnemiesFeature
{
    public class EnemiesFeature : IAsyncStartable, ITickable
    {
        [Inject] private IObjectResolver _objectResolver;
        
        private readonly EnemiesSpawnSystem _enemiesSpawnSystem = new EnemiesSpawnSystem();
        private readonly EnemiesMoveSystem _enemiesMoveSystem = new EnemiesMoveSystem();
        private readonly EnemiesShootSystem _enemiesShootSystem = new EnemiesShootSystem();
        
        public void Inject(IObjectResolver objectResolver)
        { 
            objectResolver.Inject(_enemiesSpawnSystem);
            objectResolver.Inject(_enemiesMoveSystem);
            objectResolver.Inject(_enemiesShootSystem);
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await _enemiesSpawnSystem.StartAsync(cancellation);
            _enemiesMoveSystem.Start();
            _enemiesShootSystem.Start();
        }

        public void Tick()
        {
            _enemiesSpawnSystem.Tick();
            _enemiesMoveSystem.Tick();
            _enemiesShootSystem.Tick();
        }
    }
}