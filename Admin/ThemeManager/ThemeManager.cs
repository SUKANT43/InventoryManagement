using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.ThemeManager
{
    public enum ThemeMode
    {
        Light,
        Dark
    }

    public static class ThemeManager
    {
        public static event EventHandler ThemeChange;
        public static ColorPallates CurrentTheme { get; set; }
        public static ThemeMode CurrentThemeMode { get; set; }
        private static List<ColorPallates> themes;

        static ThemeManager()
        {
            themes.Add(new ColorPallates()
            {
                PalatteModeName = ThemeMode.Light,

                // Primary (Brand / Actions)
                PrimaryI = ColorTranslator.FromHtml("#194a7a"),
                PrimaryII = ColorTranslator.FromHtml("#3c668e"),
                PrimaryIII = ColorTranslator.FromHtml("#567694"),

                // Secondary (Surfaces)
                SecondaryI = ColorTranslator.FromHtml("#7593af"),  // borders/icons
                SecondaryII = ColorTranslator.FromHtml("#a3b7ca"), // cards
                SecondaryIII = ColorTranslator.FromHtml("#f4f7fb"), // background

                // Text
                TextPrimary = ColorTranslator.FromHtml("#0f172a"),
                TextSecondary = ColorTranslator.FromHtml("#64748b"),

                // States
                Success = ColorTranslator.FromHtml("#22c55e"),
                Warning = ColorTranslator.FromHtml("#f59e0b"),
                Danger = ColorTranslator.FromHtml("#ef4444")
            });

            themes.Add(new ColorPallates()
            {
                PalatteModeName = ThemeMode.Dark,

                // Primary (Brighter for dark UI)
                PrimaryI = ColorTranslator.FromHtml("#60a5fa"),
                PrimaryII = ColorTranslator.FromHtml("#3b82f6"),
                PrimaryIII = ColorTranslator.FromHtml("#2563eb"),

                // Background layers
                SecondaryI = ColorTranslator.FromHtml("#1e293b"), // cards
                SecondaryII = ColorTranslator.FromHtml("#0f172a"), // main bg
                SecondaryIII = ColorTranslator.FromHtml("#020617"), // deep bg

                // Text
                TextPrimary = ColorTranslator.FromHtml("#f8fafc"),
                TextSecondary = ColorTranslator.FromHtml("#cbd5e1"),

                // States
                Success = ColorTranslator.FromHtml("#4ade80"),
                Warning = ColorTranslator.FromHtml("#fbbf24"),
                Danger = ColorTranslator.FromHtml("#f87171")
            });

        }
    }
}
