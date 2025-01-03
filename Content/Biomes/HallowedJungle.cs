﻿using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using InfectedQualities.Core;
using Terraria.ID;
using InfectedQualities.Content.Extras;

namespace InfectedQualities.Content.Biomes
{
    public class HallowedJungle : ModBiome
    {
        public override int Music
        {
            get
            {
                if(ModContent.GetInstance<InfectedQualitiesClientConfig>().HallowedJungleMusic && !Main.LocalPlayer.ZoneGraveyard && !Main.LocalPlayer.ZoneMeteor)
                {
                    if(Main.LocalPlayer.MusicUnderground())
                    {
                        if(Main.remixWorld)
                        {
                            return WorldUtilities.OtherworldMusic() ? MusicID.OtherworldlyHallow : MusicID.TheHallow;
                        }
                        return WorldUtilities.OtherworldMusic() ? MusicID.OtherworldlyUGHallow : MusicID.UndergroundHallow;
                    }
                    else if(!Main.raining)
                    {
                        if (WorldUtilities.OtherworldMusic() && Main.dayTime)
                        {
                            return MusicID.OtherworldlyHallow;
                        }

                        if (Main.dayTime)
                        {
                            if (Main.IsItAHappyWindyDay && !Main.remixWorld)
                            {
                                return MusicID.WindyDay;
                            }
                            return MusicID.TheHallow;
                        }
                        else if(InfectedQualitiesModSupport.HallowNightMusic())
                        {
                            return MusicLoader.GetMusicSlot(InfectedQualitiesModSupport.SpiritMod, "Sounds/Music/HallowNight");
                        }
                    }
                }
                return -1;
            }
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;

        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Jungle;

        public override string BestiaryIcon => "InfectedQualities/Content/Extras/BestiaryIcons/Hallowed_Jungle";

        public override string BackgroundPath => "Terraria/Images/MapBG22";

        public override Color? BackgroundColor => Color.Blue;

        public override bool IsBiomeActive(Player player)
        {
            if (Main.hardMode && player.ZoneHallow && player.ZoneJungle && !player.ZoneGlowshroom)
            {
                return !player.ZoneSkyHeight && !player.ZoneUnderworldHeight && !player.ZoneDungeon && !player.ZoneLihzhardTemple;
            }
            return false;
        }

        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes;
    }
}
