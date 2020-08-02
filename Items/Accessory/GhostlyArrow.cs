using BagOfNonsense.Items.Accessory;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static BagOfNonsense.CoolStuff.CoolStuff;

namespace BagOfNonsense
{
    public class Dropchance : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.SkeletonSniper || npc.type == NPCID.SkeletonCommando || npc.type == NPCID.TacticalSkeleton)
                if (Main.rand.Next(100) < 8)
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GhostlyArrow>());
        }
    }
}

namespace BagOfNonsense.Items.Accessory
{
    public class GhostlyArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ghostly arrow");
            Tooltip.SetDefault("This arrow moves on it's own\n" +
                                "15% increased arrow damage and 2% chance to spawn ghost arrows that deal quad damage");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(20, 4));
        }

        public override void SetDefaults()
        {
            item.scale = 1f;
            item.rare = ItemRarityID.Yellow;
            item.width = 24;
            item.height = 24;
            item.value = 30000;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.arrowDamage += 0.15f;
            player.rangedDamage += 0.1f;
            player.GetModPlayer<Arrowspawn>().spawnarrow = true;
        }
    }
}