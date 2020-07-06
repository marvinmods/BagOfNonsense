using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Projectiles
{
    public class ScytheProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cold touch");
        }

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
            projectile.timeLeft = 180;
            projectile.coldDamage = true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = mod.GetTexture("Projectiles/ScytheProjGlow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    projectile.position.X - Main.screenPosition.X + projectile.width * 0.5f,
                    projectile.position.Y - Main.screenPosition.Y + projectile.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                projectile.rotation,
                texture.Size() * 0.5f,
                projectile.scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 900);
            target.AddBuff(BuffID.Chilled, 900);
            target.immune[projectile.owner] = 5;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 255, default, 1f);
            Main.dust[dust].noGravity = false;
            projectile.spriteDirection = -1;
            projectile.rotation = projectile.rotation + projectile.direction * 0.05f;
            projectile.rotation = projectile.rotation + (float)(projectile.direction * 0.5 * (projectile.timeLeft / 180.0));
            projectile.velocity = Vector2.Multiply(projectile.velocity, 0.96f);
        }
    }
}