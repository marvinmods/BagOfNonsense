using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Projectiles
{
    public class IcegunProj1 : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Ice bolt");

        public override void SetDefaults()
        {
            projectile.extraUpdates = 1;
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 2700;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.light = 0.15f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 14; i++)
            {
                int dusty = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 135, projectile.direction * 2, 0.0f, 150, default, 1.45f);
                var dust = Main.dust[dusty];
                Vector2 vector2 = Vector2.Multiply(dust.velocity, 1.75f);
                dust.velocity = vector2;
                Main.dust[dusty].noGravity = false;
            }

            float v = Main.rand.NextFloat(1f);
            target.AddBuff(BuffID.Frostburn, 240);
            if (v < 0.1f) target.AddBuff(BuffID.Chilled, 240);
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Main.rand.NextFloat(1f) < 0.25f)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 5, 5, 135, 0f, 0f, 100, default, 1f);
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int i = 0; i < 10; i++)
            {
                int dusty = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 135, projectile.direction * 2, 0.0f, 150, default, 0.9f);
                var dust = Main.dust[dusty];
                Vector2 vector2 = Vector2.Multiply(dust.velocity, 1.5f);
                dust.velocity = vector2;
                Main.dust[dusty].noGravity = false;
            }
        }
    }
}

namespace BagOfNonsense.Projectiles
{
    public class IcegunProj2 : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Ice bolt");

        public override void SetDefaults()
        {
            projectile.extraUpdates = 1;
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 2700;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.light = 0.15f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 14; i++)
            {
                int dusty = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 135, projectile.direction * 2, 0.0f, 150, default, 1.45f);
                var dust = Main.dust[dusty];
                Vector2 vector2 = Vector2.Multiply(dust.velocity, 1.75f);
                dust.velocity = vector2;
                Main.dust[dusty].noGravity = false;
            }

            target.AddBuff(BuffID.Frostburn, 240);
            target.AddBuff(BuffID.Chilled, 240);
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Main.rand.NextFloat(1f) < 0.25f)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 5, 5, 135, 0f, 0f, 100, default, 1f);
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int i = 0; i < 10; i++)
            {
                int dusty = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 135, projectile.direction * 2, 0.0f, 150, default, 0.9f);
                var dust = Main.dust[dusty];
                Vector2 vector2 = Vector2.Multiply(dust.velocity, 1.5f);
                dust.velocity = vector2;
                Main.dust[dusty].noGravity = false;
            }
        }
    }
}

namespace BagOfNonsense.Projectiles
{
    public class IcegunProj3 : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Ice bolt");

        public override void SetDefaults()
        {
            projectile.extraUpdates = 1;
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 2700;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.light = 0.15f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 14; i++)
            {
                int dusty = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 135, projectile.direction * 2, 0.0f, 150, default, 1.45f);
                var dust = Main.dust[dusty];
                Vector2 vector2 = Vector2.Multiply(dust.velocity, 1.75f);
                dust.velocity = vector2;
                Main.dust[dusty].noGravity = false;
            }

            target.AddBuff(BuffID.Frostburn, 240);
            target.AddBuff(BuffID.Chilled, 240);
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Main.rand.NextFloat(1f) < 0.25f)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 5, 5, 135, 0f, 0f, 100, default, 1f);
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int i = 0; i < 10; i++)
            {
                int dusty = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 135, projectile.direction * 2, 0.0f, 150, default, 0.9f);
                var dust = Main.dust[dusty];
                Vector2 vector2 = Vector2.Multiply(dust.velocity, 1.5f);
                dust.velocity = vector2;
                Main.dust[dusty].noGravity = false;
            }
        }
    }
}