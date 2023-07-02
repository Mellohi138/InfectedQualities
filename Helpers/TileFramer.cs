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

        public static void ApplyTileMerge(ushort tileFrom, ushort tileTo, bool merge = true)
        {
            Main.tileMerge[tileFrom][tileTo] = merge;
            Main.tileMerge[tileTo][tileFrom] = merge;
        }

        public static void ApplyTileMerge(ushort tileFrom, ushort[] tileToList, bool merge = true)
        {
            for(int i = 0; i < tileToList.Length; i++)
            {
                ushort tileTo = tileToList[i];

                Main.tileMerge[tileFrom][tileTo] = merge;
                Main.tileMerge[tileTo][tileFrom] = merge;
            }
        }

        public static void ApplyTileMerge(ushort[] tileFromList, ushort[] tileToList, bool merge = true)
        {
            for (int i = 0; i < tileFromList.Length; i++)
            {
                ushort tileFrom = tileFromList[i];

                for (int j = 0; j < tileToList.Length; j++)
                {
                    ushort tileTo = tileToList[j];

                    Main.tileMerge[tileFrom][tileTo] = merge;
                    Main.tileMerge[tileTo][tileFrom] = merge;
                }
            }
        }

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

            if (!TileID.Sets.Mud[num] && TileID.Sets.OreMergesWithMud[num])
            {
                WorldGen.TileMergeAttempt(num, TileID.Sets.Mud, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
            }
        }

        public static void TileMergeAttempt(int myType, bool[] lookfor, ushort exclude, ref int up, ref int down, ref int left, ref int right, ref int upLeft, ref int upRight, ref int downLeft, ref int downRight)
        {
            if (up > -1 && lookfor[up] && up != exclude)
            {
                up = myType;
            }

            if (down > -1 && lookfor[down] && down != exclude)
            {
                down = myType;
            }

            if (left > -1 && lookfor[left] && left != exclude)
            {
                left = myType;
            }

            if (right > -1 && lookfor[right] && right != exclude)
            {
                right = myType;
            }

            if (upLeft > -1 && lookfor[upLeft] && upLeft != exclude)
            {
                upLeft = myType;
            }

            if (upRight > -1 && lookfor[upRight] && upRight != exclude)
            {
                upRight = myType;
            }

            if (downLeft > -1 && lookfor[downLeft] && downLeft != exclude)
            {
                downLeft = myType;
            }

            if (downRight > -1 && lookfor[downRight] && downRight != exclude)
            {
                downRight = myType;
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
