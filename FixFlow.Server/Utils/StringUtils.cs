using System.Globalization;

namespace Server.Models.Utils;

public static class StringUtils {

	public static string NameCaseNormalizer(string name) {
		if (string.IsNullOrEmpty(name)) {
			return name;
		}
		name.Trim();
		// Split the name into parts and capitalize the first letter of each part
		var nameParts = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
		var normalizedParts = nameParts.Select(part =>
			CultureInfo.CurrentCulture.TextInfo.ToTitleCase(part.ToLowerInvariant())
		).ToArray();

		return string.Join(" ", normalizedParts);
	}

	//Puts the first letter and letters after a dot uppercase
	public static string? PhraseCaseNormalizer(string? phrase) {
		if (string.IsNullOrEmpty(phrase)) {
			return phrase;
		}

		phrase!.Trim();

		// Trim any leading spaces and make the first letter uppercase
		phrase = char.ToUpper(phrase[0]) + phrase.Substring(1).ToLower();

		// Iterate through the string and make characters after dots uppercase
		for (int i = 1; i < phrase.Length; i++) {
			if (phrase[i - 1] == '.') {
				phrase = phrase.Substring(0, i) + char.ToUpper(phrase[i]) + phrase.Substring(i + 1);
			}
		}

		return phrase;
	}
}
