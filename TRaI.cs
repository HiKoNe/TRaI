using Microsoft.Xna.Framework;
using MonoMod.RuntimeDetour.HookGen;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TRaI.APIs;
using TRaI.Contents;

namespace TRaI
{
    public class TRaI : Mod
    {
        public static TRaI Instance { get; set; }

        public static Item[] AllItems { get; private set; }
        public static List<int>[] AdjacentTiles { get; private set; }
        public static Dictionary<int, string> AllToolTips { get; private set; } = new();
        public static int ItemsCount { get; private set; }

        public static void Debug(object message) => Instance.Logger.Debug(message);

        public override void Load()
        {
            base.Load();
            Instance = this;
            TRaIAsset.Load(this);
            On.Terraria.Main.DrawInventory += this.Main_DrawInventory;
            OnPostAddRecipes += TRaI_OnPostAddRecipes;
            On.Terraria.Item.NewItem_int_int_int_int_int_int_bool_int_bool_bool += Item_NewItem_int_int_int_int_int_int_bool_int_bool_bool;
            On.Terraria.Main.DrawPendingMouseText += Main_DrawPendingMouseText;
        }

        public override void Unload()
        {
            base.Unload();
            Instance = null;
            TRaIAsset.Unload();
            On.Terraria.Main.DrawInventory -= this.Main_DrawInventory;
            OnPostAddRecipes -= TRaI_OnPostAddRecipes;
            On.Terraria.Item.NewItem_int_int_int_int_int_int_bool_int_bool_bool -= Item_NewItem_int_int_int_int_int_int_bool_int_bool_bool;
            On.Terraria.Main.DrawPendingMouseText -= Main_DrawPendingMouseText;

            AllItems = null;
            AllToolTips.Clear();
        }

        public override void PostSetupContent()
        {
            base.PostSetupContent();
            var list = new List<Item>();
            for (int i = 1; i < ItemLoader.ItemCount; i++)
            {
                //AllItems
                var item = new Item();
                item.SetDefaults(i);
                if (item.type == ItemID.None)
                    continue;
                list.Add(item);

                //AllToolTips
                int yoyoLogo = 0;
                int researchLine = 0;
                int numLines = 1;
                int num2 = 30;
                string[] toolTipLine = new string[num2];
                bool[] preFixLine = new bool[num2];
                bool[] badPreFixLine = new bool[num2];
                string[] tooltipNames = new string[num2];
                Main.MouseText_DrawItemTooltip_GetLinesInfo(item, ref yoyoLogo, ref researchLine, 0, ref numLines, toolTipLine, preFixLine, badPreFixLine, tooltipNames);
                ItemLoader.ModifyTooltips(item, ref numLines, tooltipNames, ref toolTipLine, ref preFixLine, ref badPreFixLine, ref yoyoLogo, out _);
                AllToolTips[item.type] = string.Join("\n", toolTipLine).ToLower();
            }
            AllItems = list.ToArray();
            ItemsCount = list.Count;

            //AdjacentTiles
            AdjacentTiles = new List<int>[TileLoader.TileCount];
            for (int i = 0; i < TileLoader.TileCount; i++)
            {
                var adjTiles = new List<int>();

                //ModTile
                ModTile modTile = TileLoader.GetTile(i);
                if (modTile is not null)
                    adjTiles.AddRange(modTile.AdjTiles);

                //GlobalTile
                var delegateObjArray = (object[])typeof(TileLoader)
                    .GetField("HookAdjTiles", BindingFlags.NonPublic | BindingFlags.Static)
                    .GetValue(null);
                foreach (var delegateObj in delegateObjArray)
                    adjTiles.AddRange((int[])delegateObj.GetType()
                        .GetMethod("Invoke")
                        .Invoke(delegateObj, new object[] { i }));

                //VanillaTile
                switch (i)
                {
                    case TileID.Furnaces:
                        adjTiles.Add(TileID.GlassKiln);
                        adjTiles.Add(TileID.Hellforge);
                        adjTiles.Add(TileID.AdamantiteForge);
                        break;
                    case TileID.Hellforge:
                        adjTiles.Add(TileID.AdamantiteForge);
                        break;
                    case TileID.Anvils:
                        adjTiles.Add(TileID.MythrilAnvil);
                        break;
                    case TileID.Bottles:
                        adjTiles.Add(TileID.AlchemyTable);
                        break;
                    case TileID.Tables:
                        adjTiles.Add(TileID.Tables2);
                        adjTiles.Add(TileID.PicnicTable);
                        adjTiles.Add(TileID.AlchemyTable);
                        adjTiles.Add(TileID.BewitchingTable);
                        break;
                }

                if (adjTiles.Count > 0)
                    AdjacentTiles[i] = adjTiles;
            }
        }

        //public override void AddRecipes()
        //{
        //    CreateRecipe(ItemID.DirtBlock, 999)
        //        .AddTile(TileID.WorkBenches)
        //        .AddTile(TileID.Anvils)
        //        .AddTile(TileID.Furnaces)
        //        .AddIngredient(ItemID.DirtBlock, 999)
        //        .AddIngredient(ItemID.StoneBlock, 1)
        //        .AddIngredient(ItemID.Wood, 1)
        //        .AddIngredient(ItemID.IronOre, 1)
        //        .AddIngredient(ItemID.CopperOre, 1)
        //        .AddIngredient(ItemID.GoldOre, 1)
        //        .AddIngredient(ItemID.SilverOre, 1)
        //        .AddIngredient(ItemID.GoldBar, 1)
        //        .AddIngredient(ItemID.CopperBar, 1)
        //        .AddIngredient(ItemID.SilverBar, 1)
        //        .AddIngredient(ItemID.IronBar, 1)
        //        .AddIngredient(ItemID.Gel, 1)
        //        .AddIngredient(ItemID.Acorn, 1)
        //        .AddIngredient(ItemID.Bottle, 1)
        //        .AddIngredient(ItemID.Lens, 1)
        //        .AddIngredient(ItemID.CopperCoin, 1)
        //        .AddIngredient(ItemID.SilverCoin, 1)
        //        .AddIngredient(ItemID.GoldCoin, 1)
        //        .AddIngredient(ItemID.PlatinumCoin, 1)
        //        .AddIngredient(ItemID.Torch, 1)
        //        .AddCondition(Recipe.Condition.TimeDay, Recipe.Condition.NearWater, Recipe.Condition.NearLava)
        //        .Register();
        //}

        void Main_DrawInventory(On.Terraria.Main.orig_DrawInventory orig, Main self)
        {
            orig(self);

            if (Main.InReforgeMenu
                || Main.hidePlayerCraftingMenu
                || Main.LocalPlayer.tileEntityAnchor.InUse
                || (Main.CreativeMenu.Enabled && !Main.CreativeMenu.Blocked))
                return;

            int x = 94;
            int y = (Main.screenHeight - 600) / 2;
            if (Main.screenHeight < 700)
                y = (Main.screenHeight - 508) / 2;
            y += 450;
            if (Main.InGuideCraftMenu)
                y -= 150;

            y += Main.InGuideCraftMenu ? -35 : 35;
            bool isHover = Main.mouseX > x - 15 && Main.mouseX < x + 15 && Main.mouseY > y - 15 && Main.mouseY < y + 15 && !PlayerInput.IgnoreMouseInterface;
            Main.spriteBatch.Draw(TRaIAsset.Button[isHover.ToInt()].Value,
                new Vector2(x, y), null,
                Color.White, 0f, TRaIAsset.Button[isHover.ToInt()].Value.Size() / 2f, 1f, 0, 0f);
            if (isHover)
            {
                Main.instance.MouseText("TRaIItems".GetLocalizeText());
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeft && Main.mouseLeftRelease)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    TRaIUI.ActiveItems = !TRaIUI.ActiveItems;
                }
            }

            y += Main.InGuideCraftMenu ? -35 : 35;
            isHover = Main.mouseX > x - 15 && Main.mouseX < x + 15 && Main.mouseY > y - 15 && Main.mouseY < y + 15 && !PlayerInput.IgnoreMouseInterface;
            Main.spriteBatch.Draw(TRaIAsset.Button[6 + isHover.ToInt()].Value,
                new Vector2(x, y), null,
                Color.White, 0f, TRaIAsset.Button[6 + isHover.ToInt()].Value.Size() / 2f, 1f, 0, 0f);
            if (isHover)
            {
                Main.instance.MouseText("TRaIRecipes".GetLocalizeText());
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeft && Main.mouseLeftRelease)
                {
                    TRaIUI.OpenRecipes();
                }
            }

            y += Main.InGuideCraftMenu ? -35 : 35;
            isHover = Main.mouseX > x - 15 && Main.mouseX < x + 15 && Main.mouseY > y - 15 && Main.mouseY < y + 15 && !PlayerInput.IgnoreMouseInterface;
            Main.spriteBatch.Draw(TRaIAsset.Button[2 + isHover.ToInt()].Value,
                new Vector2(x, y), null,
                Color.White, 0f, TRaIAsset.Button[2 + isHover.ToInt()].Value.Size() / 2f, 1f, 0, 0f);
            if (isHover)
            {
                Main.instance.MouseText("TRaIConfig".GetLocalizeText());
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeft && Main.mouseLeftRelease)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);

                    var InterfaceType = typeof(Mod).Assembly.GetType("Terraria.ModLoader.UI.Interface");
                    var modConfigField = InterfaceType.GetField("modConfig", BindingFlags.NonPublic | BindingFlags.Static);
                    var modConfigObj = modConfigField.GetValue(null);

                    var UIModConfigType = typeof(Mod).Assembly.GetType("Terraria.ModLoader.Config.UI.UIModConfig");
                    var SetModMethod = UIModConfigType.GetMethod("SetMod", BindingFlags.NonPublic | BindingFlags.Instance);

                    IngameOptions.Close();
                    IngameFancyUI.CoverNextFrame();
                    Main.playerInventory = false;
                    Main.editChest = false;
                    Main.npcChatText = "";
                    Main.inFancyUI = true;
                    SetModMethod.Invoke(modConfigObj, new object[] { this, ModContent.GetInstance<TRaIConfig>() });
                    Main.InGameUI.SetState((UIState)modConfigObj);
                }
            }
        }

        void TRaI_OnPostAddRecipes(orig_PostAddRecipes orig)
        {
            orig();
            RecipeCategoryLoader.InitRecipes();
        }

        public static int Item_NewItem_int_int_int_int_int_int_bool_int_bool_bool(On.Terraria.Item.orig_NewItem_int_int_int_int_int_int_bool_int_bool_bool orig, int X, int Y, int Width, int Height, int Type, int Stack, bool noBroadcast, int pfix, bool noGrabDelay, bool reverseLookup)
        {
            int result = 0;
            if (LootDropEmulation.Emulation)
                LootDropEmulation.OnItemDrop(Type, Stack);
            else
                result = orig(X, Y, Width, Height, Type, Stack, noBroadcast, pfix, noGrabDelay, reverseLookup);
            return result;
        }

        void Main_DrawPendingMouseText(On.Terraria.Main.orig_DrawPendingMouseText orig)
        {
            orig();
            TRaIUtils.OnPostDraw(Main.spriteBatch);
        }

        internal static MethodBase MethodPostAddRecipes => typeof(Mod).Assembly.GetType("Terraria.ModLoader.RecipeLoader").GetMethod("PostAddRecipes", BindingFlags.NonPublic | BindingFlags.Static);
        internal delegate void orig_PostAddRecipes();
        internal delegate void hook_PostAddRecipes(orig_PostAddRecipes orig);
        internal static event hook_PostAddRecipes OnPostAddRecipes
        {
            add => HookEndpointManager.Add<hook_PostAddRecipes>(MethodPostAddRecipes, value);
            remove => HookEndpointManager.Remove<hook_PostAddRecipes>(MethodPostAddRecipes, value);
        }
    }
}