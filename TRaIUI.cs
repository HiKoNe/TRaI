using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TRaI.APIs.Ingredients;
using TRaI.UIs;

namespace TRaI
{
    public class TRaIUI : ModSystem
    {
        public static UserInterface UIItems { get; private set; }
        public static UIStateRecipes UIRecipes { get; private set; }

        public static bool ActiveItems
        {
            get => UIItems?.CurrentState is not null;
            set
            {
                if (value)
                    UIItems?.SetState(new UIStateItems(TRaIConfig.Instance.NumberItemsWidth, TRaIConfig.Instance.NumberItemsHeight));
                else
                    UIItems?.SetState(null);
            }
        }

        public static bool IsRecipesOpened => Main.InGameUI.CurrentState is UIStateRecipes;

        public static void OpenRecipes(Item item, bool isRecipe)
        {
            if (item is null)
                return;

            OpenRecipes(isRecipe ? Mode.Recipe : Mode.Use, new ItemIngredient(item));
        }

        public static void OpenRecipes(Mode mode = Mode.All, IIngredient ingredient = null)
        {
            SoundEngine.PlaySound(SoundID.MenuOpen);
            Main.LocalPlayer.SetTalkNPC(-1, false);
            if (mode != Mode.All && ingredient is null)
                return;

            UIRecipes.Ingredient = ingredient;
            UIRecipes.Mode = mode;
            UIRecipes.Recalculate();
            if (Main.InGameUI.CurrentState is UIStateRecipes)
            {
                var h = UIRecipes.History[UIRecipes.CurrentHistory];
                h.category = UIRecipes.CurrentCategory;
                h.page = UIRecipes.CurrentPage;
                UIRecipes.History[UIRecipes.CurrentHistory] = h;

                UIRecipes.CurrentHistory++;
                UIRecipes.History.RemoveRange(UIRecipes.CurrentHistory, UIRecipes.History.Count - UIRecipes.CurrentHistory);
                UIRecipes.History.Add((UIRecipes.Mode, UIRecipes.Ingredient, UIRecipes.CurrentCategory, UIRecipes.CurrentPage));
                UIRecipes.Activate();
            }
            else
            {
                UIRecipes.History.Clear();
                UIRecipes.CurrentHistory = 0;
                UIRecipes.History.Add((UIRecipes.Mode, UIRecipes.Ingredient, UIRecipes.CurrentCategory, UIRecipes.CurrentPage));
                IngameFancyUI.OpenUIState(UIRecipes);
            }
        }

        public override void Load()
        {
            UIItems = new();
            UIRecipes = new();
        }

        public override void Unload()
        {
            UIItems = null;
            UIRecipes = null;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            var index = layers.FindIndex(layer => layer.Name == "Vanilla: Inventory");
            if (index != -1)
                layers.Insert(index + 1, new LegacyGameInterfaceLayer("TRaI: Items", () =>
                {
                    if (Main.playerInventory && !Main.inFancyUI)
                        UIItems.Draw(Main.spriteBatch, null);
                    return true;
                }, InterfaceScaleType.UI));
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (Main.playerInventory && !Main.inFancyUI)
                UIItems.Update(gameTime);
        }
    }
}
