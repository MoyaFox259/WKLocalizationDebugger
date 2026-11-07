using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class VanillaLocalizationScanner : TranslationScanner
    {
        public VanillaLocalizationScanner(TranslationCatalog catalog)
            : base(catalog)
        {
        }

        public override void Scan()
        {
            var textsAssets = Resources.LoadAll<TextAsset>("Localization/en");
            if (textsAssets is null) return;
            Array.ForEach(textsAssets, i => HandleLocalizationTextAsset(i));
        }

        public void HandleLocalizationTextAsset(TextAsset textAsset)
        {
            if (textAsset?.text is null) return;
            var jObject = JObject.Parse(textAsset.text);
            if (
                jObject.TryGetValue("id", out JToken id)
                && jObject.TryGetValue("lines", out JToken lines)
            )
            {
                HandleLocalizationLines(
                    id.ToString(),
                    lines.ToObject<Dictionary<string, string>>()
                );
            }
        }

        public void HandleLocalizationLines(
            string id,
            Dictionary<string, string> lines
        )
        {
            if (id is null || lines is null || lines.Count == 0) return;
            var (groupName, propertyName) = id switch
            {
                "announcements" =>
                    ("AnnouncementSubtitles", "AnnouncementSubtitles"),
                "deathmessages" =>
                    ("DeathTexts", "DeathMessages"),
                "items" =>
                    ("ItemDescriptions", "ItemDescriptions"),
                "mother" =>
                    ("MotherSubtitles", "MotherSubtitles"),
                "recordings" =>
                    ("RecordingSubtitles", "RecordingSubtitles"),
                "roachtext" =>
                    ("Notes", "RoachTexts"),
                "roachtrader" =>
                    ("RoachTraderSubtitles", "RoachTraderSubtitles"),
                "sound" =>
                    ("ClosedCaptionSubtitles", "ClosedCaptionSubtitles"),
                _ =>
                    (null, null)
            };
            if (groupName is null || propertyName is null) return;
            foreach (var line in lines)
            {
                RegisterOriginalText(
                    groupName,
                    propertyName,
                    line.Key,
                    line.Value
                );
            }
        }
    }
}

