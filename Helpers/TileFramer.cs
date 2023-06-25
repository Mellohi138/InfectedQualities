using Terraria;
using Terraria.ID;

using Microsoft.Xna.Framework;

namespace InfectedQualities.Helpers
{
    public class TileFramer
    {
        public static bool mergeUp;
        public static bool mergeDown;
        public static bool mergeLeft;
        public static bool mergeRight;

        public static void GetTileSurroundings(int i, int j, out int upLeft, out int up, out int upRight, out int left, out int right, out int downLeft, out int down, out int downRight)
        {
            Tile tile = Main.tile[i, j];
            int num = tile.TileType;
            if (Main.tileStone[num])
            {
                num = 1;
            }

            Tile tile38 = Main.tile[i, j - 1];
            Tile tile39 = Main.tile[i, j + 1];
            Tile tile40 = Main.tile[i - 1, j];
            Tile tile41 = Main.tile[i + 1, j];
            Tile tile42 = Main.tile[i - 1, j + 1];
            Tile tile43 = Main.tile[i + 1, j + 1];
            Tile tile44 = Main.tile[i - 1, j - 1];
            Tile tile45 = Main.tile[i + 1, j - 1];

            upLeft = -1;
            up = -1;
            upRight = -1;
            left = -1;
            right = -1;
            downLeft = -1;
            down = -1;
            downRight = -1;

            if (tile40 != null && tile40.HasTile)
            {
                left = (Main.tileStone[tile40.TileType] ? 1 : tile40.TileType);
                if (tile40.Slope == SlopeType.SlopeDownLeft || tile40.Slope == SlopeType.SlopeUpLeft)
                {
                    left = -1;
                }
            }

            if (tile41 != null && tile41.HasTile)
            {
                right = (Main.tileStone[tile41.TileType] ? 1 : tile41.TileType);
                if (tile41.Slope == SlopeType.SlopeDownRight || tile41.Slope == SlopeType.SlopeUpRight)
                {
                    right = -1;
                }
            }

            if (tile38 != null && tile38.HasTile)
            {
                up = (Main.tileStone[tile38.TileType] ? 1 : tile38.TileType);
                if (tile38.Slope == SlopeType.SlopeUpLeft || tile38.Slope == SlopeType.SlopeUpRight)
                {
                    up = -1;
                }
            }

            if (tile39 != null && tile39.HasTile)
            {
                down = (Main.tileStone[tile39.TileType] ? 1 : tile39.TileType);
                if (tile39.Slope == SlopeType.SlopeDownLeft || tile39.Slope == SlopeType.SlopeDownRight)
                {
                    down = -1;
                }
            }

            if (tile44 != null && tile44.HasTile)
            {
                upLeft = (Main.tileStone[tile44.TileType] ? 1 : tile44.TileType);
            }

            if (tile45 != null && tile45.HasTile)
            {
                upRight = (Main.tileStone[tile45.TileType] ? 1 : tile45.TileType);
            }

            if (tile42 != null && tile42.HasTile)
            {
                downLeft = (Main.tileStone[tile42.TileType] ? 1 : tile42.TileType);
            }

            if (tile43 != null && tile43.HasTile)
            {
                downRight = (Main.tileStone[tile43.TileType] ? 1 : tile43.TileType);
            }

            if (tile.Slope == SlopeType.SlopeDownRight)
            {
                up = -1;
                left = -1;
            }

            if (tile.Slope == SlopeType.SlopeDownLeft)
            {
                up = -1;
                right = -1;
            }

            if (tile.Slope == SlopeType.SlopeUpRight)
            {
                down = -1;
                left = -1;
            }

            if (tile.Slope == SlopeType.SlopeUpLeft)
            {
                down = -1;
                right = -1;
            }

            if (num == 668)
            {
                num = 0;
            }

            WorldGen.TileMergeAttempt(0, TileID.Sets.Dirt, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            if (TileID.Sets.Snow[num])
            {
                WorldGen.TileMergeAttempt(num, Main.tileBrick, TileID.Sets.Ices, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            }
            else if (!TileID.Sets.Ices[num])
            {
                if (num == TileID.BreakableIce)
                {
                    WorldGen.TileMergeAttempt(num, Main.tileBrick, TileID.Sets.IcesSnow, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                }
                else if (Main.tileBrick[num])
                {
                    if (!TileID.Sets.GrassSpecial[num])
                    {
                        if (num == TileID.AshGrass)
                        {
                            WorldGen.TileMergeAttempt(num, Main.tileBrick, TileID.Sets.Ash, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        }
                        else
                        {
                            WorldGen.TileMergeAttempt(num, Main.tileBrick, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        }
                    }
                    else
                    {
                        WorldGen.TileMergeAttempt(num, Main.tileBrick, TileID.Sets.Mud, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                    }
                }
                else if (Main.tilePile[num])
                {
                    WorldGen.TileMergeAttempt(num, Main.tilePile, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                }
            }
            else
            {
                WorldGen.TileMergeAttempt(num, Main.tileBrick, TileID.Sets.Snow, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            }

            if ((TileID.Sets.Stone[num] || Main.tileMoss[num]) && down == 165)
            {
                if (tile39 != null && tile39.TileFrameY == 72)
                {
                    down = num;
                }
                else if (tile39 != null && tile39.TileFrameY == 0)
                {
                    down = num;
                }
            }

            if ((TileID.Sets.Stone[num] || Main.tileMoss[num]) && up == 165)
            {
                if (tile38 != null && tile38.TileFrameY == 90)
                {
                    up = num;
                }
                else if (tile38 != null && tile38.TileFrameY == 54)
                {
                    up = num;
                }
            }

            if (num == TileID.Hive)
            {
                if (down == TileID.Stalactite)
                {
                    down = num;
                }

                if (up == TileID.Stalactite)
                {
                    up = num;
                }
            }

            if ((TileID.Sets.Ices[num] || num == 147) && down == 165)
            {
                down = num;
            }

            if ((tile.Slope == SlopeType.SlopeDownLeft || tile.Slope == SlopeType.SlopeDownRight) && down > -1 && !TileID.Sets.Platforms[down])
            {
                down = num;
            }

            if (up > -1 && tile38 != null && (tile38.Slope == SlopeType.SlopeDownLeft || tile38.Slope == SlopeType.SlopeDownRight) && !TileID.Sets.Platforms[up])
            {
                up = num;
            }

            if ((tile.Slope == SlopeType.SlopeUpLeft || tile.Slope == SlopeType.SlopeUpRight) && up > -1 && !TileID.Sets.Platforms[up])
            {
                up = num;
            }

            if (down > -1 && tile39 != null && (tile39.Slope == SlopeType.SlopeUpLeft || tile39.Slope == SlopeType.SlopeUpRight) && !TileID.Sets.Platforms[down])
            {
                down = num;
            }

            if (num == TileID.WoodenBeam)
            {
                if (up > -1 && Main.tileSolid[up] && !TileID.Sets.Platforms[up])
                {
                    up = num;
                }

                if (down > -1 && Main.tileSolid[down] && !TileID.Sets.Platforms[down])
                {
                    down = num;
                }
            }

            if (up > -1 && tile38 != null && tile38.IsHalfBlock && !TileID.Sets.Platforms[up])
            {
                up = num;
            }

            if (left > -1 && tile40 != null && tile40.IsHalfBlock)
            {
                if (tile.IsHalfBlock)
                {
                    left = num;
                }
                else if (tile40.TileType != num)
                {
                    left = -1;
                }
            }

            if (right > -1 && tile41 != null && tile41.IsHalfBlock)
            {
                if (tile.IsHalfBlock)
                {
                    right = num;
                }
                else if (tile41.TileType != num)
                {
                    right = -1;
                }
            }

            if (tile.IsHalfBlock)
            {
                if (left != num)
                {
                    left = -1;
                }

                if (right != num)
                {
                    right = -1;
                }

                up = -1;
            }

            if (tile39 != null && tile39.IsHalfBlock)
            {
                down = -1;
            }

            if (!Main.tileRope[num] && TileID.Sets.BlockMergesWithMergeAllBlock[num])
            {
                WorldGen.TileMergeAttempt(num, Main.tileBlendAll, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            }

            if (Main.tileBlendAll[num])
            {
                WorldGen.TileMergeAttempt(num, TileID.Sets.BlockMergesWithMergeAllBlock, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            }

            if (TileID.Sets.ForcedDirtMerging[num])
            {
                WorldGen.TileMergeAttempt(num, TileID.Sets.Dirt, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            }

            if (TileID.Sets.Dirt[num])
            {
                if (up > -1 && Main.tileMergeDirt[up])
                {
                    WorldGen.TileFrame(i, j - 1);
                    if (mergeDown)
                    {
                        up = num;
                    }
                }
                else if (up >= 0 && TileID.Sets.Snow[up])
                {
                    WorldGen.TileFrame(i, j - 1);
                    if (mergeDown)
                    {
                        up = num;
                    }
                }

                if (down > -1 && Main.tileMergeDirt[down])
                {
                    WorldGen.TileFrame(i, j + 1);
                    if (mergeUp)
                    {
                        down = num;
                    }
                }
                else if (down >= 0 && TileID.Sets.Snow[down])
                {
                    WorldGen.TileFrame(i, j + 1);
                    if (mergeUp)
                    {
                        down = num;
                    }
                }

                if (left > -1 && Main.tileMergeDirt[left])
                {
                    WorldGen.TileFrame(i - 1, j);
                    if (mergeRight)
                    {
                        left = num;
                    }
                }
                else if (left >= 0 && TileID.Sets.Snow[left])
                {
                    WorldGen.TileFrame(i - 1, j);
                    if (mergeRight)
                    {
                        left = num;
                    }
                }

                if (right > -1 && Main.tileMergeDirt[right])
                {
                    WorldGen.TileFrame(i + 1, j);
                    if (mergeLeft)
                    {
                        right = num;
                    }
                }
                else if (right == 147)
                {
                    WorldGen.TileFrame(i + 1, j);
                    if (mergeLeft)
                    {
                        right = num;
                    }
                }

                bool[] mergesWithDirtInASpecialWay = TileID.Sets.Conversion.MergesWithDirtInASpecialWay;
                if (up > -1 && mergesWithDirtInASpecialWay[up])
                {
                    up = num;
                }

                if (down > -1 && mergesWithDirtInASpecialWay[down])
                {
                    down = num;
                }

                if (left > -1 && mergesWithDirtInASpecialWay[left])
                {
                    left = num;
                }

                if (right > -1 && mergesWithDirtInASpecialWay[right])
                {
                    right = num;
                }

                if (upLeft > -1 && Main.tileMergeDirt[upLeft])
                {
                    upLeft = num;
                }
                else if (upLeft > -1 && mergesWithDirtInASpecialWay[upLeft])
                {
                    upLeft = num;
                }

                if (upRight > -1 && Main.tileMergeDirt[upRight])
                {
                    upRight = num;
                }
                else if (upRight > -1 && mergesWithDirtInASpecialWay[upRight])
                {
                    upRight = num;
                }

                if (downLeft > -1 && Main.tileMergeDirt[downLeft])
                {
                    downLeft = num;
                }
                else if (downLeft > -1 && mergesWithDirtInASpecialWay[downLeft])
                {
                    downLeft = num;
                }

                if (downRight > -1 && Main.tileMergeDirt[downRight])
                {
                    downRight = num;
                }
                else if (downRight > -1 && mergesWithDirtInASpecialWay[downRight])
                {
                    downRight = num;
                }

                WorldGen.TileMergeAttempt(TileID.Dirt, TileID.Sets.Dirt, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                WorldGen.TileMergeAttempt(-2, TileID.Sets.Mud, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                WorldGen.TileMergeAttempt(TileID.Dirt, TileID.LivingWood, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                if (up > -1 && TileID.Sets.ForcedDirtMerging[up])
                {
                    up = num;
                }

                if (down > -1 && TileID.Sets.ForcedDirtMerging[down])
                {
                    down = num;
                }

                if (left > -1 && TileID.Sets.ForcedDirtMerging[left])
                {
                    left = num;
                }

                if (right > -1 && TileID.Sets.ForcedDirtMerging[right])
                {
                    right = num;
                }

                if (upLeft > -1 && TileID.Sets.ForcedDirtMerging[upLeft])
                {
                    upLeft = num;
                }

                if (upRight > -1 && TileID.Sets.ForcedDirtMerging[upRight])
                {
                    upRight = num;
                }

                if (downLeft > -1 && TileID.Sets.ForcedDirtMerging[downLeft])
                {
                    downLeft = num;
                }

                if (downRight > -1 && TileID.Sets.ForcedDirtMerging[downRight])
                {
                    downRight = num;
                }
            }
            else if (Main.tileRope[num])
            {
                if (num != TileID.MysticSnakeRope && up != num && WorldGen.IsRope(i, j - 1))
                {
                    up = num;
                }

                if (down != num && WorldGen.IsRope(i, j + 1))
                {
                    down = num;
                }

                if (num != 504 && up > -1 && Main.tileSolid[up] && !Main.tileSolidTop[up])
                {
                    up = num;
                }

                if (down > -1 && Main.tileSolid[down])
                {
                    down = num;
                }

                if (num != TileID.MysticSnakeRope && up != num)
                {
                    if (left > -1 && Main.tileSolid[left])
                    {
                        left = num;
                    }

                    if (right > -1 && Main.tileSolid[right])
                    {
                        right = num;
                    }
                }
            }
            else
            {
                switch (num)
                {
                    case TileID.Sand:
                        TileMergeAttemptFrametest(i, j, num, TileID.HardenedSand, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        TileMergeAttemptFrametest(i, j, num, TileID.Sandstone, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        break;
                    case TileID.Ebonsand:
                        TileMergeAttemptFrametest(i, j, num, TileID.CorruptHardenedSand, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        TileMergeAttemptFrametest(i, j, num, TileID.CorruptSandstone, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        break;
                    case TileID.Crimsand:
                        TileMergeAttemptFrametest(i, j, num, TileID.CrimsonHardenedSand, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        TileMergeAttemptFrametest(i, j, num, TileID.CrimsonSandstone, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        break;
                    case TileID.Pearlsand:
                        TileMergeAttemptFrametest(i, j, num, TileID.HallowHardenedSand, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        TileMergeAttemptFrametest(i, j, num, TileID.HallowSandstone, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        break;
                }
            }

            if (Main.tileMergeDirt[num])
            {
                WorldGen.TileMergeAttempt(-2, TileID.Sets.Dirt, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                if (num == 1)
                {
                    if (j > Main.rockLayer)
                    {
                        TileMergeAttemptFrametest(i, j, num, TileID.Sets.Mud, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                    }

                    TileMergeAttemptFrametest(i, j, num, TileID.Sets.Ash, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                }
            }
            else
            {
                if (!TileID.Sets.HellSpecial[num])
                {
                    if (num == TileID.Ash)
                    {
                        WorldGen.TileMergeAttempt(-2, 1, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        WorldGen.TileMergeAttempt(num, 633, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        TileMergeAttemptFrametest(i, j, num, TileID.Sets.HellSpecial, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                    }
                    else if (!TileID.Sets.Mud[num])
                    {
                        switch (num)
                        {
                            case TileID.Chlorophyte:
                                WorldGen.TileMergeAttempt(TileID.Mud, TileID.JungleGrass, ref up, ref down, ref left, ref right);
                                WorldGen.TileMergeAttempt(-2, TileID.Sets.Mud, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                                break;
                            case TileID.Hive:
                            case TileID.LihzahrdBrick:
                                WorldGen.TileMergeAttempt(-2, TileID.Sets.Mud, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                                break;
                            case TileID.JungleGrass:
                                WorldGen.TileMergeAttempt(TileID.Mud, 211, ref up, ref down, ref left, ref right);
                                break;
                            case TileID.Cloud:
                                TileMergeAttemptFrametest(i, j, num, TileID.Sets.MergesWithClouds, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                                break;
                            case TileID.RainCloud:
                                WorldGen.TileMergeAttempt(-2, TileID.Cloud, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                                WorldGen.TileMergeAttempt(num, TileID.SnowCloud, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                                break;
                            case TileID.SnowCloud:
                                WorldGen.TileMergeAttempt(-2, TileID.Cloud, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                                WorldGen.TileMergeAttempt(num, TileID.RainCloud, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                                break;
                            default:
                                if (TileID.Sets.Snow[num])
                                {
                                    TileMergeAttemptFrametest(i, j, num, TileID.Sets.IcesSlush, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                                }
                                else
                                {
                                    WorldGen.TileMergeAttempt(-2, TileID.SnowBlock, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                                }

                                break;
                        }
                    }
                    else
                    {
                        if (j > Main.rockLayer)
                        {
                            WorldGen.TileMergeAttempt(-2, TileID.Stone, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        }

                        WorldGen.TileMergeAttempt(num, TileID.Sets.GrassSpecial, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        TileMergeAttemptFrametest(i, j, num, TileID.Sets.JungleSpecial, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        if (j < Main.rockLayer)
                        {
                            TileMergeAttemptFrametest(i, j, num, TileID.Sets.Dirt, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                        }
                        else
                        {
                            WorldGen.TileMergeAttempt(num, TileID.Sets.Dirt, ref up, ref down, ref left, ref right);
                        }
                    }
                }
                else
                {
                    WorldGen.TileMergeAttempt(-2, TileID.Sets.Ash, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                }
            }

            if (num == TileID.Dirt)
            {
                WorldGen.TileMergeAttempt(num, Main.tileMoss, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                WorldGen.TileMergeAttempt(num, TileID.Sets.tileMossBrick, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            }
            else if (Main.tileMoss[num] || TileID.Sets.tileMossBrick[num])
            {
                WorldGen.TileMergeAttempt(num, TileID.Sets.Dirt, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            }
            else if (Main.tileStone[num] || num == TileID.Stone)
            {
                WorldGen.TileMergeAttempt(num, Main.tileMoss, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            }
            else if (num == TileID.GrayBrick)
            {
                WorldGen.TileMergeAttempt(num, TileID.Sets.tileMossBrick, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            }

            if (TileID.Sets.Conversion.Grass[num])
            {
                WorldGen.TileMergeAttempt(num, TileID.Sets.Ore, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            }
            else if (TileID.Sets.Ore[num])
            {
                WorldGen.TileMergeAttempt(num, TileID.Sets.Conversion.Grass, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            }

            if (TileID.Sets.Mud[num])
            {
                WorldGen.TileMergeAttempt(num, TileID.Sets.OreMergesWithMud, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            }
            else if (TileID.Sets.OreMergesWithMud[num])
            {
                WorldGen.TileMergeAttempt(num, TileID.Sets.Mud, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            }
        }

        public static void TileMergeAttemptFrametest(int i, int j, int myType, int lookfor, ref int up, ref int down, ref int left, ref int right, ref int upLeft, ref int upRight, ref int downLeft, ref int downRight)
        {
            if (up == lookfor)
            {
                WorldGen.TileFrame(i, j - 1);
                if (mergeDown)
                {
                    up = myType;
                }
            }

            if (down == lookfor)
            {
                WorldGen.TileFrame(i, j + 1);
                if (mergeUp)
                {
                    down = myType;
                }
            }

            if (left == lookfor)
            {
                WorldGen.TileFrame(i - 1, j);
                if (mergeRight)
                {
                    left = myType;
                }
            }

            if (right == lookfor)
            {
                WorldGen.TileFrame(i + 1, j);
                if (mergeLeft)
                {
                    right = myType;
                }
            }

            if (upLeft == lookfor)
            {
                upLeft = myType;
            }

            if (upRight == lookfor)
            {
                upRight = myType;
            }

            if (downLeft == lookfor)
            {
                downLeft = myType;
            }

            if (downRight == lookfor)
            {
                downRight = myType;
            }
        }

        public static void TileMergeAttemptFrametest(int i, int j, int myType, bool[] lookfor, ref int up, ref int down, ref int left, ref int right, ref int upLeft, ref int upRight, ref int downLeft, ref int downRight)
        {
            if (up > -1 && lookfor[up])
            {
                WorldGen.TileFrame(i, j - 1);
                if (mergeDown)
                {
                    up = myType;
                }
            }

            if (down > -1 && lookfor[down])
            {
                WorldGen.TileFrame(i, j + 1);
                if (mergeUp)
                {
                    down = myType;
                }
            }

            if (left > -1 && lookfor[left])
            {
                WorldGen.TileFrame(i - 1, j);
                if (mergeRight)
                {
                    left = myType;
                }
            }

            if (right > -1 && lookfor[right])
            {
                WorldGen.TileFrame(i + 1, j);
                if (mergeLeft)
                {
                    right = myType;
                }
            }

            if (upLeft > -1 && lookfor[upLeft])
            {
                upLeft = myType;
            }

            if (upRight > -1 && lookfor[upRight])
            {
                upRight = myType;
            }

            if (downLeft > -1 && lookfor[downLeft])
            {
                downLeft = myType;
            }

            if (downRight > -1 && lookfor[downRight])
            {
                downRight = myType;
            }
        }

        public static void CustomTileFrame(int i, int j, ref int upLeft, ref int up, ref int upRight, ref int left, ref int right, ref int downLeft, ref int down, ref int downRight, bool resetFrame)
        {
            Tile tile = Main.tile[i, j];
            int num = tile.TileType;
            if (Main.tileStone[num])
            {
                num = 1;
            }

            int frameX = tile.TileFrameX;
            int frameY = tile.TileFrameY;
            Rectangle rectangle = new(-1, -1, 0, 0);

            Tile tile38 = Main.tile[i, j - 1];
            Tile tile39 = Main.tile[i, j + 1];
            Tile tile40 = Main.tile[i - 1, j];
            Tile tile41 = Main.tile[i + 1, j];
            Tile tile42 = Main.tile[i - 1, j + 1];
            Tile tile43 = Main.tile[i + 1, j + 1];
            Tile tile44 = Main.tile[i - 1, j - 1];
            Tile tile45 = Main.tile[i + 1, j - 1];

            if (!Main.tileSolid[num])
            {
                switch (num)
                {
                    case 49:
                        WorldGen.CheckOnTable1x1(i, j, (byte)num);
                        return;
                    case 80:
                        WorldGen.CactusFrame(i, j);
                        return;
                }
            }

            mergeUp = false;
            mergeDown = false;
            mergeLeft = false;
            mergeRight = false;
            int num29;
            if (resetFrame)
            {
                num29 = WorldGen.genRand.Next(0, 3);
                tile.TileFrameNumber = (byte)num29;
            }
            else
            {
                num29 = tile.TileFrameNumber;
            }

            if (Main.tileLargeFrames[num] == 1)
            {
                int num30 = j % 4;
                int num31 = i % 3;
                num29 = (new int[4, 3]
                {
                                { 2, 4, 2 },
                                { 1, 3, 1 },
                                { 2, 2, 4 },
                                { 1, 1, 3 }
                })[num30, num31] - 1;
            }

            if (Main.tileLargeFrames[num] == 2)
            {
                int num32 = i % 2;
                int num33 = j % 2;
                num29 = num32 + num33 * 2;
            }

            bool flag = false;
            WorldGen.TileMergeCullCache tileMergeCullCache = default;
            if (!Main.ShouldShowInvisibleWalls())
            {
                bool flag2 = tile.IsTileInvisible;
                tileMergeCullCache.CullTop |= tile38 != null && tile38.IsTileInvisible != flag2;
                tileMergeCullCache.CullBottom |= tile39 != null && tile39.IsTileInvisible != flag2;
                tileMergeCullCache.CullLeft |= tile40 != null && tile40.IsTileInvisible != flag2;
                tileMergeCullCache.CullRight |= tile41 != null && tile41.IsTileInvisible != flag2;
                tileMergeCullCache.CullTopLeft |= tile44 != null && tile44.IsTileInvisible != flag2;
                tileMergeCullCache.CullTopRight |= tile45 != null && tile45.IsTileInvisible != flag2;
                tileMergeCullCache.CullBottomLeft |= tile42 != null && tile42.IsTileInvisible != flag2;
                tileMergeCullCache.CullBottomRight |= tile43 != null && tile43.IsTileInvisible != flag2;
            }

            if (TileID.Sets.Grass[num] || TileID.Sets.GrassSpecial[num] || Main.tileMoss[num] || TileID.Sets.NeedsGrassFraming[num] || TileID.Sets.tileMossBrick[num])
            {
                flag = true;
                WorldGen.TileMergeAttemptWeird(num, -1, Main.tileSolid, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                int num34 = TileID.Sets.NeedsGrassFramingDirt[num];
                if (TileID.Sets.GrassSpecial[num])
                {
                    num34 = 59;
                }
                else if (Main.tileMoss[num])
                {
                    num34 = 1;
                }
                else if (TileID.Sets.tileMossBrick[num])
                {
                    num34 = 38;
                }
                else
                {
                    switch (num)
                    {
                        case TileID.Grass:
                        case TileID.GolfGrass:
                            WorldGen.TileMergeAttempt(num34, TileID.CorruptGrass, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                            break;
                        case TileID.CorruptGrass:
                            WorldGen.TileMergeAttempt(num34, TileID.Grass, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                            break;
                    }
                }

                tileMergeCullCache.Cull(ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                if (up != num && up != num34 && (down == num || down == num34))
                {
                    if (left == num34 && right == num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 0;
                                rectangle.Y = 198;
                                break;
                            case 1:
                                rectangle.X = 18;
                                rectangle.Y = 198;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 198;
                                break;
                        }
                    }
                    else if (left == num && right == num34)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 198;
                                break;
                            case 1:
                                rectangle.X = 72;
                                rectangle.Y = 198;
                                break;
                            default:
                                rectangle.X = 90;
                                rectangle.Y = 198;
                                break;
                        }
                    }
                }
                else if (down != num && down != num34 && (up == num || up == num34))
                {
                    if (left == num34 && right == num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 0;
                                rectangle.Y = 216;
                                break;
                            case 1:
                                rectangle.X = 18;
                                rectangle.Y = 216;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 216;
                                break;
                        }
                    }
                    else if (left == num && right == num34)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 216;
                                break;
                            case 1:
                                rectangle.X = 72;
                                rectangle.Y = 216;
                                break;
                            default:
                                rectangle.X = 90;
                                rectangle.Y = 216;
                                break;
                        }
                    }
                }
                else if (left != num && left != num34 && (right == num || right == num34))
                {
                    if (up == num34 && down == num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 72;
                                rectangle.Y = 144;
                                break;
                            case 1:
                                rectangle.X = 72;
                                rectangle.Y = 162;
                                break;
                            default:
                                rectangle.X = 72;
                                rectangle.Y = 180;
                                break;
                        }
                    }
                    else if (down == num && up == num34)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 72;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 72;
                                rectangle.Y = 108;
                                break;
                            default:
                                rectangle.X = 72;
                                rectangle.Y = 126;
                                break;
                        }
                    }
                }
                else if (right != num && right != num34 && (left == num || left == num34))
                {
                    if (up == num34 && down == num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 90;
                                rectangle.Y = 144;
                                break;
                            case 1:
                                rectangle.X = 90;
                                rectangle.Y = 162;
                                break;
                            default:
                                rectangle.X = 90;
                                rectangle.Y = 180;
                                break;
                        }
                    }
                    else if (down == num && right == up)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 90;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 90;
                                rectangle.Y = 108;
                                break;
                            default:
                                rectangle.X = 90;
                                rectangle.Y = 126;
                                break;
                        }
                    }
                }
                else if (up == num && down == num && left == num && right == num)
                {
                    if (upLeft != num && upRight != num && downLeft != num && downRight != num)
                    {
                        if (downRight == num34)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 108;
                                    rectangle.Y = 324;
                                    break;
                                case 1:
                                    rectangle.X = 126;
                                    rectangle.Y = 324;
                                    break;
                                default:
                                    rectangle.X = 144;
                                    rectangle.Y = 324;
                                    break;
                            }
                        }
                        else if (upRight == num34)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 108;
                                    rectangle.Y = 342;
                                    break;
                                case 1:
                                    rectangle.X = 126;
                                    rectangle.Y = 342;
                                    break;
                                default:
                                    rectangle.X = 144;
                                    rectangle.Y = 342;
                                    break;
                            }
                        }
                        else if (downLeft == num34)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 108;
                                    rectangle.Y = 360;
                                    break;
                                case 1:
                                    rectangle.X = 126;
                                    rectangle.Y = 360;
                                    break;
                                default:
                                    rectangle.X = 144;
                                    rectangle.Y = 360;
                                    break;
                            }
                        }
                        else if (upLeft == num34)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 108;
                                    rectangle.Y = 378;
                                    break;
                                case 1:
                                    rectangle.X = 126;
                                    rectangle.Y = 378;
                                    break;
                                default:
                                    rectangle.X = 144;
                                    rectangle.Y = 378;
                                    break;
                            }
                        }
                        else
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 144;
                                    rectangle.Y = 234;
                                    break;
                                case 1:
                                    rectangle.X = 198;
                                    rectangle.Y = 234;
                                    break;
                                default:
                                    rectangle.X = 252;
                                    rectangle.Y = 234;
                                    break;
                            }
                        }
                    }
                    else if (upLeft != num && downRight != num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 36;
                                rectangle.Y = 306;
                                break;
                            case 1:
                                rectangle.X = 54;
                                rectangle.Y = 306;
                                break;
                            default:
                                rectangle.X = 72;
                                rectangle.Y = 306;
                                break;
                        }
                    }
                    else if (upRight != num && downLeft != num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 90;
                                rectangle.Y = 306;
                                break;
                            case 1:
                                rectangle.X = 108;
                                rectangle.Y = 306;
                                break;
                            default:
                                rectangle.X = 126;
                                rectangle.Y = 306;
                                break;
                        }
                    }
                    else if (upLeft != num && upRight == num && downLeft == num && downRight == num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 108;
                                break;
                            case 1:
                                rectangle.X = 54;
                                rectangle.Y = 144;
                                break;
                            default:
                                rectangle.X = 54;
                                rectangle.Y = 180;
                                break;
                        }
                    }
                    else if (upLeft == num && upRight != num && downLeft == num && downRight == num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 36;
                                rectangle.Y = 108;
                                break;
                            case 1:
                                rectangle.X = 36;
                                rectangle.Y = 144;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 180;
                                break;
                        }
                    }
                    else if (upLeft == num && upRight == num && downLeft != num && downRight == num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 54;
                                rectangle.Y = 126;
                                break;
                            default:
                                rectangle.X = 54;
                                rectangle.Y = 162;
                                break;
                        }
                    }
                    else if (upLeft == num && upRight == num && downLeft == num && downRight != num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 36;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 36;
                                rectangle.Y = 126;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 162;
                                break;
                        }
                    }
                }
                else if (up == num && down == num34 && left == num && right == num && upLeft == -1 && upRight == -1)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 108;
                            rectangle.Y = 18;
                            break;
                        case 1:
                            rectangle.X = 126;
                            rectangle.Y = 18;
                            break;
                        default:
                            rectangle.X = 144;
                            rectangle.Y = 18;
                            break;
                    }
                }
                else if (up == num34 && down == num && left == num && right == num && downLeft == -1 && downRight == -1)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 108;
                            rectangle.Y = 36;
                            break;
                        case 1:
                            rectangle.X = 126;
                            rectangle.Y = 36;
                            break;
                        default:
                            rectangle.X = 144;
                            rectangle.Y = 36;
                            break;
                    }
                }
                else if (up == num && down == num && left == num34 && right == num && upRight == -1 && downRight == -1)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 198;
                            rectangle.Y = 0;
                            break;
                        case 1:
                            rectangle.X = 198;
                            rectangle.Y = 18;
                            break;
                        default:
                            rectangle.X = 198;
                            rectangle.Y = 36;
                            break;
                    }
                }
                else if (up == num && down == num && left == num && right == num34 && upLeft == -1 && downLeft == -1)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 180;
                            rectangle.Y = 0;
                            break;
                        case 1:
                            rectangle.X = 180;
                            rectangle.Y = 18;
                            break;
                        default:
                            rectangle.X = 180;
                            rectangle.Y = 36;
                            break;
                    }
                }
                else if (up == num && down == num34 && left == num && right == num)
                {
                    if (upRight != -1)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 108;
                                break;
                            case 1:
                                rectangle.X = 54;
                                rectangle.Y = 144;
                                break;
                            default:
                                rectangle.X = 54;
                                rectangle.Y = 180;
                                break;
                        }
                    }
                    else if (upLeft != -1)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 36;
                                rectangle.Y = 108;
                                break;
                            case 1:
                                rectangle.X = 36;
                                rectangle.Y = 144;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 180;
                                break;
                        }
                    }
                }
                else if (up == num34 && down == num && left == num && right == num)
                {
                    if (downRight != -1)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 54;
                                rectangle.Y = 126;
                                break;
                            default:
                                rectangle.X = 54;
                                rectangle.Y = 162;
                                break;
                        }
                    }
                    else if (downLeft != -1)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 36;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 36;
                                rectangle.Y = 126;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 162;
                                break;
                        }
                    }
                }
                else if (up == num && down == num && left == num && right == num34)
                {
                    if (upLeft != -1)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 54;
                                rectangle.Y = 126;
                                break;
                            default:
                                rectangle.X = 54;
                                rectangle.Y = 162;
                                break;
                        }
                    }
                    else if (downLeft != -1)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 108;
                                break;
                            case 1:
                                rectangle.X = 54;
                                rectangle.Y = 144;
                                break;
                            default:
                                rectangle.X = 54;
                                rectangle.Y = 180;
                                break;
                        }
                    }
                }
                else if (up == num && down == num && left == num34 && right == num)
                {
                    if (upRight != -1)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 36;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 36;
                                rectangle.Y = 126;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 162;
                                break;
                        }
                    }
                    else if (downRight != -1)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 36;
                                rectangle.Y = 108;
                                break;
                            case 1:
                                rectangle.X = 36;
                                rectangle.Y = 144;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 180;
                                break;
                        }
                    }
                }
                else if ((up == num34 && down == num && left == num && right == num) || (up == num && down == num34 && left == num && right == num) || (up == num && down == num && left == num34 && right == num) || (up == num && down == num && left == num && right == num34))
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 18;
                            rectangle.Y = 18;
                            break;
                        case 1:
                            rectangle.X = 36;
                            rectangle.Y = 18;
                            break;
                        default:
                            rectangle.X = 54;
                            rectangle.Y = 18;
                            break;
                    }
                }

                if ((up == num || up == num34) && (down == num || down == num34) && (left == num || left == num34) && (right == num || right == num34))
                {
                    if (upLeft != num && upLeft != num34 && (upRight == num || upRight == num34) && (downLeft == num || downLeft == num34) && (downRight == num || downRight == num34))
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 108;
                                break;
                            case 1:
                                rectangle.X = 54;
                                rectangle.Y = 144;
                                break;
                            default:
                                rectangle.X = 54;
                                rectangle.Y = 180;
                                break;
                        }
                    }
                    else if (upRight != num && upRight != num34 && (upLeft == num || upLeft == num34) && (downLeft == num || downLeft == num34) && (downRight == num || downRight == num34))
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 36;
                                rectangle.Y = 108;
                                break;
                            case 1:
                                rectangle.X = 36;
                                rectangle.Y = 144;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 180;
                                break;
                        }
                    }
                    else if (downLeft != num && downLeft != num34 && (upLeft == num || upLeft == num34) && (upRight == num || upRight == num34) && (downRight == num || downRight == num34))
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 54;
                                rectangle.Y = 126;
                                break;
                            default:
                                rectangle.X = 54;
                                rectangle.Y = 162;
                                break;
                        }
                    }
                    else if (downRight != num && downRight != num34 && (upLeft == num || upLeft == num34) && (downLeft == num || downLeft == num34) && (upRight == num || upRight == num34))
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 36;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 36;
                                rectangle.Y = 126;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 162;
                                break;
                        }
                    }
                }

                if (up != num34 && up != num && down == num && left != num34 && left != num && right == num && downRight != num34 && downRight != num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 90;
                            rectangle.Y = 270;
                            break;
                        case 1:
                            rectangle.X = 108;
                            rectangle.Y = 270;
                            break;
                        default:
                            rectangle.X = 126;
                            rectangle.Y = 270;
                            break;
                    }
                }
                else if (up != num34 && up != num && down == num && left == num && right != num34 && right != num && downLeft != num34 && downLeft != num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 144;
                            rectangle.Y = 270;
                            break;
                        case 1:
                            rectangle.X = 162;
                            rectangle.Y = 270;
                            break;
                        default:
                            rectangle.X = 180;
                            rectangle.Y = 270;
                            break;
                    }
                }
                else if (down != num34 && down != num && up == num && left != num34 && left != num && right == num && upRight != num34 && upRight != num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 90;
                            rectangle.Y = 288;
                            break;
                        case 1:
                            rectangle.X = 108;
                            rectangle.Y = 288;
                            break;
                        default:
                            rectangle.X = 126;
                            rectangle.Y = 288;
                            break;
                    }
                }
                else if (down != num34 && down != num && up == num && left == num && right != num34 && right != num && upLeft != num34 && upLeft != num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 144;
                            rectangle.Y = 288;
                            break;
                        case 1:
                            rectangle.X = 162;
                            rectangle.Y = 288;
                            break;
                        default:
                            rectangle.X = 180;
                            rectangle.Y = 288;
                            break;
                    }
                }
                else if (up != num && up != num34 && down == num && left == num && right == num && downLeft != num && downLeft != num34 && downRight != num && downRight != num34)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 144;
                            rectangle.Y = 216;
                            break;
                        case 1:
                            rectangle.X = 198;
                            rectangle.Y = 216;
                            break;
                        default:
                            rectangle.X = 252;
                            rectangle.Y = 216;
                            break;
                    }
                }
                else if (down != num && down != num34 && up == num && left == num && right == num && upLeft != num && upLeft != num34 && upRight != num && upRight != num34)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 144;
                            rectangle.Y = 252;
                            break;
                        case 1:
                            rectangle.X = 198;
                            rectangle.Y = 252;
                            break;
                        default:
                            rectangle.X = 252;
                            rectangle.Y = 252;
                            break;
                    }
                }
                else if (left != num && left != num34 && down == num && up == num && right == num && upRight != num && upRight != num34 && downRight != num && downRight != num34)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 126;
                            rectangle.Y = 234;
                            break;
                        case 1:
                            rectangle.X = 180;
                            rectangle.Y = 234;
                            break;
                        default:
                            rectangle.X = 234;
                            rectangle.Y = 234;
                            break;
                    }
                }
                else if (right != num && right != num34 && down == num && up == num && left == num && upLeft != num && upLeft != num34 && downLeft != num && downLeft != num34)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 162;
                            rectangle.Y = 234;
                            break;
                        case 1:
                            rectangle.X = 216;
                            rectangle.Y = 234;
                            break;
                        default:
                            rectangle.X = 270;
                            rectangle.Y = 234;
                            break;
                    }
                }
                else if (up != num34 && up != num && (down == num34 || down == num) && left == num34 && right == num34)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 36;
                            rectangle.Y = 270;
                            break;
                        case 1:
                            rectangle.X = 54;
                            rectangle.Y = 270;
                            break;
                        default:
                            rectangle.X = 72;
                            rectangle.Y = 270;
                            break;
                    }
                }
                else if (down != num34 && down != num && (up == num34 || up == num) && left == num34 && right == num34)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 36;
                            rectangle.Y = 288;
                            break;
                        case 1:
                            rectangle.X = 54;
                            rectangle.Y = 288;
                            break;
                        default:
                            rectangle.X = 72;
                            rectangle.Y = 288;
                            break;
                    }
                }
                else if (left != num34 && left != num && (right == num34 || right == num) && up == num34 && down == num34)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 0;
                            rectangle.Y = 270;
                            break;
                        case 1:
                            rectangle.X = 0;
                            rectangle.Y = 288;
                            break;
                        default:
                            rectangle.X = 0;
                            rectangle.Y = 306;
                            break;
                    }
                }
                else if (right != num34 && right != num && (left == num34 || left == num) && up == num34 && down == num34)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 18;
                            rectangle.Y = 270;
                            break;
                        case 1:
                            rectangle.X = 18;
                            rectangle.Y = 288;
                            break;
                        default:
                            rectangle.X = 18;
                            rectangle.Y = 306;
                            break;
                    }
                }
                else if (up == num && down == num34 && left == num34 && right == num34)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 198;
                            rectangle.Y = 288;
                            break;
                        case 1:
                            rectangle.X = 216;
                            rectangle.Y = 288;
                            break;
                        default:
                            rectangle.X = 234;
                            rectangle.Y = 288;
                            break;
                    }
                }
                else if (up == num34 && down == num && left == num34 && right == num34)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 198;
                            rectangle.Y = 270;
                            break;
                        case 1:
                            rectangle.X = 216;
                            rectangle.Y = 270;
                            break;
                        default:
                            rectangle.X = 234;
                            rectangle.Y = 270;
                            break;
                    }
                }
                else if (up == num34 && down == num34 && left == num && right == num34)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 198;
                            rectangle.Y = 306;
                            break;
                        case 1:
                            rectangle.X = 216;
                            rectangle.Y = 306;
                            break;
                        default:
                            rectangle.X = 234;
                            rectangle.Y = 306;
                            break;
                    }
                }
                else if (up == num34 && down == num34 && left == num34 && right == num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 144;
                            rectangle.Y = 306;
                            break;
                        case 1:
                            rectangle.X = 162;
                            rectangle.Y = 306;
                            break;
                        default:
                            rectangle.X = 180;
                            rectangle.Y = 306;
                            break;
                    }
                }

                if (up != num && up != num34 && down == num && left == num && right == num)
                {
                    if ((downLeft == num34 || downLeft == num) && downRight != num34 && downRight != num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 0;
                                rectangle.Y = 324;
                                break;
                            case 1:
                                rectangle.X = 18;
                                rectangle.Y = 324;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 324;
                                break;
                        }
                    }
                    else if ((downRight == num34 || downRight == num) && downLeft != num34 && downLeft != num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 324;
                                break;
                            case 1:
                                rectangle.X = 72;
                                rectangle.Y = 324;
                                break;
                            default:
                                rectangle.X = 90;
                                rectangle.Y = 324;
                                break;
                        }
                    }
                }
                else if (down != num && down != num34 && up == num && left == num && right == num)
                {
                    if ((upLeft == num34 || upLeft == num) && upRight != num34 && upRight != num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 0;
                                rectangle.Y = 342;
                                break;
                            case 1:
                                rectangle.X = 18;
                                rectangle.Y = 342;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 342;
                                break;
                        }
                    }
                    else if ((upRight == num34 || upRight == num) && upLeft != num34 && upLeft != num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 342;
                                break;
                            case 1:
                                rectangle.X = 72;
                                rectangle.Y = 342;
                                break;
                            default:
                                rectangle.X = 90;
                                rectangle.Y = 342;
                                break;
                        }
                    }
                }
                else if (left != num && left != num34 && up == num && down == num && right == num)
                {
                    if ((upRight == num34 || upRight == num) && downRight != num34 && downRight != num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 360;
                                break;
                            case 1:
                                rectangle.X = 72;
                                rectangle.Y = 360;
                                break;
                            default:
                                rectangle.X = 90;
                                rectangle.Y = 360;
                                break;
                        }
                    }
                    else if ((downRight == num34 || downRight == num) && upRight != num34 && upRight != num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 0;
                                rectangle.Y = 360;
                                break;
                            case 1:
                                rectangle.X = 18;
                                rectangle.Y = 360;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 360;
                                break;
                        }
                    }
                }
                else if (right != num && right != num34 && up == num && down == num && left == num)
                {
                    if ((upLeft == num34 || upLeft == num) && downLeft != num34 && downLeft != num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 0;
                                rectangle.Y = 378;
                                break;
                            case 1:
                                rectangle.X = 18;
                                rectangle.Y = 378;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 378;
                                break;
                        }
                    }
                    else if ((downLeft == num34 || downLeft == num) && upLeft != num34 && upLeft != num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 378;
                                break;
                            case 1:
                                rectangle.X = 72;
                                rectangle.Y = 378;
                                break;
                            default:
                                rectangle.X = 90;
                                rectangle.Y = 378;
                                break;
                        }
                    }
                }

                if ((up == num || up == num34) && (down == num || down == num34) && (left == num || left == num34) && (right == num || right == num34) && upLeft != -1 && upRight != -1 && downLeft != -1 && downRight != -1)
                {
                    if ((i + j) % 2 == 1)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 108;
                                rectangle.Y = 198;
                                break;
                            case 1:
                                rectangle.X = 126;
                                rectangle.Y = 198;
                                break;
                            default:
                                rectangle.X = 144;
                                rectangle.Y = 198;
                                break;
                        }
                    }
                    else
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 18;
                                rectangle.Y = 18;
                                break;
                            case 1:
                                rectangle.X = 36;
                                rectangle.Y = 18;
                                break;
                            default:
                                rectangle.X = 54;
                                rectangle.Y = 18;
                                break;
                        }
                    }
                }

                if (num34 >= 0 && TileID.Sets.Dirt[num34])
                {
                    WorldGen.TileMergeAttempt(-2, TileID.Sets.Dirt, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                }
                else if (num34 >= 0 && TileID.Sets.Mud[num34])
                {
                    WorldGen.TileMergeAttempt(-2, TileID.Sets.Mud, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                }
                else
                {
                    WorldGen.TileMergeAttempt(-2, num34, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                }

                tileMergeCullCache.Cull(ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            }

            WorldGen.TileMergeAttempt(num, Main.tileMerge[num], ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);

            if (rectangle.X == -1 && rectangle.Y == -1 && (Main.tileMergeDirt[num] || TileID.Sets.ChecksForMerge[num]))
            {
                if (!flag)
                {
                    flag = true;
                    WorldGen.TileMergeAttemptWeird(num, -1, Main.tileSolid, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                }

                if (up > -1 && up != num)
                {
                    up = -1;
                }

                if (down > -1 && down != num)
                {
                    down = -1;
                }

                if (left > -1 && left != num)
                {
                    left = -1;
                }

                if (right > -1 && right != num)
                {
                    right = -1;
                }

                tileMergeCullCache.Cull(ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                if (up != -1 && down != -1 && left != -1 && right != -1)
                {
                    if (up == -2 && down == num && left == num && right == num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 144;
                                rectangle.Y = 108;
                                break;
                            case 1:
                                rectangle.X = 162;
                                rectangle.Y = 108;
                                break;
                            default:
                                rectangle.X = 180;
                                rectangle.Y = 108;
                                break;
                        }

                        mergeUp = true;
                    }
                    else if (up == num && down == -2 && left == num && right == num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 144;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 162;
                                rectangle.Y = 90;
                                break;
                            default:
                                rectangle.X = 180;
                                rectangle.Y = 90;
                                break;
                        }

                        mergeDown = true;
                    }
                    else if (up == num && down == num && left == -2 && right == num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 162;
                                rectangle.Y = 126;
                                break;
                            case 1:
                                rectangle.X = 162;
                                rectangle.Y = 144;
                                break;
                            default:
                                rectangle.X = 162;
                                rectangle.Y = 162;
                                break;
                        }

                        mergeLeft = true;
                    }
                    else if (up == num && down == num && left == num && right == -2)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 144;
                                rectangle.Y = 126;
                                break;
                            case 1:
                                rectangle.X = 144;
                                rectangle.Y = 144;
                                break;
                            default:
                                rectangle.X = 144;
                                rectangle.Y = 162;
                                break;
                        }

                        mergeRight = true;
                    }
                    else if (up == -2 && down == num && left == -2 && right == num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 36;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 36;
                                rectangle.Y = 126;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 162;
                                break;
                        }

                        mergeUp = true;
                        mergeLeft = true;
                    }
                    else if (up == -2 && down == num && left == num && right == -2)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 54;
                                rectangle.Y = 126;
                                break;
                            default:
                                rectangle.X = 54;
                                rectangle.Y = 162;
                                break;
                        }

                        mergeUp = true;
                        mergeRight = true;
                    }
                    else if (up == num && down == -2 && left == -2 && right == num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 36;
                                rectangle.Y = 108;
                                break;
                            case 1:
                                rectangle.X = 36;
                                rectangle.Y = 144;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 180;
                                break;
                        }

                        mergeDown = true;
                        mergeLeft = true;
                    }
                    else if (up == num && down == -2 && left == num && right == -2)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 108;
                                break;
                            case 1:
                                rectangle.X = 54;
                                rectangle.Y = 144;
                                break;
                            default:
                                rectangle.X = 54;
                                rectangle.Y = 180;
                                break;
                        }

                        mergeDown = true;
                        mergeRight = true;
                    }
                    else if (up == num && down == num && left == -2 && right == -2)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 180;
                                rectangle.Y = 126;
                                break;
                            case 1:
                                rectangle.X = 180;
                                rectangle.Y = 144;
                                break;
                            default:
                                rectangle.X = 180;
                                rectangle.Y = 162;
                                break;
                        }

                        mergeLeft = true;
                        mergeRight = true;
                    }
                    else if (up == -2 && down == -2 && left == num && right == num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 144;
                                rectangle.Y = 180;
                                break;
                            case 1:
                                rectangle.X = 162;
                                rectangle.Y = 180;
                                break;
                            default:
                                rectangle.X = 180;
                                rectangle.Y = 180;
                                break;
                        }

                        mergeUp = true;
                        mergeDown = true;
                    }
                    else if (up == -2 && down == num && left == -2 && right == -2)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 198;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 198;
                                rectangle.Y = 108;
                                break;
                            default:
                                rectangle.X = 198;
                                rectangle.Y = 126;
                                break;
                        }

                        mergeUp = true;
                        mergeLeft = true;
                        mergeRight = true;
                    }
                    else if (up == num && down == -2 && left == -2 && right == -2)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 198;
                                rectangle.Y = 144;
                                break;
                            case 1:
                                rectangle.X = 198;
                                rectangle.Y = 162;
                                break;
                            default:
                                rectangle.X = 198;
                                rectangle.Y = 180;
                                break;
                        }

                        mergeDown = true;
                        mergeLeft = true;
                        mergeRight = true;
                    }
                    else if (up == -2 && down == -2 && left == num && right == -2)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 216;
                                rectangle.Y = 144;
                                break;
                            case 1:
                                rectangle.X = 216;
                                rectangle.Y = 162;
                                break;
                            default:
                                rectangle.X = 216;
                                rectangle.Y = 180;
                                break;
                        }

                        mergeUp = true;
                        mergeDown = true;
                        mergeRight = true;
                    }
                    else if (up == -2 && down == -2 && left == -2 && right == num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 216;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 216;
                                rectangle.Y = 108;
                                break;
                            default:
                                rectangle.X = 216;
                                rectangle.Y = 126;
                                break;
                        }

                        mergeUp = true;
                        mergeDown = true;
                        mergeLeft = true;
                    }
                    else if (up == -2 && down == -2 && left == -2 && right == -2)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 108;
                                rectangle.Y = 198;
                                break;
                            case 1:
                                rectangle.X = 126;
                                rectangle.Y = 198;
                                break;
                            default:
                                rectangle.X = 144;
                                rectangle.Y = 198;
                                break;
                        }

                        mergeUp = true;
                        mergeDown = true;
                        mergeLeft = true;
                        mergeRight = true;
                    }
                    else if (up == num && down == num && left == num && right == num)
                    {
                        if (upLeft == -2)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 18;
                                    rectangle.Y = 108;
                                    break;
                                case 1:
                                    rectangle.X = 18;
                                    rectangle.Y = 144;
                                    break;
                                default:
                                    rectangle.X = 18;
                                    rectangle.Y = 180;
                                    break;
                            }
                        }

                        if (upRight == -2)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 0;
                                    rectangle.Y = 108;
                                    break;
                                case 1:
                                    rectangle.X = 0;
                                    rectangle.Y = 144;
                                    break;
                                default:
                                    rectangle.X = 0;
                                    rectangle.Y = 180;
                                    break;
                            }
                        }

                        if (downLeft == -2)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 18;
                                    rectangle.Y = 90;
                                    break;
                                case 1:
                                    rectangle.X = 18;
                                    rectangle.Y = 126;
                                    break;
                                default:
                                    rectangle.X = 18;
                                    rectangle.Y = 162;
                                    break;
                            }
                        }

                        if (downRight == -2)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 0;
                                    rectangle.Y = 90;
                                    break;
                                case 1:
                                    rectangle.X = 0;
                                    rectangle.Y = 126;
                                    break;
                                default:
                                    rectangle.X = 0;
                                    rectangle.Y = 162;
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    if (!TileID.Sets.Grass[num] && !TileID.Sets.GrassSpecial[num])
                    {
                        if (up == -1 && down == -2 && left == num && right == num)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 234;
                                    rectangle.Y = 0;
                                    break;
                                case 1:
                                    rectangle.X = 252;
                                    rectangle.Y = 0;
                                    break;
                                default:
                                    rectangle.X = 270;
                                    rectangle.Y = 0;
                                    break;
                            }

                            mergeDown = true;
                        }
                        else if (up == -2 && down == -1 && left == num && right == num)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 234;
                                    rectangle.Y = 18;
                                    break;
                                case 1:
                                    rectangle.X = 252;
                                    rectangle.Y = 18;
                                    break;
                                default:
                                    rectangle.X = 270;
                                    rectangle.Y = 18;
                                    break;
                            }

                            mergeUp = true;
                        }
                        else if (up == num && down == num && left == -1 && right == -2)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 234;
                                    rectangle.Y = 36;
                                    break;
                                case 1:
                                    rectangle.X = 252;
                                    rectangle.Y = 36;
                                    break;
                                default:
                                    rectangle.X = 270;
                                    rectangle.Y = 36;
                                    break;
                            }

                            mergeRight = true;
                        }
                        else if (up == num && down == num && left == -2 && right == -1)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 234;
                                    rectangle.Y = 54;
                                    break;
                                case 1:
                                    rectangle.X = 252;
                                    rectangle.Y = 54;
                                    break;
                                default:
                                    rectangle.X = 270;
                                    rectangle.Y = 54;
                                    break;
                            }

                            mergeLeft = true;
                        }
                    }

                    if (up != -1 && down != -1 && left == -1 && right == num)
                    {
                        if (up == -2 && down == num)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 72;
                                    rectangle.Y = 144;
                                    break;
                                case 1:
                                    rectangle.X = 72;
                                    rectangle.Y = 162;
                                    break;
                                default:
                                    rectangle.X = 72;
                                    rectangle.Y = 180;
                                    break;
                            }

                            mergeUp = true;
                        }
                        else if (down == -2 && up == num)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 72;
                                    rectangle.Y = 90;
                                    break;
                                case 1:
                                    rectangle.X = 72;
                                    rectangle.Y = 108;
                                    break;
                                default:
                                    rectangle.X = 72;
                                    rectangle.Y = 126;
                                    break;
                            }

                            mergeDown = true;
                        }
                    }
                    else if (up != -1 && down != -1 && left == num && right == -1)
                    {
                        if (up == -2 && down == num)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 90;
                                    rectangle.Y = 144;
                                    break;
                                case 1:
                                    rectangle.X = 90;
                                    rectangle.Y = 162;
                                    break;
                                default:
                                    rectangle.X = 90;
                                    rectangle.Y = 180;
                                    break;
                            }

                            mergeUp = true;
                        }
                        else if (down == -2 && up == num)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 90;
                                    rectangle.Y = 90;
                                    break;
                                case 1:
                                    rectangle.X = 90;
                                    rectangle.Y = 108;
                                    break;
                                default:
                                    rectangle.X = 90;
                                    rectangle.Y = 126;
                                    break;
                            }

                            mergeDown = true;
                        }
                    }
                    else if (up == -1 && down == num && left != -1 && right != -1)
                    {
                        if (left == -2 && right == num)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 0;
                                    rectangle.Y = 198;
                                    break;
                                case 1:
                                    rectangle.X = 18;
                                    rectangle.Y = 198;
                                    break;
                                default:
                                    rectangle.X = 36;
                                    rectangle.Y = 198;
                                    break;
                            }

                            mergeLeft = true;
                        }
                        else if (right == -2 && left == num)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 54;
                                    rectangle.Y = 198;
                                    break;
                                case 1:
                                    rectangle.X = 72;
                                    rectangle.Y = 198;
                                    break;
                                default:
                                    rectangle.X = 90;
                                    rectangle.Y = 198;
                                    break;
                            }

                            mergeRight = true;
                        }
                    }
                    else if (up == num && down == -1 && left != -1 && right != -1)
                    {
                        if (left == -2 && right == num)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 0;
                                    rectangle.Y = 216;
                                    break;
                                case 1:
                                    rectangle.X = 18;
                                    rectangle.Y = 216;
                                    break;
                                default:
                                    rectangle.X = 36;
                                    rectangle.Y = 216;
                                    break;
                            }

                            mergeLeft = true;
                        }
                        else if (right == -2 && left == num)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 54;
                                    rectangle.Y = 216;
                                    break;
                                case 1:
                                    rectangle.X = 72;
                                    rectangle.Y = 216;
                                    break;
                                default:
                                    rectangle.X = 90;
                                    rectangle.Y = 216;
                                    break;
                            }

                            mergeRight = true;
                        }
                    }
                    else if (up != -1 && down != -1 && left == -1 && right == -1)
                    {
                        if (up == -2 && down == -2)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 108;
                                    rectangle.Y = 216;
                                    break;
                                case 1:
                                    rectangle.X = 108;
                                    rectangle.Y = 234;
                                    break;
                                default:
                                    rectangle.X = 108;
                                    rectangle.Y = 252;
                                    break;
                            }

                            mergeUp = true;
                            mergeDown = true;
                        }
                        else if (up == -2)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 126;
                                    rectangle.Y = 144;
                                    break;
                                case 1:
                                    rectangle.X = 126;
                                    rectangle.Y = 162;
                                    break;
                                default:
                                    rectangle.X = 126;
                                    rectangle.Y = 180;
                                    break;
                            }

                            mergeUp = true;
                        }
                        else if (down == -2)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 126;
                                    rectangle.Y = 90;
                                    break;
                                case 1:
                                    rectangle.X = 126;
                                    rectangle.Y = 108;
                                    break;
                                default:
                                    rectangle.X = 126;
                                    rectangle.Y = 126;
                                    break;
                            }

                            mergeDown = true;
                        }
                    }
                    else if (up == -1 && down == -1 && left != -1 && right != -1)
                    {
                        if (left == -2 && right == -2)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 162;
                                    rectangle.Y = 198;
                                    break;
                                case 1:
                                    rectangle.X = 180;
                                    rectangle.Y = 198;
                                    break;
                                default:
                                    rectangle.X = 198;
                                    rectangle.Y = 198;
                                    break;
                            }

                            mergeLeft = true;
                            mergeRight = true;
                        }
                        else if (left == -2)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 0;
                                    rectangle.Y = 252;
                                    break;
                                case 1:
                                    rectangle.X = 18;
                                    rectangle.Y = 252;
                                    break;
                                default:
                                    rectangle.X = 36;
                                    rectangle.Y = 252;
                                    break;
                            }

                            mergeLeft = true;
                        }
                        else if (right == -2)
                        {
                            switch (num29)
                            {
                                case 0:
                                    rectangle.X = 54;
                                    rectangle.Y = 252;
                                    break;
                                case 1:
                                    rectangle.X = 72;
                                    rectangle.Y = 252;
                                    break;
                                default:
                                    rectangle.X = 90;
                                    rectangle.Y = 252;
                                    break;
                            }

                            mergeRight = true;
                        }
                    }
                    else if (up == -2 && down == -1 && left == -1 && right == -1)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 108;
                                rectangle.Y = 144;
                                break;
                            case 1:
                                rectangle.X = 108;
                                rectangle.Y = 162;
                                break;
                            default:
                                rectangle.X = 108;
                                rectangle.Y = 180;
                                break;
                        }

                        mergeUp = true;
                    }
                    else if (up == -1 && down == -2 && left == -1 && right == -1)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 108;
                                rectangle.Y = 90;
                                break;
                            case 1:
                                rectangle.X = 108;
                                rectangle.Y = 108;
                                break;
                            default:
                                rectangle.X = 108;
                                rectangle.Y = 126;
                                break;
                        }

                        mergeDown = true;
                    }
                    else if (up == -1 && down == -1 && left == -2 && right == -1)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 0;
                                rectangle.Y = 234;
                                break;
                            case 1:
                                rectangle.X = 18;
                                rectangle.Y = 234;
                                break;
                            default:
                                rectangle.X = 36;
                                rectangle.Y = 234;
                                break;
                        }

                        mergeLeft = true;
                    }
                    else if (up == -1 && down == -1 && left == -1 && right == -2)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 54;
                                rectangle.Y = 234;
                                break;
                            case 1:
                                rectangle.X = 72;
                                rectangle.Y = 234;
                                break;
                            default:
                                rectangle.X = 90;
                                rectangle.Y = 234;
                                break;
                        }

                        mergeRight = true;
                    }
                }
            }

            if (TileID.Sets.HasSlopeFrames[num])
            {
                BlockType num35 = tile.BlockType;
                if (num35 == BlockType.Solid)
                {
                    bool flag3 = num == up && tile38.TopSlope;
                    bool flag4 = num == left && tile40.LeftSlope;
                    bool flag5 = num == right && tile41.RightSlope;
                    bool flag6 = num == down && tile39.BottomSlope;
                    int num36 = 0;
                    int num37 = 0;
                    if (flag3.ToInt() + flag4.ToInt() + flag5.ToInt() + flag6.ToInt() > 2)
                    {
                        int num38 = (tile38.Slope == SlopeType.SlopeDownLeft).ToInt() + (tile41.Slope == SlopeType.SlopeDownLeft).ToInt() + (tile39.Slope == SlopeType.SlopeUpRight).ToInt() + (tile40.Slope == SlopeType.SlopeUpRight).ToInt();
                        int num39 = (tile38.Slope == SlopeType.SlopeDownRight).ToInt() + (tile41.Slope == SlopeType.SlopeUpLeft).ToInt() + (tile39.Slope == SlopeType.SlopeUpLeft).ToInt() + (tile40.Slope == SlopeType.SlopeDownRight).ToInt();
                        if (num38 == num39)
                        {
                            num36 = 2;
                            num37 = 4;
                        }
                        else if (num38 > num39)
                        {
                            bool num40 = num == upLeft && tile44.Slope == SlopeType.Solid;
                            bool flag7 = num == downRight && tile43.Slope == SlopeType.Solid;
                            if (num40 && flag7)
                            {
                                num37 = 4;
                            }
                            else if (flag7)
                            {
                                num36 = 6;
                            }
                            else
                            {
                                num36 = 7;
                                num37 = 1;
                            }
                        }
                        else
                        {
                            bool num41 = num == upRight && tile45.Slope == SlopeType.Solid;
                            bool flag8 = num == downLeft && tile42.Slope == SlopeType.Solid;
                            if (num41 && flag8)
                            {
                                num37 = 4;
                                num36 = 1;
                            }
                            else if (flag8)
                            {
                                num36 = 7;
                            }
                            else
                            {
                                num36 = 6;
                                num37 = 1;
                            }
                        }

                        rectangle.X = (18 + num36) * 18;
                        rectangle.Y = num37 * 18;
                    }
                    else
                    {
                        if (flag3 && flag4 && num == down && num == right)
                        {
                            num37 = 2;
                        }
                        else if (flag3 && flag5 && num == down && num == left)
                        {
                            num36 = 1;
                            num37 = 2;
                        }
                        else if (flag5 && flag6 && num == up && num == left)
                        {
                            num36 = 1;
                            num37 = 3;
                        }
                        else if (flag6 && flag4 && num == up && num == right)
                        {
                            num37 = 3;
                        }

                        if (num36 != 0 || num37 != 0)
                        {
                            rectangle.X = (18 + num36) * 18;
                            rectangle.Y = num37 * 18;
                        }
                    }
                }

                if (((int)num35) >= 2 && (rectangle.X < 0 || rectangle.Y < 0))
                {
                    int num42 = -1;
                    int num43 = -1;
                    int num44 = -1;
                    int num45 = 0;
                    int num46 = 0;
                    switch ((int)num35)
                    {
                        case 2:
                            num42 = left;
                            num43 = down;
                            num44 = downLeft;
                            num45++;
                            break;
                        case 3:
                            num42 = right;
                            num43 = down;
                            num44 = downRight;
                            break;
                        case 4:
                            num42 = left;
                            num43 = up;
                            num44 = upLeft;
                            num45++;
                            num46++;
                            break;
                        case 5:
                            num42 = right;
                            num43 = up;
                            num44 = upRight;
                            num46++;
                            break;
                    }

                    if (num != num42 || num != num43 || num != num44)
                    {
                        if (num == num42 && num == num43)
                        {
                            num45 += 2;
                        }
                        else if (num == num42)
                        {
                            num45 += 4;
                        }
                        else if (num == num43)
                        {
                            num45 += 4;
                            num46 += 2;
                        }
                        else
                        {
                            num45 += 2;
                            num46 += 2;
                        }
                    }

                    rectangle.X = (18 + num45) * 18;
                    rectangle.Y = num46 * 18;
                }
            }

            if (rectangle.X < 0 || rectangle.Y < 0)
            {
                if (!flag)
                {
                    WorldGen.TileMergeAttemptWeird(num, -1, Main.tileSolid, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                    tileMergeCullCache.Cull(ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                }

                if (TileID.Sets.Grass[num] || TileID.Sets.GrassSpecial[num] || Main.tileMoss[num] || TileID.Sets.tileMossBrick[num])
                {
                    WorldGen.TileMergeAttempt(num, -2, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                    tileMergeCullCache.Cull(ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                }

                if (up == num && down == num && left == num && right == num)
                {
                    if (upLeft != num && upRight != num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 108;
                                rectangle.Y = 18;
                                break;
                            case 1:
                                rectangle.X = 126;
                                rectangle.Y = 18;
                                break;
                            default:
                                rectangle.X = 144;
                                rectangle.Y = 18;
                                break;
                        }
                    }
                    else if (downLeft != num && downRight != num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 108;
                                rectangle.Y = 36;
                                break;
                            case 1:
                                rectangle.X = 126;
                                rectangle.Y = 36;
                                break;
                            default:
                                rectangle.X = 144;
                                rectangle.Y = 36;
                                break;
                        }
                    }
                    else if (upLeft != num && downLeft != num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 180;
                                rectangle.Y = 0;
                                break;
                            case 1:
                                rectangle.X = 180;
                                rectangle.Y = 18;
                                break;
                            default:
                                rectangle.X = 180;
                                rectangle.Y = 36;
                                break;
                        }
                    }
                    else if (upRight != num && downRight != num)
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 198;
                                rectangle.Y = 0;
                                break;
                            case 1:
                                rectangle.X = 198;
                                rectangle.Y = 18;
                                break;
                            default:
                                rectangle.X = 198;
                                rectangle.Y = 36;
                                break;
                        }
                    }
                    else
                    {
                        switch (num29)
                        {
                            case 0:
                                rectangle.X = 18;
                                rectangle.Y = 18;
                                break;
                            case 1:
                                rectangle.X = 36;
                                rectangle.Y = 18;
                                break;
                            default:
                                rectangle.X = 54;
                                rectangle.Y = 18;
                                break;
                        }
                    }
                }
                else if (up != num && down == num && left == num && right == num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 18;
                            rectangle.Y = 0;
                            break;
                        case 1:
                            rectangle.X = 36;
                            rectangle.Y = 0;
                            break;
                        default:
                            rectangle.X = 54;
                            rectangle.Y = 0;
                            break;
                    }
                }
                else if (up == num && down != num && left == num && right == num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 18;
                            rectangle.Y = 36;
                            break;
                        case 1:
                            rectangle.X = 36;
                            rectangle.Y = 36;
                            break;
                        default:
                            rectangle.X = 54;
                            rectangle.Y = 36;
                            break;
                    }
                }
                else if (up == num && down == num && left != num && right == num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 0;
                            rectangle.Y = 0;
                            break;
                        case 1:
                            rectangle.X = 0;
                            rectangle.Y = 18;
                            break;
                        default:
                            rectangle.X = 0;
                            rectangle.Y = 36;
                            break;
                    }
                }
                else if (up == num && down == num && left == num && right != num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 72;
                            rectangle.Y = 0;
                            break;
                        case 1:
                            rectangle.X = 72;
                            rectangle.Y = 18;
                            break;
                        default:
                            rectangle.X = 72;
                            rectangle.Y = 36;
                            break;
                    }
                }
                else if (up != num && down == num && left != num && right == num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 0;
                            rectangle.Y = 54;
                            break;
                        case 1:
                            rectangle.X = 36;
                            rectangle.Y = 54;
                            break;
                        default:
                            rectangle.X = 72;
                            rectangle.Y = 54;
                            break;
                    }
                }
                else if (up != num && down == num && left == num && right != num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 18;
                            rectangle.Y = 54;
                            break;
                        case 1:
                            rectangle.X = 54;
                            rectangle.Y = 54;
                            break;
                        default:
                            rectangle.X = 90;
                            rectangle.Y = 54;
                            break;
                    }
                }
                else if (up == num && down != num && left != num && right == num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 0;
                            rectangle.Y = 72;
                            break;
                        case 1:
                            rectangle.X = 36;
                            rectangle.Y = 72;
                            break;
                        default:
                            rectangle.X = 72;
                            rectangle.Y = 72;
                            break;
                    }
                }
                else if (up == num && down != num && left == num && right != num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 18;
                            rectangle.Y = 72;
                            break;
                        case 1:
                            rectangle.X = 54;
                            rectangle.Y = 72;
                            break;
                        default:
                            rectangle.X = 90;
                            rectangle.Y = 72;
                            break;
                    }
                }
                else if (up == num && down == num && left != num && right != num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 90;
                            rectangle.Y = 0;
                            break;
                        case 1:
                            rectangle.X = 90;
                            rectangle.Y = 18;
                            break;
                        default:
                            rectangle.X = 90;
                            rectangle.Y = 36;
                            break;
                    }
                }
                else if (up != num && down != num && left == num && right == num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 108;
                            rectangle.Y = 72;
                            break;
                        case 1:
                            rectangle.X = 126;
                            rectangle.Y = 72;
                            break;
                        default:
                            rectangle.X = 144;
                            rectangle.Y = 72;
                            break;
                    }
                }
                else if (up != num && down == num && left != num && right != num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 108;
                            rectangle.Y = 0;
                            break;
                        case 1:
                            rectangle.X = 126;
                            rectangle.Y = 0;
                            break;
                        default:
                            rectangle.X = 144;
                            rectangle.Y = 0;
                            break;
                    }
                }
                else if (up == num && down != num && left != num && right != num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 108;
                            rectangle.Y = 54;
                            break;
                        case 1:
                            rectangle.X = 126;
                            rectangle.Y = 54;
                            break;
                        default:
                            rectangle.X = 144;
                            rectangle.Y = 54;
                            break;
                    }
                }
                else if (up != num && down != num && left != num && right == num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 162;
                            rectangle.Y = 0;
                            break;
                        case 1:
                            rectangle.X = 162;
                            rectangle.Y = 18;
                            break;
                        default:
                            rectangle.X = 162;
                            rectangle.Y = 36;
                            break;
                    }
                }
                else if (up != num && down != num && left == num && right != num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 216;
                            rectangle.Y = 0;
                            break;
                        case 1:
                            rectangle.X = 216;
                            rectangle.Y = 18;
                            break;
                        default:
                            rectangle.X = 216;
                            rectangle.Y = 36;
                            break;
                    }
                }
                else if (up != num && down != num && left != num && right != num)
                {
                    switch (num29)
                    {
                        case 0:
                            rectangle.X = 162;
                            rectangle.Y = 54;
                            break;
                        case 1:
                            rectangle.X = 180;
                            rectangle.Y = 54;
                            break;
                        default:
                            rectangle.X = 198;
                            rectangle.Y = 54;
                            break;
                    }
                }
            }

            if (rectangle.X <= -1 || rectangle.Y <= -1)
            {
                if (num29 <= 0)
                {
                    rectangle.X = 18;
                    rectangle.Y = 18;
                }
                else if (num29 == 1)
                {
                    rectangle.X = 36;
                    rectangle.Y = 18;
                }

                if (num29 >= 2)
                {
                    rectangle.X = 54;
                    rectangle.Y = 18;
                }
            }

            if (Main.tileLargeFrames[num] == 1 && num29 == 3)
            {
                rectangle.Y += 90;
            }

            if (Main.tileLargeFrames[num] == 2 && num29 == 3)
            {
                rectangle.Y += 90;
            }

            tile.TileFrameX = (short)rectangle.X;
            tile.TileFrameY = (short)rectangle.Y;

            if ((rectangle.X != frameX && rectangle.Y != frameY && frameX >= 0 && frameY >= 0))
            {
                WorldGen.tileReframeCount++;
                if (WorldGen.tileReframeCount < 25)
                {
                    bool num52 = mergeUp;
                    bool flag15 = mergeDown;
                    bool flag16 = mergeLeft;
                    bool flag17 = mergeRight;
                    WorldGen.TileFrame(i - 1, j);
                    WorldGen.TileFrame(i + 1, j);
                    WorldGen.TileFrame(i, j - 1);
                    WorldGen.TileFrame(i, j + 1);
                    mergeUp = num52;
                    mergeDown = flag15;
                    mergeLeft = flag16;
                    mergeRight = flag17;
                }

                WorldGen.tileReframeCount--;
            }
        }

        public static void CustomTileFrame(int i, int j, bool resetFrame)
        {
            GetTileSurroundings(i, j, out int upLeft, out int up, out int upRight, out int left, out int right, out int downLeft, out int down, out int downRight);
            CustomTileFrame(i, j, ref upLeft, ref up, ref upRight, ref left, ref right, ref downLeft, ref down, ref downRight, resetFrame);
        }
    }
}
