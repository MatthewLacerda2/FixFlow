import 'package:flutter/material.dart';

import '../../../components/Buttons/custom_button.dart';
import '../../components/Inputs/password_input_field.dart';
import '../main/account/app_config_screen.dart';
import 'leave_successful.dart';

class AreYouSureScreen extends StatelessWidget {
  const AreYouSureScreen(
      {super.key,
      required this.title,
      required this.description,
      required this.changeSuccessfulScreenTitle,
      required this.changeSuccessfulScreenDescription,
      required this.onPressed});
  final String title;
  final String description;
  final String changeSuccessfulScreenTitle;
  final String changeSuccessfulScreenDescription;
  final VoidCallback onPressed;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(24),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            const Spacer(),
            Center(
              child: Text(
                title,
                style: const TextStyle(
                  fontWeight: FontWeight.bold,
                  fontSize: 31,
                ),
              ),
            ),
            const SizedBox(height: 26),
            Text(
              description,
              textAlign: TextAlign.justify,
              style: const TextStyle(fontSize: 18),
            ),
            const SizedBox(height: 40),
            const PasswordInputField(placeholder: "Senha"),
            const SizedBox(height: 80),
            Align(
                child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                  CustomButton(
                    text: "Voltar",
                    textSize: 18,
                    backgroundColor: Colors.white,
                    borderRadius: 12,
                    borderWidth: 1.6,
                    padding:
                        const EdgeInsets.symmetric(vertical: 1, horizontal: 30),
                    onPressed: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                            builder: (BuildContext context) =>
                                const AppConfigScreen()),
                      );
                    },
                  ),
                  CustomButton(
                    text: "Confirmar",
                    textSize: 18,
                    backgroundColor: Colors.red,
                    textColor: Colors.white,
                    borderRadius: 12,
                    borderWidth: 1.6,
                    padding:
                        const EdgeInsets.symmetric(vertical: 1, horizontal: 30),
                    onPressed: () {
                      onPressed();
                      Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                            builder: (BuildContext context) =>
                                LeaveSuccessfulScreen(
                                    title: changeSuccessfulScreenTitle,
                                    description:
                                        changeSuccessfulScreenDescription)),
                      );
                    },
                  ),
                ])),
            const Spacer(),
          ],
        ),
      ),
    );
  }
}
