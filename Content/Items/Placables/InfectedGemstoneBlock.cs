using InfectedQualities.Core;
using InfectedQualities.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace InfectedQualities.Content.Items.Placables;

public class CorruptSapphireGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Corrupt, GemType.Sapphire);
public class CorruptRubyGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Corrupt, GemType.Ruby);
public class CorruptEmeraldGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Corrupt, GemType.Emerald);
public class CorruptTopazGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Corrupt, GemType.Topaz);
public class CorruptAmethystGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Corrupt, GemType.Amethyst);
public class CorruptDiamondGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Corrupt, GemType.Diamond);
public class CorruptAmberGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Corrupt, GemType.Amber);

public class CrimsonSapphireGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Crimson, GemType.Sapphire);
public class CrimsonRubyGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Crimson, GemType.Ruby);
public class CrimsonEmeraldGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Crimson, GemType.Emerald);
public class CrimsonTopazGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Crimson, GemType.Topaz);
public class CrimsonAmethystGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Crimson, GemType.Amethyst);
public class CrimsonDiamondGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Crimson, GemType.Diamond);
public class CrimsonAmberGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Crimson, GemType.Amber);

public class HallowedSapphireGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Hallowed, GemType.Sapphire);
public class HallowedRubyGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Hallowed, GemType.Ruby);
public class HallowedEmeraldGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Hallowed, GemType.Emerald);
public class HallowedTopazGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Hallowed, GemType.Topaz);
public class HallowedAmethystGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Hallowed, GemType.Amethyst);
public class HallowedDiamondGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Hallowed, GemType.Diamond);
public class HallowedAmberGemstoneBlock() : InfectedGemstoneBlock(InfectionType.Hallowed, GemType.Amber);

public abstract class InfectedGemstoneBlock(InfectionType infectionType, GemType gemType) : ModItem
{
    public override LocalizedText Tooltip => LocalizedText.Empty;

    public override string LocalizationCategory => "Items.Placables";

    private short InfectedStoneType => infectionType switch
    {
        InfectionType.Corrupt => ItemID.EbonstoneBlock,
        InfectionType.Crimson => ItemID.CrimstoneBlock,
        InfectionType.Hallowed => ItemID.PearlstoneBlock,
        _ => ItemID.StoneBlock
    };

    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
        ItemID.Sets.DisableAutomaticPlaceableDrop[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(TileUtilities.GetGemstoneType(infectionType, gemType));
        ItemID.Sets.ExtractinatorMode[Type] = Type;
    }

    public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        Main.GetItemDrawFrame(InfectedStoneType, out var itemTexture, out var itemFrame);
        spriteBatch.Draw(itemTexture, position, itemFrame, drawColor, 0, itemFrame.Size() / 2f, scale, SpriteEffects.None, 0);
        return true;
    }

    public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
    {
        Main.GetItemDrawFrame(InfectedStoneType, out var itemTexture, out var itemFrame);
        Vector2 origin = itemFrame.Size() / 2f;
        Vector2 drawPosition = Item.Bottom - Main.screenPosition - new Vector2(0, origin.Y);

        spriteBatch.Draw(itemTexture, drawPosition, itemFrame, lightColor, rotation, origin, scale, SpriteEffects.None, 0);
        return true;
    }

    public override void ExtractinatorUse(int extractinatorBlockType, ref int resultType, ref int resultStack)
    {
        if (extractinatorBlockType == TileID.ChlorophyteExtractinator)
        {
            resultType = ItemID.Search.GetId(gemType.ToString() + "StoneBlock");
            resultStack = 1;
        }
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(InfectedStoneType)
            .AddIngredient(ItemID.Search.GetId(gemType.ToString()))
            .AddCondition(Condition.InGraveyard)
            .AddTile(TileID.HeavyWorkBench)
            .Register();
    }

    public override string Texture => "InfectedQualities/Content/Extras/Items/" + gemType.ToString() + "_GemstoneBlock";

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedGemstones;
}
