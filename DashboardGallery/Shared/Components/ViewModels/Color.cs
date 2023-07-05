using Core.Utilities.Ensures;

namespace DashboardGallery.ViewModels
{
    public class Color
    {
        private readonly string value;
        private Color(string value)
        {
            Ensure.That(value, nameof(value)).IsHexadecimalColor();
            this.value = value;
        }
        #region Constructor name
        // Colores básicos
        public static Color Black => new ("#000000");
        public static Color White => new ("#FFFFFF");
        public static Color Red => new ("#FF0000");
        public static Color Green => new ("#00FF00");
        public static Color Blue => new ("#0000FF");
        public static Color Yellow => new ("#FFFF00");
        public static Color Cyan => new ("#00FFFF");
        public static Color Magenta => new ("#FF00FF");

        // Colores adicionales
        public static Color Orange => new ("#FFA500");
        public static Color Purple => new ("#800080");
        public static Color Pink => new ("#FFC0CB");
        public static Color Gray => new ("#808080");
        public static Color Brown => new ("#A52A2A");
        public static Color Gold => new ("#FFD700");
        public static Color Silver => new ("#C0C0C0");
        public static Color Olive => new ("#808000");
        public static Color Teal => new ("#008080");
        public static Color Indigo => new ("#4B0082");
        public static Color Coral => new ("#FF7F50");
        public static Color Lavender => new ("#E6E6FA");
        public static Color DarkGoldenrod => new ("#B8860B");
        public static Color MidnightBlue => new ("#191970");

        // Colores de la paleta web-safe
        public static Color WebSafeDarkCyan => new ("#008B8B");
        public static Color WebSafeDarkMagenta => new ("#8B008B");
        public static Color WebSafeOlive => new("#808000");
        public static Color WebSafeTeal => new("#008080");
        public static Color WebSafeMaroon => new("#800000");
        public static Color WebSafeNavy => new("#000080");
        public static Color WebSafePurple => new("#800080");
        public static Color WebSafeSilver => new("#C0C0C0");

        // Colores "light"
        public static Color LightGray => new ("#D3D3D3");
        public static Color LightBlue => new ("#ADD8E6");
        public static Color LightGreen => new ("#90EE90");
        public static Color LightPink => new ("#FFB6C1");
        public static Color LightYellow => new ("#FFFFE0");
        public static Color LightCyan => new ("#E0FFFF");
        public static Color LightPurple => new ("#DCD0FF");
        public static Color LightOrange => new ("#FFE4B2");

        // Colores fundamentales
        public static Color Transparent => new ("transparent");
        public static Color MediumGray => new ("#808080");
        public static Color DarkGray => new ("#A9A9A9");
        public static Color PaleVioletRed => new ("#DB7093");
        public static Color LightSeaGreen => new ("#20B2AA");
        public static Color DarkSlateBlue => new ("#483D8B");
        public static Color Chocolate => new ("#D2691E");

        // Colores adicionales
        public static Color DeepSkyBlue => new ("#00BFFF");
        public static Color Lime => new ("#00FF00");
        public static Color Tomato => new ("#FF6347");
        public static Color SlateBlue => new ("#6A5ACD");
        public static Color SteelBlue => new ("#4682B4");
        public static Color ForestGreen => new ("#228B22");
        public static Color Sienna => new ("#A0522D");
        public static Color Orchid => new ("#DA70D6");

        // Colores pastel
        public static Color PastelPink => new ("#FFD1DC");
        public static Color PastelGreen => new ("#77DD77");
        public static Color PastelBlue => new ("#AEC6CF");
        public static Color PastelPurple => new ("#B39EB5");
        public static Color PastelYellow => new ("#FDFD96");
        public static Color PastelOrange => new ("#FFB347");

        // Colores neutros
        public static Color Beige => new ("#F5F5DC");
        public static Color Ivory => new ("#FFFFF0");
        public static Color Taupe => new ("#483C32");
        public static Color SlateGray => new ("#708090");
        public static Color Pewter => new ("#8D8989");
        public static Color Charcoal => new ("#36454F");


        // Colores vibrantes
        public static Color HotPink => new ("#FF69B4");
        public static Color LimeGreen => new ("#32CD32");
        public static Color ElectricBlue => new ("#7DF9FF");
        public static Color CoralRed => new ("#FF4040");
        public static Color NeonYellow => new ("#FFFF00");
        public static Color ElectricPurple => new ("#BF00FF");

        #endregion

        #region Templates 

        public static Color Primary => new ("#295F66");
        public static Color Success => new ("#59BC4D");
        public static Color Alert => new("#E81A00");
        #endregion

        #region operators
        public static implicit operator Color(string value)
        {
            return new Color(value);
        }

        public static explicit operator string(Color color)
        {
            return color.value;
        }
        #endregion
        public static Color FromHex(string value)
        {
            return new Color(value);
        }

        public override string ToString()
        {
            return value;
        }
    }
}
