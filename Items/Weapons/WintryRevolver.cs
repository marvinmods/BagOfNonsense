using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class WintryRevolver : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wintry Handcannon");
            Tooltip.SetDefault("Has a hidden barrel\n" +
                "Pairs well with musket bullets\n" +
                "5% chance to shoot a rocket!!");
        }

        public override void SetDefaults()
        {
            item.autoReuse = false;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 28;
            item.useTime = 28;
            item.width = 72;
            item.height = 44;
            item.shoot = ProjectileID.Bullet;
            item.knockBack = 6f;
            item.useAmmo = AmmoID.Bullet;
            item.damage = 49;
            item.shootSpeed = 20f;
            item.noMelee = true;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.scale = 0.7f;
            item.rare = ItemRarityID.Pink;
            item.ranged = true;
            item.crit = 9;
        }

        public override void UpdateInventory(Player player)
        {
            if (Main.rand.Next(2) != 0)
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/357_shot1");
            else
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/357_shot2");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 12f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;

            if (type == ProjectileID.Bullet)
            {
                type = ProjectileID.Blizzard;
                damage = (int)(damage * 1.2f);
            }

            Projectile.NewProjectile(position.X, position.Y - 8f, speedX, speedY, type, damage, knockBack, player.whoAmI);
            Vector2 spread = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4));
            Projectile.NewProjectile(position.X, position.Y - 8f, (float)(spread.X * 2.5), (float)(spread.Y * 2.5), ProjectileID.Blizzard, (int)(damage * 1.25), (int)(knockBack * 1.55), player.whoAmI);
            if (Main.rand.NextFloat(1f) <= 0.05f)
                Projectile.NewProjectile(position.X, position.Y - 8f, speedX, speedY, ProjectileID.RocketI, (int)(damage * 1.5), knockBack, player.whoAmI);
            return false;
        }

        public override Vector2? HoldoutOffset() => new Vector2(0, 0);

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostCore, 2);
            recipe.AddIngredient(ModContent.ItemType<Coldrevolver>());
            recipe.AddIngredient(ItemID.IllegalGunParts, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}