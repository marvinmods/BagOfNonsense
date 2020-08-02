using BagOfNonsense.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Projectiles
{
    public class XenobusterProj1 : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Xenobuster");

        public override void SetDefaults()
        {
            projectile.width = 45;
            projectile.height = 45;
            projectile.scale = 1f;
            projectile.aiStyle = 0;
            projectile.melee = true;
            projectile.penetrate = 1;
            projectile.light = 0.4f;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 200;
            projectile.extraUpdates = 1;
            projectile.alpha = 255;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            projectile.velocity.X *= 1.025f;
            projectile.velocity.Y *= 1.025f;

            if (projectile.timeLeft < 200 && projectile.timeLeft > 192)
                projectile.alpha = 255;
            else
                projectile.alpha = 0;

            int dusty = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 229, projectile.direction * 2, 0.0f, 150, default, 1f);
            var dust = Main.dust[dusty];
            Vector2 vector2 = Vector2.Multiply(dust.velocity, 0.2f);
            dust.velocity = vector2;
            Main.dust[dusty].noGravity = true;

            float light = projectile.alpha / 255f;
            Lighting.AddLight((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16, 0.3f * light, 0.4f * light, 1f * light);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 14; i++)
            {
                int dusty = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 229, projectile.direction * 2, 0.0f, 150, default, 1f);
                var dust = Main.dust[dusty];
                Vector2 vector2 = Vector2.Multiply(dust.velocity, 1.1f);
                dust.velocity = vector2;
                Main.dust[dusty].noGravity = false;
            }

            for (int i = 0; i < 31; i++)
            {
                int dusty = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 226, projectile.direction * 2, 0.0f, 150, default, 0.66f);
                var dust = Main.dust[dusty];
                Vector2 vector2 = Vector2.Multiply(dust.velocity, 1.5f);
                dust.velocity = vector2;
                Main.dust[dusty].noGravity = false;
            }

            target.AddBuff(ModContent.BuffType<Highwattage>(), 1800);
        }
    }
}

namespace BagOfNonsense.Projectiles
{
    public class XenobusterProj : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Xenobuster");

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.scale = 1f;
            projectile.aiStyle = 0;
            projectile.melee = true;
            projectile.penetrate = 4;
            projectile.light = 0.4f;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 3600;
            projectile.extraUpdates = 1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            var player = Main.player[projectile.owner];
            target.immune[projectile.owner] = 6;
            for (int i = 0; i < 4; i++)
            {
                Vector2 NPCpos = Vector2.Add(Main.npc[target.whoAmI].position, Vector2.Multiply(Main.npc[target.whoAmI].Size, Utils.RandomVector2(Main.rand, -8f, 8f))); // spawn "zone"
                Vector2 projspawn = Vector2.Multiply(Main.npc[target.whoAmI].DirectionFrom(NPCpos), 6f); // projectile spawn position
                float speedy = Main.rand.NextFloat(0.2f, 0.5f);
                Projectile.NewProjectile(NPCpos.X, NPCpos.Y, projspawn.X * speedy, projspawn.Y * speedy, ModContent.ProjectileType<XenobusterProj1>(), (int)(damage * 0.85), 0.0f, player.whoAmI, target.whoAmI, 0.0f);
            }

            float num = 16f;
            for (int index1 = 0; index1 < num; ++index1)
            {
                Vector2 v = Vector2.Add(Vector2.Multiply(Vector2.UnitX, 0.0f), Vector2.Multiply(Vector2.Negate(Vector2.UnitY.RotatedBy((double)index1 * (6.28318548202515 / (double)num), (Vector2.One))), new Vector2(1f, 4f))).RotatedBy((double)projectile.velocity.ToRotation(), (Vector2.One));
                int index2 = Dust.NewDust(target.position, 0, 0, 107, 0.0f, 0.0f, 0, default, 1f);
                Main.dust[index2].scale = 3f;
                Main.dust[index2].noGravity = true;
                Main.dust[index2].position = Vector2.Add(projectile.Center, (v * 1.6f));
                Main.dust[index2].velocity = Vector2.Add(Vector2.Multiply(projectile.velocity, 0.0f), Vector2.Multiply(v.SafeNormalize(Vector2.UnitY), 1f));
            }
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (projectile.timeLeft == 3600)
            {
                Main.PlaySound(SoundID.Item60, projectile.position);
            }

            if (projectile.timeLeft < 3595)
            {
                for (int i = 0; i < 3; ++i)
                {
                    int dusty = Dust.NewDust(projectile.Center, 6, 6, 107, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.25f);
                    var dust = Main.dust[dusty];
                    dust.velocity *= -0.25f;
                    dusty = Dust.NewDust(projectile.Center, 6, 6, 107, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.25f);
                    dust = Main.dust[dusty];
                    dust.velocity *= -0.25f;
                    dust = Main.dust[dusty];
                    dust.position -= projectile.velocity * 0.5f;
                }
            }
        }
    }
}