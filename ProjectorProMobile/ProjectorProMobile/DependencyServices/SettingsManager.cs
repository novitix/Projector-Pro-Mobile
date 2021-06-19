using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjectorProMobile.DependencyServices
{

    public static class SettingsManager
    {
        private static readonly int defaultFontSize = 18;
        static Dictionary<string, string> defaultSettings = new Dictionary<string, string>()
        {
            ["SessionCode"] = "0000",
            ["HostStatus"] = SessionManager.HostStatus.Solo.ToString(),
            ["ServerAddress"] = "www.ppmserver.tk",
            ["DarkMode"] = "True",
            ["LyricOverride"] = "True",

            ["EnglishShow"] = "True",
            ["ChineseShow"] = "True",
            ["PinyinShow"] = "True",
            ["ChorusShow"] = "True", // c
            ["EmptyShow"] = "True", // c

            ["EnglishColour"] = "#fff8f5",
            ["ChineseColour"] = "#cccccc",
            ["PinyinColour"] = "#2b2421",
            ["ChorusColour"] = "#262830", // c
            ["EmptyColour"] = "#262830", // c

            ["EnglishBold"] = "True",
            ["ChineseBold"] = "True",
            ["PinyinBold"] = "True",
            ["ChorusBold"] = "True", // c
            ["EmptyBold"] = "True", // c

            ["EnglishItalic"] = "False",
            ["ChineseItalic"] = "False",
            ["PinyinItalic"] = "False",
            ["ChorusItalic"] = "False", // c
            ["EmptyItalic"] = "False", // c

            ["EnglishUnderline"] = "False",
            ["ChineseUnderline"] = "False",
            ["PinyinUnderline"] = "False",
            ["ChorusUnderline"] = "False", // c
            ["EmptyUnderline"] = "False", // c

            ["EnglishFontSize"] = defaultFontSize.ToString(),
            ["ChineseFontSize"] = defaultFontSize.ToString(),
            ["PinyinFontSize"] = defaultFontSize.ToString(),
            ["ChorusFontSize"] = defaultFontSize.ToString(), // c
            ["EmptyFontSize"] = 1.ToString(), // c
            ["FontFamily"] = "Roboto",
            ["HttpTimeout"] = 10.ToString(),
            ["SearchDelay"] = 200.ToString()
        };

        /// <summary>
        /// Updates the property with the new value.
        /// </summary>
        public static void Set(string property, string value)
        {
            Set(new Setting(property, value));
        }
        public static void Set(Setting setting)
        {
            if (defaultSettings.ContainsKey(setting.Property))
            {
                Preferences.Set(setting.Property.ToString(), setting.Value);
                FormattedStringBuilder.ClearFontCache();
            }
            else
            {
                throw new SettingNotFoundException(setting.Property);
            }
            
        }

        /// <summary>
        /// Returns the value related to the property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string Get(string property)
        {
            if (defaultSettings.ContainsKey(property))
            {
                if (!Preferences.ContainsKey(property))
                {
                    Preferences.Set(property, defaultSettings[property]);
                }
                return Preferences.Get(property, defaultSettings[property]);
            }
            else
            {
                throw new SettingNotFoundException(property);
            }
        }


    }

    class SettingNotFoundException : Exception
    {
        public SettingNotFoundException()
        {
        }

        public SettingNotFoundException(string propertyName) : base("Property '" + propertyName + "' was not found.")
        {
        }

        public SettingNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SettingNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class Setting
    {
        public string Property { get; set; }
        public string Value { get; set; }
        public string DefaultProperty { get; set; }

        public Setting(string property, string value)
        {
            this.Property = property;
            this.Value = value;
        }
        public Setting(string property, string value, string defaultProperty)
        {
            this.Property = property;
            this.Value = value;
            this.DefaultProperty = defaultProperty;
        }
    }
}
