using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
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
            item.damage = 38;
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
            if (consume == 1) return false;

            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int number = Main.rand.Next(6);
            float radians;
            if (number == 6) radians = 6; else radians = 4;
            if (number == 0) number = 1;
            {
                for (int i = 0; i < number; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(radians));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.BoneArrow, damage, knockBack, player.whoAmI);
                }
            }

            return false;
        }

        public override Vector2? HoldoutOffset() => new Vector2(x: 3, y: 0);

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