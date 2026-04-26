using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using InfectedQualities.Core;
using Terraria.Localization;
using InfectedQualities.Content.Dusts;

namespace InfectedQualities.Content.Projectiles;

public class DivinePowderProjectile : ModProjectile
{
    public override LocalizedText DisplayName => Language.GetText("Mods.InfectedQualities.Items.DivinePowder.DisplayName");

    public ref float Lifespan => ref Projectile.ai[0];

    private bool DustSpawned
    {
        get => Projectile.ai[1] != 0;
        set => Projectile.ai[1] = value.ToInt();
    }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.PurificationPowder);
        Projectile.aiStyle = 0;
    }

    public override void AI()
    {
        Projectile.velocity *= 0.95f;
        Lifespan++;
        if (Lifespan == 180)
        {
            Projectile.Kill();
        }

        if (!DustSpawned)
        {
            DustSpawned = true;
            for (int i = 0; i < 30; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<DivinePowderDust>(), Projectile.velocity.X, Projectile.velocity.Y, 50);
            }
        }

        if (Main.myPlayer == Projectile.owner)
        {
            int left = (int)(Projectile.position.X / 16f) - 1;
            int right = (int)((Projectile.position.X + Projectile.width) / 16f) + 2;
            int top = (int)(Projectile.position.Y / 16f) - 1;
            int bottom = (int)((Projectile.position.Y + Projectile.height) / 16f) + 2;

            for (int j = left; j < right; j++)
            {
                for (int k = top; k < bottom; k++)
                {
                    if (Projectile.position.X + Projectile.width > j * 16 && Projectile.position.X < j * 16 && Projectile.position.Y + Projectile.height > k * 16 && Projectile.position.Y < k * 16 && Main.tile[j, k].HasTile)
                    {
                        WorldGen.Convert(j, k, BiomeConversionID.Hallow, 1, true, true);
                    }
                }
            }
        }
    }

    public override bool? CanCutTiles() => false;

    public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.PurificationPowder}";

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().DivinePowder;
}
