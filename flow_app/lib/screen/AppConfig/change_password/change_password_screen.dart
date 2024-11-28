import 'package:flutter/material.dart';

import '../../../components/Buttons/custom_button.dart';
import '../../../components/Inputs/password_input_field.dart';
import '../change_successful.dart';

class ChangePasswordScreen extends StatelessWidget {
  const ChangePasswordScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Trocar senha'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(20),
        child: Column(
          children: <Widget>[
            const SizedBox(height: 32),
            PasswordInputField(
              placeholder: 'Senha atual',
              onPasswordChanged: (String password) {
                print('Senha atual: $password');
              },
            ),
            const SizedBox(height: 24),
            PasswordInputField(
              placeholder: 'Nova senha',
              onPasswordChanged: (String password) {
                print('Nova senha: $password');
              },
            ),
            const SizedBox(height: 24),
            PasswordInputField(
              placeholder: 'Confirmar nova senha',
              onPasswordChanged: (String password) {
                print('Confirmar nova senha: $password');
              },
            ),
            const SizedBox(height: 70),
            Align(
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: <Widget>[
                  CustomButton(
                    text: "Voltar",
                    textSize: 16,
                    backgroundColor: Colors.transparent,
                    borderRadius: 12,
                    padding:
                        const EdgeInsets.symmetric(vertical: 1, horizontal: 30),
                    onPressed: () {
                      Navigator.pop(context);
                    },
                  ),
                  //TODO: snackbar in case the change is unsuccessful
                  CustomButton(
                    text: "Confirmar",
                    textSize: 16,
                    backgroundColor: Colors.grey[300]!,
                    borderRadius: 12,
                    padding:
                        const EdgeInsets.symmetric(vertical: 1, horizontal: 30),
                    onPressed: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                            builder: (BuildContext context) =>
                                const ChangeSuccessfulScreen(
                                    title: "Sucesso",
                                    description:
                                        "Sua senha foi trocada com sucesso")),
                      );
                    },
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
