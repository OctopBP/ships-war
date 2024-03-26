using Core;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private Assets _assets;
        [SerializeField] private Config _config;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterInstance(_assets);
            builder.RegisterInstance(_config);
            builder.RegisterInstance(World.Create());
            
            builder.Register<AssetProvider>(Lifetime.Singleton);
            builder.RegisterEntryPoint<Bootstrap>();
        }
    }
}
