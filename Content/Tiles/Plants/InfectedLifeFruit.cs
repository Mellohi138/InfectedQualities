using InfectedQualities.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace InfectedQualities.Content.Tiles.Plants;

public class InfectedLifeFruit : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileOreFinderPriority[Type] = 810;
        Main.tileSpelunker[Type] = true;
        Main.tileCut[Type] = true;

        TileID.Sets.ReplaceTileBreakUp[Type] = true;
        TileID.Sets.FriendlyFairyCanLureTo[Type] = true;
        TileID.Sets.MultiTileSway[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.RandomStyleRange = 3;
        TileObjectData.newTile.StyleWrapLimit = 3;
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
        TileObjectData.newTile.AnchorValidTiles = [TileID.JungleGrass, TileID.CorruptJungleGrass, TileID.CrimsonJungleGrass, ModContent.TileType<HallowedJungleGrass>()];
        TileObjectData.addTile(Type);

        RegisterItemDrop(ItemID.LifeFruit);
        AddMapEntry(new(87, 84, 151), Language.GetText("ItemName.LifeFruit"));
        AddMapEntry(new(180, 82, 82), Language.GetText("ItemName.LifeFruit"));
        AddMapEntry(new(82, 166, 199), Language.GetText("ItemName.LifeFruit"));

        VanillaFallbackOnModDeletion = TileID.LifeFruit;
    }

    public override ushort GetMapOption(int i, int j) => (ushort)(Main.tile[i, j].TileFrameY / 36);

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 3 : 10;

    public override bool CreateDust(int i, int j, ref int type)
    {
        switch (Main.tile[i, j].TileFrameY / 36)
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
        int tileStyle = Main.tile[i, j].TileFrameY / 36;
        if (tileStyle == 1)
        {
            return;
        }
        Vector2 vector = new (i * 16, j * 16);
        if (tileStyle == 0 && Main.rand.NextBool(500))
        {
            Dust.NewDust(vector, 16, 16, DustID.Demonite);
        }
        else if (tileStyle == 2 && Main.rand.NextBool(127))
        {
            Dust shineDust = Main.dust[Dust.NewDust(vector, 16, 16, DustID.TintableDustLighted, 0f, 0f, 254, Color.White, 0.5f)];
            shineDust.velocity *= 0f;
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
                                Main.tile[m, n].TileType = TileID.LifeFruit;
                                Main.tile[m, n].TileFrameY %= 36;
                            }
                            else
                            {
                                Main.tile[m, n].TileFrameY = (short)(num + Main.tile[m, n].TileFrameY % 36);
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

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes;
}
