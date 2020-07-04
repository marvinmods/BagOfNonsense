using Terraria.ModLoader;

namespace BagOfNonsense.Projectiles
{
    public class minigunround : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Minigun round");

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 200;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.damage = 1;
            projectile.light = 0.2f;
            projectile.noDropItem = true;
        }
    }
}