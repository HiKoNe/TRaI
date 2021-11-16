using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TRaI
{
    public class TRaIKeybind : ModPlayer
    {
        public const string TAG_ITEM_PANEL_SHOW = "itemPanelShow";

        public static ModKeybind ItemUsageKeybind { get; set; }
        public static ModKeybind ItemRecipeKeybind { get; set; }
        public static ModKeybind RecipeBackKeybind { get; set; }

        public override void Load()
        {
            base.Load();
            ItemUsageKeybind = KeybindLoader.RegisterKeybind(Mod, "Item Usage", Keys.U);
            ItemRecipeKeybind = KeybindLoader.RegisterKeybind(Mod, "Item Recipe", Keys.R);
            RecipeBackKeybind = KeybindLoader.RegisterKeybind(Mod, "Recipe Back", Keys.Back);
        }

        public override void Unload()
        {
            base.Unload();
            ItemUsageKeybind = null;
            ItemRecipeKeybind = null;
            RecipeBackKeybind = null;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            base.ProcessTriggers(triggersSet);
            bool shift = Main.keyState.PressingShift();

            if (Main.HoverItem is not null && !Main.HoverItem.IsAir)
            {
                if (ItemUsageKeybind.JustPressed)
                    TRaIUI.OpenRecipes(Main.HoverItem, shift);
                else if (ItemRecipeKeybind.JustPressed)
                    TRaIUI.OpenRecipes(Main.HoverItem, !shift);
            }

            if (RecipeBackKeybind.JustPressed)
            {
                if (shift)
                    TRaIUI.UIRecipes.HistoryForward();
                else
                    TRaIUI.UIRecipes.HistoryBack();
            }
        }

        public override void SaveData(TagCompound tag)
        {
            base.SaveData(tag);
            tag[TAG_ITEM_PANEL_SHOW] = TRaIUI.ActiveItems;
        }

        public override void LoadData(TagCompound tag)
        {
            base.LoadData(tag);

            if (tag.ContainsKey(TAG_ITEM_PANEL_SHOW))
                TRaIUI.ActiveItems = tag.GetBool(TAG_ITEM_PANEL_SHOW);
        }
    }
}
