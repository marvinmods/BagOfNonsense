using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace BagOfNonsense.Projectiles
{
    public class HolyCross : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Holy Cross");

        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 36;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = 2;
            projectile.alpha = 0;
            projectile.timeLeft = 2000;
            projectile.extraUpdates = 3;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player owner = Main.player[projectile.owner];
            target.immune[projectile.owner] = 4;
        }

        public override void AI()
        {
            if (Main.rand.NextFloat(1f) < 0.1f)
            {
                int dusttype;
                if (Main.rand.Next(2) == 0)
                {
                    dusttype = 6;
                }
                else
                {
                    dusttype = 75;
                }
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, dusttype, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default, 0.9f);
                Main.dust[dust].velocity *= 0.25f;
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            float light = projectile.alpha / 255f;
            Lighting.AddLight((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16, 0.3f * light, 0.4f * light, 1f * light);
        }
    }
}