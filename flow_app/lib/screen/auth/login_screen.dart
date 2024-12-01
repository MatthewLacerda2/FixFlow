import 'package:flutter/material.dart';

import '../../components/Buttons/colored_border_text_button.dart';
import '../../utils/login_utils.dart';
import '../main/main_screen.dart';

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  LoginScreenState createState() => LoginScreenState();
}

class LoginScreenState extends State<LoginScreen> {
  final TextEditingController emailController = TextEditingController();
  final TextEditingController passwordController = TextEditingController();
  String errorMessage = '';

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Login'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            TextField(
              controller: emailController,
              decoration: const InputDecoration(
                labelText: 'Email',
              ),
            ),
            TextField(
              controller: passwordController,
              decoration: InputDecoration(
                labelText: 'Password',
                errorText: errorMessage.isEmpty ? null : errorMessage,
              ),
              obscureText: true,
            ),
            const SizedBox(height: 24),
            ColoredBorderTextButton(
              text: "Login",
              textColor: Colors.grey[700]!,
              width: 40,
              onPressed: () {
                final String email = emailController.text;
                final String password = passwordController.text;
                LoginUtils.login(
                  email,
                  password,
                  context,
                  const MainScreen(initialIndex: 0),
                );
              },
            ),
          ],
        ),
      ),
    );
  }
}
