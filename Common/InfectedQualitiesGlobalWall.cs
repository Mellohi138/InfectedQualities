using InfectedQualities.Helpers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Common
{
    public class InfectedQualitiesGlobalWall : GlobalWall
    {
        public override void SetStaticDefaults()
        {
            if (ModContent.GetInstance<InfectedQualitiesConfig>().EnableLimeSolution)
            {
                WallID.Sets.Conversion.Dirt[WallID.Cave6Unsafe] = true;
                WallID.Sets.Conversion.Dirt[WallID.Cave6Echo] = true;
                WallID.Sets.Conversion.Dirt[WallID.CaveWall] = true;
                WallID.Sets.Conversion.Dirt[WallID.CaveWall2] = true;
                WallID.Sets.Conversion.Dirt[WallID.CaveWall1Echo] = true;
                WallID.Sets.Conversion.Dirt[WallID.CaveWall2Echo] = true;
            }

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                TileUtils.IDSets.WallMud[calamityMod.Find<ModWall>("AstralMudWall").Type] = true;
            }
        }

        public override void RandomUpdate(int i, int j, int type)
        {
            if (type == WallID.HallowedGrassUnsafe)
            {
                if (ModContent.GetInstance<InfectedQualitiesConfig>().EnableInfectedJungleBiomes)
                {
                    if (WorldGen.AllowedToSpreadInfections)
                    {
                        int x = i + WorldGen.genRand.Next(-2, 3);
                        int y = j + WorldGen.genRand.Next(-2, 3);

                        if (Main.tile[x, y].WallType is WallID.Jungle or WallID.JungleUnsafe)
                        {
                            Main.tile[x, y].WallType = WallID.HallowedGrassUnsafe;
                            WorldGen.SquareWallFrame(x, y);
                            if (Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.SendTileSquare(-1, x, y);
                            }
                        }
                    }
                }
            }
        }
    }
}