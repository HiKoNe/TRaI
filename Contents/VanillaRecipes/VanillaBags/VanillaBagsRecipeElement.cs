using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TRaI.APIs;
using TRaI.APIs.Ingredients;

namespace TRaI.Contents.VanillaRecipes.VanillaBags
{
    public class VanillaBagsRecipeElement : IRecipeElement
    {
        public Item BagInput { get; set; }
        public List<ItemIngredient> BagOutputs { get; set; }

        public Mod Mod => BagInput.ModItem?.Mod;

        public VanillaBagsRecipeElement(int bag)
        {
            BagInput = new(bag);
            BagOutputs = bag switch
            {
                //HerbBag
                3093 => LootDropEmulation.Emulate(() => Main.LocalPlayer.OpenHerbBag()),
                //CanofWorms
                4345 => LootDropEmulation.Emulate(() => Main.LocalPlayer.OpenCanofWorms()),
                //Oyster
                4410 => LootDropEmulation.Emulate(() => Main.LocalPlayer.OpenOyster()),
                //GoodieBag
                1774 => LootDropEmulation.Emulate(() => Main.LocalPlayer.OpenGoodieBag()),
                //LockBox
                3085 => LootDropEmulation.Emulate(() => Main.LocalPlayer.OpenLockBox()),
                //ShadowLockbox
                4879 => LootDropEmulation.Emulate(() => Main.LocalPlayer.OpenShadowLockbox()),
                //Present
                1869 => LootDropEmulation.Emulate(() => Main.LocalPlayer.openPresent()),
                //Presents 1, 2, 3
                _ => new()
                {
                    new ItemIngredient(602, 1, 1, 0.0666f),
                    new ItemIngredient(586, 20, 49, 0.168f),
                    new ItemIngredient(591, 20, 49, 0.168f)
                },
            };
        }

        public void GetIngredients(RecipeIngredients ingredients)
        {
            ingredients.SetInput(BagInput);
            ingredients.SetOutputs(BagOutputs);
        }
    }
}
