using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace ProjectorProMobile.DependencyServices
{
    public class LyricStyler : INotifyPropertyChanged
    {
        ResourceDictionary _resDict;
        public LyricStyler(ResourceDictionary resDict)
        {
            _resDict = resDict;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        FormattedString _formattedLyrics;
        public FormattedString FormattedLyrics
        {
            set
            {
                if (_formattedLyrics != value)
                {
                    _formattedLyrics = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("FormattedLyrics"));
                    }
                }
            }
            get
            {
                return _formattedLyrics;
            }
        }

        public void FormatAndSet(string content)
        {
            FormattedStringBuilder builder = new FormattedStringBuilder(_resDict);

            using (var reader = new StringReader(content))
            {
                for (string line = reader.ReadLine(); (line != null); line = reader.ReadLine())
                {
                    if (!int.TryParse(line.Substring(1, 1), out int tag)) continue;
                    LineType lineType = (LineType)tag;
                    string styleName;
                    switch (lineType)
                    {
                        case LineType.OnlyEnglish:
                            styleName = "englishStyle";
                            break;
                        case LineType.OnlyChinese:
                            styleName = "chineseStyle";
                            break;
                        case LineType.OnlyPinyin:
                            styleName = "pinyinStyle";
                            break;
                        case LineType.EmptyOrWhitespace:
                            styleName = "emptyStyle";
                            break;
                        case LineType.ChorusTag:
                            styleName = "chorusStyle";
                            break;
                        default:
                            styleName = "unknownStyle";
                            break;
                    }
                    if (lineType == LineType.EmptyOrWhitespace)
                    {
                        builder.AddSpan(Environment.NewLine, styleName);
                    }
                    else
                    {
                        builder.AddSpan(line.Substring(3), styleName);
                    }
                    
                }
            }

            //builder.WithoutNewLine();

            FormattedString formattedString = builder.Build();
            FormattedLyrics = formattedString;
        }
    }
}
