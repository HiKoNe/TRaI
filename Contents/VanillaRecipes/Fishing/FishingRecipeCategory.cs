//using Microsoft.Xna.Framework.Graphics;
//using ReLogic.Content;
//using Terraria;
//using Terraria.DataStructures;
//using Terraria.GameContent;
//using Terraria.ID;
//using Terraria.Localization;
//using Terraria.UI;
//using TRaI.APIs;
//using TRaI.UIs.UIElements;

//namespace TRaI.Contents.VanillaRecipes.Fishing
//{
//    public class FishingRecipeCategory : RecipeCategory
//    {
//        public override Asset<Texture2D> TextureIcon => TextureAssets.Item[ItemID.WoodFishingPole];
//        public override LocalizedText Name => Language.GetText("Mods.TRaI.Fishing");
//        public override StyleDimension Width => new(0, 1f);
//        public override int Height => 228;

//        public override void InitRecipes()
//        {
//            FishingAttempt fisher = default;
//            fisher.CanFishInLava = true;
//            fisher.fishingLevel = 100;
//            fisher.waterTilesCount = 1000;
//            fisher.waterNeededToFish = 0;
//            fisher.heightLevel = 0;

//            //LootDropEmulation.SetLoadingName(Name.Value);
//            //var list = new List<int>();
//            //foreach (var item in TRaI.AllItems)
//            //    if (ItemID.Sets.ExtractinatorMode[item.type] > -1)
//            //        list.Add(item.type);

//            //for (int i = 0; i < list.Count; i++)
//            //{
//            //    LootDropEmulation.SetLoadingProgress(i / (float)list.Count);
//            //    Recipes.Add(new FishingRecipeElement(list[i]));
//            //}
//        }

//        public override void InitElement(UIRecipeLayout layout, IRecipeElement recipeElement)
//        {
//            var fishingRecipe = (FishingRecipeElement)recipeElement;

//            //var ingredient = layout.AddIngredient(extractinatorRecipe.RecipeIngredients.Inputs[0], true, 0, 0);
//            //ingredient.HAlign = 0.5f;

//            //var image = layout.AddImage(TRaIAsset.Content[2], 0, 54);
//            //image.HAlign = 0.5f;

//            //layout.AddIngredients(extractinatorRecipe.RecipeIngredients.Outputs, false, 0, 54 * 2, 14, 2);
//        }
//    }
//}
