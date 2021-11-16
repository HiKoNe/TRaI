using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TRaI.APIs;
using TRaI.APIs.Ingredients;

namespace TRaI.Contents.VanillaRecipes.FishCrate
{
    public class FishCrateRecipeElement : IRecipeElement
    {
        public Item FishingCrateInput { get; set; }
        public List<ItemIngredient> FishingCrateOutputs { get; set; }

        public Mod Mod => FishingCrateInput.ModItem?.Mod;

        public FishCrateRecipeElement(int fishingCrate)
        {
            FishingCrateInput = new(fishingCrate);
            FishingCrateOutputs = LootDropEmulation.Emulate(() => Main.LocalPlayer.OpenFishingCrate(fishingCrate));
        }

        public void GetIngredients(RecipeIngredients ingredients)
        {
            ingredients.SetInput(FishingCrateInput);
            ingredients.SetOutputs(FishingCrateOutputs);
        }
    }
}
