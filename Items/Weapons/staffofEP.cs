using BagOfNonsense.Buffs;
using BagOfNonsense.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class staffofEP : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Staff of Extreme Prejudice");
            Tooltip.SetDefault("From another world.\n" +
                                "Holding it seems to make you stronger");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.WandofSparking);
            item.width = 34;
            item.height = 34;
            item.damage = 75;
            item.useTime = 24;
            item.useAnimation = 24;
            item.knockBack = 2f;
            item.rare = ItemRarityID.Yellow;
            item.mana = 9;
            item.magic = true;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shootSpeed = 10f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = MathHelper.ToRadians(360);
            float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
            double deltaAngle = spread / 12;
            double offsetAngle;

            for (int j = 0; j < 12; j++)
            {
                offsetAngle = startAngle + deltaAngle * j;
                Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<staffproj>(), damage, knockBack, player.whoAmI);
            }

            return false;
        }

        public override void HoldItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<staffbuff>(), 2, true);
        }

        public override Vector2? HoldoutOffset() => new Vector2(x: -8, y: 1);

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WandofSparking);
            recipe.AddIngredient(ItemID.DarkShard, 4);
            recipe.AddIngredient(ItemID.SoulofNight, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}