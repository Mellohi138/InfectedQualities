using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using InfectedQualities.Content.Items;

namespace InfectedQualities.Common.ModPlayers
{
    public class NaughtKeySpawning : ModPlayer
    {
		//Most of the code here was taken from the Confection's source code, credits belong to their respective owners
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<InfectedQualitiesConfig.ServerConfig>().EnableKeyOfNaught;
        }

        private int LastChest = -1;

        public override void PreUpdateBuffs()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Main.hardMode && Player.chest == -1 && LastChest >= 0 && Main.chest[LastChest] != null)
                {
                    int x = Main.chest[LastChest].x;
                    int y = Main.chest[LastChest].y;

                    int num = Chest.FindChest(x, y);

                    if (num != -1)
                    {
                        int key = 0;
                        int other = 0;

                        ushort type = Framing.GetTileSafely(Main.chest[num].x, Main.chest[num].y).TileType;
                        int tileFrame = Framing.GetTileSafely(Main.chest[num].x, Main.chest[num].y).TileFrameX / 36;

                        if (TileID.Sets.BasicChest[type] && (tileFrame < 5 || tileFrame > 6))
                        {
                            for (int i = 0; i < 40; i++)
                            {
                                if (Main.chest[num].item[i].type == ModContent.ItemType<KeyOfNaught>())
                                {
                                    key += Main.chest[num].item[i].stack;
                                }
                                else if (Main.chest[num].item[i].type > ItemID.None)
                                {
                                    other++;
                                }
                            }
                        }

                        if (other == 0 && key == 1)
                        {
                            if (TileID.Sets.BasicChest[Framing.GetTileSafely(x, y).TileType])
                            {
                                if (Framing.GetTileSafely(x, y).TileFrameX % 36 != 0)
                                {
                                    x--;
                                }
                                if (Framing.GetTileSafely(x, y).TileFrameY % 36 != 0)
                                {
                                    y--;
                                }

                                for (int l = 0; l < 40; l++)
                                {
                                    Main.chest[num].item[l].TurnToAir(true);
                                }

                                for (int j = x; j <= x + 1; j++)
                                {
                                    for (int k = y; k <= y + 1; k++)
                                    {
                                        if (TileID.Sets.BasicChest[Framing.GetTileSafely(j, k).TileType])
                                        {
                                            Tile chest = Framing.GetTileSafely(j, k);
                                            chest.HasTile = false;
                                        }
                                    }
                                }

                                Chest.DestroyChest(x, y);
                                NetMessage.SendData(MessageID.ChestUpdates, -1, -1, null, 1, x, y, 0f, Chest.FindChest(x, y));
                                NetMessage.SendTileSquare(-1, x, y, 3);
                            }
                            int npcIndex = NPC.NewNPC(Terraria.Entity.GetSource_NaturalSpawn(), x * 16 + 16, y * 16 + 32, WorldGen.crimson ? NPCID.BigMimicCorruption : NPCID.BigMimicCrimson);
                            Main.npc[npcIndex].whoAmI = npcIndex;
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcIndex);
                            Main.npc[npcIndex].BigMimicSpawnSmoke();
                        }
                    }
                }

                LastChest = Player.chest;
            }
        }

        public override void UpdateAutopause()
        {
            LastChest = Player.chest;
        }
    }
}
