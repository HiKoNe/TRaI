//using System.Collections.Generic;
//using Terraria.GameContent.ItemDropRules;
//using TRaI.APIs;

//namespace TRaI.Contents.VanillaRecipes.Fishing
//{
//    public class FishingRecipeElement : IRecipeElement
//    {
//        public List<DropRateInfo> FishingOutputs { get; set; }
        
//        public FishingRecipeElement(int extractinatorItem)
//        {


//            //var extractinatorMode = ItemID.Sets.ExtractinatorMode[extractinatorItem];
//            //var method = typeof(Player).GetMethod("ExtractinatorUse", BindingFlags.NonPublic | BindingFlags.Instance);
//            //ExtractinatorOutputs = LootDropEmulation.Emulate(() => method.Invoke(Main.LocalPlayer, new object[] { extractinatorMode }), 100000);
//        }

//        public RecipeIngredients RecipeIngredients =>
//            new() { Inputs = new(), Outputs = FishingOutputs };
//    }
//}
