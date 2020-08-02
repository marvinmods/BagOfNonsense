using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Projectiles
{
    public class CrossbowBolt : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Crossbow Bolt");

        public override void SetDefaults()
        {
            projectile.alpha = 0;
            projectile.arrow = true;
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            if (Main.rand.NextFloat(1f) < 0.4f)
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

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            if (projectile.owner == Main.myPlayer)
            {
                var owner = Main.player[projectile.owner];
                int damage = owner.inventory[owner.selectedItem].damage;
                float knockback = owner.inventory[owner.selectedItem].knockBack;
                int num630 = Main.rand.Next(4, 10); ;
                int num3;
                for (int num631 = 0; num631 < num630; num631 = num3 + 1)
                {
                    if (num631 % 2 != 1 || Main.rand.Next(3) == 0)
                    {
                        Vector2 vector22 = projectile.position;
                        Vector2 value15 = projectile.oldVelocity;
                        value15.Normalize();
                        value15 *= 8f;
                        float num632 = (float)Main.rand.Next(-35, 36) * 0.01f;
                        float num633 = (float)Main.rand.Next(-35, 36) * 0.01f;
                        vector22 -= value15 * (float)num631;
                        num632 += projectile.oldVelocity.X / 5f;
                        num633 += projectile.oldVelocity.Y / 5f;
                        int num634 = Projectile.NewProjectile(vector22.X, vector22.Y, num632, num633, ModContent.ProjectileType<HolyCross>(), (int)(damage * 0.5f), knockback * 0.33f, Main.myPlayer, 0f, 0f); ;
                        Main.projectile[num634].magic = false;
                        Main.projectile[num634].ranged = true;
                        Main.projectile[num634].penetrate = 2;
                    }

                    num3 = num631;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                int dusty = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, projectile.direction * 2, 0.0f, 150, default, 0.9f);
                var dust = Main.dust[dusty];
                Vector2 vector2 = Vector2.Multiply(dust.velocity, 1.5f);
                dust.velocity = vector2;
                Main.dust[dusty].noGravity = false;
            }
        }
    }
}