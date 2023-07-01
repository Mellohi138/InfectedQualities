using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using InfectedQualities.Content.Tiles;
using InfectedQualities.Helpers;
using Terraria.GameContent.Drawing;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using System;

namespace InfectedQualities.Common.GlobalTiles
{
    public class SnowTileTweaks : GlobalTile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig>().EnableInfectedSnowBiomes;
        }

        public override void Load()
        {
            On_TileDrawing.GetTreeVariant += (orig, x, y) =>
            {
                if (Main.tile[x, y].TileType == ModContent.TileType<CorruptSnow>())
                {
                    return 0;
                }
                else if (Main.tile[x, y].TileType == ModContent.TileType<CrimsonSnow>())
                {
                    return 4;
                }
                else if (Main.tile[x, y].TileType == ModContent.TileType<HallowedSnow>())
                {
                    return 2;
                }
                return orig(x, y);
            };

            IL_WorldGen.CheckAlch += (ILContext il) =>
            {
                ILCursor cursor = new(il);

                for (int i = 0; i < 4; i++)
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
                cursor.Emit(OpCodes.Ldc_I4, TileID.CorruptIce);
                cursor.Emit(OpCodes.Beq, label);

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
                cursor.EmitDelegate(() => ModContent.TileType<CorruptSnow>());
                cursor.Emit(OpCodes.Beq, label);

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
                cursor.Emit(OpCodes.Ldc_I4, TileID.FleshIce);
                cursor.Emit(OpCodes.Beq, label);

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
                cursor.EmitDelegate(() => ModContent.TileType<CrimsonSnow>());
                cursor.Emit(OpCodes.Beq, label);

                cursor.Index += 2;

                cursor.MarkLabel(label);

                for (int i = 0; i < 2; i++)
                {
                    if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.PlanterBox)))
                    {
                        return;
                    }
                }

                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.CorruptIce)))
                {
                    cursor.Emit(OpCodes.Pop);
                    cursor.Emit(OpCodes.Ldc_I4_M1);
                }

                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.FleshIce)))
                {
                    cursor.Emit(OpCodes.Pop);
                    cursor.EmitDelegate(() => ModContent.TileType<HallowedSnow>());
                }
            };

            IL_WorldGen.PlaceAlch += (ILContext il) =>
            {
                ILCursor cursor = new(il);

                for (int i = 0; i < 4; i++)
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
                cursor.Emit(OpCodes.Ldc_I4, TileID.CorruptIce);
                cursor.Emit(OpCodes.Beq, label);

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
                cursor.EmitDelegate(() => ModContent.TileType<CorruptSnow>());
                cursor.Emit(OpCodes.Beq, label);

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
                cursor.Emit(OpCodes.Ldc_I4, TileID.FleshIce);
                cursor.Emit(OpCodes.Beq, label);

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
                cursor.EmitDelegate(() => ModContent.TileType<CrimsonSnow>());
                cursor.Emit(OpCodes.Beq, label);

                cursor.Index += 2;

                cursor.MarkLabel(label);

                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.CorruptIce)))
                {
                    cursor.Emit(OpCodes.Pop);
                    cursor.Emit(OpCodes.Ldc_I4_M1);
                }

                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.FleshIce)))
                {
                    cursor.Emit(OpCodes.Pop);
                    cursor.EmitDelegate(() => ModContent.TileType<HallowedSnow>());
                }
            };

            IL_WorldGen.PlantAlch += (ILContext il) =>
            {
                ILCursor cursor = new(il);

                if(cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.CrimsonGrass)))
                {
                    ILLabel label = cursor.DefineLabel();
                    if(cursor.TryGotoNext(MoveType.After, i => i.MatchBeq(out label)))
                    {
                        cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                        cursor.Emit(OpCodes.Ldloc_0);
                        cursor.Emit(OpCodes.Ldloc_1);
                        cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", new Type[] { typeof(int), typeof(int) }));
                        cursor.Emit(OpCodes.Stloc, 8);
                        cursor.Emit(OpCodes.Ldloca, 8);
                        cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                        cursor.Emit(OpCodes.Ldind_U2);
                        cursor.EmitDelegate(() => ModContent.TileType<CorruptSnow>());
                        cursor.Emit(OpCodes.Beq, label);

                        cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                        cursor.Emit(OpCodes.Ldloc_0);
                        cursor.Emit(OpCodes.Ldloc_1);
                        cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", new Type[] { typeof(int), typeof(int) }));
                        cursor.Emit(OpCodes.Stloc, 8);
                        cursor.Emit(OpCodes.Ldloca, 8);
                        cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                        cursor.Emit(OpCodes.Ldind_U2);
                        cursor.Emit(OpCodes.Ldc_I4, TileID.CorruptIce);
                        cursor.Emit(OpCodes.Beq, label);

                        cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                        cursor.Emit(OpCodes.Ldloc_0);
                        cursor.Emit(OpCodes.Ldloc_1);
                        cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", new Type[] { typeof(int), typeof(int) }));
                        cursor.Emit(OpCodes.Stloc, 8);
                        cursor.Emit(OpCodes.Ldloca, 8);
                        cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                        cursor.Emit(OpCodes.Ldind_U2);
                        cursor.EmitDelegate(() => ModContent.TileType<CrimsonSnow>());
                        cursor.Emit(OpCodes.Beq, label);

                        cursor.Emit(OpCodes.Ldsflda, typeof(Main).GetField("tile"));
                        cursor.Emit(OpCodes.Ldloc_0);
                        cursor.Emit(OpCodes.Ldloc_1);
                        cursor.Emit(OpCodes.Call, typeof(Tilemap).GetMethod("get_Item", new Type[] { typeof(int), typeof(int) }));
                        cursor.Emit(OpCodes.Stloc, 8);
                        cursor.Emit(OpCodes.Ldloca, 8);
                        cursor.Emit(OpCodes.Call, typeof(Tile).GetMethod("get_TileType"));
                        cursor.Emit(OpCodes.Ldind_U2);
                        cursor.Emit(OpCodes.Ldc_I4, TileID.FleshIce);
                        cursor.Emit(OpCodes.Beq, label);
                    }
                }

                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.CorruptIce)))
                {
                    cursor.Emit(OpCodes.Pop);
                    cursor.Emit(OpCodes.Ldc_I4_M1);
                }

                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.FleshIce)))
                {
                    cursor.Emit(OpCodes.Pop);
                    cursor.EmitDelegate(() => ModContent.TileType<HallowedSnow>());
                }
            };

            IL_WorldGen.GetDesiredStalagtiteStyle += (ILContext il) =>
            {
                ILCursor cursor = new(il);

                ILLabel label = cursor.DefineLabel();

                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.FleshIce)))
                {
                    cursor.Emit(OpCodes.Beq, label);
                    cursor.Emit(OpCodes.Ldarg_3);
                    cursor.Emit(OpCodes.Ldind_I4);
                    cursor.EmitDelegate(() => ModContent.TileType<CrimsonSnow>());
                    cursor.Index++;
                }

                cursor.MarkLabel(label);

                label = cursor.DefineLabel();

                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.HallowedIce)))
                {
                    cursor.Emit(OpCodes.Beq, label);
                    cursor.Emit(OpCodes.Ldarg_3);
                    cursor.Emit(OpCodes.Ldind_I4);
                    cursor.EmitDelegate(() => ModContent.TileType<HallowedSnow>());
                    cursor.Index++;
                }

                cursor.MarkLabel(label);

                label = cursor.DefineLabel();

                if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdcI4(TileID.CorruptIce)))
                {
                    cursor.Emit(OpCodes.Beq, label);
                    cursor.Emit(OpCodes.Ldarg_3);
                    cursor.Emit(OpCodes.Ldind_I4);
                    cursor.EmitDelegate(() => ModContent.TileType<CorruptSnow>());
                    cursor.Index++;
                }

                cursor.MarkLabel(label);
            };
        }

        public override void SetStaticDefaults()
        {
            Main.tileMerge[TileID.SnowBlock][TileID.CorruptIce] = true;
            Main.tileMerge[TileID.SnowBlock][TileID.FleshIce] = true;
            Main.tileMerge[TileID.SnowBlock][TileID.HallowedIce] = true;
        }

        public override bool TileFrame(int i, int j, int type, ref bool resetFrame, ref bool noBreak)
        {
            if (type == TileID.CorruptIce || type == TileID.FleshIce || type == TileID.HallowedIce)
            {
                TileFramer.GetTileSurroundings(i, j, out int upLeft, out int up, out int upRight, out int left, out int right, out int downLeft, out int down, out int downRight);
                WorldGen.TileMergeAttempt(-2, TileID.Sets.Snow, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                TileFramer.CustomTileFrame(i, j, ref upLeft, ref up, ref upRight, ref left, ref right, ref downLeft, ref down, ref downRight, resetFrame);
                return false;
            }
            return true;
        }
    }
}
