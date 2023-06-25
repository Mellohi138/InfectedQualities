using InfectedQualities.Helpers;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using MonoMod.Cil;
using InfectedQualities.Content.Tiles;
using Terraria.GameContent.Drawing;
using Mono.Cecil.Cil;
using System;

namespace InfectedQualities.Common.GlobalTiles
{
    public class JungleTileTweaks : GlobalTile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig>().EnableInfectedJungleBiomes;
        }

        public override void Load()
        {
            On_TileDrawing.GetTreeVariant += (orig, x, y) =>
            {
                if (Main.tile[x, y].TileType == ModContent.TileType<HallowedJungleGrass>())
                {
                    return 2;
                }
                return orig(x, y);
            };

            IL_WorldGen.CheckAlch += (ILContext il) =>
            {
                ILCursor cursor = new(il);

                for (int i = 0; i < 2; i++)
                {
                    if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.PlanterBox)))
                    {
                        return;
                    }
                }

                cursor.Index++;

                ILLabel label = cursor.DefineLabel();

                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.Emit(OpCodes.Ldarg_1);
                cursor.Emit(OpCodes.Ldc_I4_1);
                cursor.Emit(OpCodes.Add);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", new Type[] { typeof(int), typeof(int) }));
                cursor.Emit(OpCodes.Stloc_2);
                cursor.Emit(OpCodes.Ldloca, 2);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.EmitDelegate(() => ModContent.TileType<HallowedJungleGrass>());
                cursor.Emit(OpCodes.Beq, label);

                cursor.Index += 2;

                cursor.MarkLabel(label);
            };

            IL_WorldGen.PlaceAlch += (ILContext il) =>
            {
                ILCursor cursor = new(il);

                for (int i = 0; i < 2; i++)
                {
                    if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.PlanterBox)))
                    {
                        return;
                    }
                }

                cursor.Index++;

                ILLabel label = cursor.DefineLabel();

                cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.Emit(OpCodes.Ldarg_1);
                cursor.Emit(OpCodes.Ldc_I4_1);
                cursor.Emit(OpCodes.Add);
                cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", new Type[] { typeof(int), typeof(int) }));
                cursor.Emit(OpCodes.Stloc_0);
                cursor.Emit(OpCodes.Ldloca, 0);
                cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                cursor.Emit(OpCodes.Ldind_U2);
                cursor.EmitDelegate(() => ModContent.TileType<HallowedJungleGrass>());
                cursor.Emit(OpCodes.Beq, label);

                cursor.Index += 2;

                cursor.MarkLabel(label);
            };

            IL_WorldGen.PlantAlch += (ILContext il) =>
            {
                ILCursor cursor = new(il);

                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.JungleGrass)))
                {
                    ILLabel label = cursor.DefineLabel();
                    cursor.Emit(OpCodes.Beq, label);

                    cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                    cursor.Emit(OpCodes.Ldloc_0);
                    cursor.Emit(OpCodes.Ldloc_1);
                    cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", new Type[] { typeof(int), typeof(int) }));
                    cursor.Emit(OpCodes.Stloc, 8);
                    cursor.Emit(OpCodes.Ldloca, 8);
                    cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                    cursor.Emit(OpCodes.Ldind_U2);
                    cursor.EmitDelegate(() => ModContent.TileType<HallowedJungleGrass>());

                    cursor.Index++;
                    cursor.MarkLabel(label);
                }
            };

            IL_WorldGen.CheckLilyPad += (ILContext il) =>
            {
                ILCursor cursor = new(il);

                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.HallowedGrass)))
                {
                    cursor.Emit(OpCodes.Pop);
                    cursor.EmitDelegate(() => ModContent.TileType<HallowedJungleGrass>());
                }
            };

            IL_WorldGen.PlaceLilyPad += (ILContext il) =>
            {
                ILCursor cursor = new(il);

                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.HallowedGrass)))
                {
                    cursor.Emit(OpCodes.Pop);
                    cursor.EmitDelegate(() => ModContent.TileType<HallowedJungleGrass>());
                }
            };

            IL_WorldGen.CheckCatTail += (ILContext il) =>
            {
                ILCursor cursor = new(il);

                if (cursor.TryGotoNext(i => i.MatchLdcI4(-1)))
                {
                    cursor.Index += 2;

                    ILLabel label = cursor.DefineLabel();

                    cursor.Emit(OpCodes.Ldloc, 5);
                    cursor.EmitDelegate(() => ModContent.TileType<HallowedJungleGrass>());
                    cursor.Emit(OpCodes.Bne_Un, label);
                    cursor.Emit(OpCodes.Ldc_I4, 36);
                    cursor.Emit(OpCodes.Stloc, 6);

                    cursor.MarkLabel(label);
                }
            };

            IL_WorldGen.PlaceCatTail += (ILContext il) =>
            {
                ILCursor cursor = new(il);

                if (cursor.TryGotoNext(i => i.MatchLdcI4(-1)))
                {
                    cursor.Index += 2;

                    ILLabel label = cursor.DefineLabel();

                    cursor.Emit(OpCodes.Ldloc, 6);
                    cursor.EmitDelegate(() => ModContent.TileType<HallowedJungleGrass>());
                    cursor.Emit(OpCodes.Bne_Un, label);
                    cursor.Emit(OpCodes.Ldc_I4, 36);
                    cursor.Emit(OpCodes.Stloc, 7);

                    cursor.MarkLabel(label);
                }
            };

            IL_WorldGen.CheckSunflower += (ILContext il) =>
            {
                ILCursor cursor = new(il);

                ILLabel label = cursor.DefineLabel();
                for (int i = 0; i < 2; i++)
                {
                    if (!cursor.TryGotoNext(MoveType.After, i => i.MatchBeq(out label)))
                    {
                        return;
                    }
                }

                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(633)))
                {
                    cursor.Index++;

                    cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                    cursor.Emit(OpCodes.Ldloc, 6);
                    cursor.Emit(OpCodes.Ldloc_2);
                    cursor.Emit(OpCodes.Ldc_I4_4);
                    cursor.Emit(OpCodes.Add);
                    cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", new Type[] { typeof(int), typeof(int) }));
                    cursor.Emit(OpCodes.Stloc, 5);
                    cursor.Emit(OpCodes.Ldloca, 5);
                    cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                    cursor.Emit(OpCodes.Ldind_U2);
                    cursor.EmitDelegate(() => ModContent.TileType<HallowedJungleGrass>());
                    cursor.Emit(OpCodes.Beq, label);
                }
            };
        }

        public override void SetStaticDefaults()
        {
            TileID.Sets.AddCorruptionTile(TileID.CorruptJungleGrass, 2);
            TileID.Sets.AddCrimsonTile(TileID.CrimsonJungleGrass, 2);

            TileID.Sets.AddJungleTile(TileID.CorruptJungleGrass);
            TileID.Sets.AddJungleTile(TileID.CrimsonJungleGrass);

            TileID.Sets.SlowlyDiesInWater[TileID.CorruptPlants] = false;
            TileID.Sets.SlowlyDiesInWater[TileID.CrimsonPlants] = false;
            TileID.Sets.SlowlyDiesInWater[TileID.HallowedPlants] = false;

            TileID.Sets.RemixJungleBiome[TileID.LihzahrdBrick] = 1;
        }

        public override bool TileFrame(int i, int j, int type, ref bool resetFrame, ref bool noBreak)
        {
            if (TileUtils.IDSets.TilePlant[type])
            {
                Tile soil = Main.tile[i, j + 1];

                if (soil.HasTile && !soil.IsHalfBlock && soil.Slope == 0 && soil.TileType == ModContent.TileType<HallowedJungleGrass>())
                {
                    if (type != TileID.HallowedPlants && type != TileID.HallowedPlants2)
                    {
                        Main.tile[i, j].TileType = TileID.HallowedPlants;
                    }
                    return false;
                }
            }

            if (TileID.Sets.IsVine[type])
            {
                Tile soil = Main.tile[i, j - 1];

                if (soil.HasTile && !soil.BottomSlope && soil.TileType == ModContent.TileType<HallowedJungleGrass>())
                {
                    if (type != TileID.HallowedVines)
                    {
                        Main.tile[i, j].TileType = TileID.HallowedVines;
                        WorldGen.SquareTileFrame(i, j, resetFrame);
                    }
                    else
                    {
                        TileFramer.CustomTileFrame(i, j, resetFrame);
                    }
                    return false;
                }
            }
            return true;
        }

        public override void RandomUpdate(int i, int j, int type)
        {
            if (type == TileID.CorruptJungleGrass || type == TileID.CrimsonJungleGrass)
            {
                if (WorldGen.genRand.NextBool(10) && !Main.tile[i, j - 1].HasTile && Main.tile[i, j - 1].LiquidAmount > 0 && Main.tile[i, j - 1].LiquidType == LiquidID.Water)
                {
                    if (WorldGen.PlaceTile(i, j - 1, type == TileID.CorruptJungleGrass ? TileID.CorruptPlants : TileID.CrimsonPlants))
                    {
                        Main.tile[i, j - 1].CopyPaintAndCoating(Framing.GetTileSafely(i, j));
                    }
                }

                if (!TileUtils.TileUnderground(j) || Main.remixWorld)
                {
                    if (WorldGen.genRand.NextBool(500) && (!Main.tile[i, j - 1].HasTile || TileID.Sets.IgnoredByGrowingSaplings[Main.tile[i, j - 1].TileType]))
                    {
                        if (WorldGen.GrowTree(i, j) && WorldGen.PlayerLOS(i, j))
                        {
                            WorldGen.TreeGrowFXCheck(i, j - 1);
                        }
                    }
                }
            }

            if (type == TileID.HallowedVines)
            {
                if (WorldGen.genRand.NextBool(20) && !Main.tile[i, j + 1].HasTile && Main.tile[i, j + 1].LiquidType != LiquidID.Lava)
                {
                    bool flag = false;
                    for (int num = j; num > j - 10 && num < 0; num--)
                    {
                        if (Main.tile[i, num].HasTile && Main.tile[i, num].TileType == ModContent.TileType<HallowedJungleGrass>() && !Main.tile[i, num].BottomSlope)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        Tile tile = Main.tile[i, j + 1];
                        tile.TileType = TileID.HallowedVines;
                        tile.HasTile = true;
                        tile.CopyPaintAndCoating(Main.tile[i, j]);
                        WorldGen.SquareTileFrame(i, j + 1);
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendTileSquare(-1, i, j + 1);
                        }
                    }
                }
            }
        }
    }
}
