using InfectedQualities.Content.Biomes;
using InfectedQualities.Content.Tiles;
using InfectedQualities.Helpers;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Common.GlobalNPCs
{
    public class ModNPCSpawning : GlobalNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedJungleBiomes || ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedSnowBiomes;
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if(ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedJungleBiomes)
            {
                if (spawnInfo.SpawnTileType == ModContent.TileType<HallowedJungleGrass>())
                {
                    if (!spawnInfo.PlayerInTown && spawnInfo.Player.InModBiome<HallowedJungle>())
                    {
                        if (spawnInfo.Player.ZoneSurface())
                        {
                            pool.Add(NPCID.Pixie, 1.5f);
                            pool.Add(NPCID.Unicorn, 1.0f);

                            if (!Main.dayTime)
                            {
                                pool.Add(NPCID.Gastropod, 1.5f);
                            }
                        }
                        else
                        {
                            pool.Add(NPCID.GiantTortoise, 0.6f);
                            pool.Add(NPCID.IlluminantBat, 1.5f);

                            if (PlayerHelper.ZoneCavern(spawnInfo.Player))
                            {
                                pool.Add(NPCID.ChaosElemental, 0.35f);
                                pool.Add(NPCID.EnchantedSword, 0.1f);
                            }
                        }
                    }
                }
            }

            if (ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableInfectedSnowBiomes)
            {
                if(spawnInfo.Player.ZoneSnow)
                {
                    if (spawnInfo.SpawnTileType == ModContent.TileType<CorruptSnow>() && spawnInfo.Player.ZoneCorrupt)
                    {
                        if(spawnInfo.Player.ZoneSurface())
                        {
                            if (Main.dayTime)
                            {
                                pool.Add(NPCID.IceSlime, 1.4f);
                            }
                            else
                            {
                                pool.Add(NPCID.ZombieEskimo, 1.4f);
                            }

                            if (Main.raining)
                            {
                                pool.Add(NPCID.IceGolem, 0.1f);
                            }
                        }
                        else if (spawnInfo.Player.ZoneCavern())
                        {
                            pool.Add(NPCID.IcyMerman, 0.5f);
                            pool.Add(NPCID.IceTortoise, 1.2f);
                            pool.Add(NPCID.Clinger, 2.4f);
                            pool.Add(NPCID.CursedHammer, 0.4f);
                            pool.Add(NPCID.PigronCorruption, 2.4f);
                        }

                        pool.Add(NPCID.EaterofSouls, 2.0f);
                        pool.Add(NPCID.DevourerHead, 0.2f);

                        if (Main.hardMode)
                        {
                            pool.Add(NPCID.Corruptor, 4.5f);
                            pool.Add(NPCID.CorruptSlime, 3.4f);
                            pool.Add(NPCID.Slimer, 3.0f);

                            if(!Main.dayTime || spawnInfo.Player.ZoneCavern())
                            {
                                pool.Add(NPCID.IceElemental, 0.6f);
                            }
                        }
                    }
                    else if(spawnInfo.SpawnTileType == ModContent.TileType<CrimsonSnow>() && spawnInfo.Player.ZoneCrimson)
                    {
                        if (spawnInfo.Player.ZoneSurface())
                        {
                            if (Main.dayTime)
                            {
                                pool.Add(NPCID.IceSlime, 1.4f);
                            }
                            else
                            {
                                pool.Add(NPCID.ZombieEskimo, 1.4f);
                            }

                            if (Main.raining)
                            {
                                pool.Add(NPCID.IceGolem, 0.1f);
                            }
                        }
                        else if (spawnInfo.Player.ZoneCavern())
                        {
                            pool.Add(NPCID.IcyMerman, 0.5f);
                            pool.Add(NPCID.IceTortoise, 1.2f);
                            pool.Add(NPCID.Clinger, 2.4f);
                            pool.Add(NPCID.CrimsonAxe, 0.4f);
                            pool.Add(NPCID.PigronCrimson, 2.4f);
                        }

                        pool.Add(NPCID.FaceMonster, 2.6f);
                        pool.Add(NPCID.Crimera, 3.0f);
                        pool.Add(NPCID.BloodCrawler, 1.4f);

                        if (Main.hardMode)
                        {
                            pool.Add(NPCID.Herpling, 4.0f);
                            pool.Add(NPCID.Crimslime, 3.6f);

                            if (!Main.dayTime || spawnInfo.Player.ZoneCavern())
                            {
                                pool.Add(NPCID.IceElemental, 0.6f);
                            }
                        }
                    }
                    else if (spawnInfo.SpawnTileType == ModContent.TileType<HallowedSnow>() && spawnInfo.Player.ZoneHallow && Main.hardMode && !spawnInfo.PlayerInTown)
                    {
                        if (spawnInfo.Player.ZoneSurface())
                        {
                            pool.Add(NPCID.Pixie, 2.0f);
                            pool.Add(NPCID.Unicorn, 1.2f);

                            if (!Main.dayTime)
                            {
                                pool.Add(NPCID.Gastropod, 1.2f);
                            }

                            if (Main.raining)
                            {
                                pool.Add(NPCID.IceGolem, 0.05f);
                            }
                        }
                        else 
                        {
                            pool.Add(NPCID.IlluminantBat, 1.4f);

                            if (spawnInfo.Player.ZoneCavern())
                            {
                                pool.Add(NPCID.IceTortoise, 1.4f);
                                pool.Add(NPCID.ChaosElemental, 0.6f);
                                pool.Add(NPCID.EnchantedSword, 0.2f);
                                pool.Add(NPCID.PigronHallow, 1.4f);
                            }
                        }

                        if (!Main.dayTime || spawnInfo.Player.ZoneCavern())
                        {
                            pool.Add(NPCID.IceElemental, 0.6f);
                        }
                    }
                }
            }
        }
    }
}
