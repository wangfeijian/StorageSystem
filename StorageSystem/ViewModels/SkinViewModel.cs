using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using StorageSystem.Common;
using System.Windows.Media;

namespace StorageSystem.ViewModels
{
    public class SkinViewModel : BindableBase
    {
        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (SetProperty(ref _isDarkTheme, value))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? BaseTheme.Dark : BaseTheme.Light));
                    AppSettingsManager.SaveIsDarkMode(value);
                }
            }
        }

        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

        public DelegateCommand<object> ChangeHueCommand { get; private set; }

        private static readonly PaletteHelper paletteHelper = new PaletteHelper();

        public SkinViewModel()
        {
            ChangeHueCommand = new DelegateCommand<object>(ChangeHue);
            IsDarkTheme = AppSettingsManager.IsDarkMode;
        }

        public static void ModifyTheme(Action<Theme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            Theme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }

        public static void ChangeHue(object obj)
        {
            var hue = (Color)obj!;
            paletteHelper.ChangePrimaryColor(hue);
            AppSettingsManager.SaveColor(obj);
        }

    }

    public static class PaletteHelperExtensions
    {
        public static void ChangePrimaryColor(this PaletteHelper paletteHelper, Color color)
        {
            Theme theme = paletteHelper.GetTheme();

            theme.PrimaryLight = new ColorPair(color.Lighten());
            theme.PrimaryMid = new ColorPair(color);
            theme.PrimaryDark = new ColorPair(color.Darken());

            paletteHelper.SetTheme(theme);
        }

        public static void ChangeSecondaryColor(this PaletteHelper paletteHelper, Color color)
        {
            Theme theme = paletteHelper.GetTheme();

            theme.SecondaryLight = new ColorPair(color.Lighten());
            theme.SecondaryMid = new ColorPair(color);
            theme.SecondaryDark = new ColorPair(color.Darken());

            paletteHelper.SetTheme(theme);
        }
    }
}
