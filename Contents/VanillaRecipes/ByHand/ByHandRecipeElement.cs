using System.Linq;
using Terraria;
using Terraria.ModLoader;
using TRaI.APIs;
using TRaI.APIs.Ingredients;

namespace TRaI.Contents.VanillaRecipes.ByHand
{
    public class ByHandRecipeElement : IRecipeElement
    {
        public Recipe Recipe { get; set; }

        public Mod Mod => Recipe.Mod;

        public ByHandRecipeElement(Recipe recipe)
        {
            Recipe = recipe;
        }

        public void GetIngredients(RecipeIngredients ingredients)
        {
            ingredients.SetOutput(Recipe.createItem, 1f, Recipe.Conditions.Select(c => c.Description).ToList());
            ingredients.SetInputs(Recipe.requiredItem);
            ingredients.SetInputs(Recipe.requiredTile.Select(t => new TileIngredient(t)));
        }
    }
}
