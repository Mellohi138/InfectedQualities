using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using InfectedQualities.Core;
using InfectedQualities.Utilities;

namespace InfectedQualities.Content.Tiles;

[Autoload(false)]
public class InfectedSnow(InfectionType infectionType) : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileBrick[Type] = true;

		Main.tileMergeDirt[Type] = true;
		Main.tileMerge[TileID.Dirt][Type] = true;
		TileUtilities.TileMerge(Type, TileID.Slush);

		TileID.Sets.CanBeDugByShovel[Type] = true;
		TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
		TileID.Sets.Snow[Type] = true;
		TileID.Sets.IcesSnow[Type] = true;
		TileID.Sets.ChecksForMerge[Type] = true;

		TileID.Sets.SnowBiome[Type] = 1;
		TileID.Sets.Conversion.Snow[Type] = true;

		MineResist = 0.5f;
		HitSound = SoundID.Item50;
		DustType = DustID.SnowBlock;

		switch (infectionType)
		{
			case InfectionType.Corrupt:
				TileID.Sets.Corrupt[Type] = true;
				TileID.Sets.AddCorruptionTile(Type);
				TileID.Sets.CorruptBiomeSight[Type] = true;
				TileID.Sets.CorruptCountCollection.Add(Type);
				TileUtilities.TileMerge(Type, TileID.CorruptIce);
				AddMapEntry(new(214, 203, 236));
				break;
			case InfectionType.Crimson:
				TileID.Sets.Crimson[Type] = true;
				TileID.Sets.AddCrimsonTile(Type);
				TileID.Sets.CrimsonBiomeSight[Type] = true;
				TileID.Sets.CrimsonCountCollection.Add(Type);
				TileUtilities.TileMerge(Type, TileID.FleshIce);
				AddMapEntry(new(234, 210, 205));
				break;
			case InfectionType.Hallowed:
				Main.tileShine[Type] = 9000;
				TileID.Sets.Hallow[Type] = true;
				TileID.Sets.HallowBiome[Type] = 1;
				TileID.Sets.HallowBiomeSight[Type] = true;
				TileID.Sets.HallowCountCollection.Add(Type);
				TileID.Sets.CanGrowCrystalShards[Type] = true;
				TileUtilities.TileMerge(Type, TileID.HallowedIce);
				AddMapEntry(new(247, 228, 233));
				break;
		}

		TileLoader.RegisterSimpleConversion(TileID.SnowBlock, infectionType.ToConversionID(), Type);
		VanillaFallbackOnModDeletion = TileID.SnowBlock;
	}

	public override bool HasWalkDust() => true;

	public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 3 : 10;

	public override void RandomUpdate(int i, int j) => WorldGen.SpreadInfectionToNearbyTile(i, j, infectionType.ToConversionID());

	public override void ModifyFrameMerge(int i, int j, ref int up, ref int down, ref int left, ref int right, ref int upLeft, ref int upRight, ref int downLeft, ref int downRight)
	{
		if (down == TileID.Stalactite)
		{
			down = Type;
		}
	}

	public override string Name => infectionType.ToString() + "Snow";

	public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes;
}
