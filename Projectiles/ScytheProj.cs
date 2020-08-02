using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Projectiles
{
    public class ScytheProj : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Cold touch");

        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 42;
            projectile.alpha = 100;
            projectile.light = 0.5f;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.scale = 1.1f;
            projectile.melee = true;
            projectile.timeLeft = 360;
            projectile.coldDamage = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 900);
            target.AddBuff(BuffID.Chilled, 900);
            target.immune[projectile.owner] = 5;
        }

        public override void AI()
        {
            if (Main.rand.NextFloat(1f) < 0.25f)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 255, default, 1f);
                Main.dust[dust].noGravity = false;
            }

            projectile.spriteDirection = -1;
            projectile.rotation = projectile.rotation + projectile.direction * 0.05f;
            projectile.rotation = projectile.rotation + (float)(projectile.direction * 0.5 * (projectile.timeLeft / 180.0));
            projectile.velocity = Vector2.Multiply(projectile.velocity, 0.96f);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 31; i++)
            {
                int dusty = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 135, projectile.direction * 2, 0.0f, 150, default, 1.2f);
                var dust = Main.dust[dusty];
                Vector2 vector2 = Vector2.Multiply(dust.velocity, 1.5f);
                dust.velocity = vector2;
                Main.dust[dusty].noGravity = false;
            }

            Main.PlaySound(SoundID.Item10, projectile.position);
        }
    }
}