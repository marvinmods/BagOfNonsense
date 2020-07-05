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
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 270;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.light = 0.15f;
        }

        public override void AI()
        {
            Dust dust;
            dust = Dust.NewDustPerfect(projectile.Center, 169, Vector2.One, 0, Color.Honeydew, 1f);
            dust.noGravity = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            var owner = Main.player[projectile.owner];
            int heal = damage / 40;
            float chance = Main.rand.NextFloat(0.005f, 1f);
            if (heal < 1f)
            {
                heal = 1;
            }

            if (chance < 0.02f)
            {
                owner.AddBuff(BuffID.RapidHealing, 150);
            }

            if (chance < 0.01f)
            {
                owner.statLife += heal;
                owner.HealEffect(heal, true);
            }

            int type = 0;
            Vector2 vector2 = Vector2.Multiply(Main.npc[target.whoAmI].DirectionFrom(Main.player[owner.whoAmI].position), 6f);
            int nerfdamage = (int)(owner.GetWeaponDamage(owner.inventory[owner.selectedItem]) * 0.66);
            if (nerfdamage < 20f)
            {
                nerfdamage = 20;
            }

            for (int i = 0; i < 4; i++)
            {
                type = Main.rand.Next(new int[] { type, mod.ProjectileType("DoomArrowEX1"), mod.ProjectileType("DoomArrowEX2"), mod.ProjectileType("DoomArrowEX3"), mod.ProjectileType("DoomArrowEX4") });
                float rand = Main.rand.NextFloat(2f, 2.8f);
                float randspeed = Main.rand.NextFloat(3f, 6f);
                Vector2 playerpos = Vector2.Add(Main.player[owner.whoAmI].position, Vector2.Multiply(Main.player[owner.whoAmI].Size * 1.5f, Utils.RandomVector2(Main.rand, -1f, 1f)));
                Projectile.NewProjectile(playerpos.X + rand, playerpos.Y + rand, vector2.X * randspeed, vector2.Y * randspeed, type, nerfdamage, 0.0f, owner.whoAmI, target.whoAmI, 0.0f);
            }
        }

        public override bool OnTileCollide(Vector2 velocityChange)
        {
            if (projectile.velocity.X != velocityChange.X)
            {
                projectile.velocity.X = velocityChange.X / (.975f);
            }

            if (projectile.velocity.Y != velocityChange.Y)
            {
                projectile.velocity.Y = velocityChange.Y / (.975f);
            }

            return false;
        }

        public override bool PreKill(int timeLeft)
        {
            projectile.type = 0;
            return true;
        }
    }
}