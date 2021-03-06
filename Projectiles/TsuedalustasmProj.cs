using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Projectiles
{
    public class Hiddenproj : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Daedalus");

        public override void SetDefaults()
        {
            projectile.arrow = true;
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 2700;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.light = 0.15f;
            projectile.extraUpdates = 1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            var player = Main.player[projectile.owner];
            float num75 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
            int num42 = 14;
            float scaleFactor11 = 14f;
            int weaponDamage2 = player.GetWeaponDamage(player.inventory[player.selectedItem]);
            bool flag16 = true;
            float weaponKnockback2 = player.inventory[player.selectedItem].knockBack;
            player.PickAmmo(player.inventory[player.selectedItem], ref num42, ref scaleFactor11, ref flag16, ref weaponDamage2, ref weaponKnockback2, false);
            for (int index = 0; index < 4; ++index)
            {
                Vector2 vector2_1 = new Vector2((float)((double)player.position.X + (double)player.width * 0.5 + (double)(Main.rand.Next(201) * -player.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)player.position.X)), (float)((double)player.position.Y + (double)player.height * 0.5 - 600.0));   //this defines the projectile width, direction and position
                vector2_1.X = (float)(((double)vector2_1.X + (double)player.Center.X) / 2.0) + (float)Main.rand.Next(-200, 201);
                vector2_1.Y -= (float)(100 * index);
                float num12 = (float)Main.mouseX + Main.screenPosition.X - vector2_1.X;
                float num13 = (float)Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                if ((double)num13 < 0.0) num13 *= -1f;
                if ((double)num13 < 20.0) num13 = 20f;
                float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                float num15 = num75 / num14;
                float num16 = num12 * num15;
                float num17 = num13 * num15;
                float SpeedX = num16 + (float)Main.rand.Next(-40, 41) * 0.02f; //change the Main.rand.Next here to, for example, (-10, 11) to reduce the spread. Change this to 0 to remove it altogether
                float SpeedY = num17 + (float)Main.rand.Next(-40, 41) * 0.02f;
                int shoot = Projectile.NewProjectile(vector2_1.X, vector2_1.Y, SpeedX, SpeedY, num42, weaponDamage2, weaponKnockback2, player.whoAmI, 0.0f, Main.rand.Next(5)); ;
                Main.projectile[shoot].noDropItem = true;
            }
        }
    }
}

namespace BagOfNonsense.Projectiles
{
    public class TsuedalustasmProj : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Phantasm");

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Phantasm);
            projectile.width = 66;
            projectile.height = 32;
        }

        public override bool PreKill(int timeLeft)
        {
            projectile.type = ProjectileID.Phantasm;
            return true;
        }

        public override void AI()
        {
            var player = Main.player[projectile.owner];

            if (Main.rand.NextFloat(1f) < 0.6f)
            {
                int dusty = Dust.NewDust(player.position, projectile.height, projectile.height, 229, projectile.velocity.X * 0.2225f, projectile.velocity.Y * 0.25f, 255, default, 0.7f);
                Main.dust[dusty].fadeIn = 0.2f;
            }

            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            float num;
            num = 0f;
            if (projectile.spriteDirection == -1)
            {
                num = 3.14159274f;
            }

            // projectile.ai[0] += 1f;
            int num39 = 0;
            if (projectile.ai[0] >= 40f) num39++;

            if (projectile.ai[0] >= 80f) num39++;

            if (projectile.ai[0] >= 120f) num39++;

            int num40 = 24;
            int num41 = 2;
            projectile.ai[1] -= 1f;
            bool flag15 = false;
            if (projectile.ai[1] <= 0f)
            {
                projectile.ai[1] = (float)(num40 - num41 * num39);
                flag15 = true;
                int arg_1F5C_0 = (int)projectile.ai[0] / (num40 - num41 * num39);
            }

            bool flag16 = player.channel && player.HasAmmo(player.inventory[player.selectedItem], true) && !player.noItems && !player.CCed;
            if (projectile.localAI[0] > 0f)
            {
                projectile.localAI[0] -= 1f;
            }

            if (projectile.soundDelay <= 0 && flag16)
            {
                projectile.soundDelay = num40 - num41 * num39;
                if (projectile.ai[0] != 1f)
                {
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 5);
                }

                projectile.localAI[0] = 12f;
            }

            if (flag15 && Main.myPlayer == projectile.owner)
            {
                int num42 = 14;
                float scaleFactor11 = 14f;
                float num75 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                int weaponDamage2 = player.GetWeaponDamage(player.inventory[player.selectedItem]);
                float weaponKnockback2 = player.inventory[player.selectedItem].knockBack;
                if (flag16)
                {
                    player.PickAmmo(player.inventory[player.selectedItem], ref num42, ref scaleFactor11, ref flag16, ref weaponDamage2, ref weaponKnockback2, false);
                    weaponKnockback2 = player.GetWeaponKnockback(player.inventory[player.selectedItem], weaponKnockback2);
                    float scaleFactor12 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    Vector2 vector19 = vector;
                    Vector2 value18 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector19;
                    if (player.gravDir == -1f)
                    {
                        value18.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector19.Y;
                    }

                    Vector2 value19 = Vector2.Normalize(value18);
                    if (float.IsNaN(value19.X) || float.IsNaN(value19.Y))
                    {
                        value19 = -Vector2.UnitY;
                    }

                    value19 *= scaleFactor12;
                    if (value19.X != projectile.velocity.X || value19.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }

                    projectile.velocity = value19 * 0.55f;
                    for (int num43 = 0; num43 < 5; num43++)
                    {
                        Vector2 vector20 = Vector2.Normalize(projectile.velocity) * scaleFactor11 * (0.6f + Main.rand.NextFloat() * 0.8f);
                        if (float.IsNaN(vector20.X) || float.IsNaN(vector20.Y))
                        {
                            vector20 = -Vector2.UnitY;
                        }

                        Vector2 vector21 = vector19 + Utils.RandomVector2(Main.rand, -15f, 15f);

                        int num44 = Projectile.NewProjectile(vector21.X, vector21.Y, vector20.X, vector20.Y, num42, weaponDamage2, weaponKnockback2, projectile.owner, 0f, 0f);
                        Main.projectile[num44].noDropItem = true;
                    }
                }
                else
                {
                    projectile.Kill();
                }
            }

            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + num;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
        }
    }
}