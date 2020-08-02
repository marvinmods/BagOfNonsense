using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class DoomArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doom Arrow");
            Tooltip.SetDefault("Inflicts every debuff know to man and spawns additional arrows on impact.\n" +
                                "Gains velocity while travelling thru blocks, non consumable.\n" +
                                "[c/E5AD00:Hit them and watch them melt]");
        }

        public override void SetDefaults()
        {
            item.shootSpeed = 8.6f;
            item.shoot = mod.ProjectileType("DoomArrow");
            item.damage = 16;
            item.width = 14;
            item.height = 14;
            item.maxStack = 1;
            item.consumable = false;
            item.ammo = AmmoID.Arrow;
            item.knockBack = 7f;
            item.rare = ItemRarityID.Red;
            item.value = 1000;
            item.ranged = true;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodenArrow, 111);
            recipe.AddIngredient(ItemID.FrostburnArrow, 111);
            recipe.AddIngredient(ItemID.FlamingArrow, 111);
            recipe.AddIngredient(ItemID.LunarBar, 30);
            recipe.AddIngredient(ItemID.Stinger, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}