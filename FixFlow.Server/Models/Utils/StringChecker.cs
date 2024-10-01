using System.Text.RegularExpressions;

namespace Server.Models.Utils;

public static class StringChecker {

	static Regex uppercaseRegex = new Regex(@"[A-Z]");
	static Regex lowercaseRegex = new Regex(@"[a-z]");
	static Regex fullNameRegex = new Regex(@"^[A-Za-z]+(\s[A-Za-z]+)+$");
	static Regex numberRegex = new Regex(@"[0-9]");
	static Regex specialCharRegex = new Regex(@"[^A-Za-z0-9]");

	public static bool IsPasswordStrong(string password) {

		if (password.Length < 7) {
			return false;
		}

		bool hasUppercase = uppercaseRegex.IsMatch(password);
		bool hasLowercase = lowercaseRegex.IsMatch(password);
		bool hasDigit = numberRegex.IsMatch(password);
		bool hasSpecialChar = specialCharRegex.IsMatch(password);

		return hasUppercase && hasLowercase && hasDigit && hasSpecialChar;
	}

	public static bool IsFullNameValid(string entry) {
		bool hasLength = entry.Length >= 5;
		bool hasSpace = entry.Contains(" ");
		bool isValid = fullNameRegex.IsMatch(entry);

		return hasLength && isValid;
	}

	public static bool isCPFvalid(string entry) {
		if (entry.Length != 14) {
			return false;
		}

		for (int i = 0; i < entry.Length; i++) {
			if (i == 3 || i == 7) {
				if (entry[i] != '.') {
					return false;
				}
			}
			else if (i == 11) {
				if (entry[i] != '-') {
					return false;
				}
			}
			else if (!char.IsDigit(entry[i])) {
				return false;
			}
		}

		return true;
	}
}
