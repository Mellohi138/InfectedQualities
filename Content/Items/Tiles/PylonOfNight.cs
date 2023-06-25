using InfectedQualities.Common;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Content.Items.Tiles
{
    public class PylonOfNight : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig>().EnablePylonOfNight;
        }

        public override string Texture => "InfectedQualities/Client/Assets/Items/Tiles/PylonOfNight";

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Content.Tiles.PylonOfNight>());

            Item.SetShopValues(ItemRarityColor.Blue1, Terraria.Item.buyPrice(gold: 15));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TeleportationPylonHallow)
                .AddIngredient(ItemID.SoulofNight, 10)
                .AddIngredient(ItemID.DarkShard, 3)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}