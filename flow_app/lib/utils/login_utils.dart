import 'package:client_sdk/api.dart';
import 'package:http/src/response.dart';
import 'package:dart_jsonwebtoken/dart_jsonwebtoken.dart';
import 'package:flutter/material.dart';

import 'flow_storage.dart';

class LoginUtils {
  void debug() {}

  static Future<void> Login(String email, String password, BuildContext context,
      Widget nextPage) async {
    final FlowLoginRequest flr =
        FlowLoginRequest(email: email, password: password);

    final Response loginResponse = await AccountsApi()
        .apiV1AccountsPostWithHttpInfo(flowLoginRequest: flr);

    if (loginResponse.statusCode != 200) {
      ScaffoldMessenger.of(context).showSnackBar(
        //TODO: proper response at snackbar
        const SnackBar(
          content: Text('Registration failed. Please check your input.'),
        ),
      );
      return;
    }

    FlowStorage.saveToken(loginResponse.body);

    FetchBusinessDTO();

    Navigator.pushAndRemoveUntil(
      context,
      MaterialPageRoute<void>(
        builder: (BuildContext context) => nextPage,
      ),
      (Route<dynamic> route) => false,
    );
  }

  static Future<void> FetchBusinessDTO() async {
    final String? jwtToken = await FlowStorage.getToken();
    final JWT jwtTokenDecoded = JWT.decode(jwtToken!);

    final String businessId = jwtTokenDecoded.payload['id'];

    final BusinessDTO? businessDTO =
        await BusinessApi().apiV1BusinessGet(businessId: businessId);

    FlowStorage.saveBusinessDTO(businessDTO!);
  }
}
