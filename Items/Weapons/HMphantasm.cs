using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class HMphantasm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phantasmal");
            Tooltip.SetDefault("33% chance not to consume ammo");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Phantasm); // copy defaults from an item, (basic values like damage only)
            item.noUseGraphic = false;
            item.damage = 20;
            item.crit = 14;
            item.useTime = 18;
            item.useAnimation = 18;
            item.channel = true;
            item.noMelee = true;
            item.glowMask = 200;
            item.rare = ItemRarityID.LightPurple;
        }

        public override void HoldItem(Player player) => player.phantasmTime = 2;

        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.NextFloat(3) != 1)
            {
                return false;
            }

            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 weirdtest = new Vector2(speedX, speedY);
            Vector2 playercenter = player.RotatedRelativePoint(player.MountedCenter, true);
            for (int i = 0; i < 4; i++)
            {
                float speed = 2.4f * Main.rand.NextFloat(0.5f, 1.2f);
                Vector2 projectilev = Vector2.Multiply(Vector2.Multiply(weirdtest, speed), (float)(0.6 + Main.rand.NextFloat() * 0.8));
                if (float.IsNaN(projectilev.X) || float.IsNaN(projectilev.Y))
                    projectilev = Vector2.Negate(Vector2.UnitY);
                Vector2 PlCeRrandomized = Vector2.Add(playercenter, Utils.RandomVector2(Main.rand, -15f, 15f));
                int shooty = Projectile.NewProjectile(PlCeRrandomized.X, PlCeRrandomized.Y, projectilev.X, projectilev.Y, type, damage, knockBack, player.whoAmI, (float)(5 * Main.rand.Next(0, 20)), 0.0f);
                Main.projectile[shooty].noDropItem = true;
            }

            return false;
        }

        public override Vector2? HoldoutOffset() => new Vector2(x: -5, y: 0);

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}