using BagOfNonsense.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class Xenobuster : ModItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Xenobuster");

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 17;
            item.useTime = 17;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<XenobusterProj>();
            item.shootSpeed = 16f;
            item.knockBack = 4.5f;
            item.width = 72;
            item.height = 72;
            item.damage = 100;
            item.scale = 1.05f;
            item.UseSound = SoundID.Item1;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.melee = true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(2) != 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    int type = Utils.SelectRandom(Main.rand, new int[2] { 229, 107 });
                    int dusty = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, type, item.direction * 2, 0.0f, 150, default, 1f);
                    var dust = Main.dust[dusty];
                    Vector2 vector2 = Vector2.Multiply(dust.velocity, 0.2f);
                    dust.velocity = vector2;
                    Main.dust[dusty].noGravity = true;
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 mousepos = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            if (Main.player[player.whoAmI].gravDir == -1f)
            {
                mousepos.Y = Main.screenHeight - Main.mouseY + Main.screenPosition.Y;
            }

            Vector2 spawn = player.Center + new Vector2(0, -Main.rand.Next(6, 12));
            Vector2 gotomouse = Vector2.Normalize(mousepos - player.Center);
            float randspawn = Main.rand.NextFloat(6f, 18f);
            float randspeed = Main.rand.NextFloat(1f, 2f);
            Projectile.NewProjectile(spawn.X + randspawn, spawn.Y + randspawn, gotomouse.X * (item.shootSpeed * randspeed), gotomouse.Y * (item.shootSpeed * randspeed), ModContent.ProjectileType<XenobusterProj>(), (int)(damage * 1.5), knockBack, player.whoAmI, 0f, 0f);
            return false;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.InfluxWaver);
            recipe.AddIngredient(ItemID.TerraBlade);
            recipe.AddIngredient(ItemID.FragmentSolar, 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}