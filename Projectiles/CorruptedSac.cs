using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Projectiles
{
    public class CorruptedSac : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Corrupted Sac");

        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.friendly = true;
            projectile.damage = 1;
            projectile.width = 11;
            projectile.height = 11;
            projectile.timeLeft = 300;
            projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            Dust dust;
            dust = Dust.NewDustPerfect(projectile.Center, 16, Vector2.One, 0, Color.MediumPurple, 1f);
            dust.noGravity = true;
            projectile.velocity.X = projectile.velocity.X * 0.97f;
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
        }

        public override void Kill(int timeLeft)
        {
            if (projectile.owner == Main.myPlayer)
            {
                for (int i = 0; i < Main.rand.Next(3) + 3; i++)
                {
                    Projectile.NewProjectile(projectile.position.X, projectile.position.Y, Main.rand.Next(-35, 36) * 0.02f * 10f, Main.rand.Next(-35, 36) * 0.02f * 10f, ProjectileID.TinyEater, (int)(projectile.damage * 0.7), (int)(projectile.knockBack * 0.35), Main.myPlayer, 0.0f, 0.0f);
                }
            }

            int chance = Main.rand.Next(2);
            if (chance == 1)
            {
                Main.PlaySound(SoundID.NPCDeath1, projectile.position);
            }
        }

        public override bool PreKill(int timeLeft)
        {
            projectile.type = 0;
            return true;
        }
    }
}