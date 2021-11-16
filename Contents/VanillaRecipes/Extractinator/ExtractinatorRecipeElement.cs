using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TRaI.APIs;
using TRaI.APIs.Ingredients;

namespace TRaI.Contents.VanillaRecipes.Extractinator
{
    public class ExtractinatorRecipeElement : IRecipeElement
    {
        public Item ExtractinatorInput { get; set; }
        public List<ItemIngredient> ExtractinatorOutputs { get; set; }

        public Mod Mod => ExtractinatorInput.ModItem?.Mod;

        public ExtractinatorRecipeElement(int extractinatorItem)
        {
            ExtractinatorInput = new(extractinatorItem);
            var extractinatorMode = ItemID.Sets.ExtractinatorMode[extractinatorItem];
            var method = typeof(Player).GetMethod("ExtractinatorUse", BindingFlags.NonPublic | BindingFlags.Instance);
            ExtractinatorOutputs = LootDropEmulation.Emulate(() => method.Invoke(Main.LocalPlayer, new object[] { extractinatorMode }), 100000);
        }

        public void GetIngredients(RecipeIngredients ingredients)
        {
            ingredients.SetInput(ExtractinatorInput);
            ingredients.SetOutputs(ExtractinatorOutputs);
        }
    }
}
