using Terraria;

namespace InfectedQualities.Helpers
{
    public static class PlayerHelper
    {
        public static bool ZoneSurface(this Player player)
        {
            if (Main.remixWorld)
            {
                return player.ZoneRockLayerHeight;
            }
            return player.ZoneOverworldHeight;
        }

        public static bool ZoneCavern(this Player player)
        {
            if (Main.remixWorld)
            {
                return player.ZoneOverworldHeight || player.ZoneDirtLayerHeight;
            }
            return player.ZoneRockLayerHeight;
        }
    }
}
