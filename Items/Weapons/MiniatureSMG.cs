using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class MiniatureSMG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Miniature SMG");
            Tooltip.SetDefault("45% chance not to consume ammo");
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.knockBack = 1f;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 16;
            item.useTime = 4;
            item.reuseDelay = 14;
            item.width = 54;
            item.height = 30;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item31;
            item.damage = 8;
            item.shootSpeed = 12.75f;
            item.noMelee = true;
            item.value = 150000;
            item.rare = ItemRarityID.LightRed;
            item.ranged = true;
            item.scale = 0.8f;
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.NextFloat(1f) < 0.45f) return false;
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 spread = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4));
            Projectile.NewProjectile(position.X, position.Y, spread.X, spread.Y, type, damage, knockBack, player.whoAmI);
            return false;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-5, 0);

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Minishark);
            recipe.AddIngredient(ItemID.IllegalGunParts, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}