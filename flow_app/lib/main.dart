import 'package:flutter/material.dart';

import 'screen/auth/initial_screen.dart';
import 'screen/main/main_screen.dart';

void main() {
  runApp(FlowApp());
}

class FlowApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flow',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: FutureBuilder(
        // TODO: Check if the user is logged in
        future: checkIfLoggedIn(),
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return CircularProgressIndicator();
          } else if (snapshot.hasData && snapshot.data == true) {
            return MainScreen();
          } else {
            return InitialScreen();
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
