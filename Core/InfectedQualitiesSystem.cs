using InfectedQualities.Content.Worldgen;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace InfectedQualities.Core;

public class InfectedQualitiesSystem : ModSystem
{
    public override void AddRecipes()
    {
        if(!ModLoader.HasMod("CalamityQoLRestored") && !ModLoader.HasMod("ThoriumMod"))
        {
            Recipe.Create(ItemID.Leather)
                .AddIngredient(ItemID.Vertebrae, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        Recipe.Create(ItemID.Vertebrae)
            .AddIngredient(ItemID.RottenChunk)
            .AddTile(TileID.WorkBenches)
            .AddCondition(Condition.InGraveyard)
            .DisableDecraft()
            .Register();

        Recipe.Create(ItemID.RottenChunk)
            .AddIngredient(ItemID.Vertebrae)
            .AddTile(TileID.WorkBenches)
            .AddCondition(Condition.InGraveyard)
            .DisableDecraft()
            .Register();

        Recipe.Create(ItemID.LightShard)
            .AddIngredient(ItemID.DarkShard)
            .AddIngredient(ItemID.SoulofLight, 7)
            .AddTile(TileID.MythrilAnvil)
            .AddCondition(Condition.InHallow, Condition.NotEclipseAndNotBloodMoon)
            .DisableDecraft()
            .Register();

        Recipe.Create(ItemID.DarkShard)
            .AddIngredient(ItemID.LightShard)
            .AddIngredient(ItemID.SoulofNight, 7)
            .AddTile(TileID.MythrilAnvil)
            .AddCondition(Condition.EclipseOrBloodMoon)
            .DisableDecraft()
            .Register();
    }

    public override void AddRecipeGroups()
    {
        if (ModContent.GetInstance<InfectedQualitiesServerConfig>().PylonOfNight)
        {
            RecipeGroup pylons = new(() => $"{Language.GetTextValue("LegacyMisc.37")} {Language.GetTextValue("Mods.InfectedQualities.Misc.Pylon")}", ItemID.TeleportationPylonPurity, ItemID.TeleportationPylonJungle, ItemID.TeleportationPylonMushroom, ItemID.TeleportationPylonOcean, ItemID.TeleportationPylonDesert, ItemID.TeleportationPylonSnow, ItemID.TeleportationPylonUnderground);
            RecipeGroup.RegisterGroup("TeleportationPylons", pylons);
        }

        InfectedQualitiesModSupport.HandleRecipeGroups();
    }

    public override void PostAddRecipes()
    {
        for (int i = 0; i < Recipe.numRecipes; i++)
        {
            Recipe recipe = Main.recipe[i];
            if (recipe.HasTile(TileID.WorkBenches))
            {
                if (ModContent.GetInstance<InfectedQualitiesServerConfig>().KeyOfNaught && recipe.HasIngredient(ItemID.SoulofNight) && recipe.HasResult(ItemID.NightKey))
                {
                    recipe.AddCondition(Condition.NotInGraveyard);
                }
                else if (recipe.HasResult(ItemID.Leather))
                {
                    if(recipe.HasIngredient(ItemID.RottenChunk))
                    {
                        recipe.AddDecraftCondition(Condition.CorruptWorld);
                    }
                    else if (recipe.HasIngredient(ItemID.RottenChunk))
                    {
                        recipe.AddDecraftCondition(Condition.CrimsonWorld);
                    }
                }
            }
        }
    }

    public override void ModifyHardmodeTasks(List<GenPass> list)
    {
        if (!Main.remixWorld && ModContent.GetInstance<InfectedQualitiesServerConfig>().HardmodeChasmPurification && !ModLoader.HasMod("Remnants"))
        {
            int hardmodeGood = list.FindIndex(genPass => genPass.Name.Equals("Hardmode Good"));
            list.Insert(hardmodeGood, new WorldGenChasmPurifyer());
        }
    }
}
