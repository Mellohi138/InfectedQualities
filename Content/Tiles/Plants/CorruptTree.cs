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
    public class CorruptTree : ModTree
    {
        public override TreeTypes CountsAsTreeType => TreeTypes.Corrupt;

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
                ModContent.GetInstance<ServerConfig>().EnableInfectedSnowBiomes ? ModContent.TileType<CorruptSnow>() : -1
            };
        }

        public override int DropWood()
        {
            return ItemID.Ebonwood;
        }

        public override int TreeLeaf()
        {
            return GoreID.TreeLeaf_Corruption;
        }

        public override int CreateDust()
        {
            return DustID.Ebonwood;
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 3;
            return ModContent.TileType<ModSapling>();
        }

        public override Asset<Texture2D> GetBranchTextures()
        {
            return TextureAssets.TreeBranch[1];
        }

        public override Asset<Texture2D> GetTexture()
        {
            return null;
        }

        public override Asset<Texture2D> GetTopTextures()
        {
            return TextureAssets.TreeTop[1];
        }

        public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
        {
        }
    }
}
