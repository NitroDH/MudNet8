using MudBlazor;

namespace MudNet8.Shared
{
    public class ThemeStateProvider
    {
        private bool _systemDarkMode;

        public MudThemeProvider MudThemeProvider { get; set; } = new();
        public MudTheme MudTheme { get; set; } = new()
        {
            Palette = new PaletteLight()
            {
                AppbarBackground = Colors.Blue.Default,
                Primary = Colors.Blue.Default,
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.Blue.Default,
            },
            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "265px"
            }
        };
        public ThemeMode ActiveMode { get; set; } = ThemeModes.First();
        private static List<ThemeMode> ThemeModes =>
        [
            new ThemeMode { Name = "DarkMode", IsDark = true, Icon = Icons.Material.Outlined.DarkMode, ToolTip = "Switch to Light Mode" },
            new ThemeMode { Name = "LightMode", IsDark = false, Icon = Icons.Material.Outlined.LightMode, ToolTip = "Switch to Dark Mode" }
        ];

        public bool NavBarOpen = true;

        public event Action? NavBarStateChanged;
        public event Action? NavAsideStateChanged;
        public event Action? MainLayoutStateChanged;

        public async Task OnAfterMainLayoutRender(bool firstRender)
        {
            if (firstRender)
            {
                _systemDarkMode = await MudThemeProvider.GetSystemPreference();
                SetActiveThemeMode(true);
            }
        }

        public void SetActiveThemeMode(bool fromAuto = false)
        {
            if (fromAuto)
            {
                ActiveMode = ThemeModes.First(x => x.Name == "DarkMode");
                ActiveMode.IsDark = _systemDarkMode;
            }
            else
            {
                ActiveMode = ActiveMode.Name switch
                {
                    "DarkMode" => ThemeModes.First(x => x.Name == "LightMode"),
                    "LightMode" or _ => ThemeModes.First(x => x.Name == "DarkMode")
                };
            }

            NotifyMainLayoutChanged();
            NotifyNavBarStateChanged();
            NotifyNavAsideStateChanged();
        }

        public void ToggleNavAside()
        {
            NavBarOpen = !NavBarOpen;
            NotifyNavAsideStateChanged();
        }

        private void NotifyMainLayoutChanged() => MainLayoutStateChanged?.Invoke();
        private void NotifyNavBarStateChanged() => NavBarStateChanged?.Invoke();
        private void NotifyNavAsideStateChanged() => NavAsideStateChanged?.Invoke();

    }

    public class ThemeMode
    {
        public string? Name { get; set; }
        public bool IsDark { get; set; }
        public string? Icon { get; set; }
        public string? ToolTip { get; set; }
    }
}
