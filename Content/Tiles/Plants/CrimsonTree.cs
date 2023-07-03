using Terraria;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Enums;
using InfectedQualities.Common;

namespace InfectedQualities.Content.Tiles.Plants
{
    public class CrimsonTree : ModTree
    {
        public override TreeTypes CountsAsTreeType => TreeTypes.Crimson;

        public override TreePaintingSettings TreeShaderSettings => new()
        {
            UseSpecialGroups = true,
            SpecialGroupMinimalHueValue = 11f / 72f,
            SpecialGroupMaximumHueValue = 0.25f,
            SpecialGroupMinimumSaturationValue = 0.88f,
            SpecialGroupMaximumSaturationValue = 1f
        };

        public override void SetStaticDefaults()
        {
            GrowsOnTileId = new int[]
            {
                ModContent.GetInstance<ServerConfig>().EnableInfectedSnowBiomes ? ModContent.TileType<CrimsonSnow>() : -1
            };
        }

        public override int DropWood()
        {
            return ItemID.Shadewood;
        }

        public override int TreeLeaf()
        {
            return GoreID.TreeLeaf_Crimson;
        }

        public override int CreateDust()
        {
            return DustID.Shadewood;
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 4;
            return ModContent.TileType<ModSapling>();
        }

        public override Asset<Texture2D> GetBranchTextures()
        {
            return TextureAssets.TreeBranch[5];
        }

        public override Asset<Texture2D> GetTexture()
        {
            return null;
        }

        public override Asset<Texture2D> GetTopTextures()
        {
            return TextureAssets.TreeTop[5];
        }

        public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
        {
        }
    }
}
