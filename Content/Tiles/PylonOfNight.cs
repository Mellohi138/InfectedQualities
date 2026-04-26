using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Map;
using ReLogic.Content;
using Terraria.Localization;
using InfectedQualities.Core;
using InfectedQualities.Content.Items.Placables;
using InfectedQualities.Content.Tiles.TileEntities;

namespace InfectedQualities.Content.Tiles;

public class PylonOfNight : ModPylon
{
    private Asset<Texture2D> PylonCrystalTexture { get; set; } = null;
    private Asset<Texture2D> PylonCrystalHighlightTexture { get; set; } = null;
    private Asset<Texture2D> PylonMapIcon { get; set; } = null;

    private static Color PylonColor => new(162, 95, 234);

    public override void SetStaticDefaults()
    {
        Main.tileLighted[Type] = true;
        Main.tileFrameImportant[Type] = true;

        TileID.Sets.InteractibleByNPCs[Type] = true;
        TileID.Sets.PreventsSandfall[Type] = true;
        TileID.Sets.AvoidedByMeteorLanding[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.HookCheckIfCanPlace = new(ModContent.GetInstance<PylonTileEntity>().PlacementPreviewHook_CheckIfCanPlace, 1, 0, true);
        TileObjectData.newTile.HookPostPlaceMyPlayer = new(ModContent.GetInstance<PylonTileEntity>().Hook_AfterPlacement, -1, 0, false);
        TileObjectData.addTile(Type);

        AddToArray(ref TileID.Sets.CountsAsPylon);
        AddMapEntry(PylonColor, Language.GetText("Mods.InfectedQualities.Items.Placables.PylonOfNightBlock.DisplayName"));

        PylonCrystalTexture = ModContent.Request<Texture2D>("InfectedQualities/Content/Tiles/" + Name + "_Crystal");
        PylonCrystalHighlightTexture = ModContent.Request<Texture2D>("InfectedQualities/Content/Extras/Tiles/Pylon_CrystalHighlight");
        PylonMapIcon = ModContent.Request<Texture2D>("InfectedQualities/Content/Extras/MapIcons/" + Name + "_MapIcon");

        VanillaFallbackOnModDeletion = TileID.TeleportationPylon;
    }

    public override NPCShop.Entry GetNPCShopEntry() => null;

    public override void MouseOver(int i, int j)
    {
        Main.LocalPlayer.cursorItemIconEnabled = true;
        Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<PylonOfNightBlock>();
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY) => ModContent.GetInstance<PylonTileEntity>().Kill(i, j);

    public override bool ValidTeleportCheck_NPCCount(TeleportPylonInfo pylonInfo, int defaultNecessaryNPCCount) => true;

    public override bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData) => sceneData.EnoughTilesForCorruption || sceneData.EnoughTilesForCrimson || InfectedQualitiesModSupport.EnoughTilesForAltEvilBiome(sceneData);

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = PylonColor.R / 255f;
        g = PylonColor.G / 255f;
        b = PylonColor.B / 255f;
    }

    public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch) => DefaultDrawPylonCrystal(spriteBatch, i, j, PylonCrystalTexture, PylonCrystalHighlightTexture, new Vector2(0f, -12f), PylonColor * 0.1f, PylonColor, 12, 8);

    public override void DrawMapIcon(ref MapOverlayDrawContext context, ref string mouseOverText, TeleportPylonInfo pylonInfo, bool isNearPylon, Color drawColor, float deselectedScale, float selectedScale)
    {
        bool mouseOver = DefaultDrawMapIcon(ref context, PylonMapIcon, pylonInfo.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f), drawColor, deselectedScale, selectedScale);
        DefaultMapClickHandle(mouseOver, pylonInfo, ModContent.GetInstance<PylonOfNightBlock>().DisplayName.Key, ref mouseOverText);
    }

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfectedQualitiesServerConfig>().PylonOfNight;
}