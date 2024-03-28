using System.Threading;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game
{
    public class Bootstrap : IAsyncStartable, ITickable
    {
        [Inject] private IObjectResolver _container;
        [Inject] private LifetimeScope _currentScope;
        [Inject] private World _world;
        
        private readonly Features.Features _features = new Features.Features();

        private bool _inited;

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            Debug.Log($"Bootstrap StartAsync");
            _features.Inject(_container);
            _features.Initialize();
            await _features.StartAsync(cancellation);

            _inited = true;
        }

        public void Tick()
        {
            if (!_inited)
            {
                return;
            }
            
            _world.Commit();
            _features.Tick();
        }
    }
}