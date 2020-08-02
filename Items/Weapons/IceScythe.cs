using BagOfNonsense.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BagOfNonsense.Items.Weapons
{
    public class IceScythe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sub Zero Scythe");
            Tooltip.SetDefault("It's burning but it's still cold, somehow");
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item71;
            item.value = 750000;
            item.melee = true;
            item.damage = 44;
            item.width = 72;
            item.height = 64;
            item.knockBack = 8f;
            item.useTime = 21;
            item.scale = 1f;
            item.useAnimation = 21;
            item.shoot = ModContent.ProjectileType<ScytheProj>();
            item.shootSpeed = 9f;
            item.rare = ItemRarityID.Cyan;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Oiled, 3900);
            target.AddBuff(BuffID.Frostburn, 3900);
            target.AddBuff(BuffID.Chilled, 900);
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 135);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float rotation = MathHelper.ToRadians(Main.rand.Next(60));
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (2 - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * 6f, perturbedSpeed.Y * 6f, ModContent.ProjectileType<ScytheProj>(), (int)(damage * 1.2), knockBack, player.whoAmI);
            }

            return true;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceSickle);
            recipe.AddIngredient(ItemID.DeathSickle);
            recipe.AddIngredient(ItemID.FrostCore, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}