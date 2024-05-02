using System.Text.RegularExpressions;

namespace Server.Models.Utils;

public static class StringChecker
{

    static Regex uppercaseRegex = new Regex(@"[A-Z]");
    static Regex lowercaseRegex = new Regex(@"[a-z]");
    static Regex numberRegex = new Regex(@"[0-9]");
    static Regex specialCharRegex = new Regex(@"[^A-Za-z0-9]");
    static Regex noLetterOrSpaceRegex = new Regex(@"^[^A-Za-z\s]+$");

    public static bool IsPasswordStrong(string password)
    {

        bool hasUppercase = uppercaseRegex.IsMatch(password);
        bool hasLowercase = lowercaseRegex.IsMatch(password);
        bool hasDigit = numberRegex.IsMatch(password);
        bool hasSpecialChar = specialCharRegex.IsMatch(password);

        return hasUppercase && hasLowercase && hasDigit && hasSpecialChar;
    }

    public static bool IsFullNameValid(string entry)
    {
        bool containsSpace = entry.Contains(" ");
        bool hasLength = entry.Length > 5;
        bool lettersAndSpacesOnly = uppercaseRegex.IsMatch(entry) || lowercaseRegex.IsMatch(entry);
        bool noNumbers = numberRegex.IsMatch(entry) == false;
        bool noSpecialCharRegex = specialCharRegex.IsMatch(entry) == false;

        return containsSpace && hasLength && lettersAndSpacesOnly && noNumbers && noSpecialCharRegex;
    }

    public static bool isCPFvalid(string entry)
    {
        bool containsSpace = entry.Contains(" ") == false;
        bool hasLength = entry.Length == 14;
        bool noLetters = uppercaseRegex.IsMatch(entry) == false && lowercaseRegex.IsMatch(entry) == false;

        return containsSpace && hasLength && noLetters;
    }
}