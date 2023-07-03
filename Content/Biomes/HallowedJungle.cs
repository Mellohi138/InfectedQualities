using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using InfectedQualities.Common;

namespace InfectedQualities.Content.Biomes
{
    public class HallowedJungle : ModBiome
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().EnableInfectedJungleBiomes;
        }

        public override int Music => -1;

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;

        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Jungle;

        public override string BestiaryIcon => "InfectedQualities/Client/Assets/BestiaryIcons/HallowedJungle";

        public override string BackgroundPath => "Terraria/Images/MapBG22";

        public override Color? BackgroundColor => Color.Blue;

        public override bool IsBiomeActive(Player player)
        {
            if(Main.hardMode)
            {
                if (player.ZoneHallow && player.ZoneJungle)
                {
                    return !player.ZoneDungeon && !player.ZoneLihzhardTemple && !player.ZoneGlowshroom && !player.ZoneSkyHeight && !player.ZoneUnderworldHeight;
                }
            }
            return false;
        }
    }
}
