using Microsoft.Xna.Framework;
using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Terraria;
using Terraria.ModLoader;

namespace BagOfNonsense.Projectiles
{
    public class GhostlyArrowproj : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Ghostly Arrow");

        public override void SetDefaults()
        {
            projectile.arrow = false;
            projectile.width = 14;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 1;
            projectile.tileCollide = false;
            projectile.timeLeft = 350;
            projectile.aiStyle = 0;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.damage = 1;
            projectile.light = 0.6f;
            projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            Dust dust;
            dust = Dust.NewDustPerfect(projectile.Center, 6, Vector2.One, 0, default, 1f);
            dust.noGravity = true;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            float num197 = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
            float num198 = projectile.localAI[0];
            if (num198 == 0f)
            {
                projectile.localAI[0] = num197;
                num198 = num197;
            }

            if (projectile.alpha > 0) projectile.alpha -= 25;
            if (projectile.alpha < 0) projectile.alpha = 0;
            float num199 = projectile.position.X;
            float num200 = projectile.position.Y;
            float num201 = 800f;
            bool flag5 = false;
            int num202 = 0;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 20f)
            {
                projectile.ai[0] -= 1f;
                if (projectile.ai[1] == 0f)
                {
                    for (int num203 = 0; num203 < 200; num203++)
                    {
                        if (Main.npc[num203].CanBeChasedBy(projectile, false) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(num203 + 1)))
                        {
                            float num204 = Main.npc[num203].position.X + (float)(Main.npc[num203].width / 2);
                            float num205 = Main.npc[num203].position.Y + (float)(Main.npc[num203].height / 2);
                            float num206 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num204) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num205);
                            if (num206 < num201 && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[num203].position, Main.npc[num203].width, Main.npc[num203].height))
                            {
                                num201 = num206;
                                num199 = num204;
                                num200 = num205;
                                flag5 = true;
                                num202 = num203;
                            }
                        }
                    }

                    if (flag5) projectile.ai[1] = (float)(num202 + 1);
                    flag5 = false;
                }

                if (projectile.ai[1] != 0f)
                {
                    int num207 = (int)(projectile.ai[1] - 1f);
                    if (Main.npc[num207].active && Main.npc[num207].CanBeChasedBy(projectile, true))
                    {
                        float num208 = Main.npc[num207].position.X + (float)(Main.npc[num207].width / 2);
                        float num209 = Main.npc[num207].position.Y + (float)(Main.npc[num207].height / 2);
                        if (Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num208) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num209) < 1000f)
                        {
                            flag5 = true;
                            num199 = Main.npc[num207].position.X + (float)(Main.npc[num207].width / 2);
                            num200 = Main.npc[num207].position.Y + (float)(Main.npc[num207].height / 2);
                        }
                    }
                }

                if (!projectile.friendly) flag5 = false;
                if (flag5)
                {
                    float arg_9B40_0 = num198;
                    Vector2 vector22 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    float num210 = num199 - vector22.X;
                    float num211 = num200 - vector22.Y;
                    float num212 = (float)Math.Sqrt((double)(num210 * num210 + num211 * num211));
                    num212 = arg_9B40_0 / num212;
                    num210 *= num212;
                    num211 *= num212;
                    int num213 = 8;
                    projectile.velocity.X = (projectile.velocity.X * (float)(num213 - 1) + num210) / (float)num213;
                    projectile.velocity.Y = (projectile.velocity.Y * (float)(num213 - 1) + num211) / (float)num213;
                }
            }
        }

        public override bool PreKill(int timeLeft)
        {
            projectile.type = 0;
            return true;
        }
    }
}