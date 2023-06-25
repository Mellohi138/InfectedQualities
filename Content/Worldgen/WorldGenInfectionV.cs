using Terraria;
using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria.ID;
using Terraria.Utilities;
using System;

using ReLogic.Utilities;

namespace InfectedQualities.Content.Worldgen
{
    public class WorldGenInfectionV : GenPass
    {
        public WorldGenInfectionV() :
            base("Infection V", 1.0f)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            if (Main.rand == null)
            {
                Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
            }

            double num6 = WorldGen.genRand.Next(300, 400) * 0.001;
            double num7 = WorldGen.genRand.Next(200, 300) * 0.001;
            int num3 = (int)(Main.maxTilesX * num6);
            int num4 = (int)(Main.maxTilesX * (1.0 - num6));
            int num5 = 1;
            if (WorldGen.genRand.NextBool(2))
            {
                num3 = (int)(Main.maxTilesX * (1.0 - num6));
                num4 = (int)(Main.maxTilesX * num6);
                num5 = -1;
            }

            if (GenVars.dungeonX < Main.maxTilesX / 2)
            {
                if (num4 < num3)
                {
                    num4 = (int)(Main.maxTilesX * num7);
                }
                else
                {
                    num3 = (int)(Main.maxTilesX * num7);
                }
            }
            else
            {
                if (num4 > num3)
                {
                    num4 = (int)(Main.maxTilesX * (1.0 - num7));
                }
                else
                {
                    num3 = (int)(Main.maxTilesX * (1.0 - num7));
                }
            }

            GenerateInfectionSlash(num3, 3f * num5, true);
            GenerateInfectionSlash(num4, 3f * -num5, false);
        }

        private static void GenerateInfectionSlash(int i, double speedX, bool good)
        {
            int num2 = WorldGen.genRand.Next(200, 250);
            double num3 = Main.maxTilesX / 4200.0;
            num2 *= (int)(num2 * num3);
            double num4 = num2;
            Vector2D vector2D = default;
            vector2D.X = i;
            Vector2D vector2D2 = default;
            vector2D2.X = speedX;
            vector2D2.Y = 5.0;

            bool flag = true;
            while (flag)
            {
                int num5 = (int)(vector2D.X - num4 * 0.5);
                int num6 = (int)(vector2D.X + num4 * 0.5);
                int num7 = (int)(vector2D.Y - num4 * 0.5);
                int num8 = (int)(vector2D.Y + num4 * 0.5);
                if (num5 < 0)
                {
                    num5 = 0;
                }

                if (num6 > Main.maxTilesX)
                {
                    num6 = Main.maxTilesX;
                }

                if (num7 < 0)
                {
                    num7 = 0;
                }

                if (num8 > Main.maxTilesY - 5)
                {
                    num8 = Main.maxTilesY - 5;
                }
                for (int m = num5; m < num6; m++)
                {
                    for (int n = num7; n < num8; n++)
                    {
                        if (Math.Abs(m - vector2D.X) + Math.Abs(n - vector2D.Y) < num2 * 0.5 * (1.0 + WorldGen.genRand.Next(-10, 11) * 0.015))
                        {
                            if (good)
                            {
                                WorldGen.Convert(m, n, BiomeConversionID.Hallow, 0);
                            }
                            else
                            {
                                if (WorldGen.crimson)
                                {
                                    WorldGen.Convert(m, n, BiomeConversionID.Crimson, 0);
                                }
                                else
                                {
                                    WorldGen.Convert(m, n, BiomeConversionID.Corruption, 0);
                                }
                            }

                            if (TileID.Sets.Conversion.MossBrick[Main.tile[m, n].TileType])
                            {
                                Main.tile[m, n].TileType = TileID.GrayBrick;
                                WorldGen.SquareTileFrame(m, n);
                                NetMessage.SendTileSquare(-1, m, n);
                            }

                            if (Main.notTheBeesWorld)
                            {
                                if (Main.tile[m, n].TileType == TileID.Hive)
                                {
                                    if(good)
                                    {
                                        Main.tile[m, n].TileType = TileID.Pearlstone;
                                    }
                                    else
                                    {
                                        if(WorldGen.crimson)
                                        {
                                            Main.tile[m, n].TileType = TileID.Crimstone;
                                        }
                                        else
                                        {
                                            Main.tile[m, n].TileType = TileID.Ebonstone;
                                        }
                                    }
                                    WorldGen.SquareTileFrame(m, n);
                                    NetMessage.SendTileSquare(-1, m, n);
                                }
                                else if (Main.tile[m, n].TileType == TileID.CrispyHoneyBlock)
                                {
                                    if (good)
                                    {
                                        Main.tile[m, n].TileType = TileID.HallowHardenedSand;
                                    }
                                    else
                                    {
                                        if (WorldGen.crimson)
                                        {
                                            Main.tile[m, n].TileType = TileID.CrimsonHardenedSand;
                                        }
                                        else
                                        {
                                            Main.tile[m, n].TileType = TileID.CorruptHardenedSand;
                                        }
                                    }
                                    WorldGen.SquareTileFrame(m, n);
                                    NetMessage.SendTileSquare(-1, m, n);
                                }
                            }
                        }
                    }
                }

                vector2D += vector2D2;
                vector2D2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                if (vector2D2.X > speedX + 1.0)
                {
                    vector2D2.X = speedX + 1.0f;
                }
                if (vector2D2.X < speedX - 1.0)
                {
                    vector2D2.X = speedX - 1.0f;
                }
                if (vector2D.X < (double)-(double)num2 || vector2D.Y < (double)-(double)num2 || vector2D.X > (Main.maxTilesX + num2) || vector2D.Y > (Main.maxTilesY + num2))
                {
                    flag = false;
                }
            }
        }
    }
}
