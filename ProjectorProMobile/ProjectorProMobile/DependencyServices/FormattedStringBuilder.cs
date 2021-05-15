using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectorProMobile.DependencyServices
{
    public class FormattedStringBuilder
    {
        private static Dictionary<string, FontData> _fontDataCache = new Dictionary<string, FontData>();
        private readonly IList<Span> _spans = new List<Span>();
        private bool _withNewLine = true;

        public FormattedStringBuilder() { }

        public static void ClearFontCache()
        {
            _fontDataCache = new Dictionary<string, FontData>();
        }

        public void AddSpan(string text, Span span)
        {
            span.Text = text;
            _spans.Add(span);
        }

        public void AddSpan(Span span)
        {
            _spans.Add(span);
        }

        public void AddSpan(string text, string styleResource)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException($"'Text' cannot be empty.");
            }

            if (!_fontDataCache.ContainsKey(styleResource))
            {
                _fontDataCache.Add(styleResource, new FontData(styleResource));
            }
            FontData data = _fontDataCache[styleResource];

            _spans.Add(new Span
            {
                Text = text,
                FontAttributes = data.FontAttributes,
                FontFamily = data.FontFamily,
                FontSize = data.FontSize,
                ForegroundColor = data.TextColour,
                TextDecorations = data.TextDecorations

            });
        }

        public void WithoutNewLine()
        {
            _withNewLine = false;
        }

        public FormattedString Build()
        {
            var result = new FormattedString();
            var count = _spans.Count;
            for (var index = 0; index < count; index++)
            {
                var span = _spans[index];
                result.Spans.Add(span);
                if (index < count && _withNewLine)
                {
                    result.Spans.Add(new Span { Text = Environment.NewLine });
                }
            }
            return result;
        }
    }
}
