using BagOfNonsense.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class Tsuedalustasm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tsuedalustasm");
            Tooltip.SetDefault("[c/10A5E5:Defeating you like this has no meaning.]\n" +
                                "Does not consume ammo and ignores enemy armor");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Phantasm);
            item.damage = 50;
            item.rare = ItemRarityID.Red;
            item.noUseGraphic = false;
            item.shoot = ModContent.ProjectileType<TsuedalustasmProj>();
            item.value = Item.sellPrice(0, 50, 0, 0);
        }

        public override void HoldItem(Player player)
        {
            player.armorPenetration += 25000;
            player.phantasmTime = 2;
        }

        public override bool ConsumeAmmo(Player player) => false;

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = Main.mouseX + Main.screenPosition.X - position.X;
            float num79 = Main.mouseY + Main.screenPosition.Y - position.Y;
            int num73 = player.GetWeaponDamage(player.inventory[player.selectedItem]);
            float num74 = player.inventory[player.selectedItem].knockBack;
            int bow = Projectile.NewProjectile(position.X, position.Y, num78, num79, ModContent.ProjectileType<TsuedalustasmProj>(), num73, num74, player.whoAmI, 0f, 0f);
            int gun = Projectile.NewProjectile(vector2.X, vector2.Y, num78, num79, ModContent.ProjectileType<PhantasmalGun>(), num73, num74, player.whoAmI, (float)(5 * Main.rand.Next(0, 10)), 0f);
            Main.projectile[bow].noDropItem = true;
            Main.projectile[gun].alpha = 255;
            return true;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-5, 0);

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Phantasm);
            recipe.AddIngredient(ItemID.DaedalusStormbow);
            recipe.AddIngredient(ItemID.Tsunami);
            recipe.AddIngredient(ItemID.LunarBar, 40);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}