using BagOfNonsense.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class Iceray : ModItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Ice ray");

        public override void SetDefaults()
        {
            item.mana = 7;
            item.UseSound = SoundID.Item91;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.damage = 38;
            item.useAnimation = 16;
            item.useTime = 16;
            item.autoReuse = true;
            item.width = 62;
            item.height = 28;
            item.shoot = ModContent.ProjectileType<IcerayProj>();
            item.shootSpeed = 6f;
            item.knockBack = 3.25f;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.magic = true;
            item.rare = ItemRarityID.Pink;
            item.noMelee = true;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-12, 0);

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddIngredient(ItemID.LaserRifle);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

namespace BagOfNonsense.Projectiles
{
    public class IcerayProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 200;
            projectile.penetrate = -1;
        }

        public override string Texture => "Terraria/Projectile_" + ProjectileID.ShadowBeamFriendly;

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.position.X = projectile.position.X + projectile.velocity.X;
                projectile.velocity.X = -oldVelocity.X;
            }

            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.position.Y = projectile.position.Y + projectile.velocity.Y;
                projectile.velocity.Y = -oldVelocity.Y;
            }

            return false;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            projectile.damage = (int)(projectile.damage * 0.9);
            float v = Main.rand.NextFloat(1f);
            if (v < 0.2f)
            {
                if (v < 0.5f)
                {
                    target.AddBuff(BuffID.Frostburn, 240);
                    if (v < 0.04f)
                    {
                        target.AddBuff(BuffID.Chilled, 240);
                    }
                }

                target.AddBuff(BuffID.Oiled, 240);
            }
        }

        public override void AI()
        {
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 9f)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 projectilePosition = projectile.position;
                    projectilePosition -= projectile.velocity * (i * 0.25f);
                    projectile.alpha = 255;
                    int dust = Dust.NewDust(projectilePosition, 1, 1, 135, 0f, 0f, 0, default, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = projectilePosition;
                    Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[dust].velocity *= 0.2f;
                }
            }
        }
    }
}