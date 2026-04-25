using InfectedQualities.Content.Tiles;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using InfectedQualities.Content.Tiles.Plants;
using Terraria.Enums;
using InfectedQualities.Utilities;
using InfectedQualities.Common;

namespace InfectedQualities.Core
{
    public class InfectedQualitiesILEdits : ILoadable
    {
        public void Load(Mod mod)
        {
            if(ModContent.GetInstance<InfectedQualitiesClientConfig>().InfectedPlantera)
            {
                IL_NPC.SpawnOnPlayer += NPC_SpawnOnPlayer;
                On_Gore.NewGore_IEntitySource_Vector2_Vector2_int_float += Gore_NewGore;
                On_Dust.NewDust += Dust_NewDust;
            }

            if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes)
            {
                On_TileDrawing.GetTreeVariant += TileDrawing_GetTreeVariant;
                On_WorldGen.GetTreeLeaf += WorldGen_GetTreeLeaf;
                IL_WorldGen.GetCommonTreeFoliageData += WorldGen_GetCommonTreeFoliageData;
                
                IL_WorldGen.CheckAlch += WorldGen_CheckAlch;
                IL_WorldGen.PlaceAlch += WorldGen_PlaceAlch;
                IL_WorldGen.PlantAlch += WorldGen_PlantAlch;
                IL_WorldGen.IsFitToPlaceFlowerIn += WorldGen_IsFitToPlaceFlowerIn;
                On_WorldGen.nearbyChlorophyte += WorldGen_nearbyChlorophyte;
            }

            if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedMosses)
            {
                On_WorldGen.GetTileMossColor += WorldGen_GetTileMossColor;
            }

            IL_WorldGen.UpdateWorld_GrassGrowth += WorldGen_UpdateWorld_GrassGrowth;
            IL_WorldGen.GERunner += WorldGen_GERunner;
            IL_WorldGen.ScoreRoom += WorldGen_ScoreRoom;
            On_WorldGen.GetTileTypeCountByCategory += WorldGen_GetTileTypeCountByCategory;

            IL_WorldGen.UpdateWorld_Inner += WorldGen_UpdateWorld_Inner;
            IL_WorldGen.hardUpdateWorld += WorldGen_hardUpdateWorld;

            IL_WorldGen.CheckLilyPad += WorldGen_CheckLilyPad;
            IL_WorldGen.CheckCatTail += WorldGen_CheckCatTail;
            On_WorldGen.GetDesiredStalagtiteStyle += WorldGen_GetDesiredStalagtiteStyle;
        }

        private void NPC_SpawnOnPlayer(ILContext il)
        {
            ILCursor cursor = new(il);

            cursor.Emit(OpCodes.Ldarg_0);
            cursor.Emit(OpCodes.Ldarg_1);
            cursor.EmitDelegate<Action<int, int>>((targetIndex, npcType) =>
            {
                if(Main.netMode != NetmodeID.Server && npcType == NPCID.Plantera && targetIndex > -1 && targetIndex < 255)
                {
                    Player targetPlayer = Main.player[targetIndex];
                    if(targetPlayer.active && !targetPlayer.dead && !targetPlayer.ghost)
                    {
                        InfectedPlanteraSystem.ReplacePlanteraTextures(targetPlayer);
                    }
                }
            });
        }

        private static int Gore_NewGore(On_Gore.orig_NewGore_IEntitySource_Vector2_Vector2_int_float orig, IEntitySource source, Vector2 Position, Vector2 Velocity, int Type, float Scale)
        {
            InfectionType? planteraType = InfectedPlanteraSystem.PlanteraType;
            if (planteraType.HasValue)
            {
                if (Type >= 378 && Type < 381)
                {
                    Type = ModContent.GetInstance<InfectedQualities>().Find<ModGore>(planteraType.ToString() + "Plantera_1_" + (Type - 378)).Type;
                }
                else if (Type >= 381 && Type < 388)
                {
                    Type = ModContent.GetInstance<InfectedQualities>().Find<ModGore>(planteraType.ToString() + "Plantera_2_" + (Type - 381)).Type;
                }
                else if (Type >= 388 && Type < 390)
                {
                    Type = ModContent.GetInstance<InfectedQualities>().Find<ModGore>(planteraType.ToString() + "Plantera_Tentacle_" + (Type - 388)).Type;
                }
                else if (Type >= 390 && Type < 392)
                {
                    Type = ModContent.GetInstance<InfectedQualities>().Find<ModGore>(planteraType.ToString() + "Plantera_Hook_" + (Type - 390)).Type;
                }
            }
            return orig(source, Position, Velocity, Type, Scale);
        }

        private static int Dust_NewDust(On_Dust.orig_NewDust orig, Vector2 Position, int Width, int Height, int Type, float SpeedX, float SpeedY, int Alpha, Color newColor, float Scale)
        {
            InfectionType? planteraType = InfectedPlanteraSystem.PlanteraType;
            if(planteraType.HasValue)
            {
                if (Type == DustID.Plantera_Green)
                {
                    switch (planteraType)
                    {
                        case InfectionType.Corrupt:
                            Type = DustID.CorruptPlants;
                            break;
                        case InfectionType.Crimson:
                            Type = DustID.CrimsonPlants;
                            break;
                        case InfectionType.Hallowed:
                            Type = DustID.HallowedPlants;
                            break;
                    }
                }
                else if (Type == DustID.Plantera_Pink)
                {
                    switch (planteraType)
                    {
                        case InfectionType.Corrupt:
                            Type = DustID.Corruption;
                            break;
                        case InfectionType.Crimson:
                            Type = DustID.Crimson;
                            break;
                        case InfectionType.Hallowed:
                            newColor.MultiplyRGB(Color.Magenta);
                            break;
                    }
                }
            }
            return orig(Position, Width, Height, Type, SpeedX, SpeedY, Alpha, newColor, Scale);
        }

        private static void WorldGen_GERunner(ILContext il)
        {
            ILCursor cursor = new(il);

            bool flag = false;
            bool infectedBiomes = ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes;
            bool infectedMosses = ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedMosses;
			cursor.Emit(OpCodes.Ldarg, 4);
            cursor.EmitDelegate<Action<bool>>((good) =>
            {
                flag = InfectedQualitiesModSupport.AltLibraryInfection(good);
            });

            if (cursor.TryGotoNext(i => i.MatchLdarg(4)))
            {
                cursor.Emit(OpCodes.Ldloc, 15);
                cursor.Emit(OpCodes.Ldloc, 16);
                cursor.Emit(OpCodes.Ldarg, 4);
                cursor.EmitDelegate<Action<int, int, bool>>((m, n, good) =>
                {
                    if(!flag)
                    {
						if (Main.tile[m, n].WallType is WallID.JungleUnsafe or WallID.Jungle && infectedBiomes)
						{
                            if(good) Main.tile[m, n].WallType = WallID.HallowedGrassUnsafe;
							else if (WorldGen.crimson) Main.tile[m, n].WallType = WallID.CrimsonGrassUnsafe;
							else Main.tile[m, n].WallType = WallID.CorruptGrassUnsafe;
						}
						else if (Main.tile[m, n].WallType == WallID.Stone)
						{
                            if (good) Main.tile[m, n].WallType = WallID.PearlstoneBrickUnsafe;
							else if (WorldGen.crimson) Main.tile[m, n].WallType = WallID.CrimstoneUnsafe;
							else Main.tile[m, n].WallType = WallID.EbonstoneUnsafe;
						}

						if (Main.tile[m, n].TileType is TileID.CorruptThorns or TileID.CrimsonThorns)
						{
                            if (good) WorldGen.KillTile(m, n);
							else if (WorldGen.crimson)
                            {
								Main.tile[m, n].TileType = TileID.CrimsonThorns;
								WorldGen.SquareTileFrame(m, n);
							}
                            else
                            {
								Main.tile[m, n].TileType = TileID.CorruptThorns;
								WorldGen.SquareTileFrame(m, n);
							}
						}
						else if (Main.tileMoss[Main.tile[m, n].TileType] && !infectedMosses)
						{
                            if (good) Main.tile[m, n].TileType = TileID.Pearlstone;
							else if (WorldGen.crimson) Main.tile[m, n].TileType = TileID.Crimstone;
							else Main.tile[m, n].TileType = TileID.Ebonstone;
							WorldGen.SquareTileFrame(m, n);
						}
                    }
                });
            }
        }

        private static int TileDrawing_GetTreeVariant(On_TileDrawing.orig_GetTreeVariant orig, int x, int y)
        {
            if(Main.tile[x, y].TileType == ModContent.TileType<HallowedJungleGrass>())
            {
                return 2;
            }
            return orig(x, y);
        }

        private static void WorldGen_GetTreeLeaf(On_WorldGen.orig_GetTreeLeaf orig, int x, Tile topTile, Tile t, ref int treeHeight, out int treeFrame, out int passStyle)
        {
            if (t.TileType == ModContent.TileType<HallowedJungleGrass>())
            {
                treeFrame = 0;
                passStyle = 917;
                if (WorldGen.GetHollowTreeFoliageStyle() != 20)
                {
                    treeFrame += x % 3 * 3;
                    switch (treeFrame)
                    {
                        case 0:
                            passStyle += 2;
                            break;
                        case 1:
                            passStyle += 1;
                            break;
                        case 2:
                            passStyle += 7;
                            break;
                        case 3:
                            passStyle += 4;
                            break;
                        case 4:
                            passStyle += 5;
                            break;
                        case 5:
                            passStyle += 6;
                            break;
                        case 6:
                            passStyle += 3;
                            break;
                        case 7:
                            passStyle += 8;
                            break;
                    }
                }
                else
                {
                    passStyle += 196;
                    treeFrame += x % 6 * 3;
                    switch (treeFrame)
                    {
                        case 3:
                        case 5:
                            passStyle += 1;
                            break;
                        case 4:
                            passStyle += 2;
                            break;
                        case 6:
                            passStyle += 3;
                            break;
                        case 7:
                            passStyle += 4;
                            break;
                        case 8:
                            passStyle += 5;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            passStyle += 6;
                            break;
                        case 12:
                        case 13:
                        case 14:
                            passStyle += 7;
                            break;
                        case 15:
                        case 16:
                        case 17:
                            passStyle += 8;
                            break;
                    }
                }
                treeHeight += 5;
                return;
            }
            else if (t.TileType == TileUtilities.GetSnowType(InfectionType.Hallowed))
            {
                treeHeight += 5;
            }
            orig(x, topTile, t, ref treeHeight, out treeFrame, out passStyle);
        }

        private static void WorldGen_GetCommonTreeFoliageData(ILContext il)
        {
            ILCursor cursor = new(il);
            ILLabel label = cursor.DefineLabel();

            if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.GolfGrassHallowed), i => i.MatchBeq(out label)))
            {
                cursor.Emit(OpCodes.Ldloc, 5);
                cursor.EmitDelegate(ModContent.TileType<HallowedJungleGrass>);
                cursor.Emit(OpCodes.Beq, label);
            }
        }

        private static void WorldGen_CheckAlch(ILContext il)
        {
            ILCursor cursor = new(il);
            ILLabel label = cursor.DefineLabel();

            if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.JungleGrass), i => i.MatchBeq(out label)))
            {
                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.Emit(OpCodes.Ldarg_1);
                cursor.Emit(OpCodes.Ldc_I4_1);
                cursor.Emit(OpCodes.Add);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                cursor.Emit(OpCodes.Stloc_2);
                cursor.Emit(OpCodes.Ldloca, 2);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.EmitDelegate(ModContent.TileType<HallowedJungleGrass>);
                cursor.Emit(OpCodes.Beq, label);
            }

            label = cursor.DefineLabel();
            if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.CorruptJungleGrass), i => i.MatchBeq(out label)))
            {
                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.Emit(OpCodes.Ldarg_1);
                cursor.Emit(OpCodes.Ldc_I4_1);
                cursor.Emit(OpCodes.Add);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                cursor.Emit(OpCodes.Stloc_2);
                cursor.Emit(OpCodes.Ldloca, 2);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.Emit(OpCodes.Ldc_I4, TileID.CorruptIce);
                cursor.Emit(OpCodes.Beq, label);

                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.Emit(OpCodes.Ldarg_1);
                cursor.Emit(OpCodes.Ldc_I4_1);
                cursor.Emit(OpCodes.Add);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                cursor.Emit(OpCodes.Stloc_2);
                cursor.Emit(OpCodes.Ldloca, 2);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.EmitDelegate(() => TileUtilities.GetSnowType(InfectionType.Corrupt));
                cursor.Emit(OpCodes.Beq, label);

                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.Emit(OpCodes.Ldarg_1);
                cursor.Emit(OpCodes.Ldc_I4_1);
                cursor.Emit(OpCodes.Add);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                cursor.Emit(OpCodes.Stloc_2);
                cursor.Emit(OpCodes.Ldloca, 2);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.Emit(OpCodes.Ldc_I4, TileID.FleshIce);
                cursor.Emit(OpCodes.Beq, label);

                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.Emit(OpCodes.Ldarg_1);
                cursor.Emit(OpCodes.Ldc_I4_1);
                cursor.Emit(OpCodes.Add);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                cursor.Emit(OpCodes.Stloc_2);
                cursor.Emit(OpCodes.Ldloca, 2);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.EmitDelegate(() => TileUtilities.GetSnowType(InfectionType.Crimson));
                cursor.Emit(OpCodes.Beq, label);
            }

            if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.CorruptIce)))
            {
                cursor.Emit(OpCodes.Pop);
                cursor.Emit(OpCodes.Ldc_I4_M1);
            }

            if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.FleshIce)))
            {
                cursor.Emit(OpCodes.Pop);
                cursor.EmitDelegate(() => TileUtilities.GetSnowType(InfectionType.Hallowed));
            }
        }

        private static void WorldGen_PlaceAlch(ILContext il)
        {
            ILCursor cursor = new(il);
            ILLabel label = cursor.DefineLabel();
            if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.JungleGrass), i => i.MatchBeq(out label)))
            {
                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.Emit(OpCodes.Ldarg_1);
                cursor.Emit(OpCodes.Ldc_I4_1);
                cursor.Emit(OpCodes.Add);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                cursor.Emit(OpCodes.Stloc_0);
                cursor.Emit(OpCodes.Ldloca, 0);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.EmitDelegate(ModContent.TileType<HallowedJungleGrass>);
                cursor.Emit(OpCodes.Beq, label);
            }

            label = cursor.DefineLabel();
            if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.CorruptJungleGrass), i => i.MatchBeq(out label)))
            {
                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.Emit(OpCodes.Ldarg_1);
                cursor.Emit(OpCodes.Ldc_I4_1);
                cursor.Emit(OpCodes.Add);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                cursor.Emit(OpCodes.Stloc_0);
                cursor.Emit(OpCodes.Ldloca, 0);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.Emit(OpCodes.Ldc_I4, TileID.CorruptIce);
                cursor.Emit(OpCodes.Beq, label);

                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.Emit(OpCodes.Ldarg_1);
                cursor.Emit(OpCodes.Ldc_I4_1);
                cursor.Emit(OpCodes.Add);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                cursor.Emit(OpCodes.Stloc_0);
                cursor.Emit(OpCodes.Ldloca, 0);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.EmitDelegate(() => TileUtilities.GetSnowType(InfectionType.Corrupt));
                cursor.Emit(OpCodes.Beq, label);

                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.Emit(OpCodes.Ldarg_1);
                cursor.Emit(OpCodes.Ldc_I4_1);
                cursor.Emit(OpCodes.Add);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                cursor.Emit(OpCodes.Stloc_0);
                cursor.Emit(OpCodes.Ldloca, 0);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.Emit(OpCodes.Ldc_I4, TileID.FleshIce);
                cursor.Emit(OpCodes.Beq, label);

                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.Emit(OpCodes.Ldarg_1);
                cursor.Emit(OpCodes.Ldc_I4_1);
                cursor.Emit(OpCodes.Add);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                cursor.Emit(OpCodes.Stloc_0);
                cursor.Emit(OpCodes.Ldloca, 0);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.EmitDelegate(() => TileUtilities.GetSnowType(InfectionType.Crimson));
                cursor.Emit(OpCodes.Beq, label);
            }

            if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.CorruptIce)))
            {
                cursor.Emit(OpCodes.Pop);
                cursor.Emit(OpCodes.Ldc_I4_M1);
            }

            if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.FleshIce)))
            {
                cursor.Emit(OpCodes.Pop);
                cursor.EmitDelegate(() => TileUtilities.GetSnowType(InfectionType.Hallowed));
            }
        }

        private static void WorldGen_PlantAlch(ILContext il)
        {
            ILCursor cursor = new(il);
            ILLabel label = cursor.DefineLabel();

            if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.JungleGrass), i => i.MatchBeq(out label)))
            {
                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldloc_0);
                cursor.Emit(OpCodes.Ldloc_1);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                cursor.Emit(OpCodes.Stloc, 8);
                cursor.Emit(OpCodes.Ldloca, 8);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.EmitDelegate(ModContent.TileType<HallowedJungleGrass>);
                cursor.Emit(OpCodes.Beq, label);
            }

            label = cursor.DefineLabel();
            if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.CrimsonGrass), i => i.MatchBeq(out label)))
            {
                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldloc_0);
                cursor.Emit(OpCodes.Ldloc_1);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                cursor.Emit(OpCodes.Stloc, 8);
                cursor.Emit(OpCodes.Ldloca, 8);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.Emit(OpCodes.Ldc_I4, TileID.CorruptIce);
                cursor.Emit(OpCodes.Beq, label);

                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldloc_0);
                cursor.Emit(OpCodes.Ldloc_1);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                cursor.Emit(OpCodes.Stloc, 8);
                cursor.Emit(OpCodes.Ldloca, 8);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.EmitDelegate(() => TileUtilities.GetSnowType(InfectionType.Corrupt));
                cursor.Emit(OpCodes.Beq, label);

                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldloc_0);
                cursor.Emit(OpCodes.Ldloc_1);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                cursor.Emit(OpCodes.Stloc, 8);
                cursor.Emit(OpCodes.Ldloca, 8);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.Emit(OpCodes.Ldc_I4, TileID.FleshIce);
                cursor.Emit(OpCodes.Beq, label);

                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldloc_0);
                cursor.Emit(OpCodes.Ldloc_1);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                cursor.Emit(OpCodes.Stloc, 8);
                cursor.Emit(OpCodes.Ldloca, 8);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.EmitDelegate(() => TileUtilities.GetSnowType(InfectionType.Crimson));
                cursor.Emit(OpCodes.Beq, label);
            }

            if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.CorruptIce)))
            {
                cursor.Emit(OpCodes.Pop);
                cursor.Emit(OpCodes.Ldc_I4_M1);
            }

            if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.FleshIce)))
            {
                cursor.Emit(OpCodes.Pop);
                cursor.EmitDelegate(() => TileUtilities.GetSnowType(InfectionType.Hallowed));
            }
        }

        private static int WorldGen_GetTileMossColor(On_WorldGen.orig_GetTileMossColor orig, int tileType)
        {
            foreach (InfectionType infectionType in Enum.GetValues(typeof(InfectionType)))
            {
                foreach (MossType mossType in Enum.GetValues(typeof(MossType)))
                {
                    if (tileType == TileUtilities.GetMossType(infectionType, mossType))
                    {
                        return (int)mossType;
                    }
                }
            }
            return orig(tileType);
        }

        private static void WorldGen_IsFitToPlaceFlowerIn(ILContext il)
        {
            ILCursor cursor = new(il);
            ILLabel label = cursor.DefineLabel();
            
            if(cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.HallowedGrass), i => i.MatchBeq(out label)))
            {
                cursor.Emit(OpCodes.Ldloca, 0);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.EmitDelegate(ModContent.TileType<HallowedJungleGrass>);
                cursor.Emit(OpCodes.Beq, label);
            }
        }

        private static void WorldGen_ScoreRoom(ILContext il)
        {
            ILCursor cursor = new(il);
            ILLabel label = cursor.DefineLabel();

            if (cursor.TryGotoNext(MoveType.After, i => i.MatchStloc(6)))
            {
                cursor.EmitDelegate(() => ModContent.GetInstance<InfectedQualitiesServerConfig>().AllowNPCsInEvilBiomes);
                cursor.Emit(OpCodes.Brfalse, label);
                cursor.Emit(OpCodes.Ldc_I4_0);
                cursor.Emit(OpCodes.Stloc, 6);

                cursor.MarkLabel(label);
            }
        }

        private static int WorldGen_GetTileTypeCountByCategory(On_WorldGen.orig_GetTileTypeCountByCategory orig, int[] tileTypeCounts, TileScanGroup group)
        {
            int extra = 0;
            switch(group)
            {
                case TileScanGroup.Corruption:
                    extra += tileTypeCounts[TileID.CorruptVines];
                    extra += tileTypeCounts[TileID.CorruptJungleGrass];
                    if(ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes)
                    {
                        extra += tileTypeCounts[TileUtilities.GetSnowType(InfectionType.Corrupt)];
                    }
                    if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedMosses)
                    {
                        foreach (MossType mossType in Enum.GetValues(typeof(MossType)))
                        {
                            extra += tileTypeCounts[TileUtilities.GetMossType(InfectionType.Corrupt, mossType)];
                        }
                    }
                    if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedGemstones)
                    {
                        foreach (GemType gemType in Enum.GetValues(typeof(GemType)))
                        {
                            extra += tileTypeCounts[TileUtilities.GetGemstoneType(InfectionType.Corrupt, gemType)];
                        }
                    }
                    break;
                case TileScanGroup.Crimson:
                    extra += tileTypeCounts[TileID.CrimsonPlants];
                    extra += tileTypeCounts[TileID.CrimsonVines];
                    extra += tileTypeCounts[TileID.CrimsonJungleGrass];
                    if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes)
                    {
                        extra += tileTypeCounts[TileUtilities.GetSnowType(InfectionType.Crimson)];
                    }
                    if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedMosses)
                    {
                        foreach (MossType mossType in Enum.GetValues(typeof(MossType)))
                        {
                            extra += tileTypeCounts[TileUtilities.GetMossType(InfectionType.Crimson, mossType)];
                        }
                    }
                    if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedGemstones)
                    {
                        foreach (GemType gemType in Enum.GetValues(typeof(GemType)))
                        {
                            extra += tileTypeCounts[TileUtilities.GetGemstoneType(InfectionType.Crimson, gemType)];
                        }
                    }
                    break;
                case TileScanGroup.Hallow:
                    extra += tileTypeCounts[TileID.HallowedVines];
                    extra += tileTypeCounts[TileID.GolfGrassHallowed];
                    if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes)
                    {
                        extra += tileTypeCounts[ModContent.TileType<HallowedThorns>()];
                        extra += tileTypeCounts[TileUtilities.GetSnowType(InfectionType.Hallowed)];
                        extra += tileTypeCounts[ModContent.TileType<HallowedJungleGrass>()];
                    }
                    if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedMosses)
                    {
                        foreach (MossType mossType in Enum.GetValues(typeof(MossType)))
                        {
                            extra += tileTypeCounts[TileUtilities.GetMossType(InfectionType.Hallowed, mossType)];
                        }
                    }
                    if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedGemstones)
                    {
                        foreach (GemType gemType in Enum.GetValues(typeof(GemType)))
                        {
                            extra += tileTypeCounts[TileUtilities.GetGemstoneType(InfectionType.Hallowed, gemType)];
                        }
                    }
                    break;
            }
            return orig(tileTypeCounts, group) + extra;
        }

        private static void WorldGen_UpdateWorld_Inner(ILContext il)
        {
            ILCursor cursor = new(il);
            if (cursor.TryGotoNext(MoveType.After, i => i.MatchCeq(), i => i.MatchStsfld(typeof(WorldGen).GetField("AllowedToSpreadInfections"))))
            {
                cursor.EmitDelegate(() =>
                {
                    if (ModContent.GetInstance<InfectedQualitiesServerConfig>().DisableInfectionSpread || WorldGen.IsGeneratingHardMode)
                    {
                        WorldGen.AllowedToSpreadInfections = false;
                    }
                });
            }
        }

        private static void WorldGen_UpdateWorld_GrassGrowth(ILContext il)
        {
            ILCursor cursor = new(il);
            ILLabel label;

            if(ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes)
            {
                label = cursor.DefineLabel();
                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.PlanteraBulb), i => i.MatchBneUn(out label)))
                {
                    cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                    cursor.Emit(OpCodes.Ldloc, 17);
                    cursor.Emit(OpCodes.Ldloc, 18);
                    cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                    cursor.Emit(OpCodes.Stloc, 9);
                    cursor.Emit(OpCodes.Ldloca, 9);
                    cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                    cursor.Emit(OpCodes.Ldind_U4);
                    cursor.EmitDelegate(ModContent.TileType<InfectedPlanteraBulb>);
                    cursor.Emit(OpCodes.Bne_Un, label);
                }

                label = cursor.DefineLabel();
                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.LifeFruit), i => i.MatchBneUn(out label)))
                {
                    cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                    cursor.Emit(OpCodes.Ldloc, 21);
                    cursor.Emit(OpCodes.Ldloc, 22);
                    cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", [typeof(int), typeof(int)]));
                    cursor.Emit(OpCodes.Stloc, 9);
                    cursor.Emit(OpCodes.Ldloca, 9);
                    cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                    cursor.Emit(OpCodes.Ldind_U4);
                    cursor.EmitDelegate(ModContent.TileType<InfectedLifeFruit>);
                    cursor.Emit(OpCodes.Bne_Un, label);
                }
            }
            else
            {
                cursor.TryGotoNext(i => i.MatchLdcI4(TileID.LifeFruit));
            }

            for(int i = 0; i < 2; i++)
            {
                label = cursor.DefineLabel();
                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdsfld(typeof(WorldGen).GetField(nameof(WorldGen.AllowedToSpreadInfections)))))
                {
                    cursor.Emit(OpCodes.Brfalse, label);
                    cursor.EmitDelegate(() => Main.hardMode);

                    cursor.Index++;
                    cursor.MarkLabel(label);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                label = cursor.DefineLabel();
                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdsfld(typeof(WorldGen).GetField(nameof(WorldGen.AllowedToSpreadInfections))), i => i.MatchBrfalse(out label)))
                {
                    cursor.EmitDelegate(() => Main.hardMode);
                    cursor.Emit(OpCodes.Brfalse, label);
                }
            }

            ushort[] goodGrass = [TileID.HallowedGrass, TileID.GolfGrassHallowed];

            foreach(ushort grass in goodGrass)
            {
                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(grass)))
                {
                    cursor.Emit(OpCodes.Pop);
                    cursor.Emit(OpCodes.Ldc_I4_M1);
                }
            }

            if(!cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.CorruptGrass))) return;

            ushort[] evilGrass = [TileID.CorruptGrass, TileID.CrimsonGrass];

            foreach (ushort grass in evilGrass)
            {
                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(grass)))
                {
                    cursor.Emit(OpCodes.Pop);
                    cursor.Emit(OpCodes.Ldc_I4_M1);
                }
            }
        }

        private static void WorldGen_hardUpdateWorld(ILContext il)
        {
            ILCursor cursor = new(il);
            ILLabel label = cursor.DefineLabel();

            if (cursor.TryGotoNext(i => i.MatchLdcI4(1), i => i.MatchStloc(15), i => i.MatchBr(out label)))
            {
                cursor.EmitDelegate(() => NPC.downedPlantBoss);
                cursor.Emit(OpCodes.Brtrue, label);
                cursor.Emit(OpCodes.Ldc_I4_0);
                cursor.Emit(OpCodes.Stloc, 15);
            }
        }

        private static bool WorldGen_nearbyChlorophyte(On_WorldGen.orig_nearbyChlorophyte orig, int i, int j)
        {
            if (!NPC.downedPlantBoss)
            {
                return false;
            }
            return orig(i, j);
        }

        private static void WorldGen_CheckLilyPad(ILContext il)
        {
            ILCursor cursor = new(il);
            ILLabel label = cursor.DefineLabel();
            ILLabel nextLabel = cursor.DefineLabel();

            if (cursor.TryGotoNext(MoveType.After, i => i.MatchStloc2()))
            {
                cursor.Emit(OpCodes.Ldloc_1);
                cursor.Emit(OpCodes.Ldc_I4, TileID.CorruptGrass);
                cursor.Emit(OpCodes.Beq, label);
                cursor.Emit(OpCodes.Ldloc_1);
                cursor.Emit(OpCodes.Ldc_I4, TileID.CorruptJungleGrass);
                cursor.Emit(OpCodes.Bne_Un, nextLabel);

                cursor.MarkLabel(label);
                cursor.Emit(OpCodes.Ldc_I4, 72);
                cursor.Emit(OpCodes.Stloc_2);
                cursor.MarkLabel(nextLabel);

                label = cursor.DefineLabel();
                nextLabel = cursor.DefineLabel();

                cursor.Emit(OpCodes.Ldloc_1);
                cursor.Emit(OpCodes.Ldc_I4, TileID.CrimsonGrass);
                cursor.Emit(OpCodes.Beq, label);
                cursor.Emit(OpCodes.Ldloc_1);
                cursor.Emit(OpCodes.Ldc_I4, TileID.CrimsonJungleGrass);
                cursor.Emit(OpCodes.Bne_Un, nextLabel);

                cursor.MarkLabel(label);
                cursor.Emit(OpCodes.Ldc_I4, 54);
                cursor.Emit(OpCodes.Stloc_2);
                cursor.MarkLabel(nextLabel);
            }

            if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes && cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.HallowedGrass)))
            {
                cursor.Emit(OpCodes.Pop);
                cursor.EmitDelegate(ModContent.TileType<HallowedJungleGrass>);
            }
        }

        private static void WorldGen_CheckCatTail(ILContext il)
        {
            ILCursor cursor = new(il);
            ILLabel label = cursor.DefineLabel();
            ILLabel nextLabel = cursor.DefineLabel();

            if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(-1), i => i.MatchStloc(6)))
            {
                cursor.Emit(OpCodes.Ldloc, 5);
                cursor.Emit(OpCodes.Ldc_I4, TileID.JungleGrass);
                cursor.Emit(OpCodes.Bne_Un, label);

                cursor.Emit(OpCodes.Ldc_I4, 18);
                cursor.Emit(OpCodes.Stloc, 6);
                cursor.MarkLabel(label);
                label = cursor.DefineLabel();

                cursor.Emit(OpCodes.Ldloc, 5);
                cursor.Emit(OpCodes.Ldc_I4, TileID.HallowedGrass);
                cursor.Emit(OpCodes.Beq, label);

                if(ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes)
                {
                    cursor.Emit(OpCodes.Ldloc, 5);
                    cursor.EmitDelegate(ModContent.TileType<HallowedJungleGrass>);
                    cursor.Emit(OpCodes.Beq, label);
                }

                cursor.Emit(OpCodes.Ldloc, 5);
                cursor.Emit(OpCodes.Ldc_I4, TileID.Pearlsand);
                cursor.Emit(OpCodes.Bne_Un, nextLabel);

                cursor.MarkLabel(label);
                cursor.Emit(OpCodes.Ldc_I4, 36);
                cursor.Emit(OpCodes.Stloc, 6);
                cursor.MarkLabel(nextLabel);
            }
        }

        private static void WorldGen_GetDesiredStalagtiteStyle(On_WorldGen.orig_GetDesiredStalagtiteStyle orig, int x, int j, out bool fail, out int desiredStyle, out int height, out int y)
        {
            orig(x, j, out fail, out desiredStyle, out height, out y);

            if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes)
            {
                if (desiredStyle == TileUtilities.GetSnowType(InfectionType.Corrupt))
                {
                    desiredStyle = 9;
                    fail = false;
                    return;
                }
                else if (desiredStyle == TileUtilities.GetSnowType(InfectionType.Crimson))
                {
                    desiredStyle = 10;
                    fail = false;
                    return;
                }
                else if (desiredStyle == TileUtilities.GetSnowType(InfectionType.Hallowed))
                {
                    desiredStyle = 8;
                    fail = false;
                    return;
                }
            }

            if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedMosses)
            {
                foreach (MossType mossType in Enum.GetValues(typeof(MossType)))
                {
                    if (desiredStyle == TileUtilities.GetMossType(InfectionType.Corrupt, mossType))
                    {
                        desiredStyle = 2;
                        fail = false;
                        return;
                    }
                    else if (desiredStyle == TileUtilities.GetMossType(InfectionType.Crimson, mossType))
                    {
                        desiredStyle = 3;
                        fail = false;
                        return;
                    }
                    else if (desiredStyle == TileUtilities.GetMossType(InfectionType.Hallowed, mossType))
                    {
                        desiredStyle = 1;
                        fail = false;
                        return;
                    }
                }
            }
        }

        public void Unload() { }
    }
}