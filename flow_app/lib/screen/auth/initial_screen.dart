import 'package:flutter/material.dart';

import '../../components/Buttons/colored_border_text_button.dart';
import 'login_screen.dart';
import 'register_screen.dart';

class InitialScreen extends StatelessWidget {
  const InitialScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            const Text(
              'Flow',
              style: TextStyle(fontSize: 60, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 20),
            const Text(
              'Agendamentos Automatizados',
              style: TextStyle(fontSize: 18),
            ),
            const SizedBox(height: 180),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                ColoredBorderTextButton(
                  text: 'Registrar',
                  textColor: Colors.blue,
                  width: 46,
                  onPressed: () {
                    Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                            builder: (BuildContext context) =>
                                RegisterScreen()));
                  },
                ),
                const SizedBox(width: 14),
                ColoredBorderTextButton(
                  text: 'Login',
                  textColor: Colors.blue,
                  width: 58,
                  onPressed: () {
                    Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                            builder: (BuildContext context) =>
                                const LoginScreen()));
                  },
                ),
              ],
            ),
            const SizedBox(height: 30),
            GestureDetector(
              onTap: () {
                // Handle forgot password tap
              },
              child: const Text(
                'Esqueceu sua senha?',
                style: TextStyle(
                  fontSize: 14,
                  color: Colors.blue,
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
