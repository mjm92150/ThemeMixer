﻿using ColossalFramework.Packaging;
using ColossalFramework.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ThemeMixer.Resources
{
    public class ThemeSprites
    {
        private static UITextureAtlas _atlas;
        public static UITextureAtlas Atlas {
            get {
                if (_atlas == null) _atlas = CreateAtlas();
                return _atlas;
            }
            private set => _atlas = value;
        }

        private static List<string> SpriteNames { get; } = new List<string>();
        private static List<Texture2D> SpriteTextures { get; } = new List<Texture2D>();

        public const string SteamPreview = "SteamPreview";
        public const string SnapShot = "Snapshot";

        public const string GrassDiffuseTexture = "GrassDiffuseTexture";
        public const string RuinedDiffuseTexture = "RuinedDiffuseTexture";
        public const string PavementDiffuseTexture = "PavementDiffuseTexture";
        public const string GravelDiffuseTexture = "GravelDiffuseTexture";
        public const string CliffDiffuseTexture = "CliffDiffuseTexture";
        public const string OilDiffuseTexture = "OilDiffuseTexture";
        public const string OreDiffuseTexture = "OreDiffuseTexture";
        public const string SandDiffuseTexture = "SandDiffuseTexture";
        public const string CliffSandNormalTexture = "CliffSandNormalTexture";

        public const string WaterFoam = "WaterFoam";
        public const string WaterNormal = "WaterNormal";

        public const string UpwardRoadDiffuse = "UpwardRoadDiffuse";
        public const string DownwardRoadDiffuse = "DownwardRoadDiffuse";
        public const string FloorDiffuse = "FloorDiffuse";
        public const string BaseDiffuse = "BaseDiffuse";
        public const string BaseNormal = "BaseNormal";
        public const string BurntDiffuse = "BurntDiffuse";
        public const string AbandonedDiffuse = "AbandonedDiffuse";
        public const string LightColorPalette = "LightColorPalette";

        public const string MoonTexture = "MoonTexture";

        private static string[] AssetNames { get; } = {
            SteamPreview,
            SnapShot,
            GrassDiffuseTexture,
            RuinedDiffuseTexture,
            PavementDiffuseTexture,
            GravelDiffuseTexture,
            CliffDiffuseTexture,
            OilDiffuseTexture,
            OreDiffuseTexture,
            SandDiffuseTexture,
            CliffSandNormalTexture,
            WaterFoam,
            WaterNormal,
            UpwardRoadDiffuse,
            DownwardRoadDiffuse,
            FloorDiffuse,
            BaseDiffuse,
            BaseNormal,
            BurntDiffuse,
            AbandonedDiffuse,
            LightColorPalette,
            MoonTexture
        };

        private static UITextureAtlas CreateAtlas() {
            SpriteNames.Clear();
            SpriteTextures.Clear();
            var themeAssets = PackageManager.FilterAssets(UserAssetType.MapThemeMetaData);
            foreach (Package.Asset themeAsset in themeAssets) {
                if (themeAsset == null || themeAsset.package == null) continue;
                var meta = themeAsset.Instantiate<MapThemeMetaData>();
                if (meta == null) continue;
                for (var i = 0; i < AssetNames.Length; i++) {
                    string assetName = i < 2 ? string.Concat(meta.name, "_", AssetNames[i]) : AssetNames[i];
                    string spriteName = string.Concat(themeAsset.fullName, assetName);
                    spriteName = Regex.Replace(spriteName, @"(\s+|@|&|'|\(|\)|<|>|#|"")", "");

                    var tex = themeAsset.package.Find(assetName)?.Instantiate<Texture2D>();
                    if (tex == null) continue;
                    Texture2D spriteTex = tex.ScaledCopy(64.0f / tex.height);
                    Object.Destroy(tex);
                    spriteTex.Apply();
                    SpriteNames.Add(spriteName);
                    SpriteTextures.Add(spriteTex);
                }
            }
            return ResourceUtils.CreateAtlas("ThemesAtlas", SpriteNames.ToArray(), SpriteTextures.ToArray());
        }

        public static void RefreshAtlas()
        {
            Object.Destroy(_atlas);
            _atlas = null;
            Atlas = CreateAtlas();
        }
    }
}