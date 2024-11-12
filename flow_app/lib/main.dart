import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:shared_preferences/shared_preferences.dart';

import 'screen/auth/initial_screen.dart';
import 'screen/main/main_screen.dart';
import 'utils/flow_storage.dart';

void main() async {
  runApp(const FlowApp());

  final SharedPreferences prefs = await SharedPreferences.getInstance();
  final String? jwtToken = prefs.getString(FlowStorage.jwtTokenKey);
  if (jwtToken != null) {
    final ApiClient apiClient = ApiClient();
    apiClient.addDefaultHeader("Bearer", jwtToken);
  }
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
        // TODO: Check if the user is logged in
        future: checkIfLoggedIn(),
        builder: (BuildContext context, AsyncSnapshot<bool> snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const CircularProgressIndicator();
          } else if (snapshot.hasData && snapshot.data == true) {
            return const MainScreen();
          } else {
            return const InitialScreen();
          }
        },
      ),
    );
  }

  Future<bool> checkIfLoggedIn() async {
    // TODO: Implement the actual login check logic
    return false;
  }
}
