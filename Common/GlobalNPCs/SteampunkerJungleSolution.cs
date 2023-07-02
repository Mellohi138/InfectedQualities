using InfectedQualities.Content.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Common.GlobalNPCs
{
    public class SteampunkerJungleSolution : GlobalNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableLimeSolution;
        }

        public override void ModifyShop(NPCShop shop)
        {
            shop.Add<LimeSolution>(Condition.NotRemixWorld, Condition.DownedMoonLord);
        }

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.Steampunker;
        }
    }
}
