﻿using InfectedQualities.Content.Extras;
using InfectedQualities.Content.Extras.Tiles;
using InfectedQualities.Content.Tiles;
using InfectedQualities.Core;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Common
{
    public class InfectedQualitiesGlobalNPC : GlobalNPC
    {
        private static Asset<Texture2D> Plantera { get; set; } = null;
        private static Asset<Texture2D> PlanteraHook { get; set; } = null;
        private static Asset<Texture2D> PlanteraHookVine { get; set; } = null;
        private static Asset<Texture2D> PlanteraTentacle { get; set; } = null;
        private static Asset<Texture2D> PlanteraTentacleVine { get; set; } = null;
        private static Asset<Texture2D> PlanteraSpore { get; set; } = null;
        private static Asset<Texture2D> PlanteraSeed { get; set; } = null;
        internal static Asset<Texture2D> CorruptPlantera { get; set; } = null;
        internal static Asset<Texture2D> CrimsonPlantera { get; set; } = null;
        internal static Asset<Texture2D> HallowedPlantera { get; set; } = null;

        public override void SetStaticDefaults()
        {
            if (ModContent.GetInstance<InfectedQualitiesClientConfig>().InfectedPlantera)
            {
                Plantera = TextureAssets.Npc[NPCID.Plantera];
                PlanteraHook = TextureAssets.Npc[NPCID.PlanterasHook];
                PlanteraHookVine = TextureAssets.Chain26;
                PlanteraTentacle = TextureAssets.Npc[NPCID.PlanterasTentacle];
                PlanteraTentacleVine = TextureAssets.Chain27;
                PlanteraSpore = TextureAssets.Npc[NPCID.Spore];
                PlanteraSeed = TextureAssets.Projectile[ProjectileID.SeedPlantera];

                CorruptPlantera = ModContent.Request<Texture2D>("InfectedQualities/Content/NPCs/CorruptPlantera");
                CrimsonPlantera = ModContent.Request<Texture2D>("InfectedQualities/Content/NPCs/CrimsonPlantera");
                HallowedPlantera = ModContent.Request<Texture2D>("InfectedQualities/Content/NPCs/HallowedPlantera");
            }
        }

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if(npc.type == NPCID.Plantera && ModContent.GetInstance<InfectedQualitiesClientConfig>().InfectedPlantera)
            {
                if (TextureAssets.Npc[NPCID.Plantera] != CorruptPlantera && TextureAssets.Npc[NPCID.Plantera] != CrimsonPlantera && TextureAssets.Npc[NPCID.Plantera] != HallowedPlantera)
                {
                    Plantera = TextureAssets.Npc[NPCID.Plantera];
                    PlanteraHook = TextureAssets.Npc[NPCID.PlanterasHook];
                    PlanteraHookVine = TextureAssets.Chain26;
                    PlanteraTentacle = TextureAssets.Npc[NPCID.PlanterasTentacle];
                    PlanteraTentacleVine = TextureAssets.Chain27;
                    PlanteraSpore = TextureAssets.Npc[NPCID.Spore];
                    PlanteraSeed = TextureAssets.Projectile[ProjectileID.SeedPlantera];
                }

                if (Main.LocalPlayer.ZoneCorrupt)
                {
                    TextureUtilities.ReplacePlanteraType(InfectionType.Corrupt);
                }
                else if (Main.LocalPlayer.ZoneCrimson)
                {
                    TextureUtilities.ReplacePlanteraType(InfectionType.Crimson);
                }
                else if (Main.LocalPlayer.ZoneHallow)
                {
                    TextureUtilities.ReplacePlanteraType(InfectionType.Hallowed);
                }
                else
                {
                    TextureAssets.Npc[NPCID.Plantera] = Plantera;
                    TextureAssets.Npc[NPCID.PlanterasHook] = PlanteraHook;
                    TextureAssets.Chain26 = PlanteraHookVine;
                    TextureAssets.Npc[NPCID.PlanterasTentacle] = PlanteraTentacle;
                    TextureAssets.Chain27 = PlanteraTentacleVine;
                    TextureAssets.Npc[NPCID.Spore] = PlanteraSpore;
                    TextureAssets.Projectile[ProjectileID.SeedPlantera] = PlanteraSeed;
                }
            }
        }

        public override void BossHeadSlot(NPC npc, ref int index)
        {
            if (npc.type == NPCID.Plantera && ModContent.GetInstance<InfectedQualitiesClientConfig>().InfectedPlantera)
            {
                InfectionType? planteraType = TextureUtilities.GetPlanteraType();
                if(planteraType.HasValue)
                {
                    int phaseIndex = npc.life > npc.lifeMax / 2 ? 1 : 2;
                    index = ModContent.GetModBossHeadSlot("InfectedQualities/Content/Extras/MapIcons/" + planteraType.ToString() + "Plantera_MapIcon_" + phaseIndex);
                }
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes && pool.ContainsKey(0) && !spawnInfo.Player.ZoneUnderworldHeight && !spawnInfo.Sky && !spawnInfo.PlayerInTown)
            {
                float hardmodeSpawnChance = Main.hardMode ? 0.4f : 1f;
                if (spawnInfo.SpawnTileType == ModContent.TileType<HallowedJungleGrass>() && spawnInfo.Player.ZoneJungle && spawnInfo.Player.ZoneHallow)
                {
                    pool.Remove(0);
                    if (Main.dayTime)
                    {
                        pool.Add(NPCID.JungleSlime, hardmodeSpawnChance);
                        pool.Add(NPCID.JungleBat, hardmodeSpawnChance);
                        pool.Add(NPCID.Snatcher, hardmodeSpawnChance);
                    }
                    else
                    {
                        pool.Add(NPCID.Zombie, hardmodeSpawnChance);
                        pool.Add(NPCID.DemonEye, hardmodeSpawnChance);
                        pool.Add(NPCID.DoctorBones, 0.002f);
                    }

                    if (Main.hardMode)
                    {
                        pool.Add(NPCID.GiantTortoise, 1.0f);
                        pool.Add(NPCID.AngryTrapper, 1.0f);

                        if (spawnInfo.Player.ZoneSurface())
                        {
                            pool.Add(NPCID.Pixie, 1.0f);
                            pool.Add(NPCID.Unicorn, 1.0f);

                            if (Main.dayTime)
                            {
                                pool.Add(NPCID.Derpling, 1.0f);
                            }
                            else
                            {
                                pool.Add(NPCID.Gastropod, 1.0f);
                                pool.Add(NPCID.GiantFlyingFox, 1.0f);
                            }

                            if (Main.raining && !NPC.AnyNPCs(NPCID.RainbowSlime)) pool.Add(NPCID.RainbowSlime, 0.05f);
                        }
                        else
                        {
                            if (spawnInfo.Player.ZoneDirtLayerHeight)
                            {
                                pool.Add(NPCID.Pixie, 1.0f);
                                if (!Main.dayTime) pool.Add(NPCID.Gastropod, 1.0f);
                            }
                            else if (spawnInfo.Player.ZoneCavern())
                            {
                                pool.Add(NPCID.JungleCreeper, 1.0f);
                                pool.Add(NPCID.MossHornet, 1.0f);
                                pool.Add(NPCID.IlluminantBat, 1.0f);
                                pool.Add(NPCID.IlluminantSlime, 1.0f);
                                pool.Add(NPCID.ChaosElemental, 1.0f);

                                if (!NPC.AnyNPCs(NPCID.Moth)) pool.Add(NPCID.Moth, 0.02f);
                                pool.Add(NPCID.EnchantedSword, 0.02f);
                                pool.Add(NPCID.BigMimicHallow, 0.01f);
                            }
                        }
                    }
                }
                else if (spawnInfo.Player.ZoneSnow)
                {
                    if (spawnInfo.SpawnTileType == TileUtilities.GetSnowType(InfectionType.Corrupt) && spawnInfo.Player.ZoneCorrupt)
                    {
                        pool.Remove(0);
                        pool.Add(NPCID.EaterofSouls, hardmodeSpawnChance);
                        pool.Add(NPCID.DevourerHead, hardmodeSpawnChance / 4f);

                        if (spawnInfo.Player.ZoneSurface() || spawnInfo.Player.ZoneDirtLayerHeight)
                        {
                            if (Main.dayTime)
                            {
                                pool.Add(NPCID.IceSlime, hardmodeSpawnChance);
                            }
                            else
                            {
                                pool.Add(NPCID.ZombieEskimo, hardmodeSpawnChance);
                                if (Main.bloodMoon)
                                {
                                    pool.Add(WorldGen.crimson ? NPCID.CrimsonPenguin : NPCID.CorruptPenguin, hardmodeSpawnChance);
                                }
                            }
                        }
                        else
                        {
                            pool.Add(NPCID.IceBat, hardmodeSpawnChance);
                            pool.Add(NPCID.SnowFlinx, hardmodeSpawnChance);
                            pool.Add(NPCID.SpikedIceSlime, hardmodeSpawnChance);
                            pool.Add(NPCID.UndeadViking, hardmodeSpawnChance);
                            pool.Add(NPCID.CyanBeetle, hardmodeSpawnChance);
                        }

                        if (Main.hardMode)
                        {
                            pool.Add(NPCID.Corruptor, 1.0f);
                            pool.Add(NPCID.CorruptSlime, 1.0f);
                            pool.Add(NPCID.Slimeling, 1.0f);

                            if (spawnInfo.Player.ZoneSurface())
                            {
                                if (!Main.dayTime) pool.Add(NPCID.Wolf, 1.0f);
                                if (Main.raining && !NPC.AnyNPCs(NPCID.IceGolem)) pool.Add(NPCID.IceGolem, 0.05f);
                            }
                            else if (spawnInfo.Player.ZoneCavern())
                            {
                                pool.Add(NPCID.ArmoredViking, 1.0f);
                                pool.Add(NPCID.IceTortoise, 1.0f);
                                pool.Add(NPCID.IcyMerman, 1.0f);
                                pool.Add(NPCID.IceMimic, 1 / 70);

                                pool.Add(NPCID.PigronCorruption, 1.0f);
                                pool.Add(NPCID.Clinger, 1.0f);
                                pool.Add(NPCID.CursedHammer, 0.02f);
                                pool.Add(NPCID.BigMimicCorruption, 0.01f);
                            }

                            if (!Main.dayTime || spawnInfo.Player.ZoneCavern()) pool.Add(NPCID.IceElemental, 1.0f);
                        }
                    }
                    else if (spawnInfo.SpawnTileType == TileUtilities.GetSnowType(InfectionType.Crimson) && spawnInfo.Player.ZoneCrimson)
                    {
                        pool.Remove(0);
                        pool.Add(NPCID.BloodCrawler, hardmodeSpawnChance);
                        pool.Add(NPCID.FaceMonster, hardmodeSpawnChance);
                        pool.Add(NPCID.Crimera, hardmodeSpawnChance);

                        if (spawnInfo.Player.ZoneSurface() || spawnInfo.Player.ZoneDirtLayerHeight)
                        {
                            if (Main.dayTime)
                            {
                                pool.Add(NPCID.IceSlime, hardmodeSpawnChance);
                            }
                            else
                            {
                                pool.Add(NPCID.ZombieEskimo, hardmodeSpawnChance);
                                if (Main.bloodMoon)
                                {
                                    pool.Add(WorldGen.crimson ? NPCID.CrimsonPenguin : NPCID.CorruptPenguin, hardmodeSpawnChance);
                                }
                            }
                        }
                        else
                        {
                            pool.Add(NPCID.IceBat, hardmodeSpawnChance);
                            pool.Add(NPCID.SnowFlinx, hardmodeSpawnChance);
                            pool.Add(NPCID.SpikedIceSlime, hardmodeSpawnChance);
                            pool.Add(NPCID.UndeadViking, hardmodeSpawnChance);
                            pool.Add(NPCID.CyanBeetle, hardmodeSpawnChance);
                        }

                        if (Main.hardMode)
                        {
                            pool.Add(NPCID.Crimslime, 1.0f);
                            pool.Add(NPCID.Herpling, 1.0f);

                            if (spawnInfo.Player.ZoneSurface())
                            {
                                if (!Main.dayTime) pool.Add(NPCID.Wolf, 1.0f);
                                if (Main.raining && !NPC.AnyNPCs(NPCID.IceGolem)) pool.Add(NPCID.IceGolem, 0.05f);
                            }
                            else if (spawnInfo.Player.ZoneCavern())
                            {
                                pool.Add(NPCID.ArmoredViking, 1.0f);
                                pool.Add(NPCID.IceTortoise, 1.0f);
                                pool.Add(NPCID.IcyMerman, 1.0f);
                                pool.Add(NPCID.IceMimic, 1 / 70);

                                pool.Add(NPCID.PigronCrimson, 1.0f);
                                pool.Add(NPCID.FloatyGross, 1.0f);
                                pool.Add(NPCID.IchorSticker, 1.0f);
                                pool.Add(NPCID.CrimsonAxe, 0.02f);
                                pool.Add(NPCID.BigMimicCrimson, 0.01f);
                            }

                            if (!Main.dayTime || spawnInfo.Player.ZoneCavern()) pool.Add(NPCID.IceElemental, 1.0f);
                        }
                    }
                    else if (spawnInfo.SpawnTileType == TileUtilities.GetSnowType(InfectionType.Hallowed) && spawnInfo.Player.ZoneHallow)
                    {
                        pool.Remove(0);
                        if (spawnInfo.Player.ZoneSurface() || spawnInfo.Player.ZoneDirtLayerHeight)
                        {
                            if (Main.dayTime)
                            {
                                pool.Add(NPCID.IceSlime, hardmodeSpawnChance);
                            }
                            else
                            {
                                pool.Add(NPCID.ZombieEskimo, hardmodeSpawnChance);
                                if (Main.bloodMoon)
                                {
                                    pool.Add(WorldGen.crimson ? NPCID.CrimsonPenguin : NPCID.CorruptPenguin, hardmodeSpawnChance);
                                }
                            }
                        }
                        else
                        {
                            pool.Add(NPCID.IceBat, hardmodeSpawnChance);
                            pool.Add(NPCID.SnowFlinx, hardmodeSpawnChance);
                            pool.Add(NPCID.SpikedIceSlime, hardmodeSpawnChance);
                            pool.Add(NPCID.UndeadViking, hardmodeSpawnChance);
                            pool.Add(NPCID.CyanBeetle, 0.3f);
                        }

                        if (Main.hardMode)
                        {
                            if (spawnInfo.Player.ZoneSurface())
                            {
                                pool.Add(NPCID.Pixie, 1.0f);
                                pool.Add(NPCID.Unicorn, 1.0f);

                                if (!Main.dayTime)
                                {
                                    pool.Add(NPCID.Gastropod, 1.0f);
                                    pool.Add(NPCID.Wolf, 1.0f);
                                }

                                if (Main.raining)
                                {
                                    if (!NPC.AnyNPCs(NPCID.IceGolem)) pool.Add(NPCID.IceGolem, 0.05f);
                                    if (!NPC.AnyNPCs(NPCID.RainbowSlime)) pool.Add(NPCID.RainbowSlime, 0.05f);
                                }
                            }
                            else
                            {
                                if (spawnInfo.Player.ZoneDirtLayerHeight)
                                {
                                    pool.Add(NPCID.Pixie, 1.0f);
                                    if (!Main.dayTime) pool.Add(NPCID.Gastropod, 1.0f);
                                }
                                else if (spawnInfo.Player.ZoneCavern())
                                {
                                    pool.Add(NPCID.ArmoredViking, 1.0f);
                                    pool.Add(NPCID.IceTortoise, 1.0f);
                                    pool.Add(NPCID.IcyMerman, 1.0f);
                                    pool.Add(NPCID.IceMimic, 1 / 70);

                                    pool.Add(NPCID.PigronHallow, 1.0f);
                                    pool.Add(NPCID.IlluminantBat, 1.0f);
                                    pool.Add(NPCID.IlluminantSlime, 1.0f);
                                    pool.Add(NPCID.ChaosElemental, 1.0f);
                                    pool.Add(NPCID.EnchantedSword, 0.02f);
                                    pool.Add(NPCID.BigMimicHallow, 0.01f);
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void Unload()
        {
            if (ModContent.GetInstance<InfectedQualitiesClientConfig>().InfectedPlantera && TextureUtilities.GetPlanteraType().HasValue)
            {
                TextureAssets.Npc[NPCID.Plantera] = Main.Assets.Request<Texture2D>($"Images/NPC_{NPCID.Plantera}");
                TextureAssets.Npc[NPCID.PlanterasHook] = Main.Assets.Request<Texture2D>($"Images/NPC_{NPCID.PlanterasHook}");
                TextureAssets.Chain26 = Main.Assets.Request<Texture2D>("Images/Extra_26");
                TextureAssets.Npc[NPCID.PlanterasTentacle] = Main.Assets.Request<Texture2D>($"Images/NPC_{NPCID.PlanterasTentacle}");
                TextureAssets.Chain27 = Main.Assets.Request<Texture2D>("Images/Extra_27");
                TextureAssets.Npc[NPCID.Spore] = Main.Assets.Request<Texture2D>($"Images/NPC_{NPCID.Spore}");
                TextureAssets.Projectile[ProjectileID.SeedPlantera] = Main.Assets.Request<Texture2D>($"Images/Projectile_{ProjectileID.SeedPlantera}");
            }
        }
    }
}
