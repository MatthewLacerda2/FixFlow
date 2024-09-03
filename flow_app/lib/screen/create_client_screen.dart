import 'package:flutter/material.dart';

import '../components/Buttons/custom_button.dart';
import '../components/Inputs/CPF_input_field.dart';
import '../components/Inputs/email_input_field.dart';
import '../components/Inputs/name_input_field.dart';
import '../components/Inputs/phone_input_field.dart';

class CreateClientScreen extends StatelessWidget {
  const CreateClientScreen({super.key, required this.nextScreen});

  final Widget nextScreen;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Criar Cliente'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            const SizedBox(height: 24),
            NameInputField(
              placeholder: 'Nome do(a) Cliente',
              onNameChanged: (String name) {
                print('Name is: $name');
              },
            ),
            const SizedBox(height: 24),
            PhoneInputField(
              placeholder: 'Telefone',
              onPhoneChanged: (String phone) {
                print('Phone Number: $phone');
              },
            ),
            const SizedBox(height: 24),
            EmailInputField(
              placeholder: 'Email',
              onEmailValidated: (String email) {
                print('Validated Email: $email');
              },
            ),
            const SizedBox(height: 24),
            CPFInputField(
              placeholder: "CPF",
              onCPFChanged: (String cpf) {
                print('CPF is: $cpf');
              },
            ),
            const SizedBox(height: 46),
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
                  CustomButton(
                    text: "Próximo",
                    textSize: 16,
                    backgroundColor: Colors.grey[300]!,
                    borderRadius: 12,
                    padding:
                        const EdgeInsets.symmetric(vertical: 1, horizontal: 30),
                    onPressed: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                            builder: (BuildContext context) => nextScreen),
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
