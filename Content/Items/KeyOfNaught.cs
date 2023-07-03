using InfectedQualities.Common;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace InfectedQualities.Content.Items
{
    public class KeyOfNaught : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().EnableKeyOfNaught;
        }

        public override string Texture => "InfectedQualities/Client/Assets/Items/KeyOfNaught";

        public override LocalizedText Tooltip => Language.GetText("ItemTooltip.LightKey");

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.NightKey);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddCustomShimmerResult(ItemID.NightKey)
                .AddDecraftCondition(Condition.Hardmode)
                .Register();
        }
    }
}
