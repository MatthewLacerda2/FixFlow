import 'package:client_sdk/api.dart';
import 'package:dart_jsonwebtoken/dart_jsonwebtoken.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart';

import 'flow_snack.dart';
import 'flow_storage.dart';

class LoginUtils {
  void debug() {}

  static Future<void> login(String email, String password, BuildContext context,
      Widget nextPage) async {
    final FlowLoginRequest flr =
        FlowLoginRequest(email: email, password: password);

    final Response loginResponse = await AccountsApi()
        .apiV1AccountsPostWithHttpInfo(flowLoginRequest: flr);

    if (loginResponse.statusCode != 200) {
      FlowSnack.show(context, loginResponse.body);
      return;
    }

    FlowStorage.saveToken(loginResponse.body);

    fetchBusinessDTO();

    Navigator.pushAndRemoveUntil(
      context,
      MaterialPageRoute<void>(
        builder: (BuildContext context) => nextPage,
      ),
      (Route<dynamic> route) => false,
    );
  }

  static Future<BusinessDTO?> fetchBusinessDTO() async {
    String? jwtToken = await FlowStorage.getToken();
    jwtToken = jwtToken!.replaceAll('"', '');
    final JWT jwtTokenDecoded = JWT.decode(jwtToken);
    final Map<String, dynamic> payload =
        jwtTokenDecoded.payload as Map<String, dynamic>;
    final String businessId = payload['businessId'] as String;

    final BusinessDTO? businessDTO =
        await BusinessApi().apiV1BusinessGet(businessId: businessId);

    FlowStorage.saveBusinessDTO(businessDTO!);

    return businessDTO;
  }
}
