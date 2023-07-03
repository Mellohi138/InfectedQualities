using InfectedQualities.Helpers;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Common
{
    public class InfectedQualitiesGlobalWall : GlobalWall
    {
        public override void SetStaticDefaults()
        {
            if (ModContent.GetInstance<ServerConfig>().EnableLimeSolution)
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
    }
}