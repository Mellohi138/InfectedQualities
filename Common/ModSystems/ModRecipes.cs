using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using InfectedQualities.Content.Items;

namespace InfectedQualities.Common.ModSystems
{
    public class InfectedQualitiesSystem : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableKeyOfNaught)
            {
                for (int i = 0; i < Recipe.numRecipes; i++)
                {
                    Recipe recipe = Main.recipe[i];
                    if (recipe.TryGetResult(ItemID.NightKey, out _))
                    {
                        recipe.AddCustomShimmerResult(ModContent.ItemType<KeyOfNaught>()).AddDecraftCondition(Condition.Hardmode);
                    }
                }
            }
        }
    }
}