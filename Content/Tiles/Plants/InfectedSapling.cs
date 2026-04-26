using InfectedQualities.Core;
using InfectedQualities.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace InfectedQualities.Content.Tiles.Plants;

public class InfectedSapling : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;

        TileID.Sets.TreeSapling[Type] = true;
        TileID.Sets.CommonSapling[Type] = true;
        TileID.Sets.SwaysInWindBasic[Type] = true;
        TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]);

        TileObjectData.newTile.FullCopyFrom(TileObjectData.GetTileData(TileID.Saplings, 0));
        TileObjectData.newTile.AnchorValidTiles = [TileUtilities.GetSnowType(InfectionType.Corrupt), TileUtilities.GetSnowType(InfectionType.Crimson), TileUtilities.GetSnowType(InfectionType.Hallowed), ModContent.TileType<HallowedJungleGrass>()];
        TileObjectData.addTile(Type);
        
        AdjTiles = [TileID.Saplings];
        DustType = DustID.WoodFurniture;
        AddMapEntry(new(151, 107, 75), Language.GetText("MapObject.Sapling"));

        VanillaFallbackOnModDeletion = TileID.Saplings;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 3 : 10;

    public override bool CreateDust(int i, int j, ref int type)
    {
        switch (Main.tile[i, j].TileFrameX / 54)
        {
            case 3:
                type = DustID.Ebonwood;
                break;
            case 4:
                type = DustID.Shadewood_Tree;
                break;
            case 5:
                type = DustID.Pearlwood;
                break;
        }
        return true;
    }

    public override void RandomUpdate(int i, int j)
    {
        bool underground = j > (int)Main.worldSurface - 1;
        int rand = underground ? 5 : 20;
        if (WorldGen.genRand.NextBool(rand))
        {
            TileUtilities.TryToGrowTree(i, j, underground);
        }
    }

    public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
    {
        if (i % 2 == 1)
        {
            effects = SpriteEffects.FlipHorizontally;
        }
    }

    public override string Texture => $"Terraria/Images/Tiles_{TileID.Saplings}";

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes;
}