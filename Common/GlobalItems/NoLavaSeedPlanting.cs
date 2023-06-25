using InfectedQualities.Helpers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Common.GlobalItems
{
    public class NoLavaSeedPlanting : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if (player.IsTargetTileInItemRange(item) && TileUtils.LavaCheck(Player.tileTargetX, Player.tileTargetY))
            {
                return false;
            }
            return true;
        }

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.GrassSeeds || entity.type == ItemID.CorruptSeeds || entity.type == ItemID.CrimsonSeeds || entity.type == ItemID.HallowedSeeds || entity.type == ItemID.JungleGrassSeeds || entity.type == ItemID.MushroomGrassSeeds || entity.type == ItemID.AshGrassSeeds || entity.type == ItemID.StaffofRegrowth || entity.type == ItemID.AcornAxe;
        }
    }
}
