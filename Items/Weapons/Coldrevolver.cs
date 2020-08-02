using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class Coldrevolver : ModItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Chilled magnum");

        public override void SetDefaults()
        {
            item.autoReuse = false;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 30;
            item.useTime = 30;
            item.width = 48;
            item.height = 28;
            item.shoot = ProjectileID.Bullet;
            item.knockBack = 3f;
            item.useAmmo = AmmoID.Bullet;
            item.damage = 28;
            item.shootSpeed = 20f;
            item.noMelee = true;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.scale = 0.85f;
            item.rare = ItemRarityID.Pink;
            item.ranged = true;
            item.crit = 18;
        }

        public override void UpdateInventory(Player player)
        {
            if (Main.rand.Next(2) != 0)
            {
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/357_shot1");
            }
            else
            {
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/357_shot2");
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 12f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            if (type == ProjectileID.Bullet)
            {
                type = ProjectileID.Blizzard;
                damage = (int)(damage * 1.2f);
            }

            Vector2 spread = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(2));
            Projectile.NewProjectile(position.X, position.Y - 8f, (float)(spread.X * 1.66), (float)(spread.Y * 1.66), type, damage, knockBack, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Revolver);
            recipe.AddIngredient(ItemID.IceBlock, 30);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ModContent.ItemType<Coldrevolver>());
            recipe.AddRecipe();

            var recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(ItemID.IronBar, 10);
            recipe1.AddIngredient(ItemID.IllegalGunParts, 5);
            recipe1.AddTile(TileID.Anvils);
            recipe1.SetResult(ItemID.Revolver);
            recipe1.AddRecipe();
        }
    }
}