using InfectedQualities.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Content.Items;

public class KeyOfNaught : ModItem
{
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup) => itemGroup = ContentSamples.CreativeHelper.ItemGroup.Keys;

    public override void SetDefaults() => Item.CloneDefaults(ItemID.NightKey);

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.SoulofNight, 15)
            .AddTile(TileID.WorkBenches)
            .AddCondition(Condition.InGraveyard)
            .Register();
    }

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().KeyOfNaught;
}
