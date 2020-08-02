using BagOfNonsense.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace BagOfNonsense.NPCs
{
    public class Npc : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool highwattage;
        public bool corrupttouch;

        public override void ResetEffects(NPC npc)
        {
            highwattage = false;
            corrupttouch = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (corrupttouch == true)
            {
                if (npc.lifeRegen > 0) npc.lifeRegen = 0;

                int corrupttouch = 0;
                for (int i = 0; i < 1000; i++)
                {
                    var p = Main.projectile[i];
                    if (p.active && p.type == ModContent.ProjectileType<CorruptSpearProj>() && p.ai[0] == 1f && p.ai[1] == npc.whoAmI)
                    {
                        corrupttouch++;
                    }
                }

                npc.lifeRegen -= corrupttouch * 14 * 14;

                if (damage < corrupttouch * 3)
                {
                    damage = corrupttouch * 24 + 20;
                }
            }

            if (highwattage == true)
            {
                if (npc.lifeRegen > 0) npc.lifeRegen = 0;

                npc.lifeRegen -= 300;
                if (damage < 20) damage = 20;
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (corrupttouch == true)
            {
                if (Main.rand.Next(2) < 1)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 16, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 3.8f;
                    Main.dust[dust].velocity.Y -= 0.87f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
            }

            if (highwattage == true)
            {
                if (Main.rand.Next(2) < 1)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 229, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }

                Lighting.AddLight(npc.position, 0.65f, 0.94f, 1f);
            }
        }
    }
}