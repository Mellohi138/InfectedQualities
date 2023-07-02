using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using InfectedQualities.Helpers;
using InfectedQualities.Content.Tiles;

namespace InfectedQualities.Common.GlobalProjectiles
{
    public class SolutionSprayEdits : ILoadable
    {
        public void Load(Mod mod)
        {
            On_WorldGen.Convert += (orig, i, j, convertType, size) =>
            {
                for (int k = i - size; k <= i + size; k++)
                {
                    for (int l = j - size; l <= j + size; l++)
                    {
                        if (!WorldGen.InWorld(k, l, 1) || Math.Abs(k - i) + Math.Abs(l - j) >= 6)
                        {
                            continue;
                        }
						
						if (convertType == BiomeConversionID.Purity || convertType == BiomeConversionID.Corruption || convertType == BiomeConversionID.Crimson || convertType == BiomeConversionID.Hallow || convertType == BiomeConversionID.GlowingMushroom)
                        {
							if (TileUtils.IDSets.TileConversionMud[Main.tile[k, l].TileType] && Main.tile[k, l].TileType != TileID.Mud)
                            {
                                WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, TileID.Mud);
                                Main.tile[k, l].TileType = TileID.Mud;
                                WorldGen.SquareTileFrame(k, l);
                                NetMessage.SendTileSquare(-1, k, l);
                            }

                            if(convertType != BiomeConversionID.GlowingMushroom)
                            {
                                if (TileID.Sets.Conversion.Dirt[Main.tile[k, l].TileType] && Main.tile[k, l].TileType != TileID.Dirt)
                                {
                                    WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, TileID.Dirt);
                                    Main.tile[k, l].TileType = TileID.Dirt;
                                    WorldGen.SquareTileFrame(k, l);
                                    NetMessage.SendTileSquare(-1, k, l);
                                }
                            }
                        }

                        if (ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedSnowBiomes)
                        {
                            if (convertType == BiomeConversionID.Purity)
                            {
                                if (TileID.Sets.Conversion.Snow[Main.tile[k, l].TileType] && Main.tile[k, l].TileType != TileID.SnowBlock)
                                {
                                    WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, TileID.SnowBlock);
                                    Main.tile[k, l].TileType = TileID.SnowBlock;
                                    WorldGen.SquareTileFrame(k, l);
                                    NetMessage.SendTileSquare(-1, k, l);
                                }
                            }
                            else if (convertType == BiomeConversionID.Corruption)
                            {
                                if (TileID.Sets.Conversion.Snow[Main.tile[k, l].TileType] && Main.tile[k, l].TileType != (ushort)ModContent.TileType<CorruptSnow>())
                                {
                                    WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, (ushort)ModContent.TileType<CorruptSnow>());
                                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<CorruptSnow>();
                                    WorldGen.SquareTileFrame(k, l);
                                    NetMessage.SendTileSquare(-1, k, l);
                                }
                            }
                            else if (convertType == BiomeConversionID.Crimson)
                            {
                                if (TileID.Sets.Conversion.Snow[Main.tile[k, l].TileType] && Main.tile[k, l].TileType != (ushort)ModContent.TileType<CrimsonSnow>())
                                {
                                    WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, (ushort)ModContent.TileType<CrimsonSnow>());
                                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<CrimsonSnow>();
                                    WorldGen.SquareTileFrame(k, l);
                                    NetMessage.SendTileSquare(-1, k, l);
                                }
                            }
                            else if (convertType == BiomeConversionID.Hallow)
                            {
                                if (TileID.Sets.Conversion.Snow[Main.tile[k, l].TileType] && Main.tile[k, l].TileType != (ushort)ModContent.TileType<HallowedSnow>())
                                {
                                    WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, (ushort)ModContent.TileType<HallowedSnow>());
                                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<HallowedSnow>();
                                    WorldGen.SquareTileFrame(k, l);
                                    NetMessage.SendTileSquare(-1, k, l);
                                }
                            }
                        }
                        else
                        {
                            if(convertType == BiomeConversionID.Purity || convertType == BiomeConversionID.Corruption || convertType == BiomeConversionID.Crimson || convertType == BiomeConversionID.Hallow)
                            {
                                if (TileID.Sets.Conversion.Snow[Main.tile[k, l].TileType] && Main.tile[k, l].TileType != TileID.SnowBlock)
                                {
                                    WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, TileID.SnowBlock);
                                    Main.tile[k, l].TileType = TileID.SnowBlock;
                                    WorldGen.SquareTileFrame(k, l);
                                    NetMessage.SendTileSquare(-1, k, l);
                                }
                            }
                        }

                        if (convertType == BiomeConversionID.Hallow)
                        {
                            ushort jungleGrass = ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedJungleBiomes ? (ushort)ModContent.TileType<HallowedJungleGrass>() : TileID.JungleGrass;

                            if (TileID.Sets.Conversion.JungleGrass[Main.tile[k, l].TileType] && Main.tile[k, l].TileType != jungleGrass)
                            {
                                WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, jungleGrass);
                                Main.tile[k, l].TileType = jungleGrass;
                                WorldGen.SquareTileFrame(k, l);
                                NetMessage.SendTileSquare(-1, k, l);
                            }
                        }

                        if (ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableLimeSolution)
                        {
                            switch (convertType)
                            {
                                case BiomeConversionID.Sand:
                                    if ((WallID.Sets.Conversion.Dirt[Main.tile[k, l].WallType] || TileUtils.IDSets.WallMud[Main.tile[k, l].WallType] || TileUtils.IDSets.WallMushroom[Main.tile[k, l].WallType]) && Main.tile[k, l].WallType != WallID.Sandstone)
                                    {
                                        Main.tile[k, l].WallType = WallID.Sandstone;
                                        WorldGen.SquareWallFrame(k, l);
                                        NetMessage.SendTileSquare(-1, k, l);
                                    }
                                    else if ((WallID.Sets.Conversion.Grass[Main.tile[k, l].WallType]) && Main.tile[k, l].WallType != WallID.HardenedSand)
                                    {
                                        Main.tile[k, l].WallType = WallID.HardenedSand;
                                        WorldGen.SquareWallFrame(k, l);
                                        NetMessage.SendTileSquare(-1, k, l);
                                    }

                                    if (TileID.Sets.Conversion.JungleGrass[Main.tile[k, l].TileType] || TileID.Sets.Conversion.MushroomGrass[Main.tile[k, l].TileType] || TileUtils.IDSets.TileConversionMud[Main.tile[k, l].TileType] && Main.tile[k, l].TileType != TileID.Sand && Main.tile[k, l].TileType != TileID.HardenedSand && Main.tile[k, l].TileType != TileID.Sandstone)
                                    {
                                        int num = WorldGen.genRand.NextBool(4) && TileUtils.TileUnderground(j) ? TileID.HardenedSand : TileID.Sand;
                                        if ((TileUtils.TileIsExposedToAir(k, l, 2) && TileUtils.TileUnderground(j)) || WorldGen.BlockBelowMakesSandConvertIntoHardenedSand(k, l))
                                        {
                                            num = TileID.Sandstone;
                                        }

                                        WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, num);
                                        Main.tile[k, l].TileType = (ushort)num;
                                        WorldGen.SquareTileFrame(k, l);
                                        NetMessage.SendTileSquare(-1, k, l);
                                    }
                                    break;
                                case BiomeConversionID.Snow:
                                    if ((WallID.Sets.Conversion.Grass[Main.tile[k, l].WallType] || TileUtils.IDSets.WallMud[Main.tile[k, l].WallType] || TileUtils.IDSets.WallMushroom[Main.tile[k, l].WallType]) && !(Main.tile[k, l].WallType is WallID.IceUnsafe or WallID.SnowWallUnsafe))
                                    {
                                        if (TileUtils.TileCavern(l))
                                        {
                                            Main.tile[k, l].WallType = WallID.IceUnsafe;
                                        }
                                        else
                                        {
                                            Main.tile[k, l].WallType = WallID.SnowWallUnsafe;
                                        }
                                        WorldGen.SquareWallFrame(k, l);
                                        NetMessage.SendTileSquare(-1, k, l);
                                    }

                                    if ((TileID.Sets.Conversion.JungleGrass[Main.tile[k, l].TileType] || TileID.Sets.Conversion.MushroomGrass[Main.tile[k, l].TileType] || TileUtils.IDSets.TileConversionMud[Main.tile[k, l].TileType]) && !(Main.tile[k, l].TileType is TileID.SnowBlock or TileID.IceBlock))
                                    {
                                        ushort num = TileID.SnowBlock;
                                        if (TileUtils.TileCavern(l))
                                        {
                                            num = TileID.IceBlock;
                                        }

                                        WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, num);
                                        Main.tile[k, l].TileType = num;
                                        WorldGen.SquareTileFrame(k, l);
                                        NetMessage.SendTileSquare(-1, k, l);
                                    }
                                    break;
                                case BiomeConversionID.Dirt:
                                    if ((TileUtils.IDSets.WallMud[Main.tile[k, l].WallType] || TileUtils.IDSets.WallMushroom[Main.tile[k, l].WallType]) && Main.tile[k, l].WallType != WallID.DirtUnsafe)
                                    {
                                        if (TileUtils.TileCavern(l))
                                        {
                                            Main.tile[k, l].WallType = WallID.Stone;
                                        }
                                        else
                                        {
                                            Main.tile[k, l].WallType = WallID.DirtUnsafe;
                                        }
                                        WorldGen.SquareWallFrame(k, l);
                                        NetMessage.SendTileSquare(-1, k, l);
                                    }
                                    else if (WallID.Sets.Conversion.Grass[Main.tile[k, l].WallType] && !(Main.tile[k, l].WallType is WallID.Grass or WallID.GrassUnsafe))
                                    {
                                        Main.tile[k, l].WallType = WallID.GrassUnsafe;
                                        WorldGen.SquareWallFrame(k, l);
                                        NetMessage.SendTileSquare(-1, k, l);
                                    }

                                    if (TileUtils.IDSets.TileConversionMud[Main.tile[k, l].TileType] && !(Main.tile[k, l].TileType is TileID.Dirt or TileID.Stone))
                                    {
                                        ushort num = TileID.Dirt;
                                        if (TileUtils.TileUnderground(l))
                                        {
                                            num = TileID.Stone;
                                        }

                                        WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, num);
                                        Main.tile[k, l].TileType = num;
                                        WorldGen.SquareTileFrame(k, l);
                                        NetMessage.SendTileSquare(-1, k, l);
                                    }
                                    else if ((TileID.Sets.Conversion.JungleGrass[Main.tile[k, l].TileType] || TileID.Sets.Conversion.MushroomGrass[Main.tile[k, l].TileType]) && !(Main.tile[k, l].TileType is TileID.Grass or TileID.Stone))
                                    {
                                        ushort num = TileID.Grass;
                                        if (TileUtils.TileUnderground(l))
                                        {
                                            num = TileID.Stone;
                                        }
                                        WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, num);
                                        Main.tile[k, l].TileType = num;
                                        WorldGen.SquareTileFrame(k, l);
                                        NetMessage.SendTileSquare(-1, k, l);
                                    }
                                    break;
                            }
                        }

                        if ((convertType == BiomeConversionID.Dirt || convertType == BiomeConversionID.Sand) && Main.tile[k, l].TileType == TileID.BreakableIce)
                        {
                            WorldGen.KillTile(k, l);
                            NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j);
                        }
                    }
                }

                orig(i, j, convertType, size);
            };
        }

        public void Unload()
        {
        }
    }
}
