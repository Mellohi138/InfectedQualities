using InfectedQualities.Helpers;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using InfectedQualities.Common;
using Terraria.ID;

namespace InfectedQualities.Content.Biomes
{
    public class CorruptJungle : ModBiome
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedJungleBiomes;
        }

        public override int Music => -1;

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Corrupt;

        public override string BestiaryIcon => "InfectedQualities/Client/Assets/BestiaryIcons/CorruptJungle";

        public override string BackgroundPath => "Terraria/Images/MapBG23";

        public override Color? BackgroundColor => Color.Purple;

        public override int BiomeTorchItemType => ItemID.CursedTorch;

        public override int BiomeCampfireItemType => ItemID.CursedCampfire;

        public override bool IsBiomeActive(Player player)
        {
            if(Main.hardMode)
            {
                if (player.ZoneCorrupt && player.ZoneJungle)
                {
                    return !player.ZoneDungeon && !player.ZoneLihzhardTemple && !player.ZoneGlowshroom && player.ZoneCavern();
                }
            }
            return false;
        }
    }
}
