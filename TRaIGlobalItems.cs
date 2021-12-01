using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace TRaI
{
    public class TRaIGlobalItems : GlobalItem
    {
        public static TooltipLine AddTooltip { get; set; }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (TRaIConfig.Instance.ShowItemID)
                tooltips[0].text += $" [{item.type}]";

            if (TRaIConfig.Instance.ShowItemPrice)
                tooltips.Add(new TooltipLine(Mod, "SellPrice", $"Sell price: {item.value / 50000f} gold") { overrideColor = Color.Yellow });
            
            if (TRaIConfig.Instance.ShowModName)
                tooltips.Add(new TooltipLine(Mod, "ModName", item.ModItem != null ? item.ModItem.Mod.DisplayName : "Terraria") { overrideColor = new Color(255, 100, 100, 255) });

            if (AddTooltip is not null)
            {
                tooltips.Add(AddTooltip);
                AddTooltip = null;
            }
        }
    }
}
