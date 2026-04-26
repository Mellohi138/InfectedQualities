using Terraria;
using Terraria.IO;
using Terraria.ID;
using Terraria.WorldBuilding;
using Terraria.Chat;
using Terraria.Localization;
using InfectedQualities.Core;

namespace InfectedQualities.Content.Worldgen;

public class WorldGenChasmPurifyer : GenPass
{
    public WorldGenChasmPurifyer() : base("Chasm Purifyer", 1.0f) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        int left = 45;
        int right = Main.maxTilesX - left;
        int top = (int)(Main.worldSurface * 0.35);
        int bottom = Main.UnderworldLayer;

        for (int m = left; m < right; m++)
        {
            for (int n = top; n < bottom; n++)
            {
                if (TileID.Sets.Corrupt[Main.tile[m, n].TileType] || TileID.Sets.Crimson[Main.tile[m, n].TileType] || InfectedQualitiesModSupport.IsAltEvilBlock(m, n, true))
                {
                    WorldGen.Convert(m, n, BiomeConversionID.Purity, 0, walls: false);
                }

                if (WallID.Sets.Corrupt[Main.tile[m, n].WallType] || WallID.Sets.Crimson[Main.tile[m, n].WallType] || InfectedQualitiesModSupport.IsAltEvilBlock(m, n, false))
                {
                    WorldGen.Convert(m, n, BiomeConversionID.Purity, 0, tiles: false);
                }
            }
        }

        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText(Language.GetTextValue("Mods.InfectedQualities.Misc.ChasmPurifyMessage"), 180, 180);
        }
        else if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Language.GetTextValue("Mods.InfectedQualities.Misc.ChasmPurifyMessage")), new(180, 180, 255));
        }
    }
}
