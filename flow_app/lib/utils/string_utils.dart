class StringUtils {
  void debug() {
    print("debug");
  }

  static String normalIfBlank(String? string) {
    return string == null || string.isEmpty ? '-' : string;
  }
}
