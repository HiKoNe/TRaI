using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader;

namespace TRaI
{
    public static class TRaIAsset
    {
        public static Asset<Texture2D>[] Button { get; set; }
        public static Asset<Texture2D>[] SearchBar { get; set; }
        public static Asset<Texture2D>[] Panel { get; set; }
        public static Asset<Texture2D>[] Content { get; set; }

        public static void Load(Mod mod)
        {
            foreach (var memberInfo in typeof(TRaIAsset).GetMembers(BindingFlags.Public | BindingFlags.Static | BindingFlags.SetProperty))
            {
                if (memberInfo is PropertyInfo propertyInfo)
                {
                    int assetID = 0;
                    var assetList = new List<Asset<Texture2D>>();
                    while (mod.RequestAssetIfExists<Texture2D>($"Assets/{memberInfo.Name}_{assetID++}", out var asset))
                        assetList.Add(asset);
                    propertyInfo.SetValue(null, assetList.ToArray());
                }
            }
        }

        public static void Unload()
        {
            Button = null;
            SearchBar = null;
            Panel = null;
        }
    }
}
