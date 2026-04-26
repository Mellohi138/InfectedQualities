using InfectedQualities.Core;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace InfectedQualities.Content.Dusts;

public class DivinePowderDust : ModDust
{
    public override bool Update(Dust dust)
    {
        dust.scale += 0.005f;
        dust.velocity *= 0.94f;

        float scale = Math.Min(dust.scale * 0.8f, 1);
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), scale, scale * 0.85f, scale * 0.95f);
        return true;
    }

    public override bool MidUpdate(Dust dust)
    {
        dust.velocity.Y -= 0.1f;
        return false;
    }

    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        lightColor.A = 25;
        return lightColor;
    }

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().DivinePowder;
}
