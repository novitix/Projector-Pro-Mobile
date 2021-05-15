using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using ProjectorProMobile.DependencyServices;

namespace ProjectorProMobile.DependencyServices
{
    public class FontData
    {
        public double FontSize { get; set; }
        public FontAttributes FontAttributes { get; set; }
        public TextDecorations TextDecorations { get; set; }
        public Color TextColour { get; set; }
        public string FontFamily { get; set; }

        public FontData(string styleType)
        {
            FontSize = int.Parse(SettingsManager.Get(styleType + "FontSize"));
            bool bold = bool.Parse(SettingsManager.Get(styleType + "Bold"));
            bool italic = bool.Parse(SettingsManager.Get(styleType + "Italic"));
            if (!bold && !italic)
            {
                FontAttributes = FontAttributes.None;
            }
            else if (bold && !italic)
            {
                FontAttributes = FontAttributes.Bold;
            }
            else if (!bold && italic)
            {
                FontAttributes = FontAttributes.Italic;
            }
            else
            {
                FontAttributes = FontAttributes.Bold | FontAttributes.Italic;
            }
            bool underline = bool.Parse(SettingsManager.Get(styleType + "Underline"));
            if (underline)
            {
                TextDecorations = TextDecorations.Underline;
            }
            else
            {
                TextDecorations = TextDecorations.None;
            }
            TextColour = Color.FromHex(SettingsManager.Get(styleType + "Colour"));
            FontFamily = SettingsManager.Get("FontFamily");
        }
    }
}
