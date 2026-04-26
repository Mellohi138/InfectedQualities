using InfectedQualities.Core;
using InfectedQualities.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace InfectedQualities.Content.Tiles;

[Autoload(false)]
public class InfectedGemstone(InfectionType infectionType, GemType gemType) : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileStone[Type] = true;
		Main.tileBrick[Type] = true;
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileShine[Type] = 900;
		Main.tileShine2[Type] = true;
		Main.tileSpelunker[Type] = true;

		MinPick = 65;
		MineResist = 2;
		HitSound = SoundID.Tink;
		RegisterItemDrop(ItemID.Search.GetId(gemType.ToString()));

		switch (infectionType)
		{
			case InfectionType.Corrupt:
				TileID.Sets.Corrupt[Type] = true;
				TileID.Sets.AddCorruptionTile(Type);
				TileID.Sets.CorruptBiomeSight[Type] = true;
				TileID.Sets.CorruptCountCollection.Add(Type);
				break;
			case InfectionType.Crimson:
				TileID.Sets.Crimson[Type] = true;
				TileID.Sets.AddCrimsonTile(Type);
				TileID.Sets.CrimsonBiomeSight[Type] = true;
				TileID.Sets.CrimsonCountCollection.Add(Type);
				break;
			case InfectionType.Hallowed:
				TileID.Sets.Hallow[Type] = true;
				TileID.Sets.HallowBiome[Type] = 1;
				TileID.Sets.HallowBiomeSight[Type] = true;
				TileID.Sets.HallowCountCollection.Add(Type);
				break;
		}

		switch (gemType)
		{
			case GemType.Sapphire:
				DustType = DustID.GemSapphire;
				AddMapEntry(new(110, 140, 182), Language.GetText("ItemName.Sapphire"));
				break;
			case GemType.Ruby:
				DustType = DustID.GemRuby;
				AddMapEntry(new(196, 96, 114), Language.GetText("ItemName.Ruby"));
				break;
			case GemType.Emerald:
				DustType = DustID.GemEmerald;
				AddMapEntry(new(56, 150, 97), Language.GetText("ItemName.Emerald"));
				break;
			case GemType.Topaz:
				DustType = DustID.GemTopaz;
				AddMapEntry(new(160, 118, 58), Language.GetText("ItemName.Topaz"));
				break;
			case GemType.Amethyst:
				DustType = DustID.GemAmethyst;
				AddMapEntry(new(140, 58, 166), Language.GetText("ItemName.Amethyst"));
				break;
			case GemType.Diamond:
				DustType = DustID.GemDiamond;
				AddMapEntry(new(125, 191, 197), Language.GetText("ItemName.Diamond"));
				break;
			case GemType.Amber:
				DustType = DustID.GemAmber;
				AddMapEntry(new(233, 180, 90), Language.GetText("ItemName.Amber"));
				break;
		}

		TileLoader.RegisterSimpleConversion(TileUtilities.GetGemstoneType(null, gemType), infectionType.ToConversionID(), Type);
		VanillaFallbackOnModDeletion = TileUtilities.GetGemstoneType(null, gemType);
	}

	public override void RandomUpdate(int i, int j) => WorldGen.SpreadInfectionToNearbyTile(i, j, infectionType.ToConversionID());

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		if (TileDrawing.IsVisible(Main.tile[i, j]))
		{
			ushort infectedStoneType = infectionType switch
			{
				InfectionType.Corrupt => TileID.Ebonstone,
				InfectionType.Crimson => TileID.Crimstone,
				InfectionType.Hallowed => TileID.Pearlstone,
				_ => TileID.Stone
			};

			TextureUtilities.TileDraw(i, j, TextureUtilities.TileDrawTexture(infectedStoneType, Main.tile[i, j].TileColor), TextureUtilities.TileDrawColor(i, j), spriteBatch);
		}
		return true;
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		if (TileDrawing.IsVisible(Main.tile[i, j]) && infectionType == InfectionType.Corrupt && Main.rand.NextBool(700))
		{
			Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Demonite);
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 3 : 10;

	public override string Name => infectionType.ToString() + gemType.ToString() + "Gemstone";

	public override string Texture => "InfectedQualities/Content/Extras/Tiles/" + gemType.ToString() + "_Gemstone";

	public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedGemstones;
}
