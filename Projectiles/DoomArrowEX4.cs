﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Projectiles
{
    public class DoomArrowEX4 : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Doom Arrow");

        public override void SetDefaults()
        {
            projectile.arrow = true;
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.timeLeft = 180;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.light = 0.15f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            var owner = Main.player[projectile.owner];
            int heal = damage / 50;
            float chance = Main.rand.NextFloat(1f);
            if (chance < 0.1f)
            {
                owner.AddBuff(BuffID.RapidHealing, 150);
            }

            if (heal < 1f)
            {
                heal = 1;
            }

            if (chance < 0.02f)
            {
                owner.statLife += heal;
                owner.HealEffect(heal, true);
            }

            target.AddBuff(BuffID.Oiled, 3600);
            target.AddBuff(BuffID.BetsysCurse, 3600);
        }

        public override void AI()
        {
            Color color = new Color(255, 70, 160, 255);
            if (projectile.timeLeft == 180)
            {
                float num = 16f;
                for (int index1 = 0; index1 < num; ++index1)
                {
                    Vector2 v = Vector2.Add(Vector2.Multiply(Vector2.UnitX, 0.0f), Vector2.Multiply(Vector2.Negate(Vector2.UnitY.RotatedBy((double)index1 * (6.28318548202515 / (double)num), (Vector2.One))), new Vector2(1f, 4f))).RotatedBy((double)projectile.velocity.ToRotation(), (Vector2.One));
                    int index2 = Dust.NewDust(projectile.Center, 0, 0, 236, 0.0f, 0.0f, 0, color, 1f);
                    Main.dust[index2].scale = 1f;
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].position = Vector2.Add(projectile.Center, v);
                    Main.dust[index2].velocity = Vector2.Add(Vector2.Multiply(projectile.velocity, 0.0f), Vector2.Multiply(v.SafeNormalize(Vector2.UnitY), 1f));
                }
            }

            Dust dust;
            dust = Dust.NewDustPerfect(projectile.Center, 188, Vector2.One, 0, color, 1f);
            dust.noGravity = true;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            float num132 = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
            float num133 = projectile.localAI[0];
            if (num133 == 0f)
            {
                projectile.localAI[0] = num132;
                num133 = num132;
            }

            float num134 = projectile.position.X;
            float num135 = projectile.position.Y;
            float num136 = 300f;
            bool flag3 = false;
            int num137 = 0;
            if (projectile.ai[1] == 0f)
            {
                for (int num138 = 0; num138 < 200; num138++)
                {
                    if (Main.npc[num138].CanBeChasedBy(this, false) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(num138 + 1)))
                    {
                        float num139 = Main.npc[num138].position.X + (float)(Main.npc[num138].width / 2);
                        float num140 = Main.npc[num138].position.Y + (float)(Main.npc[num138].height / 2);
                        float num141 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num139) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num140);
                        if (num141 < num136 && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[num138].position, Main.npc[num138].width, Main.npc[num138].height))
                        {
                            num136 = num141;
                            num134 = num139;
                            num135 = num140;
                            flag3 = true;
                            num137 = num138;
                        }
                    }
                }

                if (flag3)
                {
                    projectile.ai[1] = (float)(num137 + 1);
                }

                flag3 = false;
            }

            if (projectile.ai[1] > 0f)
            {
                int num142 = (int)(projectile.ai[1] - 1f);
                if (Main.npc[num142].active && Main.npc[num142].CanBeChasedBy(this, true) && !Main.npc[num142].dontTakeDamage)
                {
                    float num143 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
                    float num144 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
                    if (Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num143) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num144) < 1000f)
                    {
                        flag3 = true;
                        num134 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
                        num135 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
                    }
                }
                else
                {
                    projectile.ai[1] = 0f;
                }
            }

            if (!projectile.friendly)
            {
                flag3 = false;
            }

            if (flag3)
            {
                float num145 = num133;
                Vector2 vector10 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num146 = num134 - vector10.X;
                float num147 = num135 - vector10.Y;
                float num148 = (float)Math.Sqrt((double)(num146 * num146 + num147 * num147));
                num148 = num145 / num148;
                num146 *= num148;
                num147 *= num148;
                int num149 = 8;
                projectile.velocity.X = (projectile.velocity.X * (float)(num149 - 1) + num146) / (float)num149;
                projectile.velocity.Y = (projectile.velocity.Y * (float)(num149 - 1) + num147) / (float)num149;
            }
        }

        public override bool PreKill(int timeLeft)
        {
            projectile.type = 0;
            return true;
        }

        public override void Kill(int timeLeft)
        {
            int num1 = Main.rand.Next(5, 10);
            for (int index1 = 0; index1 < num1; ++index1)
            {
                int index2 = Dust.NewDust(projectile.position, 0, 0, 229, 0.0f, 0.0f, 100, Color.AliceBlue, 1f);
                var dust1 = Main.dust[index2];
                dust1.velocity = Vector2.Multiply(dust1.velocity, 1.6f);
                var dust2 = Main.dust[index2];
                dust2.position = Vector2.Subtract(dust2.position, Vector2.Multiply(Vector2.One, 4f));
                Main.dust[index2].position = Vector2.Lerp(Main.dust[index2].position, projectile.Center, 0.5f);
                Main.dust[index2].noGravity = true;
            }
        }
    }
}