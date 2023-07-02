using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using InfectedQualities.Content.Tiles;

namespace InfectedQualities.Common.GlobalItems
{
    public class HallowedSeed : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedJungleBiomes;
        }

        public override bool? UseItem(Item item, Player player)
        {
            if (player.IsTargetTileInItemRange(item))
            {
                Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
                if (tile.TileType == TileID.Mud && tile.HasTile)
                {
                    WorldGen.PlaceTile(Player.tileTargetX, Player.tileTargetY, (ushort)ModContent.TileType<HallowedJungleGrass>(), forced: true);
                    player.ConsumeItem(item.type);
                    return true;
                }
            }
            return null;
        }

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.HallowedSeeds;
        }
    }
}