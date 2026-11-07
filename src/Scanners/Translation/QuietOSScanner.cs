using System;
using WKLocalizationDebugger.Localization;

namespace WKLocalizationDebugger.Scanners
{
    public class QuietOSScanner : TranslationScanner
    {
        public QuietOSScanner(TranslationCatalog catalog) : base(catalog)
        {
        }

        public override void Scan()
        {
            Scan<OS_Computer_Interface>(o => HandleComputerInterface(o));
            var database = GetFullAssetDatabase();
            database?.diskData?.ForEach(i => HandleDiskData(i));
        }

        public void HandleComputerInterface(
            OS_Computer_Interface computerInterface
        )
        {
            if (computerInterface is null) return;
            RegisterOriginalText(
                "QuietOS",
                "StationIDs",
                computerInterface.computerID
            );
        }

        public void HandleDiskData(OS_DiskData diskData)
        {
            if (diskData is null) return;
            RegisterOriginalText("QuietOS", "FolderNames", diskData.diskName);
            if (diskData.type == OS_DiskData.DiskDataType.diskDataList)
            {
                if (diskData.chosenDisk != null)
                {
                    HandleDiskData(diskData.chosenDisk);
                    return;
                }
                diskData.randomDisks?.ForEach(i => HandleDiskData(i));
                return;
            }
            if (!diskData.useRandomSelector)
            {
                diskData.files?.ForEach(i => HandleFileInfo(i));
                return;
            }
            var diskDataSelectors = diskData.diskDataSelectors;
            if (diskData.selectedDisk == -1)
            {
                diskDataSelectors?.ForEach(i => HandleDiskDataSelector(i));
                return;
            }
            HandleDiskDataSelector(diskDataSelectors?[diskData.selectedDisk]);
        }

        public void HandleDiskDataSelector(
            OS_DiskData.DiskDataSelector diskDataSelector
        )
        {
            if (diskDataSelector is null) return;
            diskDataSelector.files?.ForEach(i => HandleFileInfo(i));
        }

        public void HandleFileInfo(OS_Filesystem.FileInfo fileInfo)
        {
            if (fileInfo is null) return;
            fileInfo.children?.ForEach(i => HandleFileInfo(i));
            if (fileInfo.type == OS_Filesystem.FileInfo.fileType.folder)
            {
                RegisterOriginalText("QuietOS", "FolderNames", fileInfo.name);
                return;
            }
            RegisterOriginalText("QuietOS", "FileNames", fileInfo.name);
            if (
                fileInfo.type == OS_Filesystem.FileInfo.fileType.text
                || fileInfo.type == OS_Filesystem.FileInfo.fileType.audio
            )
            {
                var text = fileInfo.textAssetData != null
                    ? fileInfo.textAssetData.text
                    : fileInfo.data;
                RegisterOriginalText("Documents", "DocumentTexts", text);
            }
        }
    }
}

