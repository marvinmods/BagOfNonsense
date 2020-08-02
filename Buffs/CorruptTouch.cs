using BagOfNonsense.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace BagOfNonsense.Buffs
{
    public class CorruptTouch : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Corrupt Touch");
            Description.SetDefault("You're rotting away");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<Npc>().corrupttouch = true;
        }
    }
}