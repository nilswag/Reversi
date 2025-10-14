using System.Drawing;

namespace Reversi
{
    public static class Theme
    {
        public static bool IsDarkMode { get; private set; } = true;

        // Dark mode kleuren
        public static Color DarkModeBackground100 = Color.FromArgb(0, 0, 0);
        public static Color DarkModeBackground200 = Color.FromArgb(13, 13, 13);
        public static Color DarkModeBackground300 = Color.FromArgb(26, 26, 26);
        public static Color DarkModeBackground400 = Color.FromArgb(51, 51, 51);
        public static Color DarkModeText100 = Color.FromArgb(255, 255, 255);
        public static Color DarkModeText200 = Color.FromArgb(179, 179, 179);

        public static Color LightModeBackground100 = Color.FromArgb(0, 0, 0);
        public static Color LightModeBackground200 = Color.FromArgb(13, 13, 13);
        public static Color LightModeBackground300 = Color.FromArgb(26, 26, 26);
        public static Color LightModeBackground400 = Color.FromArgb(51, 51, 51);
        public static Color LightModeText100 = Color.FromArgb(255, 255, 255);
        public static Color LightModeText200 = Color.FromArgb(179, 179, 179);

        public static void Toggle(Control control)
        {
            IsDarkMode = !IsDarkMode;
        }
    }
}