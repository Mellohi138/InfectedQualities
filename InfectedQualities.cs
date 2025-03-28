using InfectedQualities.Content.Biomes;
using InfectedQualities.Content.Tiles;
using InfectedQualities.Content.Tiles.Plants;
using InfectedQualities.Core;
using InfectedQualities.Utilities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace InfectedQualities
{
    public class InfectedQualities : Mod
    {
        public override void Load()
        {
            foreach (InfectionType infectionType in Enum.GetValues(typeof(InfectionType)))
            {
                AddContent(new InfectedSnow(infectionType));
                AddContent(new InfectedSnowTree(infectionType));

                foreach(GemType gemType in Enum.GetValues(typeof(GemType)))
                {
                    AddContent(new InfectedGemstone(infectionType, gemType));
                }

                foreach (MossType mossType in Enum.GetValues(typeof(MossType)))
                {
                    AddContent(new InfectedMoss(infectionType, mossType));
                }
            }
        }

        public override void PostSetupContent() => InfectedQualitiesModSupport.PostSetupContent();

        public override object Call(params object[] args) => args switch
        {
            ["ZoneCorruptJungle", Player player] => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes && player.InModBiome<CorruptJungle>(),
            ["ZoneCrimsonJungle", Player player] => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes && player.InModBiome<CrimsonJungle>(),
            ["ZoneHallowedJungle", Player player] => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes && player.InModBiome<HallowedJungle>(),
            ["SetWallBiomeSightColor", int type, Color color] => delegate() { InfectedQualitiesModSupport.ModWallBiomeSight[type] = color; },
            ["SetDemonAltarBlock", int altar, ushort type] => delegate() { InfectedQualitiesModSupport.AltarToEvilBlock.Add(altar, type); },
            ["SetAltarGoodBlock", Func<bool> condition, ushort type] => delegate () { InfectedQualitiesModSupport.AltarToGoodBlock.Add(condition, type); },
            _ => throw new Exception("You buffoon, you failed to use InfectedQualities.Call")
        };
    }
}