using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Accessory
{
    public class GodlyEmblem : ModItem
    {
        /*public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			equips.Add(EquipType.Back);
			return true;
		}*/

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Emblem");
            Tooltip.SetDefault("33% increased damage and 15% increased crit chance.");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.value = 400000;
            item.rare = ItemRarityID.Red;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += .33f;
            player.magicDamage += .33f;
            player.thrownDamage += .33f;
            player.minionDamage += .33f;
            player.rangedDamage += .33f;
            player.magicCrit += 15;
            player.meleeCrit += 15;
            player.rangedCrit += 15;
            player.magicCrit += 15;
            player.thrownCrit += 15;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 30);
            recipe.AddIngredient(ItemID.LunarOre, 20);
            recipe.AddIngredient(ItemID.DestroyerEmblem);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}