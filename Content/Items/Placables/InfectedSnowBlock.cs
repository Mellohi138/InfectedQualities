using InfectedQualities.Core;
using InfectedQualities.Utilities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace InfectedQualities.Content.Items.Placables;

public class CorruptSnowBlock() : InfectedSnowBlock(InfectionType.Corrupt);
public class CrimsonSnowBlock() : InfectedSnowBlock(InfectionType.Crimson);
public class HallowedSnowBlock() : InfectedSnowBlock(InfectionType.Hallowed);

public abstract class InfectedSnowBlock(InfectionType infectionType) : ModItem
{
    public override LocalizedText Tooltip => LocalizedText.Empty;

    public override string LocalizationCategory => "Items.Placables";

    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.SnowBlock);
        Item.createTile = TileUtilities.GetSnowType(infectionType);
        ItemID.Sets.ExtractinatorMode[Type] = Type;
    }

    public override void ExtractinatorUse(int extractinatorBlockType, ref int resultType, ref int resultStack)
    {
        if (extractinatorBlockType == TileID.ChlorophyteExtractinator)
        {
            resultType = ItemID.SnowBlock;
            resultStack = 1;
        }
    }

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes;
}
