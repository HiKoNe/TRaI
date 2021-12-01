using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using TRaI.UIs.UIElements;

namespace TRaI.APIs
{
    public abstract class RecipeCategory : ILoadable
    {
        public Mod Mod { get; set; }
        public List<IRecipeElement> Recipes { get; set; }

        public void Load(Mod mod)
        {
            Mod = mod;
            Recipes = new();
            RecipeCategoryLoader.Categories.Add(this);
        }

        public void Unload()
        {
            RecipeCategoryLoader.Categories.Clear();
        }

        public abstract Asset<Texture2D> TextureIcon { get; }
        public abstract LocalizedText Name { get; }
        public abstract StyleDimension Width { get; }
        public abstract int Height { get; }
        public virtual bool NeedReinit { get; }

        public abstract void InitRecipes();
        public abstract void InitElement(UIRecipeLayout layout, IRecipeElement recipeElement, RecipeIngredients ingredients);
    }
}
