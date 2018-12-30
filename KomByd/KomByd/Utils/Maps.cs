using KomByd.Enums;
using KomByd.Resources;
using KomByd.Utils.Interfaces;

namespace KomByd.Utils
{
    public class Maps : IMaps
    {
        public string MapErrorTypeToMessage(ErrorType errorType)
        {
            switch (errorType)
            {
                case ErrorType.NoDepartures:
                    return AppResources.Error_NoDepartures;
                default:
                    return AppResources.Error_Unknown;
            }
        }

        public string MapUnicodeCharsToPolishChars(string word)
        {
            word = word.Replace("&oacute;", "ó");
            word = word.Replace("&#243;", "ó");
            word = word.Replace("&#261;", "ą");
            word = word.Replace("&#263;", "ć");
            word = word.Replace("&#281;", "ę");
            word = word.Replace("&#324;", "ń");
            word = word.Replace("&#321;", "Ł");
            word = word.Replace("&#322;", "ł");
            word = word.Replace("&#347;", "ś");
            word = word.Replace("&#379;", "Ż");
            word = word.Replace("&#380;", "ż");
            word = word.Replace("&#377;", "Ź");
            word = word.Replace("&#378;", "ź");
            word = word.Replace("&#346;", "Ś");
            return word;
        }
    }
}