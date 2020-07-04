using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Accessory
{
    public class SpookyQuiver : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spooky Quiver");
            Tooltip.SetDefault("Wearing this on your back might hurt!\n" +
                "20% increased arrow damage and speed, 3% chance to spawn ghostly arrows that deal quad damage\n" +
                "Turns the holder into a werewolf at night and a merfolk when entering water\n" +
                "Mild increase to damage, melee speed, critical strike chance,\n" +
                "life regeneration, defense, mining speed, and minion knockback");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(9, 3));
        }

        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Red;
            item.width = 30;
            item.height = 30;
            item.value = 300000;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (hideVisual)
            {
                player.hideMerman = true;
                player.hideWolf = true;
            }

            player.magicQuiver = true;
            player.accMerman = true;
            player.wolfAcc = true;
            player.lifeRegen += 4;
            player.statDefense += 8;
            player.meleeSpeed += 0.2f;
            player.meleeDamage += 0.2f;
            player.meleeCrit += 5;
            player.rangedDamage += 0.2f;
            player.rangedCrit += 5;
            player.magicDamage += 0.2f;
            player.magicCrit += 5;
            player.pickSpeed -= 0.25f;
            player.minionDamage += 0.2f;
            player.minionKB += 1.5f;
            player.thrownDamage += 0.2f;
            player.thrownCrit += 5;
            player.statLifeMax2 += 25;
            player.GetModPlayer<Arrowspawn>().spawnarrowquiver = true;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CelestialShell);
            recipe.AddIngredient(ItemID.MagicQuiver);
            recipe.AddIngredient(ModContent.ItemType<GhostlyArrow>());
            recipe.AddIngredient(ItemID.FragmentVortex, 15);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}