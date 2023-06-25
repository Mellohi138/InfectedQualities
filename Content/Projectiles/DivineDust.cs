using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using InfectedQualities.Common;

namespace InfectedQualities.Content.Projectiles
{
    public class DivineDust : ModProjectile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig>().EnableDivinePowder;
        }

        public override string Texture => "Terraria/Images/Projectile_10";

        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Projectile.velocity *= 0.95f;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] == 180f)
            {
                Projectile.Kill();
            }

            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                for (int i = 0; i < 30; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurificationPowder, Projectile.velocity.X, Projectile.velocity.Y, 50, Color.LightBlue);
                }
            }

            if (Main.myPlayer == Projectile.owner)
            {
                int num = (int)(Projectile.position.X / 16f) - 1;
                int num2 = (int)((Projectile.position.X + Projectile.width) / 16f) + 2;
                int num3 = (int)(Projectile.position.Y / 16f) - 1;
                int num4 = (int)((Projectile.position.Y + Projectile.height) / 16f) + 2;

                Vector2 vector = default;
                for (int j = num; j < num2; j++)
                {
                    for (int k = num3; k < num4; k++)
                    {
                        vector.X = j * 16;
                        vector.Y = k * 16;
                        if (Projectile.position.X + Projectile.width > vector.X && Projectile.position.X < vector.X + 16f && Projectile.position.Y + Projectile.height > vector.Y && Projectile.position.Y < vector.Y + 16f)
                        {
                            if(Framing.GetTileSafely(j, k).HasTile)
                            {
                                WorldGen.Convert(j, k, BiomeConversionID.Hallow, 0);
                            }
                        }
                    }
                }
            }
        }
    }
}
