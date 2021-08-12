using System;
using System.Collections.Generic;
using System.Diagnostics;
using HTMLToRTFString.Css;
using HTMLToRTFString.Rtf;

namespace HTMLToRTFString
{
    internal static class HtmlAttributeModifiers
    {
        private const string Color = "color";
        private const string BackgroundColor = "background-color";
        private const string TextAlign = "text-align";
        private const string FontSize = "font-size";
        private const string FontFamily = "font-family";

        private static readonly Dictionary<string, Action<RtfDocumentBuilder, string>> AttributeModifiers = new Dictionary<string, Action<RtfDocumentBuilder, string>>
        {
            { Color, (builder, value) => builder.ForegroundColor(value) },
            { BackgroundColor, (builder, value) => builder.BackgroundColor(value) },
            { TextAlign, (builder, value) =>
                {
                    if (Enum.TryParse(value.Trim().FirstCharToUpper(), out HorizontalAlignment horizontalAlignment))
                    {
                        builder.HorizontalAlignment(horizontalAlignment);
                    }
                }
            },
            { FontSize, (builder, value) => builder.FontSize(new CssSize(value).ToPoints()) },
            { FontFamily, (builder, value) => builder.FontFamily(value) }
        };

        public static void ApplyAttribute(string name, string value, RtfDocumentBuilder builder)
        {
            bool isImplementedFlag = true;
            if (!AttributeModifiers.ContainsKey(name))
            {
                Debug.WriteLine("Element not implemented: " + name);
                isImplementedFlag = false;
            }

            if(isImplementedFlag)
            AttributeModifiers[name].Invoke(builder, value);
        }
    }
}