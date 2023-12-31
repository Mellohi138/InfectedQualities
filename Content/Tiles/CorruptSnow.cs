﻿using InfectedQualities.Common;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using InfectedQualities.Helpers;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace InfectedQualities.Content.Tiles
{
    public class CorruptSnow : ModTile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().EnableInfectedSnowBiomes;
        }

        public override string Texture => "InfectedQualities/Client/Assets/Tiles/CorruptSnow";

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileBrick[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileMerge[TileID.Dirt][Type] = true;
            if(!ModContent.GetInstance<ClientConfig>().EnableSmoothSnowIceBlending)
            {
                TileFramer.ApplyTileMerge(Type, TileID.CorruptIce);
            }

            TileID.Sets.CanBeDugByShovel[Type] = true;
            TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
            TileID.Sets.Snow[Type] = true;
            TileID.Sets.IcesSnow[Type] = true;
            TileID.Sets.ChecksForMerge[Type] = true;

            TileID.Sets.Corrupt[Type] = true;
            TileID.Sets.AddCorruptionTile(Type);
            TileID.Sets.CorruptBiomeSight[Type] = true;
            TileID.Sets.CorruptCountCollection.Add(Type);

            TileID.Sets.SnowBiome[Type] = 1;
            TileID.Sets.Conversion.Snow[Type] = true;

            RegisterItemDrop(ModContent.ItemType<Items.Tiles.CorruptSnow>());
            DustType = DustID.SnowBlock;
            HitSound = SoundID.Item50;

            AddMapEntry(new(214, 203, 236));
        }

        public override bool HasWalkDust() => true;

        public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color)
        {
            dustType = DustID.Snow;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void RandomUpdate(int i, int j)
        {
            TileUtils.SpreadInfection(i, j, 0);

            if (Main.hardMode && Main.remixWorld)
            {
                MethodInfo nearChlorophyte = typeof(WorldGen).GetMethod("nearbyChlorophyte", BindingFlags.Static | BindingFlags.NonPublic);
                if ((bool)nearChlorophyte?.Invoke(null, new object[] { i, j }))
                {
                    Main.tile[i, j].TileType = TileID.SnowBlock;
                    WorldGen.SquareTileFrame(i, j);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, i, j);
                    }
                }
            }
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            if(ModContent.GetInstance<ClientConfig>().EnableSmoothSnowIceBlending)
            {
                TileFramer.GetTileSurroundings(i, j, out int upLeft, out int up, out int upRight, out int left, out int right, out int downLeft, out int down, out int downRight);

                WorldGen.TileMergeAttempt(Type, Main.tileBrick, TileID.Sets.Ices, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                TileFramer.TileMergeAttemptFrametest(i, j, Type, TileID.Sets.IcesSlush, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
                if (down == TileID.Stalactite)
                {
                    down = Type;
                }

                TileFramer.CustomTileFrame(i, j, ref upLeft, ref up, ref upRight, ref left, ref right, ref downLeft, ref down, ref downRight, resetFrame);
                return false;
            }
            return true;
        }
    }
}
