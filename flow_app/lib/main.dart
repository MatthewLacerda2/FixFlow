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
      title: 'Flow',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: FutureBuilder<bool>(
        future: checkIfLoggedIn(),
        builder: (BuildContext context, AsyncSnapshot<bool> snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const CircularProgressIndicator();
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
    final bool loggedIn = await FlowStorage.getToken() != "";

    if (loggedIn) {
      LoginUtils.fetchBusinessDTO();
    }

    return loggedIn;
  }
}
