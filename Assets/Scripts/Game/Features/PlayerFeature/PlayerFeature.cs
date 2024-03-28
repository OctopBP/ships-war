using ShipsWar.Game.Features.PlayerFeature.Systems;

namespace ShipsWar.Game.Features.PlayerFeature
{
    public partial class PlayerFeature : IFeature
    {
        private readonly PlayerSpawnSystem _playerSpawnSystem;
        private readonly PlayerMoveSystem _playerMoveSystem;
        private readonly PlayerShootingSystem _playerShootingSystem;
        private readonly PlayerApplyPositionSystem _playerApplyPositionSystem;
    }
}