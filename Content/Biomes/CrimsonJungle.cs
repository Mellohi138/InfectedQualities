using InfectedQualities.Helpers;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using InfectedQualities.Common;
using Terraria.ID;

namespace InfectedQualities.Content.Biomes
{
    public class CrimsonJungle : ModBiome
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig>().EnableInfectedJungleBiomes;
        }

        public override int Music => -1;

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Crimson;

        public override string BestiaryIcon => "InfectedQualities/Client/Assets/BestiaryIcons/CrimsonJungle";

        public override string BackgroundPath => "Terraria/Images/MapBG24";

        public override Color? BackgroundColor => Color.Red;

        public override int BiomeTorchItemType => ItemID.IchorTorch;

        public override int BiomeCampfireItemType => ItemID.IchorCampfire;

        public override bool IsBiomeActive(Player player)
        {
            if (Main.hardMode)
            {
                if (player.ZoneCrimson && player.ZoneJungle)
                {
                    return !player.ZoneDungeon && !player.ZoneLihzhardTemple && !player.ZoneGlowshroom && player.ZoneCavern();
                }
            }
            return false;
        }
    }
}
