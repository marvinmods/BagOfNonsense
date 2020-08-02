using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Projectiles
{
    public class DoomArrow : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Doom Arrow");

        public override void SetDefaults()
        {
            projectile.arrow = true;
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

        public override void AI()
        {
            projectile.velocity.Y = projectile.velocity.Y + 0.04f;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            int u;
            for (int i = 0; i < 1; i = u + 1)
            {
                int dusty = Dust.NewDust(projectile.position, projectile.width, projectile.height, 169, 0f, 0f, 100, default, 0.3f);
                Main.dust[dusty].scale = Main.rand.Next(1, 10) * 0.1f;
                Main.dust[dusty].noGravity = true;
                Main.dust[dusty].fadeIn = 1.1f;
                var dust = Main.dust[dusty];
                dust.velocity *= 0.75f;
                u = i;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            var owner = Main.player[projectile.owner];
            int heal = damage / 40;
            float chance = Main.rand.NextFloat(1f);
            if (heal <= 1f) heal = 1;

            if (chance <= 0.02f)
            {
                owner.AddBuff(BuffID.RapidHealing, 150);
            }

            if (chance <= 0.01f)
            {
                owner.statLife += heal;
                owner.HealEffect(heal, true);
            }

            int type = 0;
            Vector2 vector2 = Vector2.Multiply(Main.npc[target.whoAmI].DirectionFrom(Main.player[owner.whoAmI].position), 6f);
            int nerfdamage = (int)(owner.GetWeaponDamage(owner.inventory[owner.selectedItem]) * 0.66);
            if (nerfdamage < 20f) nerfdamage = 20;

            for (int i = 0; i < 4; i++)
            {
                type = Main.rand.Next(new int[] { type, ModContent.ProjectileType<DoomArrowEX1>(), ModContent.ProjectileType<DoomArrowEX2>(), ModContent.ProjectileType<DoomArrowEX3>(), ModContent.ProjectileType<DoomArrowEX4>() });
                float rand = Main.rand.NextFloat(1f, 6f);
                float randspeed = Main.rand.NextFloat(3f, 6f);
                Vector2 playerpos = Vector2.Add(Main.player[owner.whoAmI].position, Vector2.Multiply(Main.player[owner.whoAmI].Size * 1.5f, Utils.RandomVector2(Main.rand, -1f, 1f)));
                Projectile.NewProjectile(playerpos.X + rand, playerpos.Y + rand, vector2.X * randspeed, vector2.Y * randspeed, type, nerfdamage, 0.0f, owner.whoAmI, target.whoAmI, 0.0f);
            }
        }

        public override bool OnTileCollide(Vector2 velocityChange)
        {
            if (projectile.velocity.X != velocityChange.X)
            {
                projectile.velocity.X = velocityChange.X / .975f;
            }

            if (projectile.velocity.Y != velocityChange.Y)
            {
                projectile.velocity.Y = velocityChange.Y / .975f;
            }

            return false;
        }

        public override bool PreKill(int timeLeft)
        {
            projectile.type = 0;
            return true;
        }

        public override void Kill(int timeLeft)
        {
            Color color = new Color(255, 249, 70, 255);
            int num1 = Main.rand.Next(15, 20);
            for (int index1 = 0; index1 < num1; ++index1)
            {
                int index2 = Dust.NewDust(projectile.position, 0, 0, 43, 0.0f, 0.0f, 100, color, 1f);
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