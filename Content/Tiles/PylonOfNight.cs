using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ObjectData;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Map;
using ReLogic.Content;

using InfectedQualities.Content.Tiles.TileEntities;
using InfectedQualities.Common;
using Terraria.Localization;

namespace InfectedQualities.Content.Tiles
{
    public class PylonOfNight : ModPylon
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().EnablePylonOfNight;
        }

        public override string Texture => "InfectedQualities/Client/Assets/Tiles/PylonOfNight";

        private static Asset<Texture2D> crystalTexture;
        private static Asset<Texture2D> crystalHighlightTexture;
        private static Asset<Texture2D> mapIcon;

        private static Color PylonColor;

        public override void Load()
        {
            crystalTexture = ModContent.Request<Texture2D>("InfectedQualities/Client/Assets/Tiles/PylonOfNight_Crystal");
            crystalHighlightTexture = ModContent.Request<Texture2D>("InfectedQualities/Client/Assets/Tiles/Pylon_CrystalHighlight");
            mapIcon = ModContent.Request<Texture2D>("InfectedQualities/Client/Assets/PylonOfNight_MapIcon");

            PylonColor = new(162, 95, 234);
        }

        public override void Unload()
        {
            crystalTexture = null;
            crystalHighlightTexture = null;
            mapIcon = null;
        }

        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.StyleHorizontal = true;

            TEModdedPylon tilePylon = ModContent.GetInstance<PylonTileEntity>();
            TileObjectData.newTile.HookCheckIfCanPlace = new(tilePylon.PlacementPreviewHook_CheckIfCanPlace, 1, 0, true);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new(tilePylon.Hook_AfterPlacement, -1, 0, false);
            TileObjectData.addTile(Type);

            TileID.Sets.InteractibleByNPCs[Type] = true;
            TileID.Sets.PreventsSandfall[Type] = true;
            TileID.Sets.AvoidedByMeteorLanding[Type] = true;

            AddToArray(ref TileID.Sets.CountsAsPylon);
            AddMapEntry(PylonColor, Language.GetText("Mods.InfectedQualities.Items.PylonOfNight.DisplayName"));
        }

        public override NPCShop.Entry GetNPCShopEntry()
        {
            return null;
        }

        public override void MouseOver(int i, int j)
        {
            Main.LocalPlayer.cursorItemIconEnabled = true;
            Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<Items.Tiles.PylonOfNight>();
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            ModContent.GetInstance<PylonTileEntity>().Kill(i, j);
        }

        public override bool ValidTeleportCheck_NPCCount(TeleportPylonInfo pylonInfo, int defaultNecessaryNPCCount)
        {
            return true;
        }

        public override bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
        {
            return (sceneData.EnoughTilesForCorruption || sceneData.EnoughTilesForCrimson) && !sceneData.EnoughTilesForGlowingMushroom;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = PylonColor.R / 255f;
            g = PylonColor.G / 255f;
            b = PylonColor.B / 255f;
        }

        public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
        {
            DefaultDrawPylonCrystal(spriteBatch, i, j, crystalTexture, crystalHighlightTexture, new Vector2(0f, -12f), PylonColor * 0.1f, PylonColor, 4, 8);
        }

        public override void DrawMapIcon(ref MapOverlayDrawContext context, ref string mouseOverText, TeleportPylonInfo pylonInfo, bool isNearPylon, Color drawColor, float deselectedScale, float selectedScale)
        {
            bool mouseOver = DefaultDrawMapIcon(ref context, mapIcon, pylonInfo.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f), drawColor, deselectedScale, selectedScale);
            DefaultMapClickHandle(mouseOver, pylonInfo, ModContent.GetInstance<Items.Tiles.PylonOfNight>().DisplayName.Key, ref mouseOverText);
        }
    }
}