using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using InfectedQualities.Helpers;
using MonoMod.Cil;
using Mono.Cecil.Cil;

namespace InfectedQualities.Common.GlobalTiles
{
    public class TweaksAndFixes : GlobalTile
    {
        public override void Load()
        {
            IL_WorldGen.CheckLilyPad += (ILContext il) =>
            {
                ILCursor cursor = new(il);

                if (cursor.TryGotoNext(i => i.MatchStloc(2)))
                {
                    cursor.Index += 2;

                    ILLabel label = cursor.DefineLabel();
                    ILLabel nextLabel = cursor.DefineLabel();

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
            };

            IL_WorldGen.PlaceLilyPad += (ILContext il) =>
            {
                ILCursor cursor = new(il);

                if (cursor.TryGotoNext(i => i.MatchLdcI4(-1)))
                {
                    cursor.Index += 2;

                    ILLabel label = cursor.DefineLabel();
                    ILLabel nextLabel = cursor.DefineLabel();

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
            };

            IL_WorldGen.CheckCatTail += (ILContext il) =>
            {
                ILCursor cursor = new(il);

                if (cursor.TryGotoNext(i => i.MatchLdcI4(-1)))
                {
                    cursor.Index += 2;

                    ILLabel label = cursor.DefineLabel();

                    cursor.Emit(OpCodes.Ldloc, 5);
                    cursor.Emit(OpCodes.Ldc_I4, TileID.JungleGrass);
                    cursor.Emit(OpCodes.Bne_Un, label);
                    cursor.Emit(OpCodes.Ldc_I4, 18);
                    cursor.Emit(OpCodes.Stloc, 6);

                    cursor.MarkLabel(label);

                    label = cursor.DefineLabel();
                    ILLabel nextLabel = cursor.DefineLabel();

                    cursor.Emit(OpCodes.Ldloc, 5);
                    cursor.Emit(OpCodes.Ldc_I4, TileID.HallowedGrass);
                    cursor.Emit(OpCodes.Beq, label);
                    cursor.Emit(OpCodes.Ldloc, 5);
                    cursor.Emit(OpCodes.Ldc_I4, TileID.Pearlsand);
                    cursor.Emit(OpCodes.Bne_Un, nextLabel);

                    cursor.MarkLabel(label);

                    cursor.Emit(OpCodes.Ldc_I4, 36);
                    cursor.Emit(OpCodes.Stloc, 6);

                    cursor.MarkLabel(nextLabel);
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
                    cursor.Emit(OpCodes.Ldc_I4, TileID.JungleGrass);
                    cursor.Emit(OpCodes.Bne_Un, label);
                    cursor.Emit(OpCodes.Ldc_I4, 18);
                    cursor.Emit(OpCodes.Stloc, 7);

                    cursor.MarkLabel(label);

                    label = cursor.DefineLabel();
                    ILLabel nextLabel = cursor.DefineLabel();

                    cursor.Emit(OpCodes.Ldloc, 6);
                    cursor.Emit(OpCodes.Ldc_I4, TileID.HallowedGrass);
                    cursor.Emit(OpCodes.Beq, label);
                    cursor.Emit(OpCodes.Ldloc, 6);
                    cursor.Emit(OpCodes.Ldc_I4, TileID.Pearlsand);
                    cursor.Emit(OpCodes.Bne_Un, nextLabel);

                    cursor.MarkLabel(label);

                    cursor.Emit(OpCodes.Ldc_I4, 36);
                    cursor.Emit(OpCodes.Stloc, 7);

                    cursor.MarkLabel(nextLabel);
                }
            };
        }

        public override void SetStaticDefaults()
        {
            TileID.Sets.AddCorruptionTile(TileID.CorruptVines);
            TileID.Sets.AddCrimsonTile(TileID.CrimsonVines);
            TileID.Sets.HallowBiome[TileID.HallowedVines] = 1;

            TileID.Sets.AddCrimsonTile(TileID.CrimsonPlants);
            TileID.Sets.AddJungleTile(TileID.JungleThorns);

            TileID.Sets.CanGrowCrystalShards[TileID.HallowedGrass] = true;

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                TileUtils.IDSets.TileConversionMud[calamityMod.Find<ModTile>("AstralMud").Type] = true;
                TileUtils.IDSets.TilePlant[calamityMod.Find<ModTile>("AstralShortPlants").Type] = true;
                TileUtils.IDSets.TilePlant[calamityMod.Find<ModTile>("AstralTallPlants").Type] = true;

                if (ModLoader.TryGetMod("CalValEX", out Mod calamityVanities))
                {
                    TileUtils.IDSets.TileConversionMud[calamityVanities.Find<ModTile>("AstralMudPlaced").Type] = true;
                    TileUtils.IDSets.TilePlant[calamityVanities.Find<ModTile>("AstralShortGrass").Type] = true;
                    TileUtils.IDSets.TilePlant[calamityVanities.Find<ModTile>("AstralTallGrass").Type] = true;
                }
            }
        }

        public override bool TileFrame(int i, int j, int type, ref bool resetFrame, ref bool noBreak)
        {
            if (ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().RemoveThorns)
            {
                if (TileID.Sets.Conversion.Thorn[type] && type != TileID.PlanteraThorns)
                {
                    WorldGen.KillTile(i, j);
                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j);
                }
            }

            if (ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableLimeSolution)
            {
                if (type == TileID.JunglePlants)
                {
                    if (Main.tile[i, j].TileFrameX == 144 || Main.tile[i, j].TileFrameX == 162)
                    {
                        if (j <= Main.rockLayer && !Main.remixWorld)
                        {
                            WorldGen.KillTile(i, j, noItem: true);
                        }
                    }
                }
            }
            return true;
        }

        public override void RandomUpdate(int i, int j, int type)
        {
            if (type == TileID.Ebonstone || type == TileID.Crimstone || type == TileID.Pearlstone)
            {
                int x = i + WorldGen.genRand.Next(-2, 3);
                int y = j + WorldGen.genRand.Next(-2, 3);

                if (Main.tile[x, y].WallType == WallID.Stone)
                {
                    ushort wallType = type == TileID.Crimstone ? WallID.CrimstoneUnsafe : type == TileID.Pearlstone ? WallID.PearlstoneBrickUnsafe : WallID.EbonstoneUnsafe;
                    Main.tile[x, y].WallType = wallType;
                    WorldGen.SquareWallFrame(x, y);
                    NetMessage.SendTileSquare(-1, x, y);
                }
            }

            if(type == TileID.CorruptPlants)
            {
                TileUtils.SpreadInfection(i, j, 0);
            }

            if (type == TileID.CorruptGrass || type == TileID.Ebonstone || type == TileID.Ebonsand || type == TileID.CorruptSandstone || type == TileID.CorruptHardenedSand || type == TileID.CorruptIce || type == TileID.CorruptJungleGrass || type == TileID.CorruptThorns  || type == TileID.CorruptVines)
            {
                TileUtils.SpreadInfectionFixCompatilbility(i, j, 0);
            }
            else if (type == TileID.CrimsonGrass || type == TileID.Crimstone || type == TileID.Crimsand || type == TileID.CrimsonSandstone || type == TileID.CrimsonHardenedSand || type == TileID.FleshIce || type == TileID.CrimsonJungleGrass || type == TileID.CrimsonThorns || type == TileID.CrimsonPlants || type == TileID.CrimsonVines)
            {
                TileUtils.SpreadInfectionFixCompatilbility(i, j, 1);
            }
            else if (type == TileID.HallowedGrass || type == TileID.Pearlstone || type == TileID.Pearlsand || type == TileID.HallowSandstone || type == TileID.HallowHardenedSand || type == TileID.HallowedIce || type == TileID.GolfGrassHallowed || type == TileID.HallowedPlants || type == TileID.HallowedPlants2 || type == TileID.HallowedVines)
            {
                TileUtils.SpreadInfectionFixCompatilbility(i, j, 2);
            }
        }

        public override void NearbyEffects(int i, int j, int type, bool closer)
        {
            if (ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().RemoveThorns)
            {
                if (TileID.Sets.Conversion.Thorn[type] && type != TileID.PlanteraThorns)
                {
                    WorldGen.KillTile(i, j);
                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j);
                }
            }
        }
    }
}
