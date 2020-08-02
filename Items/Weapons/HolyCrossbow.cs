using BagOfNonsense.Projectiles;
using Microsoft.Xna.Framework;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class HolyCrossbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Holy Crossbow");
            Tooltip.SetDefault("Unleashes tiny crosses that homes in on enemies");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 18;
            item.useTime = 18;
            item.width = 94;
            item.height = 30;
            item.shoot = 10;
            item.useAmmo = AmmoID.Arrow;
            item.UseSound = SoundID.Item5;
            item.damage = 40;
            item.shootSpeed = 14f;
            item.knockBack = 6f;
            item.rare = ItemRarityID.LightPurple;
            item.noMelee = true;
            item.value = 108000;
            item.ranged = true;
            item.autoReuse = true;
        }

        public override void HoldItem(Player player)
        {
            player.armorPenetration += 6000;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Vector2 spread = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(2));
            speedX = spread.X;
            speedY = spread.Y;
            if (type == ProjectileID.WoodenArrowFriendly)
                type = ModContent.ProjectileType<CrossbowBolt>();
            return true;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-20, 0);

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BeesKnees);
            recipe.AddIngredient(ItemID.HallowedBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}