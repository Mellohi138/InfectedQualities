using InfectedQualities.Common;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;

namespace InfectedQualities.Content.Tiles.TileEntities
{
    public class PylonTileEntity : TEModdedPylon
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig>().EnablePylonOfNight;
        }
    }
}
