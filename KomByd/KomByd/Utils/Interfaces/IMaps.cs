using KomByd.Enums;

namespace KomByd.Utils.Interfaces
{
    public interface IMaps
    {
        string MapErrorTypeToMessage(ErrorType errorType);

        string MapUnicodeCharsToPolishChars(string word);
    }
}