import 'package:flutter/material.dart';

import '../../../components/Buttons/custom_button.dart';
import '../../components/Inputs/email_input_field.dart';
import '../AppConfig/leave_successful.dart';

class ForgotPasswordScreen extends StatelessWidget {
  const ForgotPasswordScreen({super.key});

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
            const Center(
              child: Text(
                "Esqueceu sua senha?",
                style: TextStyle(
                  fontWeight: FontWeight.bold,
                  fontSize: 31,
                ),
              ),
            ),
            const SizedBox(height: 26),
            const Text(
              "Digite seu e-mail e enviaremos um link para redefinir sua senha.",
              textAlign: TextAlign.justify,
              style: TextStyle(fontSize: 18),
            ),
            const SizedBox(height: 40),
            EmailInputField(
              placeholder: 'Email',
              onEmailValidated: (String email) {
                print('Validated Email: $email');
              },
            ),
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
                    padding: const EdgeInsets.symmetric(horizontal: 30),
                    onPressed: () {
                      Navigator.pop(context);
                    },
                  ),
                  CustomButton(
                    text: "Confirmar",
                    textSize: 18,
                    backgroundColor: Colors.blue,
                    textColor: Colors.white,
                    borderRadius: 12,
                    borderWidth: 1.6,
                    padding: const EdgeInsets.symmetric(horizontal: 30),
                    onPressed: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                            builder: (BuildContext context) =>
                                const LeaveSuccessfulScreen(
                                    title: "E-mail enviado",
                                    description:
                                        "Você receberá um e-mail com um link para redefinir sua senha. Se não receber na caixa de entrada, verifique a caixa de spam.")),
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
