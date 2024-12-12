class StringUtils {
  void debug() {
    print("debug");
  }

  static String normalIfBlank(String? string) {
    return string == null || string.isEmpty ? '-' : string;
  }

  static String abreviator(String string, int limit) {
    if (string.length > limit) {
      string = '${string.substring(0, limit)}...';
    }
    return string;
  }
}
