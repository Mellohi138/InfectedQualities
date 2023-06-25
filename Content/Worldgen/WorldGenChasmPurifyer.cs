using Terraria;
using Terraria.IO;
using Terraria.ID;
using Terraria.WorldBuilding;
using Terraria.Chat;
using Terraria.Localization;
using InfectedQualities.Helpers;

namespace InfectedQualities.Content.Worldgen
{
    public class WorldGenChasmPurifyer : GenPass
    {
        public WorldGenChasmPurifyer() :
            base("Chasm Purifyer", 1.0f)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            WorldGen.noLiquidCheck = true;
            WorldGen.noTileActions = true;

            int left = 20;
            int right = Main.maxTilesX - 20;
            int top = (int)(Main.worldSurface * 0.35);
            int bottom = Main.UnderworldLayer;

            for (int m = left; m < right; m++)
            {
                for (int n = top; n < bottom; n++)
                {
                    if (!TileID.Sets.Conversion.MushroomGrass[Main.tile[m, n].TileType] && !TileUtils.IDSets.WallMushroom[Main.tile[m, n].WallType])
                    {
                        WorldGen.Convert(m, n, BiomeConversionID.Purity, 0);
                    }
                }
            }

            WorldGen.noLiquidCheck = false;
            WorldGen.noTileActions = false;

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
}
