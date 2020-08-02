using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense
{
    public class Miniondebuff : ModPlayer
    {
        public bool DebuffOnHit;

        public override void ResetEffects() => DebuffOnHit = false;

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if ((proj.minion || ProjectileID.Sets.MinionShot[proj.type]) && DebuffOnHit && !proj.noEnchantments)
            {
                target.AddBuff(BuffID.ShadowFlame, 60 + Main.rand.Next(50, 150));
                target.AddBuff(BuffID.BetsysCurse, 60 + Main.rand.Next(80, 240));
                target.AddBuff(BuffID.Oiled, 60 + Main.rand.Next(100, 600));
                target.AddBuff(BuffID.CursedInferno, 60 + Main.rand.Next(100, 600));
                target.AddBuff(BuffID.Frostburn, 60 + Main.rand.Next(300, 600));
            }
        }
    }
}

namespace BagOfNonsense.Items.Accessory
{
    public class TikiLordEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tiki Lord Emblem");
            Tooltip.SetDefault("Increases your max number of minions by 6\n" +
                "45% increased minion damage and minions apply a variety of debuffs\n" +
                "Permanent summoning buffs\n" +
                "The ultimate summoning\n");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 100000;
            item.rare = ItemRarityID.Red;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 6;
            player.minionDamage += 0.45f;
            player.AddBuff(BuffID.Summoning, 2, true);
            player.AddBuff(BuffID.Bewitched, 2, true);
            player.GetModPlayer<Miniondebuff>().DebuffOnHit = true;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PygmyNecklace);
            recipe.AddIngredient(ItemID.PapyrusScarab);
            recipe.AddIngredient(ItemID.FragmentStardust, 20);
            recipe.AddTile(TileID.BewitchingTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}