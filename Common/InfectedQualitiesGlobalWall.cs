using InfectedQualities.Core;
using InfectedQualities.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Common;

public class InfectedQualitiesGlobalWall : GlobalWall
{
    public override void SetStaticDefaults()
    {
        for(int i = 0; i < 4; i++)
        {
            WallID.Sets.Corrupt[WallID.CorruptionUnsafe1 + i] = true;
            WallID.Sets.Crimson[WallID.CrimsonUnsafe1 + i] = true;
            WallID.Sets.Hallow[WallID.HallowUnsafe1 + i] = true;
        }
    }

    public override void RandomUpdate(int i, int j, int type)
    {
        if(WorldGen.AllowedToSpreadInfections)
        {
            if (type is WallID.EbonstoneUnsafe or WallID.CrimstoneUnsafe or WallID.PearlstoneBrickUnsafe)
            {
                int x = i + WorldGen.genRand.Next(-2, 3);
                int y = j + WorldGen.genRand.Next(-2, 3);
                if(WorldGen.InWorld(x, y, 10) && Main.tile[x, y].WallType == WallID.Stone) 
                {
                    WorldGen.ConvertWall(x, y, type);
                }
            }
            else if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes && type is WallID.CorruptGrassUnsafe or WallID.CrimsonGrassUnsafe or WallID.HallowedGrassUnsafe)
            {
                int x = i + WorldGen.genRand.Next(-2, 3);
                int y = j + WorldGen.genRand.Next(-2, 3);
                if (WorldGen.InWorld(x, y, 10) && Main.tile[x, y].WallType is WallID.GrassUnsafe or WallID.FlowerUnsafe or WallID.JungleUnsafe)
                {
                    WorldGen.ConvertWall(x, y, type);
                }
            }
        }
    }

    public override void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b)
    {
        if (Main.tile[i, j].IsWallInvisible && !Main.ShouldShowInvisibleWalls()) return;

        Color sightColor = TextureUtilities.WallBiomeColor(i, j, type);
        if (sightColor != default)
        {
            if (!Main.tile[i, j].IsWallFullbright) sightColor *= ModContent.GetInstance<InfectedQualitiesClientConfig>().BiomeSightWallHighlightBrightness / 255f;

            r = sightColor.R / 255f;
            g = sightColor.G / 255f;
            b = sightColor.B / 255f;
        }
    }

    public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
    {
        Color sightColor = TextureUtilities.WallBiomeColor(i, j, type);
        if (Main.rand.NextBool(480) && sightColor != default)
        {
            if (!Main.tile[i, j].IsWallFullbright)
            {
                sightColor *= 4 / 3f;
                sightColor *= ModContent.GetInstance<InfectedQualitiesClientConfig>().BiomeSightWallHighlightBrightness / 255f;

                sightColor.R = Math.Min(sightColor.R, (byte)255);
                sightColor.G = Math.Min(sightColor.G, (byte)255);
                sightColor.B = Math.Min(sightColor.B, (byte)255);
                sightColor.A = Math.Min(sightColor.A, (byte)255);
            }

            Dust dust = Dust.NewDustDirect(new Vector2(i * 16, j * 16), 16, 16, DustID.RainbowMk2, 0f, 0f, 150, sightColor, 0.3f);
            dust.noGravity = true;
            dust.fadeIn = 1f;
            dust.velocity *= 0.1f;
            dust.noLightEmittence = true;
        }
    }
}
