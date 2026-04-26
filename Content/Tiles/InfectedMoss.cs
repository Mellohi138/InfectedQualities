using InfectedQualities.Core;
using InfectedQualities.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Content.Tiles;

[Autoload(false)]
public class InfectedMoss(InfectionType infectionType, MossType mossType) : ModTile
{
	private Color MossColor { get; set; } = default;

	private ushort InfectedStoneType { get; set; }

	public override void SetStaticDefaults()
	{
		Main.tileBrick[Type] = true;
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;

		TileID.Sets.NeedsGrassFraming[Type] = true;
		TileID.Sets.ResetsHalfBrickPlacementAttempt[Type] = false;
		TileID.Sets.Conversion.Moss[Type] = true;
		Main.tileMerge[TileID.Dirt][Type] = true;

		MinPick = 65;
		MineResist = 2;
		HitSound = SoundID.Grass;

		switch (infectionType)
		{
			case InfectionType.Corrupt:
				InfectedStoneType = TileID.Ebonstone;
				TileID.Sets.Corrupt[Type] = true;
				TileID.Sets.AddCorruptionTile(Type);
				TileID.Sets.CorruptBiomeSight[Type] = true;
				TileID.Sets.CorruptCountCollection.Add(Type);
				RegisterItemDrop(ItemID.EbonstoneBlock);
				break;
			case InfectionType.Crimson:
				InfectedStoneType = TileID.Crimstone;
				TileID.Sets.Crimson[Type] = true;
				TileID.Sets.AddCrimsonTile(Type);
				TileID.Sets.CrimsonBiomeSight[Type] = true;
				TileID.Sets.CrimsonCountCollection.Add(Type);
				RegisterItemDrop(ItemID.CrimstoneBlock);
				break;
			case InfectionType.Hallowed:
				InfectedStoneType = TileID.Pearlstone;
				Main.tileShine[Type] = 9000;
				TileID.Sets.Hallow[Type] = true;
				TileID.Sets.HallowBiome[Type] = 1;
				TileID.Sets.HallowBiomeSight[Type] = true;
				TileID.Sets.HallowCountCollection.Add(Type);
				RegisterItemDrop(ItemID.PearlstoneBlock);
				break;
		}

		switch (mossType)
		{
			case MossType.Green:
				MossColor = new(49, 134, 114);
				AddMapEntry(MossColor);
				DustType = DustID.GreenMoss;
				break;
			case MossType.Brown:
				MossColor = new(126, 134, 49);
				AddMapEntry(MossColor);
				DustType = DustID.BrownMoss;
				break;
			case MossType.Red:
				MossColor = new(134, 59, 49);
				AddMapEntry(MossColor);
				DustType = DustID.RedMoss;
				break;
			case MossType.Blue:
				MossColor = new(43, 86, 140);
				AddMapEntry(MossColor);
				DustType = DustID.BlueMoss;
				break;
			case MossType.Purple:
				MossColor = new(121, 49, 134);
				AddMapEntry(MossColor);
				DustType = DustID.PurpleMoss;
				break;
			case MossType.Lava:
				MossColor = new(150, 100, 50, 0);
				AddMapEntry(new(254, 121, 2));
				DustType = DustID.LavaMoss;
				break;
			case MossType.Krypton:
				MossColor = new(0, 200, 0, 0);
				AddMapEntry(new(114, 254, 2));
				DustType = DustID.KryptonMoss;
				break;
			case MossType.Xenon:
				MossColor = new(0, 180, 250, 0);
				AddMapEntry(new(0, 197, 208));
				DustType = DustID.XenonMoss;
				break;
			case MossType.Argon:
				MossColor = new(225, 0, 125, 0);
				AddMapEntry(new(208, 0, 126));
				DustType = DustID.ArgonMoss;
				break;
			case MossType.Neon:
				MossColor = new(150, 0, 250, 0);
				AddMapEntry(new(220, 12, 237));
				DustType = DustID.VioletMoss;
				break;
			case MossType.Helium:
				AddMapEntry(new(255, 76, 76));
				AddMapEntry(new(255, 195, 76));
				AddMapEntry(new(195, 255, 76));
				AddMapEntry(new(76, 255, 76));
				AddMapEntry(new(76, 255, 195));
				AddMapEntry(new(76, 195, 255));
				AddMapEntry(new(77, 76, 255));
				AddMapEntry(new(196, 76, 255));
				AddMapEntry(new(255, 76, 195));
				break;
		}

		Main.tileMerge[InfectedStoneType][Type] = true;
		TileID.Sets.NeedsGrassFramingDirt[Type] = InfectedStoneType;
		TileLoader.RegisterSimpleConversion(TileUtilities.GetMossType(null, mossType), infectionType.ToConversionID(), Type);
		VanillaFallbackOnModDeletion = TileUtilities.GetMossType(null, mossType);
	}

	public override ushort GetMapOption(int i, int j)
	{
		if (mossType == MossType.Helium)
		{
			return (ushort)((i + j) % 9);
		}
		return 0;
	}

	public override void RandomUpdate(int i, int j)
	{
		WorldGen.SpreadInfectionToNearbyTile(i, j, infectionType.ToConversionID());
		if (WorldGen.genRand.NextBool())
		{
			for (int x = i - 1; x < i + 2; x++)
			{
				for (int y = j - 1; y < j + 2; y++)
				{
					if ((i != x || j != y) && Main.tile[x, y].HasTile)
					{
						ushort type = Main.tile[x, y].TileType;
						InfectionType? infectionType = type switch
						{
							TileID.Ebonstone => InfectionType.Corrupt,
							TileID.Crimstone => InfectionType.Crimson,
							TileID.Pearlstone => InfectionType.Hallowed,
							_ => null
						};

						if (infectionType.HasValue)
						{
							WorldGen.SpreadGrass(x, y, type, TileUtilities.GetMossType(infectionType, mossType), false, Main.tile[i, j].BlockColorAndCoating());
							WorldGen.SquareTileFrame(x, y);
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, i, j, 3);
							}
						}
						else if (type is TileID.Stone or TileID.GrayBrick)
						{
							WorldGen.SpreadGrass(x, y, type, TileUtilities.GetMossType(null, mossType, type == TileID.GrayBrick), false, Main.tile[i, j].BlockColorAndCoating());
							WorldGen.SquareTileFrame(x, y);
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, i, j, 3);
							}
						}
					}
				}
			}

			if (WorldGen.genRand.NextBool(6))
			{
				int x = i;
				int y = j;
				switch (WorldGen.genRand.Next(4))
				{
					case 0:
						x--;
						break;
					case 1:
						x++;
						break;
					case 2:
						y--;
						break;
					default:
						y++;
						break;
				}
				Tile tile = Main.tile[x, y];
				if (!tile.HasTile && WorldGen.SolidTile(i, j))
				{
					tile.HasTile = true;
					tile.TileType = TileID.LongMoss;
					tile.TileFrameX = (short)((int)mossType * 18);
					tile.TileFrameY = (short)(WorldGen.genRand.Next(3) * 18);
					WorldGen.SquareTileFrame(x, y);
					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendTileSquare(-1, x, y);
					}
				}
			}
		}
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		if (mossType == MossType.Helium)
		{
			Dust dust = Main.dust[Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.RainbowMk2, 0f, 0f, 0, Main.DiscoColor)];
			dust.noGravity = true;
			dust.noLightEmittence = true;
			return false;
		}
		return true;
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		bool preDraw = mossType != MossType.Helium && mossType >= MossType.Lava;

		if (TileDrawing.IsVisible(Main.tile[i, j]))
		{
			short frameX = Main.tile[i, j].TileFrameX;
			short frameY = Main.tile[i, j].TileFrameY;

			if (frameX > 216 || frameY > 72)
			{
				if (frameX is 0 or 18 && frameY is 270 or 288 or 306)
				{
					frameX = (short)(frameX == 18 ? 72 : 0);
					frameY -= 270;
				}
				else if (frameX is 36 or 54)
				{
					if (frameY is 90 or 108)
					{
						frameX = 18;
						frameY = 18;
					}
					else if (frameY is 126 or 144)
					{
						frameX = 36;
						frameY = 18;
					}
					else if (frameY is 162 or 180)
					{
						frameX = 54;
						frameY = 18;
					}
				}
				else if (frameX is 72 or 90)
				{
					if (frameY is 90 or 144)
					{
						frameX = (short)(frameX == 90 ? 72 : 0);
						frameY = 0;
					}
					else if (frameY is 108 or 162)
					{
						frameX = (short)(frameX == 90 ? 72 : 0);
						frameY = 18;
					}
					else if (frameY is 126 or 180)
					{
						frameX = (short)(frameX == 90 ? 72 : 0);
						frameY = 36;
					}
				}
				else if (frameX == 108)
				{
					if (frameY is 90 or 108 or 126)
					{
						frameX = (short)(frameY + 18);
						frameY = 0;
					}
					else if (frameY is 144 or 162 or 180)
					{
						frameX = (short)(frameY - 36);
						frameY = 54;
					}
					else if (frameY is 216 or 234 or 252)
					{
						frameX = 90;
						frameY -= 216;
					}
					else if (frameY > 306)
					{
						frameX = 18;
						frameY = 18;
					}
				}
				else if (frameX == 126)
				{
					if (frameY is 90 or 144)
					{
						frameX = 90;
						frameY = 0;
					}
					else if (frameY is 108 or 162)
					{
						frameX = 90;
						frameY = 18;
					}
					else if (frameY is 126 or 180)
					{
						frameX = 90;
						frameY = 36;
					}
					else if (frameY > 306)
					{
						frameX = 36;
						frameY = 18;
					}
				}
				else if (frameX is 144 or 162 or 180)
				{
					if (frameY is 126 or 144 or 162)
					{
						frameX = (short)(frameY - 108);
						frameY = 18;
					}
					else if (frameY is 90 or 108 or 180)
					{
						frameX -= 126;
						frameY = 18;
					}
					else if (frameX == 144 && frameY >= 324)
					{
						frameX = 54;
						frameY = 18;
					}
				}

				if (frameY is 198 or 216)
				{
					if (frameX is 0 or 54)
					{
						frameX = 18;
						frameY = (short)(frameY == 216 ? 36 : 0);
					}
					else if (frameX is 18 or 72)
					{
						frameX = 36;
						frameY = (short)(frameY == 216 ? 36 : 0);
					}
					else if (frameX is 36 or 90)
					{
						frameX = 54;
						frameY = (short)(frameY == 216 ? 36 : 0);
					}

					if (frameY == 198)
					{
						if (frameX is 108 or 126 or 144)
						{
							frameX -= 90;
							frameY = 18;
						}
						else if (frameX is 162 or 180 or 198)
						{
							frameX -= 54;
							frameY = 72;
						}
					}
					else
					{
						if (frameX is 126 or 180 or 234)
						{
							frameX = (short)(frameX == 234 ? 72 : frameX == 180 ? 36 : 0);
							frameY = 54;
						}
						else if (frameX is 144 or 198 or 252)
						{
							frameX = (short)(frameX == 252 ? 54 : frameX == 198 ? 36 : 18);
							frameY = 0;
						}
						else if (frameX is 162 or 216 or 270)
						{
							frameX = (short)(frameX == 270 ? 90 : frameX == 216 ? 54 : 18);
							frameY = 54;
						}
					}
				}
				else if (frameY == 234)
				{
					if (frameX < 108)
					{
						if (frameX < 54)
						{
							frameY = frameX;
							frameX = 216;
						}
						else
						{
							frameY = (short)(frameX - 54);
							frameX = 162;
						}
					}
					else if (frameX is 126 or 180 or 234)
					{
						frameX = 0;
						frameY = (short)(frameX == 234 ? 36 : frameX == 180 ? 18 : 0);
					}
					else if (frameX is 144 or 198 or 252)
					{
						frameX = (short)(frameX == 252 ? 54 : frameX == 198 ? 36 : 18);
						frameY = 18;
					}
					else if (frameX is 162 or 216 or 270)
					{
						frameX = 72;
						frameY = (short)(frameX == 234 ? 36 : frameX == 180 ? 18 : 0);
					}
				}
				else if (frameY == 252)
				{
					if (frameX < 108)
					{
						if (frameX > 36)
						{
							frameX -= 54;
						}
						frameX += 108;
						frameY = 72;
					}
					else if (frameX is 126 or 180 or 234)
					{
						frameX = (short)(frameX == 234 ? 72 : frameX == 180 ? 36 : 0);
						frameY = 72;
					}
					else if (frameX is 144 or 198 or 252)
					{
						frameX = (short)(frameX == 252 ? 54 : frameX == 198 ? 36 : 18);
						frameY = 36;
					}
					else if (frameX is 162 or 216 or 270)
					{
						frameX = (short)(frameX == 270 ? 90 : frameX == 216 ? 54 : 18);
						frameY = 72;
					}
				}
				else if (frameY is 270 or 288)
				{
					if (frameX is 36 or 54 or 72)
					{
						frameX -= 18;
						frameY = (short)(frameY == 288 ? 36 : 0);
					}
					else if (frameX is 90 or 108 or 126)
					{
						frameX = (short)(frameX == 126 ? 72 : frameX == 108 ? 36 : 0);
						frameY = (short)(frameY == 288 ? 72 : 54);
					}
					else if (frameX is 144 or 162 or 180)
					{
						frameX = (short)(frameX == 126 ? 90 : frameX == 108 ? 54 : 18);
						frameY = (short)(frameY == 288 ? 72 : 54);
					}
					else if (frameX is 198 or 216 or 234)
					{
						frameX -= 180;
						frameY = 18;
					}
				}
				else if (frameY == 306)
				{
					if (frameX >= 36 && frameX < 252)
					{
						while (frameX > 72) frameX -= 54;

						frameX -= 18;
						frameY = 18;
					}
				}
				else if (frameY is 324 or 342)
				{
					if (frameX < 108)
					{
						if (frameX >= 54)
						{
							frameX -= 54;
						}
						frameX += 18;
						frameY = (short)(frameY == 342 ? 36 : 0);
					}
				}
				else if (frameY is 360 or 378)
				{
					if (frameX < 108)
					{
						if (frameX > 36)
						{
							frameX -= 54;
						}
						short oldFrameY = frameY;
						frameY = frameX;
						frameX = (short)(oldFrameY == 360 ? 0 : 72);
					}
				}
			}
			TextureUtilities.TileDraw(i, j, TextureUtilities.TileDrawTexture(InfectedStoneType, Main.tile[i, j].TileColor), TextureUtilities.TileDrawColor(i, j, emitDust: !preDraw, tileShineType: InfectedStoneType), spriteBatch, new(frameX, frameY));
		}
		return preDraw;
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		if (!TileDrawing.IsVisible(Main.tile[i, j]))
		{
			return;
		}

		if (mossType == MossType.Helium)
		{
			TextureUtilities.TileDraw(i, j, TextureAssets.Tile[Type].Value, Main.DiscoColor, spriteBatch);
		}
		else if (mossType >= MossType.Lava)
		{
			TextureUtilities.TileDraw(i, j, TextureAssets.Tile[Type].Value, MossColor, spriteBatch);
		}
		else
		{
			//Due to the nature of the methods I use for the paint textures, I have to make use of other methods to make sure the moss of the tile is painted. It will still be inaccurate, but at least it will be close.
			//I consider it a small price to pay for dynamic texture creation
			Color mossColor = MossColor;
			byte tileColor = Main.tile[i, j].TileColor;
			if (tileColor == PaintID.NegativePaint)
			{
				mossColor.R = (byte)(byte.MaxValue - MossColor.R);
				mossColor.G = (byte)(byte.MaxValue - MossColor.G);
				mossColor.B = (byte)(byte.MaxValue - MossColor.B);
			}
			else
			{
				mossColor.MultiplyRGB(WorldGen.paintColor(tileColor));
			}
			TextureUtilities.TileDraw(i, j, TextureUtilities.TileDrawTexture(Type, tileColor), TextureUtilities.TileDrawColor(i, j, mossColor), spriteBatch);
		}

		if (infectionType == InfectionType.Corrupt && Main.rand.NextBool(700))
		{
			Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Demonite);
		}
	}

	public override void ModifyFrameMerge(int i, int j, ref int up, ref int down, ref int left, ref int right, ref int upLeft, ref int upRight, ref int downLeft, ref int downRight)
	{
		if (down == TileID.Stalactite && Main.tile[i, j + 1].TileFrameY is 0 or 72)
		{
			down = Type;
		}
		else if (up == TileID.Stalactite && Main.tile[i, j - 1].TileFrameY is 54 or 90)
		{
			up = Type;
		}

		WorldGen.TileMergeAttempt(Type, TileID.Sets.Dirt, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		if (fail) num = 3;
		else num = mossType > MossType.Purple ? 5 : 10;
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		if (fail && !effectOnly)
		{
			Main.tile[i, j].TileType = InfectedStoneType;
			WorldGen.SquareTileFrame(i, j);
		}
	}

	public override string Name => infectionType.ToString() + mossType.ToString() + "Moss";

	public override string Texture => mossType switch
	{
		MossType.Lava => $"Terraria/Images/Glow_{GlowMaskID.LavaMoss}",
		MossType.Krypton => $"Terraria/Images/Glow_{GlowMaskID.KryptonMoss}",
		MossType.Xenon => $"Terraria/Images/Glow_{GlowMaskID.XenonMoss}",
		MossType.Argon => $"Terraria/Images/Glow_{GlowMaskID.ArgonMoss}",
		MossType.Neon => $"Terraria/Images/Glow_{GlowMaskID.VioletMoss}",
		_ => $"Terraria/Images/Glow_{GlowMaskID.RainbowMoss}"
	};

	public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedMosses;
}
