using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using InfectedQualities.Helpers;
using Microsoft.Xna.Framework;

namespace InfectedQualities.Common.GlobalProjectiles
{
    public class PurificationPowder : GlobalProjectile
    {
        public override void AI(Projectile projectile)
        {
            int num = (int)(projectile.position.X / 16f) - 1;
            int num2 = (int)((projectile.position.X + projectile.width) / 16f) + 2;
            int num3 = (int)(projectile.position.Y / 16f) - 1;
            int num4 = (int)((projectile.position.Y + projectile.height) / 16f) + 2;

            Vector2 vector = default;
            for (int j = num; j < num2; j++)
            {
                for (int k = num3; k < num4; k++)
                {
                    vector.X = j * 16;
                    vector.Y = k * 16;
                    if (projectile.position.X + projectile.width > vector.X && projectile.position.X < vector.X + 16f && projectile.position.Y + projectile.height > vector.Y && projectile.position.Y < vector.Y + 16f)
                    {
                        if(Framing.GetTileSafely(j, k).HasTile)
                        {
                            if (!TileID.Sets.Conversion.MushroomGrass[Framing.GetTileSafely(j, k).TileType] && !TileUtils.IDSets.WallMushroom[Framing.GetTileSafely(j, k).WallType])
                            {
                                if(ModContent.GetInstance<InfectedQualitiesConfig>().EnableDivinePowder || !TileID.Sets.Hallow[Framing.GetTileSafely(j, k).TileType])
                                {
                                    WorldGen.Convert(j, k, BiomeConversionID.Purity, 0);
                                }
                            }
                        }
                    }
                }
            }
        }

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ProjectileID.PurificationPowder;
        }
    }
}
