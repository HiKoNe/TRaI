﻿using System.Collections.Generic;

namespace TRaI.APIs
{
    public static class RecipeCategoryLoader
    {
        public static List<RecipeCategory> Categories { get; private set; } = new();

        internal static void InitRecipes()
        {
            foreach (var category in Categories)
                category.InitRecipes();
        }
    }
}
