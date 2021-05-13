using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Essentials;

namespace ProjectorProMobile.DependencyServices
{

    static class SettingsManager
    {
        private static readonly int defaultFontSize = 18;
        static Dictionary<string, string> defaultSettings = new Dictionary<string, string>()
        {
            ["SessionCode"] = "0000",
            ["HostStatus"] = SessionManager.HostStatus.Solo.ToString(),
            ["ServerAddress"] = "127.0.0.1:80",
            ["DarkMode"] = "True",
            ["LyricOverride"] = "True",
            ["EnglishShow"] = "True",
            ["ChineseShow"] = "True",
            ["PinyinShow"] = "True",
            ["EnglishColour"] = "#fff8f5",
            ["ChineseColour"] = "#cccccc",
            ["PinyinColour"] = "#2b2421",
            ["EnglishBold"] = "True",
            ["ChineseBold"] = "True",
            ["PinyinBold"] = "True",
            ["EnglishItalic"] = "False",
            ["ChineseItalic"] = "False",
            ["PinyinItalic"] = "False",
            ["EnglishUnderline"] = "False",
            ["ChineseUnderline"] = "False",
            ["PinyinUnderline"] = "False",
            ["EnglishFontSize"] = defaultFontSize.ToString(),
            ["ChineseFontSize"] = defaultFontSize.ToString(),
            ["PinyinFontSize"] = defaultFontSize.ToString()
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

    class Setting
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
