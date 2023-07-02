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
    public class HallowedTree : ModTree
    {
        public override TreeTypes CountsAsTreeType => TreeTypes.Hallowed;

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
                ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedJungleBiomes ? ModContent.TileType<HallowedJungleGrass>() : -1,
                ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedSnowBiomes ? ModContent.TileType<HallowedSnow>() : -1
            };
        }

        public override int DropWood()
        {
            return ItemID.Pearlwood;
        }

        public override int TreeLeaf()
        {
            return GoreID.TreeLeaf_Hallow;
        }

        public override int CreateDust()
        {
            return DustID.t_PearlWood;
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 5;
            return ModContent.TileType<ModSapling>();
        }

        public override Asset<Texture2D> GetBranchTextures()
        {
            return TextureAssets.TreeBranch[20];
        }

        public override Asset<Texture2D> GetTexture()
        {
            return null;
        }

        public override Asset<Texture2D> GetTopTextures()
        {
            return TextureAssets.TreeTop[20];
        }

        public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
        {
            topTextureFrameWidth = 80;
            topTextureFrameHeight = 140;

            treeFrame += floorY % 16;
        }
    }
}
