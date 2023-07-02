using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using System;
using InfectedQualities.Helpers;
using InfectedQualities.Common;

namespace InfectedQualities.Content.Projectiles
{
    public class LimeSpray : ModProjectile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableLimeSolution;
        }

        private static Color dustColor;

        public override void Load()
        {
            dustColor = new(0, 255, 127);
        }

        public override string Texture => "Terraria/Images/Projectile_145";

        public override void SetDefaults()
        {
            Projectile.DefaultToSpray();
            Projectile.aiStyle = 0;
        }

        public override bool CanHitPlayer(Player target) => false;

        public override void AI()
        {
            if (Projectile.owner == Main.myPlayer)
            {
                ApplyJungleConversion((int)(Projectile.position.X + (Projectile.width * 0.5f)) / 16, (int)(Projectile.position.Y + (Projectile.height * 0.5f)) / 16, Main.player[Projectile.owner].HeldItem.type == ItemID.Clentaminator2 ? 3 : 2);
            }

            if (Projectile.timeLeft > 133)
            {
                Projectile.timeLeft = 133;
            }

            if (Projectile.ai[0] > 7f)
            {
                float dustScale = 1f;

                if (Projectile.ai[0] == 8f)
                {
                    dustScale = 0.2f;
                }
                else if (Projectile.ai[0] == 9f)
                {
                    dustScale = 0.4f;
                }
                else if (Projectile.ai[0] == 10f)
                {
                    dustScale = 0.6f;
                }
                else if (Projectile.ai[0] == 11f)
                {
                    dustScale = 0.8f;
                }

                Projectile.ai[0] += 1f;


                Dust dust = Dust.NewDustDirect(Projectile.VisualPosition, Projectile.width, Projectile.height, DustID.DirtSpray, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, dustColor);

                dust.noGravity = true;
                dust.scale *= 1.75f * dustScale;
                dust.velocity.X *= 2f;
                dust.velocity.Y *= 2f;
            }
            else
            {
                Projectile.ai[0] += 1f;
            }
        }

        public static void ApplyJungleConversion(int i, int j, int size)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (!WorldGen.InWorld(k, l, 1) || Math.Abs(k - i) + Math.Abs(l - j) >= 6)
                    {
                        continue;
                    }

                    if ((WallID.Sets.Conversion.Grass[Main.tile[k, l].WallType] || WallID.Sets.Conversion.HardenedSand[Main.tile[k, l].WallType] || WallID.Sets.Conversion.Snow[Main.tile[k, l].WallType]) && Main.tile[k, l].WallType != WallID.JungleUnsafe && Main.tile[k, l].WallType != WallID.Jungle)
                    {
                        Main.tile[k, l].WallType = WallID.JungleUnsafe;
                        WorldGen.SquareWallFrame(k, l);
                        NetMessage.SendTileSquare(-1, k, l);
                    }
                    else if ((WallID.Sets.Conversion.Stone[Main.tile[k, l].WallType] || WallID.Sets.Conversion.Dirt[Main.tile[k, l].WallType] || TileUtils.IDSets.WallMud[Main.tile[k, l].WallType] || WallID.Sets.Conversion.Sandstone[Main.tile[k, l].WallType] || WallID.Sets.Conversion.Ice[Main.tile[k, l].WallType] || TileUtils.IDSets.WallMushroom[Main.tile[k, l].WallType]) && !(Main.tile[k, l].WallType is WallID.MudUnsafe or WallID.MudWallEcho))
                    {
                        if (!(WallID.Sets.Conversion.Stone[Main.tile[k, l].WallType] && !TileUtils.TileUnderground(l)))
                        {
                            Main.tile[k, l].WallType = WallID.MudUnsafe;
                            WorldGen.SquareWallFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l);
                        }
                    }
                    else if (WallID.Sets.Conversion.NewWall1[Main.tile[k, l].WallType] && Main.tile[k, l].WallType != WallID.JungleUnsafe1 && Main.tile[k, l].WallType != WallID.Jungle1Echo)
                    {
                        Main.tile[k, l].WallType = WallID.JungleUnsafe1;
                        WorldGen.SquareWallFrame(k, l);
                        NetMessage.SendTileSquare(-1, k, l);
                    }
                    else if (WallID.Sets.Conversion.NewWall2[Main.tile[k, l].WallType] && Main.tile[k, l].WallType != WallID.JungleUnsafe2 && Main.tile[k, l].WallType != WallID.Jungle2Echo)
                    {
                        Main.tile[k, l].WallType = WallID.JungleUnsafe2;
                        WorldGen.SquareWallFrame(k, l);
                        NetMessage.SendTileSquare(-1, k, l);
                    }
                    else if (WallID.Sets.Conversion.NewWall3[Main.tile[k, l].WallType] && Main.tile[k, l].WallType != WallID.JungleUnsafe3 && Main.tile[k, l].WallType != WallID.Jungle3Echo)
                    {
                        Main.tile[k, l].WallType = WallID.JungleUnsafe3;
                        WorldGen.SquareWallFrame(k, l);
                        NetMessage.SendTileSquare(-1, k, l);
                    }
                    else if (WallID.Sets.Conversion.NewWall4[Main.tile[k, l].WallType] && Main.tile[k, l].WallType != WallID.JungleUnsafe4 && Main.tile[k, l].WallType != WallID.Jungle4Echo)
                    {
                        Main.tile[k, l].WallType = WallID.JungleUnsafe4;
                        WorldGen.SquareWallFrame(k, l);
                        NetMessage.SendTileSquare(-1, k, l);
                    }

                    if ((TileID.Sets.Conversion.Grass[Main.tile[k, l].TileType] || TileID.Sets.Conversion.JungleGrass[Main.tile[k, l].TileType] || TileID.Sets.Conversion.MushroomGrass[Main.tile[k, l].TileType]) && Main.tile[k, l].TileType != TileID.JungleGrass)
                    {
                        WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, TileID.JungleGrass);
                        Main.tile[k, l].TileType = TileID.JungleGrass;
                        WorldGen.SquareTileFrame(k, l);
                        NetMessage.SendTileSquare(-1, k, l);
                    }
                    else if ((TileID.Sets.Conversion.Stone[Main.tile[k, l].TileType] || TileID.Sets.Conversion.Sandstone[Main.tile[k, l].TileType] || TileID.Sets.Conversion.Ice[Main.tile[k, l].TileType] || TileID.Sets.Conversion.Dirt[Main.tile[k, l].TileType] || TileUtils.IDSets.TileConversionMud[Main.tile[k, l].TileType] || TileID.Sets.Conversion.Sand[Main.tile[k, l].TileType] || TileID.Sets.Conversion.HardenedSand[Main.tile[k, l].TileType] || TileID.Sets.Conversion.Snow[Main.tile[k, l].TileType]) && Main.tile[k, l].TileType != TileID.Mud)
                    {
                        ushort num2 = TileID.Mud;
                        if (WorldGen.TileIsExposedToAir(k, l))
                        {
                            num2 = TileID.JungleGrass;
                        }

                        if (TileID.Sets.Conversion.Stone[Main.tile[k, l].TileType] && !TileUtils.TileUnderground(l))
                        {
                            num2 = TileID.Stone;
                        }

                        if (!(num2 == TileID.Stone && Main.tile[k, l].TileType == TileID.Stone))
                        {
                            WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, num2);
                            Main.tile[k, l].TileType = num2;
                            WorldGen.SquareTileFrame(k, l);
                            NetMessage.SendTileSquare(-1, k, l);
                        }
                    }
                    else if (Main.tile[k, l].TileType == TileID.BreakableIce)
                    {
                        WorldGen.KillTile(k, l);
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, k, l);
                    }
                }
            }
        }
    }
}
