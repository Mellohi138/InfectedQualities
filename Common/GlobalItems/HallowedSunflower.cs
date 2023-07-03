using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using InfectedQualities.Content.Tiles;
using InfectedQualities.Helpers;
using Terraria.Audio;

namespace InfectedQualities.Common.GlobalItems
{
    public class HallowedSunflower : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().EnableInfectedJungleBiomes;
        }

        public override bool? UseItem(Item item, Player player)
        {
            if (player.IsTargetTileInItemRange(item))
            {
                Tile soil = Main.tile[Player.tileTargetX, Player.tileTargetY + 1];
                if (soil.HasTile && soil.TileType == ModContent.TileType<HallowedJungleGrass>())
                {
                    if(TileUtils.PlaceSunflower(Player.tileTargetX, Player.tileTargetY))
                    {
                        SoundEngine.PlaySound(SoundID.Dig, new(Player.tileTargetX * 16, Player.tileTargetY * 16));
                        player.ConsumeItem(item.type);
                    }
                }
            }
            return null;
        }

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.Sunflower;
        }
    }
}