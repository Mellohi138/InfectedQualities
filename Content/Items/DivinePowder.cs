using InfectedQualities.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Content.Items
{
    public class DivinePowder : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableDivinePowder;
        }

        public override string Texture => "InfectedQualities/Client/Assets/Items/DivinePowder";

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.PurificationPowder);
            Item.shoot = ModContent.ProjectileType<Projectiles.DivineDust>();
            Item.value = Item.buyPrice(silver: 1, copper : 75);
        }

        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.PurificationPowder).AddIngredient(ItemID.PixieDust, 3).AddTile(TileID.Bottles).Register();
        }
    }
}