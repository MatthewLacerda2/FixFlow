import 'package:flutter/material.dart';

import '../components/Inputs/cnpj_input_field.dart';
import '../components/Inputs/email_input_field.dart';
import '../components/Inputs/name_input_field.dart';
import '../components/Inputs/phone_input_field.dart';

class TestScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Test Screen'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            NameInputField(
              placeholder: 'Nome da Empresa',
              onNameChanged: (name) {
                print('Name is: $name');
              },
            ),
            SizedBox(height: 20),
            EmailInputField(
              placeholder: 'Email',
              onEmailValidated: (email) {
                print('Validated Email: $email');
              },
            ),
            SizedBox(height: 20),
            PhoneInputField(
              placeholder: 'Telefone',
              onPhoneChanged: (phone) {
                print('Phone Number: $phone');
              },
            ),
            SizedBox(height: 20),
            CNPJInputField(
              placeholder: 'CNPJ',
              onCNPJChanged: (cnpj) {
                print('CNPJ is: $cnpj');
              },
            ),
          ],
        ),
      ),
    );
  }
}

void main() {
  runApp(MaterialApp(
    home: TestScreen(),
  ));
}
