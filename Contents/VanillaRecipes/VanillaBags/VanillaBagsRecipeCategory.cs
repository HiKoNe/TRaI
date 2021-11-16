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

namespace TRaI.Contents.VanillaRecipes.VanillaBags
{
    public class VanillaBagsRecipeCategory : RecipeCategory
    {
        public override Asset<Texture2D> TextureIcon => TextureAssets.Item[ItemID.HerbBag];
        public override LocalizedText Name => Language.GetText("Mods.TRaI.VanillaBags");
        public override StyleDimension Width => new(0, 1f);
        public override int Height => 228;

        public override void InitRecipes()
        {
            LootDropEmulation.SetLoadingName(Name.Value);
            var list = new List<int>()
            {
                3093, //HerbBag
                4345, //CanofWorms
                4410, //Oyster
                1774, //GoodieBag
                3085, //LockBox
                4879, //ShadowLockbox
                1869, //Present
                599, //Presents 1
                600, //Presents 2
                601, //Presents 3
            };

            for (int i = 0; i < list.Count; i++)
            {
                LootDropEmulation.SetLoadingProgress(i / (float)list.Count);
                Recipes.Add(new VanillaBagsRecipeElement(list[i]));
            }
        }

        public override void InitElement(UIRecipeLayout layout, IRecipeElement recipeElement, RecipeIngredients ingredients)
        {
            var ingredient = layout.AddItemIngredient(ingredients.GetInputs<ItemIngredient>()[0], true, 0, 0);
            ingredient.HAlign = 0.5f;

            var image = layout.AddImage(TRaIAsset.Content[2], 0, 54);
            image.HAlign = 0.5f;

            layout.AddItemIngredients(ingredients.GetOutputs<ItemIngredient>(), false, 0, 54 * 2, 14, 2);
        }
    }
}
