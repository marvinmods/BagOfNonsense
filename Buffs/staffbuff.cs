using Terraria;
using Terraria.ModLoader;

namespace BagOfNonsense.Buffs
{
    public class staffbuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Staff Buff");
            Description.SetDefault("You feel stronger just by holding it.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 8;
            player.statLifeMax2 += 75;
            player.lifeRegen += 6;
            player.statManaMax2 += 100;
            player.manaRegen += 8;
            player.magicCrit += 10;
            player.noKnockback = true;
        }
    }
}