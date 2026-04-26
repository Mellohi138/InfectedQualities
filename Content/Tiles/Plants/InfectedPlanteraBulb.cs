using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.Enums;
using System;
using Terraria.GameContent.Drawing;
using InfectedQualities.Core;

namespace InfectedQualities.Content.Tiles.Plants;

public class InfectedPlanteraBulb : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileLighted[Type] = true;

        TileID.Sets.ReplaceTileBreakUp[Type] = true;
        TileID.Sets.MultiTileSway[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.AnchorValidTiles = [TileID.JungleGrass, TileID.CorruptJungleGrass, TileID.CrimsonJungleGrass, ModContent.TileType<HallowedJungleGrass>()];
        TileObjectData.addTile(Type);

        AddMapEntry(new(109, 106, 174), Language.GetText("MapObject.PlanterasBulb"));
        AddMapEntry(new(183, 69, 68), Language.GetText("MapObject.PlanterasBulb"));
        AddMapEntry(new(228, 139, 215), Language.GetText("MapObject.PlanterasBulb"));

        VanillaFallbackOnModDeletion = TileID.PlanteraBulb;
    }

    public override ushort GetMapOption(int i, int j) => (ushort)TileObjectData.GetTileStyle(Main.tile[i, j]);

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 3 : 10;

    public override bool CreateDust(int i, int j, ref int type)
    {
        switch (TileObjectData.GetTileStyle(Main.tile[i, j]))
        {
            case 0:
                type = DustID.CorruptPlants;
                break;
            case 1:
                type = DustID.CrimsonPlants;
                break;
            case 2:
                type = DustID.HallowedPlants;
                break;
        }
        return true;
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        g = 0.1f;
        switch (TileObjectData.GetTileStyle(Main.tile[i, j]))
        {
            case 0:
                r = 0.2f;
                b = 0.5f;
                break;
            case 1:
                r = 0.5f;
                b = 0.1f;
                break;
            case 2:
                r = 0.5f;
                b = 0.7f;
                break;
        }
    }

    /// <summary>
    /// I was normally going to just use plantera's tile frames but for some reason whenever the tiles change, the animation frame gets offset by 1.
    /// So I am forced to copy the animation instead of using it again.
    /// </summary>
    public override void AnimateTile(ref int frame, ref int frameCounter)
    {
        if (++frameCounter > 20)
        {
            frameCounter = 0;
            frame = ++frame % 4;
        }
    }

    public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset) => frameYOffset = Main.tileFrame[type] * 36;

    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        if (TileObjectData.IsTopLeft(i, j))
        {
            Main.instance.TilesRenderer.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileGrass);
        }
        return false;
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        bool topLeft = TileObjectData.IsTopLeft(i, j);
        Vector2 vector = new(i * 16, j * 16);

        switch (TileObjectData.GetTileStyle(Main.tile[i, j]))
        {
            case 0:
                if(Main.rand.NextBool(10))
                {
                    Dust.NewDust(vector, 16, 16, DustID.Demonite);
                }
                break;
            case 1:
                if (topLeft && WorldGen.genRand.NextBool(10))
                {
                    vector += new Vector2(16, 0);
                    Dust.NewDust(vector, 16, 16, DustID.Crimson);
                }
                break;
            case 2:
                if (Main.rand.NextBool(127))
                {
                    Dust shineDust = Main.dust[Dust.NewDust(vector, 16, 16, DustID.TintableDustLighted, 0f, 0f, 254, Color.White, 0.5f)];
                    shineDust.velocity *= 0f;
                }
                if (topLeft && WorldGen.genRand.NextBool(10))
                {
                    vector += new Vector2(16, 16);
                    Dust planteraDust = Main.dust[Dust.NewDust(vector, 16, 16, DustID.PlanteraBulb, newColor: Color.Magenta, Alpha: 200)];
                    planteraDust.noGravity = true;
                }
                break;
        }
    }

    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        if (TileObjectData.IsTopLeft(i, j))
        {
            short num = -1;
            for (int k = i; k < i + 2; k++)
            {
                if (Main.tile[k, j + 2].TileType == TileID.CorruptJungleGrass)
                {
                    num = 0;
                }
                else if (Main.tile[k, j + 2].TileType == TileID.CrimsonJungleGrass)
                {
                    num = 36;
                }
                else if (Main.tile[k, j + 2].TileType == ModContent.TileType<HallowedJungleGrass>())
                {
                    num = 72;
                }
                else if (num == -1 && Main.tile[k, j + 2].TileType == TileID.JungleGrass)
                {
                    num = -2;
                }
            }

            if (num != -1)
            {
                for (int m = i; m < i + 2; m++)
                {
                    for (int n = j; n < j + 2; n++)
                    {
                        if (Main.tile[m, n].HasTile && Main.tile[m, n].TileType == Type)
                        {
                            if (num == -2)
                            {
                                Main.tile[m, n].TileType = TileID.PlanteraBulb;
                                Main.tile[m, n].TileFrameX %= 36;
                            }
                            else
                            {
                                Main.tile[m, n].TileFrameX = (short)(num + Main.tile[m, n].TileFrameX % 36);
                            }
                        }
                    }
                }
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendTileSquare(-1, i, j, 2);
                }
            }
        }
        else if (WorldGen.TileType(i, j - 1) == Type)
        {
            WorldGen.TileFrame(i, j - 1);
        }
        return true;
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        float x = i * 16;
        float y = j * 16;
        float num8 = -1f;
        int plr = 0;
        for (int m = 0; m < 255; m++)
        {
            float num9 = Math.Abs(Main.player[m].position.X - x) + Math.Abs(Main.player[m].position.Y - y);
            if (num9 < num8 || num8 == -1f)
            {
                plr = m;
                num8 = num9;
            }
        }

        if (num8 / 16f < 50f)
        {
            NPC.SpawnOnPlayer(plr, NPCID.Plantera);
        }
    }

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes;
}
