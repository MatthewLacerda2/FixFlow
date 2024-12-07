import 'dart:convert';

import 'package:client_sdk/api.dart';
import 'package:shared_preferences/shared_preferences.dart';

class FlowStorage {
  static String jwtTokenKey = 'jwt-token';
  static String businessDTOKey = 'businessDTO';

  String getJwtTokenKey() {
    return jwtTokenKey;
  }

  static Future<void> saveToken(String token) async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    await prefs.setString(jwtTokenKey, token);
  }

  static Future<String?> getToken() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    return prefs.getString(jwtTokenKey);
  }

  static Future<void> clear() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    prefs.clear();

    await AccountsApi().apiV1AccountsLogoutPost();
  }

  static Future<void> saveBusinessDTO(BusinessDTO businessDTO) async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    await prefs.setString(businessDTOKey, json.encode(businessDTO));
  }

  static Future<BusinessDTO?> getBusinessDTO() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? jsonString = prefs.getString(businessDTOKey);
    if (jsonString != null) {
      return BusinessDTO.fromJson(json.decode(jsonString));
    }
    return null;
  }

  static Future<void> removeBusinessDTO() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    await prefs.remove(businessDTOKey);
  }
}
