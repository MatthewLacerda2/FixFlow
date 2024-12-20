import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import 'screen/auth/initial_screen.dart';
import 'screen/main/main_screen.dart';
import 'utils/flow_storage.dart';
import 'utils/login_utils.dart';

void main() async {
  runApp(const FlowApp());
}

class FlowApp extends StatelessWidget {
  const FlowApp({super.key});

  @override
  Widget build(BuildContext context) {
    SystemChrome.setPreferredOrientations(
        <DeviceOrientation>[DeviceOrientation.portraitUp]);

    return MaterialApp(
      title: 'Revisit',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: FutureBuilder<bool>(
        future: checkIfLoggedIn(),
        builder: (BuildContext context, AsyncSnapshot<bool> snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          } else if (snapshot.hasData && snapshot.data == true) {
            return const MainScreen(
              initialIndex: 0,
            );
          } else {
            return const InitialScreen();
          }
        },
      ),
    );
  }

  Future<bool> checkIfLoggedIn() async {
    final String token = await FlowStorage.getToken();

    if (token.isEmpty) {
      return false;
    }

    final BusinessDTO? fetchedDTO = await LoginUtils.fetchBusinessDTO();
    if (fetchedDTO == null) {
      return false;
    }

    return true;
  }
}
