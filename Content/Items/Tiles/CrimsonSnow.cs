using InfectedQualities.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Content.Items.Tiles
{
    public class CrimsonSnow : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedSnowBiomes;
        }

        public override string Texture => "InfectedQualities/Client/Assets/Items/Tiles/CrimsonSnow";

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Content.Tiles.CrimsonSnow>());

            ItemID.Sets.ExtractinatorMode[Type] = ModContent.ItemType<CorruptSnow>();
        }

        public override void AddRecipes()
        {
            Recipe.Create(ItemID.Snowball, 15)
                .AddIngredient(Type)
                .Register();

            Recipe.Create(ItemID.SnowBrick)
                .AddIngredient(Type, 2)
                .AddTile(TileID.WorkBenches)
                .Register();

            Recipe.Create(ItemID.SnowWallEcho, 4)
                .AddIngredient(Type)
                .AddTile(TileID.WorkBenches)
                .AddCondition(Condition.InGraveyard)
                .Register();

            Recipe.Create(ItemID.SnowFallBlock)
                .AddIngredient(Type)
                .AddIngredient(ItemID.Glass)
                .AddTile(TileID.CrystalBall)
                .Register();
        }
    }
}
