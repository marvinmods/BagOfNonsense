using Terraria;
using Terraria.ModLoader;

namespace BagOfNonsense.Buffs
{
    public class staffbuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Staff Buff");
            Description.SetDefault("You feel stronger just by holding it.\n" +
                "Defense increased by 8 and life by 75\n" +
                "Life and mana regen increased by 8\n" +
                "Increases max mana by 100 and mana crit by 10%");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 8;
            player.statLifeMax2 += 75;
            player.lifeRegen += 8;
            player.statManaMax2 += 100;
            player.manaRegen += 8;
            player.magicCrit += 10;
            player.noKnockback = true;
        }
    }
}