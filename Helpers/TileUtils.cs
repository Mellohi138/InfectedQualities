using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using InfectedQualities.Common;
using InfectedQualities.Content.Tiles;

namespace InfectedQualities.Helpers
{
    public class TileUtils
    {
        public static bool TileUnderground(int y)
        {
            if(Main.remixWorld)
            {
                return TileRemixCavern(y);
            }
            return y >= Main.worldSurface;
        }

        public static bool TileCavern(int y)
        {
            if (Main.remixWorld)
            {
                return TileRemixCavern(y);
            }
            return y >= Main.rockLayer;
        }

        private static bool TileRemixCavern(int y)
        {
            return y >= Main.worldSurface && y < Main.rockLayer;
        }

        public static bool TileIsExposedToAir(int x, int y, int fluff = 1)
        {
            for (int i = x - fluff; i <= x + fluff; i++)
            {
                for (int j = y - fluff; j <= y + fluff; j++)
                {
                    Tile tile = Main.tile[i, j];
                    if (!tile.HasTile || !Main.tileSolid[tile.TileType] || TileID.Sets.Platforms[tile.TileType])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool LavaCheck(int i, int j)
        {
            if (Main.tile[i, j - 1].LiquidType == LiquidID.Lava || Main.tile[i + 1, j].LiquidType == LiquidID.Lava || Main.tile[i - 1, j].LiquidType == LiquidID.Lava || Main.tile[i + 1, j - 1].LiquidType == LiquidID.Lava || Main.tile[i - 1, j - 1].LiquidType == LiquidID.Lava)
            {
                return true;
            }
            return false;
        }

        public static void SpreadInfection(int i, int j, int ID)
        {
            if (ID > 2 || ID < 0)
            {
                return;
            }

            if (Main.hardMode)
            {
                if (NPC.downedPlantBoss && WorldGen.genRand.NextBool(2))
                {
                    return;
                }

                if (WorldGen.AllowedToSpreadInfections)
                {
                    int x = i + WorldGen.genRand.Next(-3, 4);
                    int y = j + WorldGen.genRand.Next(-3, 4);

                    if (WorldGen.CountNearBlocksTypes(x, y, 2, 1, new int[] { TileID.Sunflower }) <= 0)
                    {
                        ushort[] tilesToConvert = {
                            TileID.Grass,
                            TileID.GolfGrass,
                            TileID.Stone,
                            TileID.JungleGrass,
                            TileID.Sand,
                            TileID.Sandstone,
                            TileID.HardenedSand,
                            TileID.SnowBlock,
                            TileID.IceBlock
                        };

                        ushort[][] convertedTiles = {
                            new ushort[] { TileID.CorruptGrass, TileID.CrimsonGrass, TileID.HallowedGrass },
                            new ushort[] { TileID.CorruptGrass, TileID.CrimsonGrass, TileID.GolfGrassHallowed },
                            new ushort[] { TileID.Ebonstone, TileID.Crimstone, TileID.Pearlstone },
                            new ushort[] { TileID.CorruptJungleGrass, TileID.CrimsonJungleGrass, TileID.JungleGrass },
                            new ushort[] { TileID.Ebonsand, TileID.Crimsand, TileID.Pearlsand },
                            new ushort[] { TileID.CorruptSandstone, TileID.CrimsonSandstone, TileID.HallowSandstone },
                            new ushort[] { TileID.CorruptHardenedSand, TileID.CrimsonHardenedSand, TileID.HallowHardenedSand },
                            new ushort[] { TileID.SnowBlock, TileID.SnowBlock, TileID.SnowBlock },
                            new ushort[] { TileID.CorruptIce, TileID.FleshIce, TileID.HallowedIce }
                        };

                        if (ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedJungleBiomes)
                        {
                            convertedTiles[3][2] = (ushort)ModContent.TileType<HallowedJungleGrass>();
                        }

                        if (ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedSnowBiomes)
                        {
                            convertedTiles[7][0] = (ushort)ModContent.TileType<CorruptSnow>();
                            convertedTiles[7][1] = (ushort)ModContent.TileType<CrimsonSnow>();
                            convertedTiles[7][2] = (ushort)ModContent.TileType<HallowedSnow>();
                        }

                        for (int tile = 0; tile < tilesToConvert.Length; tile++)
                        {
                            if (Main.tile[x, y].TileType == tilesToConvert[tile] && tilesToConvert[tile] != convertedTiles[tile][ID])
                            {
                                Main.tile[x, y].TileType = convertedTiles[tile][ID];
                                WorldGen.SquareTileFrame(x, y);
                                if (Main.netMode == NetmodeID.Server)
                                {
                                    NetMessage.SendTileSquare(-1, x, y);
                                }
                                break;
                            }
                        }

                        x = i + WorldGen.genRand.Next(-2, 3);
                        y = j + WorldGen.genRand.Next(-2, 3);

                        ushort[] wallsToConvert = {
                            WallID.GrassUnsafe,
                            WallID.FlowerUnsafe,   
                            WallID.Grass,   
                            WallID.Flower, 
                            WallID.JungleUnsafe,
                            WallID.Jungle             
                        };

                        ushort[] convertedWalls = {      
                            WallID.CorruptGrassUnsafe,
                            WallID.CrimsonGrassUnsafe,
                            WallID.HallowedGrassUnsafe
                        };

                        for (int wallIndex = 0; wallIndex < wallsToConvert.Length; wallIndex++)
                        {
                            if (Main.tile[x, y].WallType == wallsToConvert[wallIndex])
                            {
                                Main.tile[x, y].WallType = convertedWalls[ID];
                                WorldGen.SquareWallFrame(x, y);
                                if (Main.netMode == NetmodeID.Server)
                                {
                                    NetMessage.SendTileSquare(-1, x, y);
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        public static void SpreadInfectionFixCompatilbility(int i, int j, int ID)
        {
            if (ID > 2 || ID < 0)
            {
                return;
            }

            if (Main.hardMode)
            {
                if (NPC.downedPlantBoss && WorldGen.genRand.NextBool(2))
                {
                    return;
                }

                if (WorldGen.AllowedToSpreadInfections)
                {
                    int x = i + WorldGen.genRand.Next(-3, 4);
                    int y = j + WorldGen.genRand.Next(-3, 4);

                    if (WorldGen.CountNearBlocksTypes(x, y, 2, 1, new int[] { TileID.Sunflower }) <= 0)
                    {
                        ushort[] tilesToConvert = {
                            TileID.JungleGrass,
                            TileID.SnowBlock
                        };

                        ushort[][] convertedTiles = {
                            new ushort[] { TileID.JungleGrass, TileID.JungleGrass, TileID.JungleGrass },
                            new ushort[] { TileID.SnowBlock, TileID.SnowBlock, TileID.SnowBlock },
                        };

                        if (ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedJungleBiomes)
                        {
                            convertedTiles[0][2] = (ushort)ModContent.TileType<HallowedJungleGrass>();
                        }

                        if (ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedSnowBiomes)
                        {
                            convertedTiles[1][0] = (ushort)ModContent.TileType<CorruptSnow>();
                            convertedTiles[1][1] = (ushort)ModContent.TileType<CrimsonSnow>();
                            convertedTiles[1][2] = (ushort)ModContent.TileType<HallowedSnow>();
                        }

                        for (int tile = 0; tile < tilesToConvert.Length; tile++)
                        {
                            if (Main.tile[x, y].TileType == tilesToConvert[tile] && tilesToConvert[tile] != convertedTiles[tile][ID])
                            {
                                Main.tile[x, y].TileType = convertedTiles[tile][ID];
                                WorldGen.SquareTileFrame(x, y);
                                NetMessage.SendTileSquare(-1, x, y);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public static bool PlaceSunflower(int x, int y)
        {
            bool flag = true;
            for (int i = x; i < x + 2; i++)
            {
                for (int j = y - 3; j < y + 1; j++)
                {
                    if (Main.tile[i, j].TileType == TileID.Sunflower)
                    {
                        flag = false;
                    }

                    if (Main.tile[i, j].TileType != TileID.Torches)
                    {
                        flag = false;
                    }

                    if (WorldGen.SolidTile(i, j))
                    {
                        flag = false;
                    }
                }
                Tile tile = Main.tile[i, y + 1];

                if (!tile.HasUnactuatedTile)
                {
                    flag = false;
                }

                if (tile.IsHalfBlock)
                {
                    flag = false;
                }

                if (tile.Slope != SlopeType.Solid)
                {
                    flag = false;
                }
            }

            if(flag)
            {
                for (int i = x; i < x + 2; i++)
                {
                    for (int j = y - 3; j < y + 1; j++)
                    {
                        if (!WorldGen.SolidTile(i, j))
                        {
                            WorldGen.KillTile(i, j);
                        }
                    }
                }

                int num = WorldGen.genRand.Next(3);
                for (int k = 0; k < 2; k++)
                {
                    for (int l = -3; l < 1; l++)
                    {
                        int num2 = k * 18 + WorldGen.genRand.Next(3) * 36;
                        if (l <= -2)
                        {
                            num2 = k * 18 + num * 36;
                        }

                        int num3 = (l + 3) * 18;

                        int i = x + k;
                        int j = y + l;

                        Tile tile = Main.tile[i, j];
                        tile.HasTile = true;
                        tile.TileFrameX = (short)num2;
                        tile.TileFrameY = (short)num3;
                        tile.TileType = TileID.Sunflower;
                    }
                }
            }
            return flag;
        }

        public static class IDSets
        {
            public static bool[] TilePlant = TileID.Sets.Factory.CreateBoolSet(TileID.Plants, TileID.Plants2, TileID.CorruptPlants, TileID.CrimsonPlants, TileID.HallowedPlants, TileID.HallowedPlants2, TileID.JunglePlants, TileID.JunglePlants2, TileID.MushroomPlants, TileID.AshPlants);

            public static bool[] TileConversionMud = TileID.Sets.Factory.CreateBoolSet(TileID.Mud);

            public static bool[] WallMud = WallID.Sets.Factory.CreateBoolSet(WallID.MudUnsafe, WallID.MudWallEcho);

            public static bool[] WallMushroom = WallID.Sets.Factory.CreateBoolSet(WallID.MushroomUnsafe, WallID.Mushroom);
        }
    }
}
