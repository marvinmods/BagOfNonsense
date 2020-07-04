using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Projectiles
{
    public class staffproj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Staff of Extreme Prejudice bolt");
        }

        public override void SetDefaults()
        {
            projectile.width = 13;
            projectile.height = 17;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 30;
            projectile.magic = true;
            projectile.ignoreWater = false;
            projectile.damage = 0;
            projectile.light = 0.1f;
            projectile.noDropItem = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            var owner = Main.player[projectile.owner];
            int rand = Main.rand.Next(100);
            if (rand == 1)
            {
                owner.statLife += 20;
                owner.HealEffect(20, true);
            }
            else if (rand == 99)
            {
                owner.statLife += 10;
                owner.HealEffect(10, true);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            return true;
        }

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
    }
}