using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HTMLToRTFString.Rtf
{
    internal class RtfDocumentContext
    {
        private readonly List<Color> _colors = new List<Color>();
        private readonly List<string> _fonts = new List<string>();

       
        public int ListLevel { get; set; } = 0;
        public bool IsOrderedList { get; set; }
        public int ListItem { get; internal set; }
        public bool inP { get; internal set; }
        public bool skipPar { get; internal set; }

        public int GetFontNumber(string font)
        {

            //System.Diagnostics.Debug.WriteLine("Checking font: " + font);

            font = font.Replace("\"", "");

            // Only get first font if there are multiple
            int charLocation = font.IndexOf(',', StringComparison.Ordinal);
            if (charLocation > 0)
            {
                font = font.Substring(0, charLocation);
            }

            var index = _fonts.FindIndex(x => x == font);

            if (index >= 0)
            {
               // System.Diagnostics.Debug.WriteLine("Font already exists: " + font);
                return index + 1;
            }

            _fonts.Add(font);

           // System.Diagnostics.Debug.WriteLine("Adding to list of fonts: " + font);
            return _fonts.Count;
        }
        public int GetColorNumber(Color color)
        {
            var index = _colors.FindIndex(x => x.A == color.A && x.B == color.B && x.G == color.G && x.R == color.R);
            if (index >= 0)
            {
                return index + 1;
            }

            _colors.Add(color);
            return _colors.Count;
        }

        public string GetFontTable(string defaultFont = null)
        {
            var value = new StringBuilder();
            value.Append(@"{\fonttbl;");
            if (!string.IsNullOrWhiteSpace(defaultFont))
                value.Append(@"{\f0 " + defaultFont + ";}");
            for (int i = 0; i < _fonts.Count; i++)
                value.Append(@"{\f" + (i + 1) + " " + _fonts[i] + ";}");
            value.Append("}");
            return value.ToString();

        }
        public string GetColorTable()
        {
            var value = new StringBuilder();
            value.Append(@"{\colortbl;");
            foreach (var color in _colors)
                value.Append(@"\red" + color.R + @"\green" + color.G + @"\blue" + color.B + @";");
            value.Append("}");
            return value.ToString();
        }
    }
}