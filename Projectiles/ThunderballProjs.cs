using BagOfNonsense.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Projectiles
{
    public class ThunderballBomb : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Thunderball");

        public override void SetDefaults()
        {
            projectile.width = 400;
            projectile.height = 400;
            projectile.scale = 1f;
            projectile.aiStyle = 0;
            projectile.ranged = true;
            projectile.penetrate = 999;
            projectile.light = 0.4f;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 1;
            projectile.extraUpdates = 100;
            projectile.alpha = 255;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Highwattage>(), 90);
            target.immune[projectile.owner] = 0;
        }

        public override void AI()
        {
            for (int i = 0; i < 60; i++)
            {
                int dusty = Dust.NewDust(new Vector2(projectile.Hitbox.X, projectile.Hitbox.Y), 400, 400, 229, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default, 1.25f);
                var dust = Main.dust[dusty];
                dust.noGravity = true;
            }
        }
    }
}

namespace BagOfNonsense.Projectiles
{
    public class ThunderballProj1 : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Thunderball");

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.scale = 1f;
            projectile.aiStyle = 0;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.light = 0.4f;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.timeLeft = 3600;
            projectile.extraUpdates = 1;
            projectile.alpha = 0;
        }

        public override void AI()
        {
            projectile.rotation += 0.25f * (float)projectile.direction;
            projectile.velocity.Y = projectile.velocity.Y + 0.275f;
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
        }

        public override void Kill(int timeLeft)
        {
            var owner = Main.player[projectile.owner];
            int damage = owner.inventory[owner.selectedItem].damage;
            float knockback = owner.inventory[owner.selectedItem].knockBack;
            Projectile.NewProjectile(projectile.position, projectile.velocity * 0, ModContent.ProjectileType<ThunderballBomb>(), damage, 0f, owner.whoAmI);
            Main.PlaySound(SoundID.Item14, projectile.position);
            for (int i = 0; i < 10; i++)
            {
                int dusty = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 226, projectile.direction, 0.0f, 150, default, 0.9f);
                var dust = Main.dust[dusty];
                dust.noGravity = true;
            }
        }
    }
}

namespace BagOfNonsense.Projectiles
{
    public class ThunderballProj : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Thunderball");

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.scale = 1f;
            projectile.aiStyle = 0;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.light = 0.4f;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.timeLeft = 3600;
            projectile.extraUpdates = 1;
            projectile.alpha = 255;
        }

        public override void AI()
        {
            if (projectile.alpha > 0) projectile.alpha -= 15;
            if (projectile.timeLeft < 3595)
            {
                int dusty = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 6, 6, 229, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.25f);
                var dust = Main.dust[dusty];
                dust.velocity *= -0.25f;
                dust = Main.dust[dusty];
                dust.velocity *= -0.25f;
                dust = Main.dust[dusty];
                dust.position -= projectile.velocity * 0.5f;
                dust.noGravity = true;
                Main.dust[dusty].noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            var owner = Main.player[projectile.owner];
            int damage = owner.inventory[owner.selectedItem].damage;
            float knockback = owner.inventory[owner.selectedItem].knockBack;
            if (Main.rand.NextFloat(1f) <= 0.08f)
            {
                for (int i = 0; i < 4; i++)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 50f, Main.rand.NextFloat(-8f, 14f), -10f, ModContent.ProjectileType<ThunderballProj1>(), damage, knockback, Main.myPlayer);
                }
            }
            else
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 50f, 0, -10f, ModContent.ProjectileType<ThunderballProj1>(), damage, knockback, Main.myPlayer);
            }

            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int i = 0; i < 10; i++)
            {
                int dusty = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 229, projectile.direction * 2, 0.0f, 150, default, 0.9f);
                var dust = Main.dust[dusty];
                Vector2 vector2 = Vector2.Multiply(dust.velocity, 1.5f);
                dust.velocity = vector2;
                Main.dust[dusty].noGravity = false;
            }
        }
    }
}