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

  static Future<String> getToken() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();

    final String token = prefs.getString(jwtTokenKey) ?? "";

    return token.replaceAll('"', '');
  }

  static Future<void> clear() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    prefs.clear();

    final String mytoken = await FlowStorage.getToken();
    final ApiClient apiClient = FlowStorage.getApiClient(mytoken);

    await AccountsApi(apiClient).apiV1AccountsLogoutPost();
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

  static ApiClient getApiClient(String token) {
    final ApiClient cl = ApiClient();
    cl.addDefaultHeader("Authorization", "Bearer $token");
    return cl;
  }
}
