using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectorProMobile.DependencyServices
{
    public enum LineType
    {
        OnlyEnglish = 0,
        OnlyChinese = 1,
        OnlyPinyin = 2,
        ChineseEnglish = 3,
        EmptyOrWhitespace = 4, // detected through empty or whitespace line followed by line containing 副歌
        ChorusTag = 5,
        Unknown = 6
    }
    public static class LyricFormatter
    {
        // This class takes a song's body, detects lines and gives them formatting information.
        // e.g.:
        // He deng de shen jiao, he deng de huan xi,
        // 何等的深交,何等的欢喜, What a fellowship, what a joy divine
        // Turns Into:
        // <2>He deng de shen jiao, he deng de huan xi,
        // <1>何等的深交,何等的欢喜,
        // <0>What a fellowship, what a joy divine
        public static string GetFormattedBody(string body)
        {
            body = StandardizeNewline(body);

            string formattedBody = string.Empty;
            bool hasLoopedOnce = false;
            using (var reader = new StringReader(body))
            {
                string prevLine = null;
                string currLine = null;
                for (string nextLine = reader.ReadLine(); (currLine != null || !hasLoopedOnce); nextLine = reader.ReadLine())
                {
                    hasLoopedOnce = true;
                    if (currLine == null)
                    {
                        currLine = nextLine;
                        continue;
                    }


                    if (formattedBody != string.Empty)
                    {
                        formattedBody += Environment.NewLine;
                    }
                    (LineType, int) res = GetLineType(prevLine, currLine, nextLine);
                    if (res.Item1 == LineType.ChineseEnglish)
                    {
                        formattedBody += GetFormattedChineseEnglishLine(currLine, res.Item2);
                    }
                    else
                    {
                        formattedBody += "<" + (int)res.Item1 + ">" + currLine;
                    }

                    prevLine = currLine;
                    currLine = nextLine;
                }
            }

            return formattedBody;
        }

        private static string StandardizeNewline(string body)
        {
                // Some new lines were showing up as \r\n.
                body = body.Replace("\\r\\n", Environment.NewLine);
                body = body.Replace("\r\n", Environment.NewLine);
                body = body.Replace("\\n\\n", Environment.NewLine);
                body = body.Replace("\\n", Environment.NewLine);
                body = body.Replace("\n", Environment.NewLine);

                return body;
        }

        private static Regex regAlphabat = new Regex(@"[a-zA-Z]", RegexOptions.Compiled);
        private static Regex regChinese = new Regex(@"\p{IsCJKUnifiedIdeographs}", RegexOptions.Compiled);


        private static string GetFormattedChineseEnglishLine(string CEline, int englishStartPosition)
        {
            string formattedCELine = string.Empty;
            string chinesePortion = CEline.Substring(0, englishStartPosition).TrimEnd();
            string englishPortion = CEline.Substring(englishStartPosition);
            formattedCELine += "<1>" + chinesePortion;
            formattedCELine += Environment.NewLine;
            formattedCELine += "<0>" + englishPortion;
            return formattedCELine;
        }

        private static (LineType, int) GetLineType(string prevLine, string currLine, string nextLine) // first item gives line type, second tuple item gives index of first English character if ChineseEnglish type is found. Otherwise, -1 is returned.
        {
            currLine = currLine.TrimStart().TrimEnd();

            if (CheckEmptyOrWhiteSpace(currLine)) return (LineType.EmptyOrWhitespace, -1);
            if (CheckChorusTag(prevLine, currLine)) return (LineType.ChorusTag, -1);
            if (CheckOnlyPinyin(currLine, nextLine)) return (LineType.OnlyPinyin, -1);
            if (CheckOnlyEnglish(currLine)) return (LineType.OnlyEnglish, -1);
            (bool, int) chineseEnglishResult = CheckChineseEnglish(currLine);
            if (chineseEnglishResult.Item1) return (LineType.ChineseEnglish, chineseEnglishResult.Item2);

            return (LineType.Unknown, -1);
        }

        private static bool CheckChorusTag(string prevLine, string currLine)
        {
            if ((prevLine == null) && ((currLine == "CHORUS") || (currLine.Contains("副歌")))) return true;
            if (string.IsNullOrEmpty(prevLine) && ((currLine == "CHORUS") || currLine.Contains("副歌"))) return true;
            return false;
        }

        private static bool CheckEmptyOrWhiteSpace(string currLine)
        {
            return string.IsNullOrEmpty(currLine);
        }
        private static (bool, int) CheckChineseEnglish(string currLine)
        {
            Match matchEnglish = regAlphabat.Match(currLine);
            Match matchChinese = regChinese.Match(currLine);

            if (matchEnglish.Success && matchChinese.Success)
            {
                return (true, matchEnglish.Index);
            }
            else
            {
                return (false, -1);
            }
        }
        private static bool CheckOnlyEnglish(string currLine)
        {
            return !regChinese.Match(currLine).Success;
        }

        private static bool CheckOnlyPinyin(string currLine, string nextLine)
        {
            // current line only has ASCII characters
            // next line must not be empty and must have atleast some chinese
            if (nextLine == null) return false;

            bool currLineOnlyAscii = !regChinese.Match(currLine).Success;
            bool nextLineNotEmpty = !CheckEmptyOrWhiteSpace(nextLine);

            bool nextLineHaveSomeNonAscii = regChinese.Match(nextLine).Success;
            return currLineOnlyAscii && nextLineNotEmpty && nextLineHaveSomeNonAscii;
        }
    }

    public class UnkownLineTypeException : Exception
    {
        public UnkownLineTypeException() { }
        public UnkownLineTypeException(string message) : base(message) { }
        public UnkownLineTypeException(string message, Exception inner) : base(message, inner) { }
    }
}
