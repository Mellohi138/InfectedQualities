using InfectedQualities.Content.Worldgen;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace InfectedQualities.Common.ModSystems
{
    public class ModWorldgen : ModSystem
    {
        public override void ModifyHardmodeTasks(List<GenPass> list)
        {
            if (!Main.remixWorld)
            {
                int hardmodeGood = list.FindIndex(genPass => genPass.Name.Equals("Hardmode Good"));
                int i = 1;
                if (ModContent.GetInstance<ServerConfig>().EnableHardmodeChasmPurification)
                {
                    list.Insert(hardmodeGood + i, new WorldGenChasmPurifyer());
                    i++;
                }
                list.Insert(hardmodeGood + i, new WorldGenInfectionV());
                list.RemoveAt(hardmodeGood);

                int hardmodeEvil = list.FindIndex(genPass => genPass.Name.Equals("Hardmode Evil"));
                list.RemoveAt(hardmodeEvil);
            }
        }
    }
}