﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace InfectedQualities.Core
{
    public static class InfectedQualitiesModSupport
    {
        internal static readonly Mod CalamityMod = ModLoader.TryGetMod("CalamityMod", out Mod result) ? result : null;
        internal static readonly Mod ConfectionRebaked = ModLoader.TryGetMod("TheConfectionRebirth", out Mod result) ? result : null;
        internal static readonly Mod SpiritMod = ModLoader.TryGetMod("SpiritMod", out Mod result) ? result : null;
        internal static readonly Mod ExxoAvalon = ModLoader.TryGetMod("Avalon", out Mod result) ? result : null;
        internal static readonly Mod TerrariaOrigins = ModLoader.TryGetMod("Origins", out Mod result) ? result : null;

        internal static Color[] ModWallBiomeSight = WallID.Sets.Factory.CreateCustomSet(
            default(Color),
            WallID.CorruptGrassUnsafe, new Color(200, 100, 240),
            WallID.CorruptHardenedSand, new Color(200, 100, 240),
            WallID.CorruptSandstone, new Color(200, 100, 240),
            WallID.EbonstoneUnsafe, new Color(200, 100, 240),
            WallID.CorruptionUnsafe1, new Color(200, 100, 240),
            WallID.CorruptionUnsafe2, new Color(200, 100, 240),
            WallID.CorruptionUnsafe3, new Color(200, 100, 240),
            WallID.CorruptionUnsafe4, new Color(200, 100, 240),

            WallID.CrimsonGrassUnsafe, new Color(255, 100, 100),
            WallID.CrimsonHardenedSand, new Color(255, 100, 100),
            WallID.CrimsonSandstone, new Color(255, 100, 100),
            WallID.CrimstoneUnsafe, new Color(255, 100, 100),
            WallID.CrimsonUnsafe1, new Color(255, 100, 100),
            WallID.CrimsonUnsafe2, new Color(255, 100, 100),
            WallID.CrimsonUnsafe3, new Color(255, 100, 100),
            WallID.CrimsonUnsafe4, new Color(255, 100, 100),

            WallID.HallowedGrassUnsafe, new Color(255, 160, 240),
            WallID.HallowHardenedSand, new Color(255, 160, 240),
            WallID.HallowSandstone, new Color(255, 160, 240),
            WallID.PearlstoneBrickUnsafe, new Color(255, 160, 240),
            WallID.HallowUnsafe1, new Color(255, 160, 240),
            WallID.HallowUnsafe2, new Color(255, 160, 240),
            WallID.HallowUnsafe3, new Color(255, 160, 240),
            WallID.HallowUnsafe4, new Color(255, 160, 240)
        );

        private static readonly string[][] ModBlocks =
        [
            ["AstralGrassWall", "HardenedAstralSandWall", "AstralSandstoneWall", "AstralStoneWall", "AstralDirtWall", "AstralSnowWall", "CelestialRemainsWall", "AstralIceWall", "AstralMonolithWall"],
            ["CreamGrassWall", "HardenedCreamsandWall", "CreamsandstoneWall", "CreamstoneWall", "Creamstone2Wall", "Creamstone3Wall", "Creamstone4Wall", "Creamstone5Wall", "CookieWall", "CookieStonedWall", "PinkFairyFlossWall", "BlueIceWall", "CreamWall"],
            ["Chunkstone", "HardenedSnotsand", "Snotsandstone", "Ickgrass", "ContagionJungleGrass", "Snotsand", "YellowIce"],
            ["ContagionGrassWall", "ChunkstoneWall", "HardenedSnotsandWallUnsafe", "SnotsandstoneWallUnsafe", "ContagionLumpWallUnsafe", "ContagionMouldWallUnsafe", "ContagionCystWallUnsafe", "ContagionBoilWallUnsafe"],
            ["Defiled_Stone", "Defiled_Grass", "Defiled_Sand", "Defiled_Sandstone", "Hardened_Defiled_Sand", "Defiled_Ice", "Defiled_Jungle_Grass"],
            ["Defiled_Stone_Wall", "Defiled_Sandstone_Wall", "Hardened_Defiled_Sand_Wall", "Defiled_Grass_Wall_Natural"],
            ["Riven_Flesh", "Riven_Grass", "Silica", "Brittle_Quartz", "Quartz", "Primordial_Permafrost", "Riven_Jungle_Grass"],
            ["Riven_Flesh_Wall", "Quartz_Wall", "Brittle_Quartz_Wall", "Riven_Grass_Wall_Natural"]
        ];

        public static void PostSetupContent()
        {
            if (CalamityMod != null)
            {
                foreach (string wallName in ModBlocks[0])
                {
                    ModWallBiomeSight[CalamityMod.Find<ModWall>(wallName).Type] = Color.Cyan;
                }
            }

            if (ConfectionRebaked != null)
            {
                Color confectionGlow = new(210, 196, 145);
                foreach (string wallName in ModBlocks[1])
                {
                    ModWallBiomeSight[ConfectionRebaked.Find<ModWall>(wallName).Type] = confectionGlow;
                }
            }

            if (ExxoAvalon != null)
            {
                Color contagionGlow = new(170, 255, 0);
                foreach (string wallName in ModBlocks[3])
                {
                    ModWallBiomeSight[ExxoAvalon.Find<ModWall>(wallName).Type] = contagionGlow;
                }
            }

            if(TerrariaOrigins != null)
            {
                foreach(string wallName in ModBlocks[5])
                {
                    ModWallBiomeSight[TerrariaOrigins.Find<ModWall>(wallName).Type] = Color.White;
                }

                foreach (string wallName in ModBlocks[7])
                {
                    ModWallBiomeSight[TerrariaOrigins.Find<ModWall>(wallName).Type] = Color.Cyan;
                }
            }
        }

        public static void HandleRecipeGroups()
        {
            if (RecipeGroup.recipeGroupIDs.TryGetValue("TeleportationPylons", out int pylonIndex))
            {
                if (CalamityMod != null)
                {
                    RecipeGroup.recipeGroups[pylonIndex].ValidItems.Add(CalamityMod.Find<ModItem>("AstralPylon").Type);
                    RecipeGroup.recipeGroups[pylonIndex].ValidItems.Add(CalamityMod.Find<ModItem>("CragsPylon").Type);
                    RecipeGroup.recipeGroups[pylonIndex].ValidItems.Add(CalamityMod.Find<ModItem>("SulphurPylon").Type);
                    RecipeGroup.recipeGroups[pylonIndex].ValidItems.Add(CalamityMod.Find<ModItem>("SunkenPylon").Type);
                }

                if (SpiritMod != null)
                {
                    RecipeGroup.recipeGroups[pylonIndex].ValidItems.Add(SpiritMod.Find<ModItem>("AsteroidPylonItem").Type);
                    RecipeGroup.recipeGroups[pylonIndex].ValidItems.Add(SpiritMod.Find<ModItem>("BriarPylonItem").Type);
                    RecipeGroup.recipeGroups[pylonIndex].ValidItems.Add(SpiritMod.Find<ModItem>("SpiritPylonItem").Type);
                }
            }
        }

        public static bool PureglowRange(int i)
        {
            if(SpiritMod != null)
            {
                HashSet<Point16> superSunflowerPositions = (HashSet<Point16>)SpiritMod.Code.GetType("SpiritMod.MyWorld").GetField("superSunFlowerPositions").GetValue(null);
                int superSunflowerRange = (int)SpiritMod.Code.GetType("SpiritMod.Tiles.SuperSunFlower").GetField("Range").GetValue(null);

                foreach (Point16 point in superSunflowerPositions)
                {
                    if (Math.Abs(point.X - i) < superSunflowerRange * 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool EnoughTilesForAltEvilBiome(SceneMetrics sceneMetrics)
        {
            if (ExxoAvalon != null)
            {
                int contagionTileCount = 0;
                foreach (string tileName in ModBlocks[2])
                {
                    contagionTileCount += sceneMetrics.GetTileCount(ExxoAvalon.Find<ModTile>(tileName).Type);
                }

                if (contagionTileCount >= 300)
                {
                    return true;
                }
            }

            if (TerrariaOrigins != null)
            {
                int defiledTileCount = 0;
                foreach (string tileName in ModBlocks[4])
                {
                    defiledTileCount += sceneMetrics.GetTileCount(TerrariaOrigins.Find<ModTile>(tileName).Type);
                }

                if(defiledTileCount > 200)
                {
                    return true;
                }

                int rivenTileCount = 0;
                foreach (string tileName in ModBlocks[6])
                {
                    rivenTileCount += sceneMetrics.GetTileCount(TerrariaOrigins.Find<ModTile>(tileName).Type);
                }

                if (rivenTileCount > 200)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsAltEvilBlock(int i, int j)
        {
            if (ExxoAvalon != null)
            {
                foreach (string wallName in ModBlocks[3])
                {
                    if (Main.tile[i, j].WallType == ExxoAvalon.Find<ModWall>(wallName).Type)
                    {
                        return true;
                    }
                }

                foreach (string tileName in ModBlocks[2])
                {
                    if (Main.tile[i, j].TileType == ExxoAvalon.Find<ModTile>(tileName).Type)
                    {
                        return true;
                    }
                }
            }

            if (TerrariaOrigins != null)
            {
                foreach (string wallName in ModBlocks[5])
                {
                    if (Main.tile[i, j].WallType == TerrariaOrigins.Find<ModWall>(wallName).Type)
                    {
                        return true;
                    }
                }

                foreach (string tileName in ModBlocks[4])
                {
                    if (Main.tile[i, j].TileType == TerrariaOrigins.Find<ModTile>(tileName).Type)
                    {
                        return true;
                    }
                }

                foreach (string wallName in ModBlocks[7])
                {
                    if (Main.tile[i, j].WallType == TerrariaOrigins.Find<ModWall>(wallName).Type)
                    {
                        return true;
                    }
                }

                foreach (string tileName in ModBlocks[6])
                {
                    if (Main.tile[i, j].TileType == TerrariaOrigins.Find<ModTile>(tileName).Type)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool AltLibraryInfection(bool good)
        {
            if(ModLoader.TryGetMod("AltLibrary", out Mod altLib))
            {
                Type biomeManager = altLib.Code.GetType("AltLibrary.Common.Systems.WorldBiomeManager");
                if (good)
                {
                    return (string)biomeManager.GetProperty("WorldHallowName").GetValue(null) != "";
                }
                else
                {
                    if(Main.drunkWorld)
                    {
                        if(WorldGen.crimson) return false;

                        string drunkEviName = (string)biomeManager.GetField("drunkEvilName", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
                        return drunkEviName != "Terraria/Corruption";
                    }
                    return (string)biomeManager.GetProperty("WorldEvilName").GetValue(null) != "";
                }
            }
            return false;
        }

        public static bool HallowNightMusic()
        {
            if(SpiritMod != null)
            {
                Type musicConfig = SpiritMod.Code.GetType("SpiritMod.Utilities.SpiritMusicConfig");
                return (bool)musicConfig.GetProperty("HallowNightMusic").GetValue(ModContent.Find<ModConfig>(SpiritMod.Name, "SpiritMusicConfig"));
            }
            return false;
        }
    }
}
