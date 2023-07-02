using InfectedQualities.Common;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace InfectedQualities.Content.Tiles.Plants
{
    public class ModSapling : ModTile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedJungleBiomes || ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedSnowBiomes;
        }

        public override string Texture => "Terraria/Images/Tiles_20";

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Origin = new(0, 1);
            TileObjectData.newTile.AnchorBottom = new(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.AnchorValidTiles = new[] { -1, -1, -1, -1 };

            if(ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedJungleBiomes)
            {
                TileObjectData.newTile.AnchorValidTiles[0] = ModContent.TileType<HallowedJungleGrass>();
            }
			
			if(ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedSnowBiomes) {
				TileObjectData.newTile.AnchorValidTiles[1] = ModContent.TileType<HallowedSnow>();
				TileObjectData.newTile.AnchorValidTiles[2] = ModContent.TileType<CorruptSnow>();
				TileObjectData.newTile.AnchorValidTiles[3] = ModContent.TileType<CrimsonSnow>();
			}

            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.DrawFlipHorizontal = true;
            TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.newTile.RandomStyleRange = 3;
            TileObjectData.newTile.StyleMultiplier = 3;
            TileObjectData.addTile(Type);

            AddMapEntry(new(151, 107, 75), Language.GetText("MapObject.Sapling"));

            TileID.Sets.TreeSapling[Type] = true;
            TileID.Sets.CommonSapling[Type] = true;
            TileID.Sets.SwaysInWindBasic[Type] = true;
            TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]);

            AdjTiles = new int[] { TileID.Saplings };
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void RandomUpdate(int i, int j)
        {
            if (WorldGen.genRand.NextBool(20))
            {
                if (WorldGen.GrowTree(i, j) && WorldGen.PlayerLOS(i, j))
                {
                    WorldGen.TreeGrowFXCheck(i, j);
                }
            }
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
        {
            if (i % 2 == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
        }
    }
}