using BagOfNonsense.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace BagOfNonsense.Buffs
{
    public class Highwattage : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("High wattage");
            Description.SetDefault("You're overloaded");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<Npc>().highwattage = true;
        }
    }
}