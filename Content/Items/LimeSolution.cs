using InfectedQualities.Common;
using Terraria.ModLoader;

namespace InfectedQualities.Content.Items
{
    public class LimeSolution : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableLimeSolution;
        }

        public override string Texture => "InfectedQualities/Client/Assets/Items/LimeSolution";

        public override void SetDefaults()
        {
            Item.DefaultToSolution(ModContent.ProjectileType<Projectiles.LimeSpray>());
        }
    }
}
