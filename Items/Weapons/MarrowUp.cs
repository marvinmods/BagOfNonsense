using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZoaklenMod.Items.Weapons
{
    public class MarrowUp : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marrowned");
            Tooltip.SetDefault("A bow shotgun?\n" +
                                "50% chance not to consume ammo");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Marrow);
            item.damage = 28;
            item.knockBack = 4f;
            item.useTime = 8;
            item.useAnimation = 24;
            item.autoReuse = true;
            item.shoot = ProjectileID.BoneArrow;
            item.shootSpeed = 9f;
            item.useStyle = ItemUseStyleID.HoldingOut;
        }

        public override bool ConsumeAmmo(Player player)
        {
            int consume = Main.rand.Next(2);
            if (consume == 1)
            {
                return false;
            }

            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float speedX2 = Main.mouseX + Main.screenPosition.X - position.X;
            float speedY2 = Main.mouseY + Main.screenPosition.Y - position.Y;
            int nerfdamage = (int)(damage * 0.66);
            int extradamage = (int)(damage * 1.33);
            float nerfknockback = (float)(knockBack * 0.4);
            speedX2 += Main.rand.Next(-4, 6);
            speedY2 += Main.rand.Next(-4, 6);
            speedX += Main.rand.Next(-4, 5);
            speedY += Main.rand.Next(-4, 5);
            float chance = Main.rand.NextFloat(0.1f, 1f);
            {
                if (chance > 0.95f)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(13));
                        Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.BoneArrow, nerfdamage, nerfknockback, player.whoAmI);
                    }
                }
                else if (chance < 0.95f && chance > 0.45f)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(13));
                        Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.BoneArrow, nerfdamage, nerfknockback, player.whoAmI);
                    }
                }

                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX2, speedY2).RotatedByRandom(MathHelper.ToRadians(4));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.BoneArrow, extradamage, knockBack, player.whoAmI);
                }
            }

            return false;
        }

        public override Vector2? HoldoutOffset() => new Vector2(x: 0, y: 5);

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Marrow);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
            recipe.AddIngredient(ItemID.WoodenArrow, 50);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}