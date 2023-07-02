using System.ComponentModel;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace InfectedQualities.Common
{
    public class InfectedQualitiesConfig
    {
        public class ServerConfig : ModConfig
        {
            public override LocalizedText DisplayName => Language.GetText("Mods.InfectedQualities.Config.Title.Main.Server");

            public override ConfigScope Mode => ConfigScope.ServerSide;

            [Header("$Mods.InfectedQualities.Config.Title.Content")]
            [LabelKey("$Mods.InfectedQualities.Config.InfectedJungleBiomes.Label"), TooltipKey("$Mods.InfectedQualities.Config.InfectedJungleBiomes.Tooltip")]
            [DefaultValue(true)]
            [ReloadRequired]
            public bool EnableInfectedJungleBiomes;

            [LabelKey("$Mods.InfectedQualities.Config.InfectedSnowBiomes.Label"), TooltipKey("$Mods.InfectedQualities.Config.InfectedSnowBiomes.Tooltip")]
            [DefaultValue(true)]
            [ReloadRequired]
            public bool EnableInfectedSnowBiomes;

            [LabelKey("$Mods.InfectedQualities.Config.LimeSolution.Label"), TooltipKey("$Mods.InfectedQualities.Config.LimeSolution.Tooltip")]
            [DefaultValue(false)]
            [ReloadRequired]
            public bool EnableLimeSolution;

            [LabelKey("$Mods.InfectedQualities.Config.PylonOfNight.Label"), TooltipKey("$Mods.InfectedQualities.Config.PylonOfNight.Tooltip")]
            [DefaultValue(true)]
            [ReloadRequired]
            public bool EnablePylonOfNight;

            [LabelKey("$Mods.InfectedQualities.Config.KeyOfNaught.Label"), TooltipKey("$Mods.InfectedQualities.Config.KeyOfNaught.Tooltip")]
            [DefaultValue(true)]
            [ReloadRequired]
            public bool EnableKeyOfNaught;

            [LabelKey("$Mods.InfectedQualities.Config.DivinePowder.Label"), TooltipKey("$Mods.InfectedQualities.Config.DivinePowder.Tooltip")]
            [DefaultValue(true)]
            [ReloadRequired]
            public bool EnableDivinePowder;

            [Header("$Mods.InfectedQualities.Config.Title.Tweaks")]
            [LabelKey("$Mods.InfectedQualities.Config.HardmodChasmPurification.Label"), TooltipKey("$Mods.InfectedQualities.Config.HardmodChasmPurification.Tooltip")]
            [DefaultValue(true)]
            public bool EnableHardmodeChasmPurification;

            [LabelKey("$Mods.InfectedQualities.Config.RemoveThorns.Label"), TooltipKey("$Mods.InfectedQualities.Config.RemoveThorns.Tooltip")]
            [DefaultValue(false)]
            public bool RemoveThorns;
        }

        public class ClientConfig : ModConfig
        {
            public override LocalizedText DisplayName => Language.GetText("Mods.InfectedQualities.Config.Title.Main.Client");

            public override ConfigScope Mode => ConfigScope.ClientSide;

            [LabelKey("$Mods.InfectedQualities.Config.SmoothSnowIceBlending.Label"), TooltipKey("$Mods.InfectedQualities.Config.SmoothSnowIceBlending.Tooltip")]
            [DefaultValue(false)]
            [ReloadRequired]
            public bool EnableSmoothSnowIceBlending;
        }
    }
}
