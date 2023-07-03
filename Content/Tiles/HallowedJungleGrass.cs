using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using System.Reflection;
using InfectedQualities.Helpers;
using InfectedQualities.Common;

namespace InfectedQualities.Content.Tiles
{
    public class HallowedJungleGrass : ModTile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().EnableInfectedJungleBiomes;
        }

        public override string Texture => "InfectedQualities/Client/Assets/Tiles/HallowedJungleGrass";

        public override void SetStaticDefaults()
        {
            Main.tileShine[Type] = 9000;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileBrick[Type] = true;

            TileID.Sets.CanGrowCrystalShards[Type] = true;
            TileID.Sets.CanBeDugByShovel[Type] = true;
            TileID.Sets.ResetsHalfBrickPlacementAttempt[Type] = true;
            TileID.Sets.DoesntPlaceWithTileReplacement[Type] = true;
            TileID.Sets.GrassSpecial[Type] = true;
            TileID.Sets.ChecksForMerge[Type] = true;
            TileID.Sets.SpreadOverground[Type] = true;
            TileID.Sets.SpreadUnderground[Type] = true;

            TileID.Sets.Hallow[Type] = true;
            TileID.Sets.HallowBiome[Type] = 1;
            TileID.Sets.HallowBiomeSight[Type] = true;
            TileID.Sets.HallowCountCollection.Add(Type);

            TileID.Sets.AddJungleTile(Type);
            TileID.Sets.Conversion.JungleGrass[Type] = true;

            RegisterItemDrop(ItemID.MudBlock);
            DustType = DustID.HallowedPlants;
            HitSound = SoundID.Grass;

            AddMapEntry(new(78, 193, 227));
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            if (TileUtils.LavaCheck(i, j))
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

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void RandomUpdate(int i, int j)
        {
            TileUtils.SpreadInfection(i, j, 2);

            if (Main.hardMode && Main.remixWorld)
            {
                MethodInfo nearChlorophyte = typeof(WorldGen).GetMethod("nearbyChlorophyte", BindingFlags.Static | BindingFlags.NonPublic);
                if ((bool)nearChlorophyte?.Invoke(null, new object[] { i, j }))
                {
                    Main.tile[i, j].TileType = TileID.JungleGrass;
                    WorldGen.SquareTileFrame(i, j);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, i, j);
                    }
                }
            }

            Tile currentTile = Main.tile[i, j];
            Tile aboveTile = Main.tile[i, j - 1];
            Tile bellowTile = Main.tile[i, j + 1];

            if (WorldGen.genRand.NextBool(10) && !aboveTile.HasTile && !currentTile.IsHalfBlock && currentTile.Slope == 0)
            {
                if (WallID.Sets.AllowsPlantsToGrow[aboveTile.WallType] && WallID.Sets.AllowsPlantsToGrow[currentTile.WallType])
                {
                    aboveTile.HasTile = true;
                    aboveTile.TileType = TileID.HallowedPlants;

                    if (WorldGen.genRand.NextBool(50))
                    {
                        aboveTile.TileFrameX = 144;
                    }
                    else if (WorldGen.genRand.NextBool(35) || aboveTile.WallType >= WallID.GrassUnsafe && aboveTile.WallType <= WallID.HallowedGrassUnsafe)
                    {
                        aboveTile.TileFrameX = (short)(WorldGen.genRand.NextFromList(6, 7, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20) * 18);
                    }
                    else
                    {
                        aboveTile.TileFrameX = (short)(WorldGen.genRand.Next(6) * 18);
                    }
                    WorldGen.SquareTileFrame(i, j - 1);
                    aboveTile.CopyPaintAndCoating(currentTile);
                }
            }

            if (WorldGen.genRand.NextBool(60) && !bellowTile.HasTile && !currentTile.BottomSlope && bellowTile.LiquidType != LiquidID.Lava)
            {
                bellowTile.HasTile = true;
                bellowTile.TileType = TileID.HallowedVines;
                bellowTile.CopyPaintAndCoating(currentTile);
                WorldGen.SquareTileFrame(i, j + 1);
            }

            for (int num21 = i - 1; num21 < i + 2; num21++)
            {
                for (int num22 = j - 1; num22 < j + 2; num22++)
                {
                    if (i != num21 || j != num22)
                    {
                        Tile tile = Main.tile[num21, num22];
                        if ((Main.hardMode && WorldGen.AllowedToSpreadInfections && WorldGen.CountNearBlocksTypes(num21, num22, 2, 1, new int[] { TileID.Sunflower }) <= 0 && tile.TileType == TileID.JungleGrass) || tile.TileType == TileID.Mud)
                        {
                            WorldGen.SpreadGrass(num21, num22, tile.TileType, Type, false, currentTile.BlockColorAndCoating());
                            WorldGen.SquareTileFrame(num21, num22);
                            if (Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.SendTileSquare(-1, i, j, 3);
                            }
                        }
                    }
                }
            }

            if (!TileUtils.TileUnderground(j) || Main.remixWorld)
            {
                if (WorldGen.genRand.NextBool(500) && (!aboveTile.HasTile || TileID.Sets.IgnoredByGrowingSaplings[aboveTile.TileType]))
                {
                    if (WorldGen.GrowTree(i, j) && WorldGen.PlayerLOS(i, j))
                    {
                        WorldGen.TreeGrowFXCheck(i, j - 1);
                    }
                }
            }
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail && !effectOnly)
            {
                Main.tile[i, j].TileType = TileID.Mud;
                WorldGen.SquareTileFrame(i, j);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendTileSquare(-1, i, j);
                }
            }
        }
    }
}