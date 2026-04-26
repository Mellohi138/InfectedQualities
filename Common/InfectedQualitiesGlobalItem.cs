using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using InfectedQualities.Content.Tiles;
using InfectedQualities.Core;
using Terraria.Audio;
using Terraria.DataStructures;
using InfectedQualities.Utilities;
using Microsoft.Xna.Framework;

namespace InfectedQualities.Common;

public class HallowedJungleGrassPlanting : GlobalItem
{
    public override bool? UseItem(Item item, Player player)
    {
        if (player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple) && WorldGen.TileType(Player.tileTargetX, Player.tileTargetY) == TileID.Mud)
        {
            Main.tile[Player.tileTargetX, Player.tileTargetY].TileType = (ushort)ModContent.TileType<HallowedJungleGrass>();
            WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY);
            SoundEngine.PlaySound(SoundID.Dig, new Vector2(Player.tileTargetX, Player.tileTargetY) * 16);
            player.ConsumeItem(item.type);
            return true;
        }
        return null;
    }

    public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.HallowedSeeds;

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes;
}

public class InfectedMossPlanting : GlobalItem
{
    public override bool? UseItem(Item item, Player player)
    {
        MossType? mossType = item.type switch
        {
            ItemID.GreenMoss => MossType.Green,
            ItemID.BrownMoss => MossType.Brown,
            ItemID.RedMoss => MossType.Red,
            ItemID.BlueMoss => MossType.Blue,
            ItemID.PurpleMoss => MossType.Purple,
            ItemID.LavaMoss => MossType.Lava,
            ItemID.XenonMoss => MossType.Xenon,
            ItemID.ArgonMoss => MossType.Argon,
            ItemID.VioletMoss => MossType.Neon,
            ItemID.RainbowMoss => MossType.Helium,
            _ => null
        };

        if (mossType.HasValue && player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple))
        {
            InfectionType? stoneInfectionType = WorldGen.TileType(Player.tileTargetX, Player.tileTargetY) switch
            {
                TileID.Ebonstone => InfectionType.Corrupt,
                TileID.Crimstone => InfectionType.Crimson,
                TileID.Pearlstone => InfectionType.Hallowed,
                _ => null
            };

            if (stoneInfectionType.HasValue)
            {
                Main.tile[Player.tileTargetX, Player.tileTargetY].TileType = TileUtilities.GetMossType(stoneInfectionType.Value, mossType.Value);
                WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY);
                SoundEngine.PlaySound(SoundID.Dig, new Vector2(Player.tileTargetX, Player.tileTargetY) * 16);
                player.ConsumeItem(item.type);
                return true;
            }
        }
        return null;
    }

    public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type is ItemID.GreenMoss or ItemID.BrownMoss or ItemID.RedMoss or ItemID.BlueMoss or ItemID.PurpleMoss or ItemID.LavaMoss or ItemID.XenonMoss or ItemID.ArgonMoss or ItemID.VioletMoss or ItemID.RainbowMoss;

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedMosses;
}
