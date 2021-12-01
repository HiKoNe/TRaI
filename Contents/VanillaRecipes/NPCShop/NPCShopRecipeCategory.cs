using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using TRaI.APIs;
using TRaI.APIs.Ingredients;
using TRaI.Contents.VanillaRecipes.NPCDrop;
using TRaI.UIs.UIElements;

namespace TRaI.Contents.VanillaRecipes.NPCShop
{
    public class NPCShopRecipeCategory : RecipeCategory
    {
        public override Asset<Texture2D> TextureIcon => TextureAssets.Item[ItemID.PiggyBank];
        public override LocalizedText Name => Language.GetText("Mods.TRaI.NPCShop");
        public override StyleDimension Width => new(0, 1f);
        public override int Height => 228;
        public override bool NeedReinit => true;

        public override void InitRecipes()
        {
            for (int i = 0; i < NPCLoader.NPCCount; i++)
            {
                var npc = new NPC();
                npc.SetDefaults(i);
                if (npc.townNPC || i == NPCID.SkeletonMerchant)
                    Recipes.Add(new NPCShopRecipeElement(i));
            }
        }

        public override void InitElement(UIRecipeLayout layout, IRecipeElement recipeElement, RecipeIngredients ingredients)
        {
            var npcDrop = (NPCShopRecipeElement)recipeElement;

            var npcPanel = new UIPanel();
            npcPanel.Height.Set(0, 1f);
            npcPanel.Width.Set(54 * 4, 0);
            npcPanel.BackgroundColor = new Color(50, 58, 119);
            layout.Append(npcPanel);

            var npcUI = new UINPC(npcDrop.BestiaryEntry);
            npcUI.Height = new(0, 1f);
            npcUI.Width = new(0, 1f);
            npcPanel.Append(npcUI);

            var name = new NamePlateInfoElement(Lang.GetNPCName(npcDrop.NPCID).Key, npcDrop.NPCID);
            npcPanel.Append(name.ProvideUIElement(npcUI.bestiaryUICollectionInfo));

            layout.AddItemIngredients(ingredients.GetOutputs<ItemIngredient>(), false, 54 * 4, 0, 10, 4);
        }
    }
}
