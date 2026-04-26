using Terraria;

namespace InfectedQualities.Utilities;

public static class WorldUtilities
{
    public static bool DetermineZone(this Player player, bool underground)
    {
        bool surfaceLayer = player.ZoneOverworldHeight;
        bool remixSurfaceLayer = player.ZoneRockLayerHeight;
        if(underground)
        {
            Utils.Swap(ref remixSurfaceLayer, ref surfaceLayer);
        }

        if (Main.remixWorld)
        {
            return remixSurfaceLayer;
        }
        return surfaceLayer;
    }

    public static bool MusicUnderground(bool remixFlag)
    {
        if(remixFlag)
        {
            return Main.remixWorld && Main.LocalPlayer.position.Y >= Main.rockLayer * 16.0 + Main.screenHeight / 2;
			}

        return Main.LocalPlayer.position.Y >= Main.worldSurface * 16.0 + Main.screenHeight / 2 && (Main.remixWorld || !WorldGen.oceanDepths((int)(Main.screenPosition.X + Main.screenWidth / 2) / 16, (int)(Main.screenPosition.Y + Main.screenHeight / 2) / 16));
    }
}
