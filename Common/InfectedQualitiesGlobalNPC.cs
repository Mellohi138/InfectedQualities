using InfectedQualities.Content.Tiles;
using InfectedQualities.Core;
using InfectedQualities.Utilities;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfectedQualities.Common;

public class InfectedPlanteraSystem : GlobalNPC
{
    private static Asset<Texture2D> Plantera { get; set; } = null;
    private static Asset<Texture2D> PlanteraHook { get; set; } = null;
    private static Asset<Texture2D> PlanteraHookVine { get; set; } = null;
    private static Asset<Texture2D> PlanteraTentacle { get; set; } = null;
    private static Asset<Texture2D> PlanteraTentacleVine { get; set; } = null;
    private static Asset<Texture2D> PlanteraSpore { get; set; } = null;
    private static Asset<Texture2D> PlanteraSeed { get; set; } = null;
    private static Asset<Texture2D> PlanteraThornBall { get; set; } = null;

    public static InfectionType? PlanteraType { get; set; } = null;

    public override void Load()
    {
        Plantera = TextureAssets.Npc[NPCID.Plantera];
        PlanteraHook = TextureAssets.Npc[NPCID.PlanterasHook];
        PlanteraHookVine = TextureAssets.Chain26;
        PlanteraTentacle = TextureAssets.Npc[NPCID.PlanterasTentacle];
        PlanteraTentacleVine = TextureAssets.Chain27;
        PlanteraSpore = TextureAssets.Npc[NPCID.Spore];
        PlanteraSeed = TextureAssets.Projectile[ProjectileID.SeedPlantera];
        PlanteraThornBall = TextureAssets.Projectile[ProjectileID.ThornBall];
    }

    public static void ReplacePlanteraTextures(Player player)
    {
        if (!PlanteraType.HasValue)
        {
            Plantera = TextureAssets.Npc[NPCID.Plantera];
            PlanteraHook = TextureAssets.Npc[NPCID.PlanterasHook];
            PlanteraHookVine = TextureAssets.Chain26;
            PlanteraTentacle = TextureAssets.Npc[NPCID.PlanterasTentacle];
            PlanteraTentacleVine = TextureAssets.Chain27;
            PlanteraSpore = TextureAssets.Npc[NPCID.Spore];
            PlanteraSeed = TextureAssets.Projectile[ProjectileID.SeedPlantera];
            PlanteraThornBall = TextureAssets.Projectile[ProjectileID.ThornBall];
        }

        if (player.ZoneCorrupt)
        {
            PlanteraType = InfectionType.Corrupt;
        }
        else if (player.ZoneCrimson)
        {
            PlanteraType = InfectionType.Crimson;
        }
        else if (player.ZoneHallow)
        {
            PlanteraType = InfectionType.Hallowed;
        }
        else
        {
            PlanteraType = null;

            TextureAssets.Npc[NPCID.Plantera] = Plantera;
            TextureAssets.Npc[NPCID.PlanterasHook] = PlanteraHook;
            TextureAssets.Chain26 = PlanteraHookVine;
            TextureAssets.Npc[NPCID.PlanterasTentacle] = PlanteraTentacle;
            TextureAssets.Chain27 = PlanteraTentacleVine;
            TextureAssets.Npc[NPCID.Spore] = PlanteraSpore;
            TextureAssets.Projectile[ProjectileID.SeedPlantera] = PlanteraSeed;
            TextureAssets.Projectile[ProjectileID.ThornBall] = PlanteraThornBall;
            return;
        }

        string planteraType = PlanteraType.ToString();
        TextureAssets.Npc[NPCID.Plantera] = ModContent.Request<Texture2D>("InfectedQualities/Content/Extras/NPCs/" + planteraType + "Plantera", AssetRequestMode.ImmediateLoad);
        TextureAssets.Npc[NPCID.PlanterasHook] = ModContent.Request<Texture2D>("InfectedQualities/Content/Extras/NPCs/" + planteraType + "Plantera_Hook", AssetRequestMode.ImmediateLoad);
        TextureAssets.Chain26 = ModContent.Request<Texture2D>("InfectedQualities/Content/Extras/" + planteraType + "Plantera_Hook_Vine", AssetRequestMode.ImmediateLoad);
        TextureAssets.Npc[NPCID.PlanterasTentacle] = ModContent.Request<Texture2D>("InfectedQualities/Content/Extras/NPCs/" + planteraType + "Plantera_Tentacle", AssetRequestMode.ImmediateLoad);
        TextureAssets.Chain27 = ModContent.Request<Texture2D>("InfectedQualities/Content/Extras/" + planteraType + "Plantera_Tentacle_Vine", AssetRequestMode.ImmediateLoad);
        TextureAssets.Npc[NPCID.Spore] = ModContent.Request<Texture2D>("InfectedQualities/Content/Extras/NPCs/" + planteraType + "Plantera_Spore", AssetRequestMode.ImmediateLoad);
        TextureAssets.Projectile[ProjectileID.SeedPlantera] = ModContent.Request<Texture2D>("InfectedQualities/Content/Extras/Projectiles/" + planteraType + "Plantera_Seed", AssetRequestMode.ImmediateLoad);
        TextureAssets.Projectile[ProjectileID.ThornBall] = ModContent.Request<Texture2D>("InfectedQualities/Content/Extras/Projectiles/" + planteraType + "Plantera_ThornBall", AssetRequestMode.ImmediateLoad);
    }

    public override void BossHeadSlot(NPC npc, ref int index)
    {
        if (PlanteraType.HasValue)
        {
            int phaseIndex = npc.life > npc.lifeMax / 2 ? 1 : 2;
            index = ModContent.GetModBossHeadSlot("InfectedQualities/Content/Extras/MapIcons/" + PlanteraType.ToString() + "Plantera_MapIcon_" + phaseIndex);
        }
    }

    public override void Unload()
    {
        if (PlanteraType.HasValue)
        {
            TextureAssets.Npc[NPCID.Plantera] = Main.Assets.Request<Texture2D>($"Images/NPC_{NPCID.Plantera}");
            TextureAssets.Npc[NPCID.PlanterasHook] = Main.Assets.Request<Texture2D>($"Images/NPC_{NPCID.PlanterasHook}");
            TextureAssets.Chain26 = Main.Assets.Request<Texture2D>("Images/Extra_26");
            TextureAssets.Npc[NPCID.PlanterasTentacle] = Main.Assets.Request<Texture2D>($"Images/NPC_{NPCID.PlanterasTentacle}");
            TextureAssets.Chain27 = Main.Assets.Request<Texture2D>("Images/Extra_27");
            TextureAssets.Npc[NPCID.Spore] = Main.Assets.Request<Texture2D>($"Images/NPC_{NPCID.Spore}");
            TextureAssets.Projectile[ProjectileID.SeedPlantera] = Main.Assets.Request<Texture2D>($"Images/Projectile_{ProjectileID.SeedPlantera}");
            TextureAssets.Projectile[ProjectileID.ThornBall] = Main.Assets.Request<Texture2D>($"Images/Projectile_{ProjectileID.ThornBall}");
        }
    }

    public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => entity.type == NPCID.Plantera;

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesClientConfig>().InfectedPlantera;
}

public class NPCSpawningSystem : GlobalNPC
{
    public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
    {
        if (pool.ContainsKey(0) && !spawnInfo.Player.ZoneUnderworldHeight && !spawnInfo.Sky && !spawnInfo.PlayerInTown)
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

                    if (spawnInfo.Player.DetermineZone(false))
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

                        if (Main.raining && !NPC.AnyNPCs(NPCID.RainbowSlime))
                        {
                            pool.Add(NPCID.RainbowSlime, 0.05f);
                        }
                    }
                    else
                    {
                        if (spawnInfo.Player.ZoneDirtLayerHeight)
                        {
                            pool.Add(NPCID.Pixie, 1.0f);
                            if (!Main.dayTime) pool.Add(NPCID.Gastropod, 1.0f);
                        }
                        else if (spawnInfo.Player.DetermineZone(true))
                        {
                            pool.Add(NPCID.JungleCreeper, 1.0f);
                            pool.Add(NPCID.MossHornet, 1.0f);
                            pool.Add(NPCID.IlluminantBat, 1.0f);
                            pool.Add(NPCID.IlluminantSlime, 1.0f);
                            pool.Add(NPCID.ChaosElemental, 1.0f);

                            if (!NPC.AnyNPCs(NPCID.Moth))
                            {
                                pool.Add(NPCID.Moth, 0.02f);
                            }
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

                    if (spawnInfo.Player.DetermineZone(false) || spawnInfo.Player.ZoneDirtLayerHeight)
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
                                pool.Add(NPCID.CorruptPenguin, hardmodeSpawnChance);
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

                        if (spawnInfo.Player.DetermineZone(false))
                        {
                            if (!Main.dayTime) pool.Add(NPCID.Wolf, 1.0f);
                            if (Main.raining && !NPC.AnyNPCs(NPCID.IceGolem)) pool.Add(NPCID.IceGolem, 0.05f);
                        }
                        else if (spawnInfo.Player.DetermineZone(true))
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

                        if (!Main.dayTime || spawnInfo.Player.DetermineZone(true))
                        {
                            pool.Add(NPCID.IceElemental, 1.0f);
                        }
                    }
                }
                else if (spawnInfo.SpawnTileType == TileUtilities.GetSnowType(InfectionType.Crimson) && spawnInfo.Player.ZoneCrimson)
                {
                    pool.Remove(0);
                    pool.Add(NPCID.BloodCrawler, hardmodeSpawnChance);
                    pool.Add(NPCID.FaceMonster, hardmodeSpawnChance);
                    pool.Add(NPCID.Crimera, hardmodeSpawnChance);

                    if (spawnInfo.Player.DetermineZone(false) || spawnInfo.Player.ZoneDirtLayerHeight)
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
                                pool.Add(NPCID.CrimsonPenguin, hardmodeSpawnChance);
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

                        if (spawnInfo.Player.DetermineZone(false))
                        {
                            if (!Main.dayTime) pool.Add(NPCID.Wolf, 1.0f);
                            if (Main.raining && !NPC.AnyNPCs(NPCID.IceGolem)) pool.Add(NPCID.IceGolem, 0.05f);
                        }
                        else if (spawnInfo.Player.DetermineZone(true))
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

                        if (!Main.dayTime || spawnInfo.Player.DetermineZone(true))
                        {
                            pool.Add(NPCID.IceElemental, 1.0f);
                        }
                    }
                }
                else if (spawnInfo.SpawnTileType == TileUtilities.GetSnowType(InfectionType.Hallowed) && spawnInfo.Player.ZoneHallow)
                {
                    pool.Remove(0);
                    if (spawnInfo.Player.DetermineZone(false) || spawnInfo.Player.ZoneDirtLayerHeight)
                    {
                        if (Main.dayTime)
                        {
                            pool.Add(NPCID.IceSlime, hardmodeSpawnChance);
                        }
                        else
                        {
                            pool.Add(NPCID.ZombieEskimo, hardmodeSpawnChance);
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
                        if (spawnInfo.Player.DetermineZone(false))
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
                                if (!NPC.AnyNPCs(NPCID.IceGolem))
                                {
                                    pool.Add(NPCID.IceGolem, 0.05f);
                                }
                                if (!NPC.AnyNPCs(NPCID.RainbowSlime))
                                {
                                    pool.Add(NPCID.RainbowSlime, 0.05f);
                                }
                            }
                        }
                        else
                        {
                            if (spawnInfo.Player.ZoneDirtLayerHeight)
                            {
                                pool.Add(NPCID.Pixie, 1.0f);
                                if (!Main.dayTime)
                                {
                                    pool.Add(NPCID.Gastropod, 1.0f);
                                }
                            }
                            else if (spawnInfo.Player.DetermineZone(true))
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

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().InfectedBiomes;
}
