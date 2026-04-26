using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using InfectedQualities.Content.Tiles.Plants;
using InfectedQualities.Core;
using InfectedQualities.Utilities;

namespace InfectedQualities.Content.Tiles;

public class HallowedJungleGrass : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileShine[Type] = 9000;
        Main.tileLighted[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;

        TileID.Sets.CanGrowCrystalShards[Type] = true;
        TileID.Sets.CanBeDugByShovel[Type] = true;
        TileID.Sets.ResetsHalfBrickPlacementAttempt[Type] = false;
        TileID.Sets.DoesntPlaceWithTileReplacement[Type] = true;
        TileID.Sets.GrassSpecial[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
        TileID.Sets.SpreadOverground[Type] = true;
        TileID.Sets.SpreadUnderground[Type] = true;

        TileID.Sets.Hallow[Type] = true;
        TileID.Sets.HallowBiome[Type] = 1;
        TileID.Sets.HallowBiomeSight[Type] = true;
        TileID.Sets.HallowCountCollection.Add(Type);

        TileID.Sets.AddJungleTile(Type, 2);
        TileID.Sets.Conversion.JungleGrass[Type] = true;

        HitSound = SoundID.Grass;
        DustType = DustID.HallowedPlants;
        RegisterItemDrop(ItemID.MudBlock);
        AddMapEntry(new(78, 193, 227));

			TileLoader.RegisterSimpleConversion(TileID.JungleGrass, BiomeConversionID.Hallow, Type);
        TileLoader.RegisterConversionFallback(Type, TileID.JungleGrass, BiomeConversionID.Hallow, BiomeConversionID.GlowingMushroom);
			VanillaFallbackOnModDeletion = TileID.JungleGrass;
		}

		public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        if (TileUtilities.TileExposedToLava(i, j))
        {
            Main.tile[i, j].TileType = TileID.Mud;
            WorldGen.SquareTileFrame(i, j);
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendTileSquare(-1, i, j);
            }
        }
        return true;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 3 : 10;

    public override bool CreateDust(int i, int j, ref int type)
    {
        if (WorldGen.genRand.NextBool())
        {
            type = DustID.Mud;
        }
        return true;
    }

    public override void RandomUpdate(int i, int j)
    {
        WorldGen.SpreadInfectionToNearbyTile(i, j, BiomeConversionID.Hallow);

        if (WorldGen.genRand.NextBool(10) && !Main.tile[i, j - 1].HasTile && WorldGen.PlaceTile(i, j - 1, TileID.HallowedPlants, true))
        {
            if (j > Main.worldSurface && WorldGen.genRand.NextBool(16))
            {
                Main.tile[i, j - 1].TileType = (ushort)ModContent.TileType<HallowedThorns>();
                WorldGen.SquareTileFrame(i, j - 1);
            }
            Main.tile[i, j - 1].CopyPaintAndCoating(Main.tile[i, j]);
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendTileSquare(-1, i, j);
            }
        }

        if (WorldGen.genRand.NextBool(60) && WorldGen.GrowMoreVines(i, j) && !Main.tile[i, j + 1].HasTile && Main.tile[i, j + 1].LiquidType != LiquidID.Lava && Main.tile[i, j].BottomSlope)
        {
            Main.tile[i, j + 1].TileType = TileID.HallowedVines;
            Main.tile[i, j + 1].Get<TileWallWireStateData>().HasTile = true;
            Main.tile[i, j + 1].CopyPaintAndCoating(Main.tile[i, j]);
            WorldGen.SquareTileFrame(i, j + 1);
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendTileSquare(-1, i, j + 1);
            }
        }

        for (int x = i - 1; x < i + 2; x++)
        {
            for (int y = j - 1; y < j + 2; y++)
            {
                ushort tileType = Main.tile[x, y].TileType;
                bool flag = Main.hardMode && WorldGen.AllowedToSpreadInfections && Main.tile[x, y - 1].TileType != TileID.Sunflower;

                if (tileType == TileID.Mud || (tileType == TileID.JungleGrass && flag))
                {
                    WorldGen.SpreadGrass(x, y, tileType, Type, false, Main.tile[i, j].BlockColorAndCoating());
                    WorldGen.SquareTileFrame(x, y);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, x, y);
                    }
                }
                else if (tileType == TileID.Grass && flag)
                {
                    WorldGen.SpreadGrass(x, y, tileType, TileID.HallowedGrass, false, Main.tile[i, j].BlockColorAndCoating());
                    WorldGen.SquareTileFrame(x, y);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, x, y);
                    }
                }
                else if (tileType == TileID.GolfGrass && flag)
                {
                    WorldGen.SpreadGrass(x, y, tileType, TileID.GolfGrassHallowed, false, Main.tile[i, j].BlockColorAndCoating());
                    WorldGen.SquareTileFrame(x, y);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, x, y);
                    }
                }
            }
        }

        if (!Main.remixWorld && WorldGen.genRand.NextBool(500) && (!Main.tile[i, j - 1].HasTile || TileID.Sets.IgnoredByGrowingSaplings[Main.tile[i, j - 1].TileType]))
        {
            TileUtilities.TryToGrowTree(i, j, j >= Main.worldSurface);
        }

        if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && WorldGen.genRand.NextBool(60))
        {
            TileUtilities.AttemptToPlaceInfectedPlant(i, j, ModContent.TileType<InfectedPlanteraBulb>(), TileID.PlanteraBulb, 150);
        }

        if (NPC.downedMechBossAny && WorldGen.genRand.NextBool(Main.expertMode ? 30 : 40))
        {
            TileUtilities.AttemptToPlaceInfectedPlant(i, j, ModContent.TileType<InfectedLifeFruit>(), TileID.LifeFruit, Main.expertMode ? 50 : 60);
        }
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (fail && !effectOnly)
        {
            Main.tile[i, j].TileType = TileID.Mud;
            WorldGen.SquareTileFrame(i, j);
        }
    }

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes;
}