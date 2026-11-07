using System;
using System.Collections.Generic;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class ItemScanner : TranslationScanner
    {
        public ItemScanner(TranslationCatalog catalog) : base(catalog)
        {
        }

        public override void Scan()
        {
            Scan<Item_Object>(o => HandleItemObject(o));
            Scan<UT_SpawnEntityByID>(o => HandleSpawner(o));
            Scan<SpawnTable>(o => HandleSpawnTable(o));
        }

        public void HandleItemObject(Item_Object itemObject)
        {
            if (itemObject is null) return;
            HandleItem(itemObject.itemData);
        }

        public void HandleSpawner(UT_SpawnEntityByID spawner)
        {
            if (spawner is null) return;
            HandleItemDataStrings(spawner.entityData);
            // HandleItemGameObject(spawner.objectToSpawn);
        }

        public void HandleSpawnTable(SpawnTable spawnTable)
        {
            if (spawnTable is null) return;
            spawnTable.spawnList?.ForEach(i => HandleSpawnTableAsset(i));
            // var spawnTableAssets = ;
            // if (spawnTableAssets != null && spawnTableAssets.Count > 0)
            // {
            //     foreach (var spawnTableAsset in spawnTableAssets)
            //     {
            //         HandleSpawnTableAsset(spawnTableAsset);
            //     }
            // }
        }

        public void HandleSpawnTableAsset(
            SpawnTable.SpawnTableAsset spawnTableAsset
        )
        {
            if (spawnTableAsset is null) return;
            HandleItemDataStrings(spawnTableAsset.data);
            // var gameObjects = spawnTableAsset.prefabs;
            // if (gameObjects != null && gameObjects.Count > 0)
            // {
            //     foreach (var gameObject in spawnTableAsset.prefabs)
            //     {
            //         HandleItemGameObject(gameObject);
            //     }
            // }
        }

        // public void HandleItemGameObject(GameObject gameObject)
        // {
        //     if (
        //         gameObject != null
        //         && gameObject.TryGetComponent<Item_Object>(
        //             out Item_Object itemObject
        //         )
        //     )
        //     {
        //         HandleItemObject(itemObject);
        //     }
        // }

        public void HandleItem(Item item)
        {
            if (item is null) return;
            HandleItemDataStrings(item.data);
            item.dataModules?.ForEach(i => HandleItemDataModule(i));
        }

        public void HandleItemDataStrings(List<string> dataStrings)
        {
            if (dataStrings is null || dataStrings.Count == 0) return;
            var firstDataString = dataStrings[0];
            if (firstDataString.StartsWith("name:"))
            {
                RegisterOriginalText(
                    "QuietOS",
                    "FolderNames",
                    firstDataString.Split(':')[1]
                );
                dataStrings.ForEach(i => HandleDiskItemDataString(i));
                return;
            }
            dataStrings.ForEach(i => HandleNoteItemDataString(i));
        }

        public void HandleNoteItemDataString(string dataString)
        {
            if (dataString is null || !dataString.StartsWith("text:")) return;
            RegisterOriginalText(
                "Notes",
                "NoteTexts",
                dataString.Substring(5)
            );
        }

        public void HandleDiskItemDataString(string dataString)
        {
            if (dataString is null || !dataString.StartsWith("file:")) return;
            var fileDataSegments = dataString.Substring(5).Split(':');
            if (fileDataSegments.Length < 3) return;
            RegisterOriginalText(
                "QuietOS",
                "FileNames",
                fileDataSegments[1]
            );
            if (fileDataSegments[0] != "text") return;
            RegisterOriginalText(
                "Documents",
                "DocumentTexts",
                fileDataSegments[2]
            );
        }

        public void HandleItemDataModule(Item_Data dataModule)
        {
            if (dataModule is Item_Data_Text_Single st)
            {
                RegisterOriginalText("Notes", "NoteTexts", st.text);
                return;
            }
            if (dataModule is Item_Data_Text_Random rt)
            {
                rt.strings?.ForEach(
                    i => RegisterOriginalText("Notes", "NoteTexts", i)
                );
            }
        }
    }
}

