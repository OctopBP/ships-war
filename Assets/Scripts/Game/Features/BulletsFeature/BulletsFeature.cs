using ShipsWar.Game.Features.BulletsFeature.Systems;

namespace ShipsWar.Game.Features.BulletsFeature
{
    public partial class BulletsFeature : IFeature
    {
        private readonly BulletSpawnSystem _bulletSpawnSystem;
        private readonly BulletsMoveSystem _bulletsMoveSystem;
    }
}