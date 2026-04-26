using InfectedQualities.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;

namespace InfectedQualities.Utilities;

public static class TextureUtilities
{
    public static Texture2D TileDrawTexture(ushort type, byte color)
    {
        Texture2D paintTexture = Main.instance.TilePaintSystem.TryGetTileAndRequestIfNotReady(type, 0, color);
        if (paintTexture == null)
        {
            return TextureAssets.Tile[type].Value;
        }
        return paintTexture;
    }

    public static Color TileDrawColor(int i, int j, Color tintColor = default, bool emitDust = false, ushort tileShineType = 0)
    {
        Color color = Lighting.GetColor(i, j);
        if (tintColor == default)
        {
            tintColor = Color.White;
        }

        if (Main.LocalPlayer.dangerSense && TileDrawing.IsTileDangerous(i, j, Main.LocalPlayer))
        {
            color.R = byte.MaxValue;
            color.G = Math.Max((byte)50, color.G);
            color.B = Math.Max((byte)50, color.B);

            if (emitDust && Main.rand.NextBool(30))
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.RedTorch, 0f, 0f, 100, default, 0.3f)];
                dust.fadeIn = 1;
                dust.velocity *= 0.1f;
                dust.noLight = true;
                dust.noGravity = true;
            }
        }

        if (Main.LocalPlayer.findTreasure && Main.IsTileSpelunkable(i, j))
        {
            color.R = Math.Max((byte)200, color.R);
            color.G = Math.Max((byte)170, color.G);

            if (emitDust && Main.rand.NextBool(60))
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.TreasureSparkle, 0f, 0f, 150, default, 0.3f)];
                dust.fadeIn = 1;
                dust.velocity *= 0.1f;
                dust.noLight = true;
            }
        }

        if (Main.LocalPlayer.biomeSight)
        {
            Color sightColor = default;
            if (Main.IsTileBiomeSightable(i, j, ref sightColor))
            {
                color.R = Math.Max(sightColor.R, color.R);
                color.G = Math.Max(sightColor.G, color.G);
                color.B = Math.Max(sightColor.B, color.B);

                if (emitDust && Main.rand.NextBool(480))
                {
                    Dust dust = Main.dust[Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.RainbowMk2, 0f, 0f, 150, sightColor, 0.3f)];
                    dust.noGravity = true;
                    dust.fadeIn = 1;
                    dust.velocity *= 0.1f;
                    dust.noLightEmittence = true;
                }
            }
        }

        color = color.MultiplyRGB(tintColor);
        int red = Math.Min(color.R, byte.MaxValue);
        int green = Math.Min(color.G, byte.MaxValue);
        int blue = Math.Min(color.B, byte.MaxValue);

        blue <<= 16;
        green <<= 8;
        color.PackedValue = (uint)(red | green | blue) | 0xFF000000u;

        if (Main.tile[i, j].IsTileFullbright)
        {
            color = tintColor;
        }
        if (Main.tile[i, j].IsActuated)
        {
            byte alpha = color.A;
            color *= 0.4f;
            color.A = alpha;
        }
        else
        {
            if (tileShineType == 0)
            {
                tileShineType = Main.tile[i, j].TileType;
            }
            if (Main.shimmerAlpha > 0f && Main.tileSolid[tileShineType] || Main.tileShine2[tileShineType])
            {
                color = Main.shine(color, tileShineType);
            }
        }
        return color;
    }

    public static void TileDraw(int i, int j, Texture2D texture, Color color, SpriteBatch spriteBatch, Point? frames = null)
    {
        Vector2 offscreenVector = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
        Vector2 drawVector = new Vector2(i * 16, j * 16) + offscreenVector - Main.screenPosition;
        if (!frames.HasValue)
        {
            frames = new(Main.tile[i, j].TileFrameX, Main.tile[i, j].TileFrameY);
        }

        Rectangle frame = new(frames.Value.X, frames.Value.Y, 16, 16);
        if (Main.tile[i, j].Slope == SlopeType.Solid && !Main.tile[i, j].IsHalfBlock)
        {
            spriteBatch.Draw(texture, drawVector, frame, color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        else if (Main.tile[i, j].IsHalfBlock)
        {
            drawVector += new Vector2(0, 8);
            frame.Height = 8;
            spriteBatch.Draw(texture, drawVector, frame, color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        else
        {
            int width = 2;
            for (int q = 0; q < 8; q++)
            {
                int num = q * -2;
                int height = 16 - q * 2;
                int yOffset = 16 - height;
                int xOffset;
                switch (Main.tile[i, j].Slope)
                {
                    case SlopeType.SlopeDownLeft:
                        num = 0;
                        xOffset = q * 2;
                        height = 14 - q * 2;
                        yOffset = 0;
                        break;
                    case SlopeType.SlopeDownRight:
                        num = 0;
                        xOffset = 16 - q * 2 - 2;
                        height = 14 - q * 2;
                        yOffset = 0;
                        break;
                    case SlopeType.SlopeUpLeft:
                        xOffset = q * 2;
                        break;
                    default:
                        xOffset = 16 - q * 2 - 2;
                        break;
                }
                frame = new(frames.Value.X + xOffset, frames.Value.Y + yOffset, width, height);
                spriteBatch.Draw(texture, drawVector + new Vector2(xOffset, q * width + num), frame, color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }

            int slopeOffset = Main.tile[i, j].Slope <= SlopeType.SlopeDownRight ? 14 : 0;
            frame = new(frames.Value.X, frames.Value.Y + slopeOffset, 16, 2);
            spriteBatch.Draw(texture, drawVector + new Vector2(0, slopeOffset), frame, color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }

    public static Color WallBiomeColor(int i, int j, int type)
    {
        if (Main.LocalPlayer.biomeSight && !WorldGen.SolidTile(i, j, true) && (Main.tile[i, j].LiquidAmount == 0 || Main.tile[i, j].LiquidType == LiquidID.Water))
        {
            return InfectedQualitiesModSupport.ModWallBiomeSight[type];
        }
        return default;
    }
}
