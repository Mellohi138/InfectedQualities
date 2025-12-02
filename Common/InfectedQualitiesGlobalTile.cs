using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using InfectedQualities.Content.Tiles;
using Terraria.ObjectData;
using System.Linq;
using InfectedQualities.Content.Tiles.Plants;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using InfectedQualities.Core;
using Terraria.GameContent.Drawing;
using InfectedQualities.Utilities;

namespace InfectedQualities.Common
{
	public class InfectedQualitiesGlobalTile : GlobalTile
	{
		public override void SetStaticDefaults()
		{
			TileID.Sets.Corrupt[TileID.CorruptPlants] = true;
			TileID.Sets.Corrupt[TileID.CorruptThorns] = true;
			TileID.Sets.AddCorruptionTile(TileID.CorruptVines);
			TileID.Sets.CorruptCountCollection.Remove(TileID.CorruptPlants);
			TileID.Sets.CorruptCountCollection.Remove(TileID.CorruptThorns);
			TileID.Sets.CorruptCountCollection.Remove(TileID.CorruptVines);

			TileID.Sets.Crimson[TileID.CrimsonPlants] = true;
			TileID.Sets.Crimson[TileID.CrimsonThorns] = true;
			TileID.Sets.AddCrimsonTile(TileID.CrimsonVines);
			TileID.Sets.CrimsonCountCollection.Remove(TileID.CrimsonPlants);
			TileID.Sets.CrimsonCountCollection.Remove(TileID.CrimsonThorns);
			TileID.Sets.CrimsonCountCollection.Remove(TileID.CrimsonVines);

			TileID.Sets.Hallow[TileID.HallowedPlants] = true;
			TileID.Sets.Hallow[TileID.HallowedPlants2] = true;
			TileID.Sets.HallowBiome[TileID.HallowedVines] = 1;
			TileID.Sets.HallowCountCollection.Remove(TileID.HallowedPlants);
			TileID.Sets.HallowCountCollection.Remove(TileID.HallowedPlants2);
			TileID.Sets.HallowCountCollection.Remove(TileID.HallowedVines);

			TileID.Sets.CanGrowCrystalShards[TileID.HallowedGrass] = true;
			TileID.Sets.CanGrowCrystalShards[TileID.GolfGrassHallowed] = true;

			if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes)
			{
				TileID.Sets.SlowlyDiesInWater[TileID.CorruptPlants] = false;
				TileID.Sets.SlowlyDiesInWater[TileID.CrimsonPlants] = false;
				TileID.Sets.SlowlyDiesInWater[TileID.HallowedPlants] = false;

				TileID.Sets.AddJungleTile(TileID.CorruptJungleGrass, 2);
				TileID.Sets.AddJungleTile(TileID.CrimsonJungleGrass, 2);

				TileObjectData sunflower = TileObjectData.GetTileData(TileID.Sunflower, 0);
				sunflower.AnchorValidTiles = [.. sunflower.AnchorValidTiles, ModContent.TileType<HallowedJungleGrass>()];
			}
		}

		public override bool TileFrame(int i, int j, int type, ref bool resetFrame, ref bool noBreak)
		{
			if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes)
			{
				if (type is TileID.CorruptPlants or TileID.CrimsonPlants or TileID.HallowedPlants or TileID.HallowedPlants2 or TileID.JunglePlants or TileID.JunglePlants2)
				{
					Tile soil = Main.tile[i, j + 1];
					if (soil.HasUnactuatedTile && !soil.IsHalfBlock && soil.Slope == SlopeType.Solid)
					{
						bool isSpore = type == TileID.JunglePlants && Main.tile[i, j].TileFrameX == 144;
						bool isMushroom = type == TileID.CrimsonPlants ? Main.tile[i, j].TileFrameX == 270 : type is TileID.CorruptPlants or TileID.HallowedPlants && Main.tile[i, j].TileFrameX == 144;

						if (j > Main.rockLayer && soil.TileType is TileID.CorruptJungleGrass or TileID.CrimsonJungleGrass)
						{
							if (isMushroom)
							{
								Main.tile[i, j].TileType = TileID.JunglePlants;
								Main.tile[i, j].TileFrameX = 144;
								if (Main.netMode == NetmodeID.Server)
								{
									NetMessage.SendTileSquare(-1, i, j);
								}
								return false;
							}
							else if (isSpore) return false;
						}
						else if (soil.TileType == ModContent.TileType<HallowedJungleGrass>())
						{
							if (j > Main.rockLayer && isMushroom)
							{
								Main.tile[i, j].TileType = TileID.JunglePlants;
								Main.tile[i, j].TileFrameX = 144;
								if (Main.netMode == NetmodeID.Server)
								{
									NetMessage.SendTileSquare(-1, i, j);
								}
							}
							else if (type != TileID.HallowedPlants && type != TileID.HallowedPlants2 && !isSpore)
							{
								Main.tile[i, j].TileType = TileID.HallowedPlants;
								if (Main.netMode == NetmodeID.Server)
								{
									NetMessage.SendTileSquare(-1, i, j);
								}
							}
							return false;
						}
					}
				}
				else if (TileID.Sets.IsVine[type])
				{
					Tile soil = Main.tile[i, j - 1];
					if (soil.TileType == ModContent.TileType<HallowedJungleGrass>() && soil.HasUnactuatedTile && !soil.BottomSlope)
					{
						if (type != TileID.HallowedVines)
						{
							Main.tile[i, j].TileType = TileID.HallowedVines;
							WorldGen.SquareTileFrame(i, j);
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, i, j);
							}
						}
						return false;
					}
				}
				else if (type == TileID.PlanteraBulb)
				{
					TileUtilities.GetTopLeft(i, j, out int x, out int y, out short num);
					if (num != -1)
					{
						for (int m = x; m < x + 2; m++)
						{
							for (int n = y; n < y + 2; n++)
							{
								if (Main.tile[m, n].HasTile && Main.tile[m, n].TileType == type)
								{
									Main.tile[m, n].TileType = (ushort)ModContent.TileType<InfectedPlanteraBulb>();
									Main.tile[m, n].TileFrameX = (short)(num + Main.tile[m, n].TileFrameX % 36);
								}
							}
						}
						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendTileSquare(-1, i, j, 2);
						}
						return false;
					}
				}
				else if (type == TileID.LifeFruit)
				{
					TileUtilities.GetTopLeft(i, j, out int x, out int y, out short num);
					if (num != -1)
					{
						for (int m = x; m < x + 2; m++)
						{
							for (int n = y; n < y + 2; n++)
							{
								if (Main.tile[m, n].HasTile && Main.tile[m, n].TileType == type)
								{
									Main.tile[m, n].TileType = (ushort)ModContent.TileType<InfectedLifeFruit>();
									Main.tile[m, n].TileFrameY = (short)(num + Main.tile[m, n].TileFrameY % 36);
								}
							}
						}
						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendTileSquare(-1, i, j, 2);
						}
						return false;
					}
				}
			}
			return true;
		}

		public override void RandomUpdate(int i, int j, int type)
		{
			if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes)
			{
				if (type is TileID.CorruptJungleGrass or TileID.CrimsonJungleGrass)
				{
					if (!Main.remixWorld && WorldGen.genRand.NextBool(500) && (!Main.tile[i, j - 1].HasTile || TileID.Sets.IgnoredByGrowingSaplings[Main.tile[i, j - 1].TileType]))
					{
						TileUtilities.TryToGrowTree(i, j, j >= Main.worldSurface);
					}
					else if (WorldGen.genRand.NextBool(10) && !Main.tile[i, j - 1].HasTile && Main.tile[i, j - 1].LiquidAmount > 0 && Main.tile[i, j - 1].LiquidType == LiquidID.Water)
					{
						if (WorldGen.PlaceTile(i, j - 1, TileID.CorruptPlants) || WorldGen.PlaceTile(i, j - 1, TileID.CrimsonPlants))
						{
							Main.tile[i, j - 1].CopyPaintAndCoating(Main.tile[i, j]);
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, i, j - 1);
							}
						}
					}

					if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && WorldGen.genRand.NextBool(60))
					{
						TileUtilities.AttemptToPlaceInfectedPlant(i, j, ModContent.TileType<InfectedPlanteraBulb>(), TileID.PlanteraBulb, 150);
					}

					if (NPC.downedMechBossAny && WorldGen.genRand.NextBool(Main.expertMode ? 30 : 40))
					{
						TileUtilities.AttemptToPlaceInfectedPlant(i, j, ModContent.TileType<InfectedLifeFruit>(), TileID.LifeFruit, Main.expertMode ? 50 : 60);
					}
				}
				else if (type == TileID.HallowedVines && WorldGen.genRand.NextBool(20) && !Main.tile[i, j + 1].HasTile && Main.tile[i, j + 1].LiquidType != LiquidID.Lava && WorldGen.GrowMoreVines(i, j))
				{
					bool flag = false;
					for (int num = j; num > j - 10 && num < 0; num--)
					{
						if (Main.tile[i, num].HasTile && Main.tile[i, num].TileType == ModContent.TileType<HallowedJungleGrass>() && !Main.tile[i, num].BottomSlope)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						Main.tile[i, j + 1].TileType = TileID.HallowedVines;
						Main.tile[i, j + 1].Get<TileWallWireStateData>().HasTile = true;
						Main.tile[i, j + 1].CopyPaintAndCoating(Main.tile[i, j]);
						WorldGen.SquareTileFrame(i, j + 1);
						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendTileSquare(-1, i, j + 1);
						}
					}
				}
				else if (type is TileID.HallowedGrass or TileID.GolfGrassHallowed)
				{
					for (int x = i - 1; x < i + 2; x++)
					{
						for (int y = j - 1; y < j + 2; y++)
						{
							ushort tileType = Main.tile[x, y].TileType;
							bool flag = Main.hardMode && WorldGen.AllowedToSpreadInfections && Main.tile[x, y - 1].TileType != TileID.Sunflower;

							if (tileType == TileID.Mud || (tileType == TileID.JungleGrass && flag))
							{
								WorldGen.SpreadGrass(x, y, tileType, ModContent.TileType<HallowedJungleGrass>(), false, Main.tile[i, j].BlockColorAndCoating());
								WorldGen.SquareTileFrame(x, y);
								if (Main.netMode == NetmodeID.Server)
								{
									NetMessage.SendTileSquare(-1, x, y);
								}
							}
						}
					}
				}
			}

			//Making vanilla moss spread to infected blocks
			if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedMosses && (TileID.Sets.Conversion.Moss[type] || TileID.Sets.Conversion.MossBrick[type]) && type < TileID.Count && WorldGen.genRand.NextBool())
			{
				int mossColor = WorldGen.GetTileMossColor(type);
				if (mossColor != -1)
				{
					for (int x = i - 1; x < i + 2; x++)
					{
						for (int y = j - 1; y < j + 2; y++)
						{
							if ((i != x || j != y) && Main.tile[x, y].HasTile)
							{
								ushort tileType = Main.tile[x, y].TileType;
								InfectionType? infectionType = tileType switch
								{
									TileID.Ebonstone => InfectionType.Corrupt,
									TileID.Crimstone => InfectionType.Crimson,
									TileID.Pearlstone => InfectionType.Hallowed,
									_ => null
								};
								if (infectionType.HasValue)
								{
									WorldGen.SpreadGrass(x, y, tileType, TileUtilities.GetMossType(infectionType, (MossType)mossColor), false, Main.tile[i, j].BlockColorAndCoating());
									WorldGen.SquareTileFrame(x, y);
									if (Main.netMode == NetmodeID.Server)
									{
										NetMessage.SendTileSquare(-1, x, y);
									}
								}
							}
						}
					}
				}
			}
		}

		public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
		{
			if (type == TileID.CorruptJungleGrass && TileDrawing.IsVisible(Main.tile[i, j]) && Main.rand.NextBool(500))
			{
				Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Demonite);
			}
		}

		public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			if (ModContent.GetInstance<InfectedQualitiesServerConfig>().AltarEvilSpawning && InfectedQualitiesModSupport.AltarToEvilBlock.TryGetValue(type, out ushort infectedStone) && !WorldGen.genRand.NextBool(3))
			{
				if (WorldGen.genRand.NextBool())
				{
					infectedStone = InfectedQualitiesModSupport.GetGoodStone();
				}
				else if (type == TileID.DemonAltar)
				{
					infectedStone = Main.tile[i, j].TileFrameX < 54 ? TileID.Ebonstone : TileID.Crimstone;
				}

				for (int num = 0; num < 1000; num++)
				{
					int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
					int y = WorldGen.genRand.Next((int)Main.rockLayer + 50, Main.maxTilesY - 300);

					if (WorldGen.TileType(x, y) == TileID.Stone)
					{
						Main.tile[x, y].TileType = infectedStone;
						WorldGen.SquareTileFrame(x, y);
						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendTileSquare(-1, x, y);
						}
						break;
					}
				}
			}
		}

		public override void Unload()
		{
			if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes)
			{
				TileObjectData sunflower = TileObjectData.GetTileData(TileID.Sunflower, 0);
				sunflower.AnchorValidTiles = [.. sunflower.AnchorValidTiles.Except([ModContent.TileType<HallowedJungleGrass>()])];
			}
		}
	}
}
