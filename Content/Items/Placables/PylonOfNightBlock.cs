using InfectedQualities.Content.Tiles;
using InfectedQualities.Core;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Content.Items.Placables;

public class PylonOfNightBlock : ModItem
{
    public override string LocalizationCategory => "Items.Placables";

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<PylonOfNight>());
        Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(gold: 15));
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddRecipeGroup("TeleportationPylons")
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddIngredient(ItemID.DarkShard, 3)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().PylonOfNight;
}