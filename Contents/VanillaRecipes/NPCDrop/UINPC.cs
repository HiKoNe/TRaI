using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace TRaI.Contents.VanillaRecipes.NPCDrop
{
    public class UINPC : UIElement
    {
        public BestiaryEntry BestiaryEntry { get; set; }

        public BestiaryUICollectionInfo bestiaryUICollectionInfo = new();
        public EntryIconDrawSettings entryIconDrawSettings = new();

        public UINPC(BestiaryEntry bestiaryEntry)
        {
            BestiaryEntry = bestiaryEntry;

            bestiaryUICollectionInfo.OwnerEntry = BestiaryEntry;
            bestiaryUICollectionInfo.UnlockState = BestiaryEntryUnlockState.CanShowDropsWithDropRates_4;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            try
            {
                var box = GetDimensions().ToRectangle();
                entryIconDrawSettings.IsHovered = IsMouseHovering;
                entryIconDrawSettings.IsPortrait = true;
                entryIconDrawSettings.iconbox = box;
                BestiaryEntry.Icon.Update(bestiaryUICollectionInfo, box, entryIconDrawSettings);
            }
            catch
            {
            }

            //if (BestiaryEntry is not null)
            //{
            //    if (BestiaryEntry.Icon is not null)
            //    {
            //        var box = GetDimensions().ToRectangle();
            //        entryIconDrawSettings.IsHovered = IsMouseHovering;
            //        entryIconDrawSettings.IsPortrait = true;
            //        entryIconDrawSettings.iconbox = box;
            //        BestiaryEntry.Icon.Update(bestiaryUICollectionInfo, box, entryIconDrawSettings);
            //    }
            //}
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            try
            {
                BestiaryEntry.Icon.Draw(bestiaryUICollectionInfo, spriteBatch, entryIconDrawSettings);
            }
            catch
            {
            }

            //if (BestiaryEntry is not null)
            //{
            //    if (BestiaryEntry.Icon is not null)
            //    {
            //        BestiaryEntry.Icon.Draw(bestiaryUICollectionInfo, spriteBatch, entryIconDrawSettings);
            //    }
            //}
        }
    }
}
