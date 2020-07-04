using BagOfNonsense.Items.Accessory;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense
{
    public class Arrowspawn : ModPlayer
    {
        public bool spawnarrow;
        public bool spawnarrowquiver;

        public override void ResetEffects()
        {
            spawnarrow = false;
            spawnarrowquiver = false;
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (proj.arrow && spawnarrow && !proj.noEnchantments && !(proj.type == ProjectileID.PhantasmArrow) && !(proj.type == mod.ProjectileType("DoomArrowEX1")) && !(proj.type == mod.ProjectileType("DoomArrowEX2")) && !(proj.type == mod.ProjectileType("DoomArrowEX3")) && !(proj.type == mod.ProjectileType("DoomArrowEX4")))
            {
                float chance = Main.rand.NextFloat(1f);
                if (chance < 0.02f)
                {
                    Vector2 vector2 = Vector2.Multiply(Main.npc[target.whoAmI].DirectionFrom(Main.player[player.whoAmI].position), 6f);
                    int damage3x = player.GetWeaponDamage(player.inventory[player.selectedItem]) * 4;
                    if (damage3x < 20f)
                    {
                        damage3x = 80;
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        int rand = Main.rand.Next(2, 6);
                        Vector2 playerpos = Vector2.Add(Main.player[player.whoAmI].position, Vector2.Multiply(Main.player[player.whoAmI].Size, Utils.RandomVector2(Main.rand, 0.0f, 1f)));
                        Projectile.NewProjectile(playerpos.X, playerpos.Y, vector2.X * rand, vector2.Y * rand, mod.ProjectileType("GhostlyArrowproj"), damage3x, 0.0f, player.whoAmI, target.whoAmI, 0.0f);
                    }
                }
            }

            if (proj.arrow && spawnarrowquiver && !proj.noEnchantments && !(proj.type == ProjectileID.PhantasmArrow) && !(proj.type == mod.ProjectileType("DoomArrowEX1")) && !(proj.type == mod.ProjectileType("DoomArrowEX2")) && !(proj.type == mod.ProjectileType("DoomArrowEX3")) && !(proj.type == mod.ProjectileType("DoomArrowEX4")))
            {
                float chance = Main.rand.NextFloat(1f);
                if (chance < 0.03f)
                {
                    Vector2 vector2 = Vector2.Multiply(Main.npc[target.whoAmI].DirectionFrom(Main.player[player.whoAmI].position), 6f);
                    int damage3x = player.GetWeaponDamage(player.inventory[player.selectedItem]) * 5;
                    if (damage3x < 20f)
                    {
                        damage3x = 80;
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        float rand = Main.rand.NextFloat(1.2f, 3f);
                        Vector2 playerpos = Vector2.Add(Main.player[player.whoAmI].position, Vector2.Multiply(Main.player[player.whoAmI].Size * 2, Utils.RandomVector2(Main.rand, 0.0f, 1f)));
                        Projectile.NewProjectile(playerpos.X, playerpos.Y, vector2.X * rand, vector2.Y * rand, mod.ProjectileType("GhostlyArrowproj"), damage3x, 0.0f, player.whoAmI, target.whoAmI, 0.0f);
                    }
                }
            }
        }
    }

    public class Dropchance : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.SkeletonSniper || npc.type == NPCID.SkeletonCommando || npc.type == NPCID.TacticalSkeleton)
            {
                if (Main.rand.Next(100) < 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GhostlyArrow>());
                }
            }
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