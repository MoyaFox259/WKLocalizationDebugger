using System;
using System.Linq;
using System.Collections.Generic;
using Imui.Core;
using Imui.Controls;
using Imui.Rendering;
using UnityEngine;

namespace WKLocalizationDebugger.IMGUI.Controls
{
    public class EntriesTable<TEntry>
    {
        public string SearchStringPlaceholder;
        public string SearchString;
        public string LastSearchString;
        public bool IsFiltered;
        public int ColumnCount;
        public int SelectedIndex;
        public int SelectedColumn;
        public List<TEntry> Source;
        public List<int> FilteredIndexes;

        public EntriesTable(
            List<TEntry> source,
            int columnCount,
            string searchStringPlaceholder = "Search"
        )
        {
            Source = source;
            ColumnCount = columnCount;
            SearchStringPlaceholder = searchStringPlaceholder;
            Initialize();
        }

        public virtual void Initialize()
        {
            SearchString = "";
            LastSearchString = "";
            SelectedIndex = 0;
            SelectedColumn = 0;
            UpdateFilteredIndexes();
        }

        public virtual void Draw(ImGui gui)
        {
            DrawInfo(gui);
            DrawSearchBar(gui);
            DrawTable(gui);
        }

        public virtual void DrawInfo(ImGui gui)
        {
            var totalCount = Source?.Count ?? 0;
            var totalText = $"Entry count: {totalCount}";
            if (IsFiltered)
            {
                var matchCount = FilteredIndexes?.Count ?? 0;
                totalText += $", Match count: {matchCount}";
            }
            gui.Text(totalText);
            gui.Text($"Selected cell: ({SelectedIndex}, {SelectedColumn})");
        }

        public virtual void DrawSearchBar(ImGui gui)
        {
            if (
                gui.TextEdit(ref SearchString, hint: SearchStringPlaceholder)
                && SearchString != null
            )
            {
                LastSearchString = SearchString.Trim().ToLower();
                UpdateFilteredIndexes();
            }
        }

        public virtual void DrawTable(ImGui gui)
        {
            if (FilteredIndexes is null || FilteredIndexes.Count == 0)
            {
                DrawNoEntriesInfo(gui);
                return;
            }
            gui.BeginTable(ColumnCount, flags: ImTableFlag.ResizableColumns);
            var textSettings =
                ImText.GetTextSettings(gui, true, ImTextOverflow.Overflow);
            foreach (var index in FilteredIndexes)
            {
                gui.TableNextRow();
                for (var column = 0; column < ColumnCount; column++)
                {
                    gui.TableNextColumn();
                    DrawCell(gui, index, column, textSettings);
                }
            }
            gui.EndTable();
        }

        public virtual void DrawNoEntriesInfo(ImGui gui)
        {
            gui.Text("No entries found.");
        }

        public virtual void DrawCell(
            ImGui gui,
            int index,
            int column,
            ImTextSettings textSettings
        )
        {
            var coloredText = GetCellColoredText(index, column);
            if (
                gui.ClickableText(
                    coloredText.Text,
                    textSettings,
                    coloredText.Color,
                    clipClickableAreaWidth: false
                )
            )
            {
                SelectCell(index, column);
            }
            if (gui.ShouldShowTooltipAtLastControl())
            {
                var coloredTooltipTexts =
                    GetCellColoredTooltipTexts(index, column);
                if (
                    coloredTooltipTexts != null
                    && coloredTooltipTexts.Count > 0
                )
                {
                    gui.Tooltip(coloredTooltipTexts);
                }
            }
        }

        public virtual void SelectCell(int index, int column)
        {
            SelectedIndex = index;
            SelectedColumn = column;
            if (
                SelectedIndex < 0
                || Source is null
                || SelectedIndex >= Source.Count
                || SelectedColumn < 0
                || SelectedColumn >= ColumnCount
            )
            {
                return;
            }
            OnCellSelected(SelectedIndex, SelectedColumn);
        }

        public virtual bool TryGetEntry(int index, out TEntry entry)
        {
            if (Source is null || index < 0 || index >= Source.Count)
            {
                entry = default(TEntry);
                return false;
            }
            entry = Source[index];
            return true;
        }

        public virtual void UpdateFilteredIndexes()
        {
            if (Source is null || Source.Count == 0)
            {
                FilteredIndexes = new();
                IsFiltered = false;
                return;
            }
            if (string.IsNullOrWhiteSpace(LastSearchString))
            {
                FilteredIndexes = Source.Select((_, i) => i).ToList();
                IsFiltered = false;
                return;
            }
            var filteredIndexes = new List<int>();
            for (var index = 0; index < Source.Count; index++)
            {
                for (var column = 0; column < ColumnCount; column++)
                {
                    var text = GetCellColoredText(index, column).Text;
                    if (
                        text != null
                        && text.ToLower().Contains(LastSearchString)
                    )
                    {
                        filteredIndexes.Add(index);
                        break;
                    }
                }
            }
            FilteredIndexes = filteredIndexes;
            IsFiltered = true;
        }

        public virtual void OnCellSelected(int index, int column)
        {
        }

        public virtual (string Text, Color32 Color) GetCellColoredText(
            int index,
            int column
        )
        {
            return ($"Idx: {index}, Col: {column}", ColorPalette.White);
        }

        public virtual List<(string Text, Color32 Color)>
            GetCellColoredTooltipTexts(
            int index,
            int column
        )
        {
            return [($"Idx: {index}, Col: {column}", ColorPalette.White)];
        }
    }
}

