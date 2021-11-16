using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TRaI.APIs;
using TRaI.APIs.Ingredients;

namespace TRaI.Contents.VanillaRecipes.BossBag
{
    public class BossBagRecipeElement : IRecipeElement
    {
        public Item BossBagInput { get; set; }
        public List<ItemIngredient> BossBagOutputs { get; set; }

        public Mod Mod => BossBagInput.ModItem?.Mod;

        public BossBagRecipeElement(int bossBag)
        {
            BossBagInput = new(bossBag);
            BossBagOutputs = LootDropEmulation.Emulate(() => Main.LocalPlayer.OpenBossBag(bossBag), 5000);
        }

        public void GetIngredients(RecipeIngredients ingredients)
        {
            ingredients.SetInput(BossBagInput);
            ingredients.SetOutputs(BossBagOutputs);
        }
    }
}
