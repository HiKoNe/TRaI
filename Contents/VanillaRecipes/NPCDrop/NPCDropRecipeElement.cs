using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using TRaI.APIs;
using TRaI.APIs.Ingredients;

namespace TRaI.Contents.VanillaRecipes.ByHand
{
    public class NPCDropRecipeElement : IRecipeElement
    {
        public int NPCID { get; set; }
        public BestiaryEntry BestiaryEntry { get; set; }
        public List<ItemIngredient> NPCOutputs { get; set; }

        public Mod Mod => NPCLoader.GetNPC(NPCID)?.Mod;

        public NPCDropRecipeElement(int npcID)
        {
            NPCID = npcID;
            BestiaryEntry = Main.BestiaryDB.FindEntryByNPCID(npcID);
            NPCOutputs = new();

            //Loots
            foreach (var dropRule in Main.ItemDropsDB.GetRulesForNPCID(npcID))
            {
                List<DropRateInfo> drops = new();
                dropRule.ReportDroprates(drops, new DropRateInfoChainFeed(1f));
                NPCOutputs.AddRange(drops.Select(d => new ItemIngredient(d.itemId, d.stackMin, d.stackMax, d.dropRate, d.conditions?.Select(c => c.GetConditionDescription()).ToList())));
            }

            //Coins
            var npc = new NPC();
            npc.SetDefaults(npcID);
            var coins = new int[]
            {
                Utils.Clamp((int)npc.value % 100 / 1, 0, 99),
                Utils.Clamp((int)npc.value % 10000 / 100, 0, 99),
                Utils.Clamp((int)npc.value % 1000000 / 10000, 0, 99),
                Utils.Clamp((int)npc.value / 1000000, 0, 999)
            };
            for (int i = 0; i < coins.Length; i++)
                if (coins[i] > 0)
                    NPCOutputs.Add(new ItemIngredient(71 + i, coins[i], coins[i], 1f));
        }

        public void GetIngredients(RecipeIngredients ingredients)
        {
            ingredients.SetOutputs(NPCOutputs);
        }
    }
}
