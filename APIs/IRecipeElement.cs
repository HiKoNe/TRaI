using Terraria.ModLoader;

namespace TRaI.APIs
{
    public interface IRecipeElement
    {
        public Mod Mod { get; }
        void GetIngredients(RecipeIngredients ingredients);
    }
}
