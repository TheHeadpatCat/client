using Dalamud.Interface;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using GagSpeak.Services.Mediator;
using ImGuiNET;
using OtterGui.Widgets;
using System.Linq;
using System.Numerics;

namespace GagSpeak.UI.Components;
public class AchievementTabs : IconTabBarBase<AchievementTabs.SelectedTab>
{
    public enum SelectedTab
    {
        Generic,
        Orders,
        Gags,
        Wardrobe,
        Puppeteer,
        Toybox,
        Hardcore,
        Remotes,
        Secrets
    }

    private readonly UiSharedService _ui;
    public AchievementTabs(UiSharedService uiShared)
    {
        _ui = uiShared;
        AddDrawButton(FontAwesomeIcon.Book, SelectedTab.Generic, "Generic");
        AddDrawButton(FontAwesomeIcon.ClipboardList, SelectedTab.Orders, "Orders");
        AddDrawButton(FontAwesomeIcon.CommentDots, SelectedTab.Gags, "Gags");
        AddDrawButton(FontAwesomeIcon.ToiletPortable, SelectedTab.Wardrobe, "Wardrobe");
        AddDrawButton(FontAwesomeIcon.PersonHarassing, SelectedTab.Puppeteer, "Puppeteer");
        AddDrawButton(FontAwesomeIcon.BoxOpen, SelectedTab.Toybox, "Toybox");
        AddDrawButton(FontAwesomeIcon.Lock, SelectedTab.Hardcore, "Hardcore");
        AddDrawButton(FontAwesomeIcon.Mobile, SelectedTab.Remotes, "Remotes");
        AddDrawButton(FontAwesomeIcon.Vault, SelectedTab.Secrets, "Secrets");
    }

    public override void Draw(float availableWidth)
    {
        if (_tabButtons.Count == 0)
            return;

        var spacing = ImGui.GetStyle().ItemSpacing;
        var buttonX = (availableWidth - (spacing.X * (_tabButtons.Count - 1))) / _tabButtons.Count;
        var buttonY = _ui.GetIconButtonSize(FontAwesomeIcon.Pause).Y;
        var buttonSize = new Vector2(buttonX, buttonY);
        var drawList = ImGui.GetWindowDrawList();
        var btncolor = ImRaii.PushColor(ImGuiCol.Button, ImGui.ColorConvertFloat4ToU32(new(0, 0, 0, 0)));

        ImGuiHelpers.ScaledDummy(spacing.Y / 2f);

        foreach (var tab in _tabButtons)
            DrawTabButton(tab, buttonSize, spacing, drawList);

        // advance to the new line and dispose of the button color.
        ImGui.NewLine();
        btncolor.Dispose();

        ImGuiHelpers.ScaledDummy(3f);
        ImGui.Separator();
    }
}
