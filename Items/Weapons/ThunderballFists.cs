using BagOfNonsense.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class ThunderballFists : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thunderball Fists");
            Tooltip.SetDefault("Spawns a eletric ball on hit");
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 20;
            item.useTime = 20;
            item.width = 44;
            item.height = 28;
            item.shoot = ModContent.ProjectileType<ThunderballProj>();
            item.knockBack = 5f;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item41;
            item.damage = 60;
            item.shootSpeed = 13f;
            item.noMelee = true;
            item.value = 250000;
            item.scale = 1f;
            item.rare = ItemRarityID.LightPurple;
            item.ranged = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 spread = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
            Projectile.NewProjectile(position.X, position.Y - 8f, spread.X, spread.Y, ModContent.ProjectileType<ThunderballProj>(), damage, knockBack, Main.myPlayer);
            return false;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-6, 0);

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PhoenixBlaster);
            recipe.AddIngredient(ItemID.MartianConduitPlating, 33);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}