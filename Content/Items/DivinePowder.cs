﻿using InfectedQualities.Content.Projectiles;
using InfectedQualities.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Content.Items
{
    public class DivinePowder : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.PurificationPowder);
            Item.shoot = ModContent.ProjectileType<DivineDustProjectile>();
            Item.value = Item.buyPrice(silver: 3, copper : 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe(3)
                .AddIngredient(ItemID.PurificationPowder, 3)
                .AddIngredient(ItemID.PixieDust)
                .AddTile(TileID.Bottles)
                .Register();
        }

        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().DivinePowder;
    }
}