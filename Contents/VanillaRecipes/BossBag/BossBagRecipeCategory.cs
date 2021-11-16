using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using TRaI.APIs;
using TRaI.APIs.Ingredients;
using TRaI.UIs.UIElements;

namespace TRaI.Contents.VanillaRecipes.BossBag
{
    public class BossBagRecipeCategory : RecipeCategory
    {
        public override Asset<Texture2D> TextureIcon => TextureAssets.Item[ItemID.KingSlimeBossBag];
        public override LocalizedText Name => Language.GetText("Mods.TRaI.BossBags");
        public override StyleDimension Width => new(0, 1f);
        public override int Height => 282;


        public override void InitRecipes()
        {
            LootDropEmulation.SetLoadingName(Name.Value);
            var list = new List<int>();
            foreach (var item in TRaI.AllItems)
                if (ItemID.Sets.BossBag[item.type])
                    list.Add(item.type);

            for (int i = 0; i < list.Count; i++)
            {
                LootDropEmulation.SetLoadingProgress(i / (float)list.Count);
                Recipes.Add(new BossBagRecipeElement(list[i]));
            }
        }

        public override void InitElement(UIRecipeLayout layout, IRecipeElement recipeElement, RecipeIngredients ingredients)
        {
            var ingredient = layout.AddItemIngredient(ingredients.GetInputs<ItemIngredient>()[0], true, 0, 0);
            ingredient.HAlign = 0.5f;

            var image = layout.AddImage(TRaIAsset.Content[2], 0, 54);
            image.HAlign = 0.5f;

            layout.AddItemIngredients(ingredients.GetOutputs<ItemIngredient>(), false, 0, 54 * 2, 14, 3);
        }
    }
}
