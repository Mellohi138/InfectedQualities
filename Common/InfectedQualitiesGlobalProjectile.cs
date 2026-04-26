using InfectedQualities.Content.Tiles.Plants;
using InfectedQualities.Core;
using InfectedQualities.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Common;

public class FertilizerInteraction : GlobalProjectile
{
    public override void AI(Projectile projectile)
    {
        int left = (int)(projectile.position.X / 16f) - 1;
        int right = (int)((projectile.position.X + projectile.width) / 16f) + 2;
        int top = (int)(projectile.position.Y / 16f) - 1;
        int bottom = (int)((projectile.position.Y + projectile.height) / 16f) + 2;

        for (int j = left; j < right; j++)
        {
            for (int k = top; k < bottom; k++)
            {
                if (projectile.position.X + projectile.width > j * 16 && projectile.position.X < j * 16 && projectile.position.Y + projectile.height > k * 16 && projectile.position.Y < k * 16 + 16f)
                {
                    if (Main.tile[j, k].TileType == ModContent.TileType<InfectedSapling>())
                    {
                        if (Main.remixWorld && k >= (int)Main.worldSurface - 1)
                        {
                            TileUtilities.TryToGrowTree(j, k, false);
                        }
                        TileUtilities.TryToGrowTree(j, k, k > (int)Main.worldSurface - 1);
                    }
                }
            }
        }
    }

    public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) => entity.type == ProjectileID.Fertilizer;

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes;
}
