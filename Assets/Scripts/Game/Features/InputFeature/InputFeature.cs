using ShipsWar.Game.Features.InputFeature.Systems;
using VContainer;
using VContainer.Unity;

namespace ShipsWar.Game.Features.InputFeature
{
    public class InputFeature : IStartable, ITickable
    {
        private readonly InputReadSystem _inputReadSystem = new InputReadSystem();
        
        public void Inject(IObjectResolver objectResolver)
        { 
            objectResolver.Inject(_inputReadSystem);
        }
        
        public void Start()
        {
            _inputReadSystem.Start();
        }

        public void Tick()
        {
            _inputReadSystem.Tick();
        }
    }
}