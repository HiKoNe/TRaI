using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using TRaI.APIs;
using TRaI.APIs.Ingredients;
using TRaI.Contents.VanillaRecipes.NPCDrop;
using TRaI.UIs.UIElements;

namespace TRaI.Contents.VanillaRecipes.ByHand
{
    public class NPCDropRecipeCategory : RecipeCategory
    {
        public override Asset<Texture2D> TextureIcon => TRaIAsset.Content[1];
        public override LocalizedText Name => Language.GetText("Mods.TRaI.NPCDrops");
        public override StyleDimension Width => new(0, 1f);
        public override int Height => 228;

        public override void InitRecipes()
        {
            for (int i = -1000; i < NPCLoader.NPCCount; i++)
                if (Main.BestiaryDB.FindEntryByNPCID(i).Info.Count > 0)
                    Recipes.Add(new NPCDropRecipeElement(i));
        }

        public override void InitElement(UIRecipeLayout layout, IRecipeElement recipeElement, RecipeIngredients ingredients)
        {
            var npcDrop = (NPCDropRecipeElement)recipeElement;

            var npcPanel = new UIPanel();
            npcPanel.Height.Set(0, 1f);
            npcPanel.Width.Set(54 * 4, 0);
            npcPanel.BackgroundColor = new Color(50, 58, 119);
            layout.Append(npcPanel);

            var npcUI = new UINPC(npcDrop.BestiaryEntry);
            npcUI.Height = new(0, 1f);
            npcUI.Width = new(0, 1f);
            npcPanel.Append(npcUI);

            var name = new NamePlateInfoElement(Lang.GetNPCName(npcDrop.NPCID).Key, npcDrop.NPCID);
            npcPanel.Append(name.ProvideUIElement(npcUI.bestiaryUICollectionInfo));

            layout.AddItemIngredients(ingredients.GetOutputs<ItemIngredient>(), false, 54 * 4, 0, 10, 4);
        }
    }
}
