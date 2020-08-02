using BagOfNonsense.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class Icechinigun : ModItem
    {
        private int GetRandomShoot()
        {
            int bullet = 1;
            switch (Main.rand.Next(3))
            {
                case 0:
                    bullet = ModContent.ProjectileType<IcegunProj1>();
                    break;

                case 1:
                    bullet = ModContent.ProjectileType<IcegunProj2>();
                    break;

                case 2:
                    bullet = ModContent.ProjectileType<IcegunProj3>();
                    break;
            }

            return bullet;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icechinigun");
            Tooltip.SetDefault("40% chance not to consume ammo");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.autoReuse = true;
            item.useAnimation = 6;
            item.useTime = 6;
            item.width = 68;
            item.height = 38;
            item.shoot = GetRandomShoot();
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item11;
            item.damage = 31;
            item.shootSpeed = 14f;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = ItemRarityID.LightPurple;
            item.knockBack = 1.75f;
            item.ranged = true;
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.NextFloat(1f) <= 0.4f) return false;
            return true;
        }

        public override void UpdateInventory(Player player) => item.shoot = GetRandomShoot();

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 spread = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
            Projectile.NewProjectile(position.X, position.Y, spread.X, spread.Y, GetRandomShoot(), damage, knockBack, player.whoAmI);
            return false;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-15, 2);

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddIngredient(ItemID.Minishark);
            recipe.AddIngredient(ItemID.IllegalGunParts, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}