using System;
using System.Collections.Generic;
using System.Drawing;

namespace Admin.ThemeManager
{
    public enum ThemeMode
    {
        Light,
        Dark
    }

    public static class Thememaintainer
    {
        public static event EventHandler ThemeChange;

        static public ColorPallates CurrentTheme { get; private set; }
        static public ThemeMode CurrentThemeMode { get; private set; }

        private static Dictionary<ThemeMode, ColorPallates> themes;

        static Thememaintainer()
        {
            themes = new Dictionary<ThemeMode, ColorPallates>();
            themes[ThemeMode.Light] = new ColorPallates()
            {
                PalatteModeName = ThemeMode.Light,

                // 🔵 Primary colors (buttons, highlights, actions)
                PrimaryI = ColorTranslator.FromHtml("#194a7a"),   // Default button
                PrimaryII = ColorTranslator.FromHtml("#3c668e"),  // Hover
                PrimaryIII = ColorTranslator.FromHtml("#567694"), // Pressed

                // ⚪ Secondary colors (background layers)
                SecondaryI = ColorTranslator.FromHtml("#7593af"),  // Borders / icons
                SecondaryII = ColorTranslator.FromHtml("#2d5e8e"), // Cards / panels
                SecondaryIII = ColorTranslator.FromHtml("#f4f7fb"), // App background

                // 📝 Text colors
                TextPrimary = ColorTranslator.FromHtml("#0f172a"), // Main text
                TextSecondary = ColorTranslator.FromHtml("#64748b"), // Sub text

                // 🚦 Status colors
                Success = ColorTranslator.FromHtml("#22c55e"),
                Warning = ColorTranslator.FromHtml("#f59e0b"),
                Danger = ColorTranslator.FromHtml("#ef4444"),

                ButtonTextColor = Color.White
            };

            // =========================
            // 🌙 DARK THEME
            // =========================
            themes[ThemeMode.Dark] = new ColorPallates()
            {
                PalatteModeName = ThemeMode.Dark,

                // 🔵 Primary colors (brighter for dark UI)
                PrimaryI = ColorTranslator.FromHtml("#60a5fa"),
                PrimaryII = ColorTranslator.FromHtml("#3b82f6"),
                PrimaryIII = ColorTranslator.FromHtml("#2563eb"),

                // ⚫ Background layers
                SecondaryI = ColorTranslator.FromHtml("#1e293b"), // Cards
                SecondaryII = ColorTranslator.FromHtml("#0f172a"), // Main bg
                SecondaryIII = ColorTranslator.FromHtml("#020617"), // Deep bg

                // 📝 Text colors
                TextPrimary = ColorTranslator.FromHtml("#f8fafc"),
                TextSecondary = ColorTranslator.FromHtml("#cbd5e1"),

                // 🚦 Status colors
                Success = ColorTranslator.FromHtml("#4ade80"),
                Warning = ColorTranslator.FromHtml("#fbbf24"),
                Danger = ColorTranslator.FromHtml("#f87171"),

                ButtonTextColor = Color.White
            };

            CurrentThemeMode = ThemeMode.Light;
            CurrentTheme = themes[CurrentThemeMode];
        }

        public static void ToggleTheme()
        {
            CurrentThemeMode = CurrentThemeMode == ThemeMode.Light
                ? ThemeMode.Dark
                : ThemeMode.Light;

            CurrentTheme = themes[CurrentThemeMode];

            ThemeChange?.Invoke(null, EventArgs.Empty);
        }

        public static Color GetHoverColor(Color color)
        {
            int offset = IsLight(color) ? -20 : 20;

            int r = Clamp(color.R + offset);
            int g = Clamp(color.G + offset);
            int b = Clamp(color.B + offset);

            return Color.FromArgb(r, g, b);
        }

        private static bool IsLight(Color color)
        {
            return color.GetBrightness() > 0.5f;
        }

        private static int Clamp(int value)
        {
            return Math.Max(0, Math.Min(255, value));
        }
    }
}