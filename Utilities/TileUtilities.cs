using InfectedQualities.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Utilities;

public static class TileUtilities
{
    public static void TileMerge(ushort tileFrom, ushort tileTo)
    {
        Main.tileMerge[tileFrom][tileTo] = true;
        Main.tileMerge[tileTo][tileFrom] = true;
    }

    public static bool TileExposedToLava(int i, int j)
    {
        for (int x = i - 1; x <= i + 1; x++)
        {
            for (int y = j - 1; y <= j + 1; y++)
            {
                if(!WorldGen.InWorld(x, y, 10))
                {
                    return false;
                }

                if (Main.tile[x, y].LiquidType == LiquidID.Lava && Main.tile[x, y].LiquidAmount != 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static void AttemptToPlaceInfectedPlant(int i, int j, int type, ushort vanillaVariant, int range)
    {
        if (j > Main.rockLayer && WorldGen.genRand.NextBool(25) && Main.hardMode && Main.tile[i, j].LiquidAmount == 0)
        {
            bool flag = true;
            for (int m = i - range; m < i + range; m += 2)
            {
                for (int n = j - range; n < j + range; n += 2)
                {
                    if (!WorldGen.InWorld(m, n) || (Main.tile[m, n].HasTile && (Main.tile[m, n].TileType == type || Main.tile[m, n].TileType == vanillaVariant)))
                    {
                        flag = false;
                        break;
                    }
                }
            }

            if (flag && WorldGen.PlaceObject(i, j - 1, type))
            {
                WorldGen.SquareTileFrame(i, j - 1);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendTileSquare(-1, i, j - 1, 5);
                }
            }
        }
    }

    public static void GetTopLeft(int i, int j, out int x, out int y, out short num)
    {
        num = -1;
        x = Main.tile[i, j].TileFrameX / 18;
        while (x > 1)
        {
            x -= 2;
        }
        x = i - x;

        y = Main.tile[i, j].TileFrameY / 18;
        while (y > 1)
        {
            y -= 2;
        }
        y = j - y;

        for (int k = x; k < x + 2; k++)
        {
            if (Main.tile[k, y + 2].TileType == TileID.CorruptJungleGrass)
            {
                num = 0;
            }
            else if (Main.tile[k, y + 2].TileType == TileID.CrimsonJungleGrass)
            {
                num = 36;
            }
            else if (Main.tile[k, y + 2].TileType == ModContent.TileType<HallowedJungleGrass>())
            {
                num = 72;
            }
        }
    }

    public static void TryToGrowTree(int i, int j, bool underground)
    {
        if(Main.netMode != NetmodeID.MultiplayerClient)
        {
            if (underground)
            {
                return;
            }

            if (WorldGen.GrowTree(i, j) && WorldGen.PlayerLOS(i, j))
            {
                WorldGen.TreeGrowFXCheck(i, j);
            }
        }
    }

    public static ushort GetMossType(InfectionType? infectionType, MossType mossType, bool mossBrick = false)
    {
        if (!infectionType.HasValue)
        {
            if (mossBrick)
            {
                if (mossType == MossType.Helium)
                {
                    return TileID.RainbowMossBrick;
                }
                else if (mossType == MossType.Neon)
                {
                    return TileID.VioletMossBrick;
                }
                return (ushort)TileID.Search.GetId(mossType.ToString() + "MossBrick");
            }

            if (mossType == MossType.Helium)
            {
                return TileID.RainbowMoss;
            }
            else if (mossType == MossType.Neon)
            {
                return TileID.VioletMoss;
            }
            return (ushort)TileID.Search.GetId(mossType.ToString() + "Moss");
        }

        return ModContent.GetInstance<InfectedQualities>().Find<ModTile>(infectionType.ToString() + mossType.ToString() + "Moss").Type;
    }

    public static ushort GetGemstoneType(InfectionType? infectionType, GemType gemType)
    {
        if (!infectionType.HasValue)
        {
            if (gemType == GemType.Amber)
            {
                return TileID.AmberStoneBlock;
            }
            return (ushort)TileID.Search.GetId(gemType.ToString());
        }
        return ModContent.GetInstance<InfectedQualities>().Find<ModTile>(infectionType.ToString() + gemType.ToString() + "Gemstone").Type;
    }

    public static ushort GetSnowType(InfectionType infectionType) => ModContent.GetInstance<InfectedQualities>().Find<ModTile>(infectionType.ToString() + "Snow").Type;

    public static int ToConversionID(this InfectionType type) => type switch
    {
        InfectionType.Corrupt => BiomeConversionID.Corruption,
        InfectionType.Crimson => BiomeConversionID.Crimson,
        InfectionType.Hallowed => BiomeConversionID.Hallow,
        _ => BiomeConversionID.Purity
    };
}

public enum InfectionType
{
    Corrupt,
    Crimson,
    Hallowed
}

public enum MossType
{
    Green,
    Brown,
    Red,
    Blue,
    Purple,
    Lava,
    Krypton,
    Xenon,
    Argon,
    Neon,
    Helium
}

public enum GemType
{
    Sapphire,
    Ruby,
    Emerald,
    Topaz,
    Amethyst,
    Diamond,
    Amber
}
