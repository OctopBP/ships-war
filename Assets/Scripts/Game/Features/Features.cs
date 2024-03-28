namespace ShipsWar.Game.Features
{
    public partial class Features : IFeatureRunner
    {
        private readonly InputFeature.InputFeature _inputFeature;
        private readonly PlayerFeature.PlayerFeature _playerFeature;
        private readonly EnemiesFeature.EnemiesFeature _enemiesFeature;
        private readonly BulletsFeature.BulletsFeature _bulletsFeature;
    }
}