using BagOfNonsense.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class CorruptSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupstation");
            Tooltip.SetDefault("It wiggles on your hand\n" +
                                 "Ignores enemy defense");
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shootSpeed = 14f;
            item.shoot = ModContent.ProjectileType<CorruptSpearProj>();
            item.damage = 81;
            item.width = 78;
            item.height = 78;
            item.UseSound = SoundID.Item39;
            item.useAnimation = 14;
            item.useTime = 14;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.consumable = false;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.knockBack = 5f;
            item.melee = true;
            item.rare = ItemRarityID.Cyan;
        }

        public override void HoldItem(Player player)
        {
            player.armorPenetration = player.GetWeaponDamage(player.inventory[player.selectedItem]) * 60;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < (1 + Main.rand.Next(5)); i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(9));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<CorruptSpearProj>(), damage, knockBack, player.whoAmI);
            }

            return false;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ScourgeoftheCorruptor);
            recipe.AddIngredient(ItemID.BoneJavelin, 40);
            recipe.AddIngredient(ItemID.FragmentSolar, 12);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.SetResult(this);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.AddRecipe();
        }
    }
}