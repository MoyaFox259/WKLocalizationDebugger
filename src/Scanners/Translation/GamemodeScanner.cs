using System;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class GamemodeScanner : TranslationScanner
    {
        public GamemodeScanner(TranslationCatalog catalog) : base(catalog)
        {
        }

        public override void Scan()
        {
            var database = GetFullAssetDatabase();
            database?.gamemodeAssets?.ForEach(i => HandleGamemode(i));
        }

        public void HandleGamemode(M_Gamemode gamemode)
        {
            if (gamemode is null) return;
            var gamemodeName = gamemode.gamemodeName;
            var capsuleName = gamemode.useCustomCapsuleName
                ? gamemode.capsuleName
                : gamemodeName;
            RegisterOriginalText(
                "Gamemodes",
                "CapsuleNames",
                gamemodeName,
                capsuleName
            );
            RegisterOriginalText(
                "Gamemodes",
                "GamemodeUnlockHints",
                gamemode.unlockHint
            );
            RegisterOriginalText(
                "Gamemodes",
                "GamemodeDescriptions",
                gamemode.modeDescription
            );
            RegisterOriginalText(
                "Gamemodes",
                "NewGameTexts",
                gamemode.newGameText
            );
            RegisterOriginalText(
                "Gamemodes",
                "GamemodeIntroTexts",
                gamemode.introText
            );
            HandleGamemodeModule(gamemode.gamemodeModule);
            gamemode.gamemodeSettings?.ForEach(i => HandleGamemodeSetting(i));
        }

        public void HandleGamemodeModule(GamemodeModule gamemodeModule)
        {
            if (gamemodeModule is GamemodeModule_Challenge ch)
            {
                var medals = ch.medals;
                if (medals is null || medals.Count == 0) return;
                foreach (var medal in medals)
                {
                    if (medal is null) continue;
                    RegisterOriginalText(
                        "Gamemodes",
                        "MedalTitles",
                        medal.medalTitle + ch.medalAppend
                    );
                }
            }
        }

        public void HandleGamemodeSetting(GamemodeSetting gamemodeSetting)
        {
            if (gamemodeSetting is null) return;
            RegisterOriginalText(
                "Gamemodes",
                "ModifierTitles",
                gamemodeSetting.title
            );
            RegisterOriginalText(
                "Gamemodes",
                "ModifierDescriptions",
                gamemodeSetting.description
            );
            RegisterOriginalText(
                "Gamemodes",
                "ModifierUnlockDescriptions",
                "-" + gamemodeSetting.nameAppend
            );
        }
    }
}

