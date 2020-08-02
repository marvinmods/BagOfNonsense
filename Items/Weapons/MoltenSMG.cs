using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class MoltenSMG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Molten SMG");
            Tooltip.SetDefault("57% chance not to consume ammo");
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.knockBack = 2f;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 16;
            item.useTime = 3;
            item.reuseDelay = 15;
            item.width = 62;
            item.height = 32;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item31;
            item.damage = 16;
            item.shootSpeed = 14.5f;
            item.noMelee = true;
            item.value = 450000;
            item.rare = ItemRarityID.LightRed;
            item.ranged = true;
            item.scale = 0.8f;
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.NextFloat(1f) < 0.57f) return false;
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.rand.NextFloat(1f) <= 0.1f)
            {
                type = ProjectileID.BallofFire;
                damage = (int)(damage * 1.66);
            }

            Vector2 spread = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4));
            Projectile.NewProjectile(position.X, position.Y, spread.X, spread.Y, type, damage, knockBack, player.whoAmI);
            return false;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-12, 0);

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.AddIngredient(ModContent.ItemType<MiniatureSMG>());
            recipe.AddTile(TileID.Hellforge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}