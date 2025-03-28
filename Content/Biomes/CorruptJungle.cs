﻿using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

using InfectedQualities.Core;
using InfectedQualities.Utilities;

namespace InfectedQualities.Content.Biomes
{
    public class CorruptJungle : ModBiome
    {
        public override int Music => -1;

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Corrupt;

        public override string BackgroundPath => "Terraria/Images/MapBG23";

        public override bool IsBiomeActive(Player player)
        {
            if (Main.hardMode && player.ZoneCorrupt && player.ZoneJungle && !player.ZoneGlowshroom)
            {
                return player.ZoneCavern() && !player.ZoneDungeon && !player.ZoneLihzhardTemple;
            }
            return false;
        }

        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes;
    }
}
