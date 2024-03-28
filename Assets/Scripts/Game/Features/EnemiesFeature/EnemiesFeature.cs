using ShipsWar.Game.Features.EnemiesFeature.Systems;

namespace ShipsWar.Game.Features.EnemiesFeature
{
    public partial class EnemiesFeature : IFeature
    {
        private readonly EnemiesSpawnSystem _enemiesSpawnSystem;
        private readonly EnemiesMoveSystem _enemiesMoveSystem;
        private readonly EnemiesShootSystem _enemiesShootSystem;
    }
}