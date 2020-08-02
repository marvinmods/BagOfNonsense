using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class AutomaticShotgun : ModItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Automatic Boomstick");

        public override void SetDefaults()
        {
            item.knockBack = 3.75f;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 18;
            item.useTime = 18;
            item.width = 50;
            item.height = 14;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item36;
            item.damage = 15;
            item.shootSpeed = 14.35f;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 6, 0, 0);
            item.rare = ItemRarityID.LightRed;
            item.ranged = true;
            item.autoReuse = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(9));
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed *= scale;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }

            return false;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-10, 0);

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Boomstick);
            recipe.AddIngredient(ItemID.Minishark);
            recipe.AddIngredient(ItemID.HellstoneBar, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}