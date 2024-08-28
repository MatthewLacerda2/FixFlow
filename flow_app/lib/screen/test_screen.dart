import 'package:flutter/material.dart';

import '../components/Inputs/check_input_field.dart';
import '../components/Inputs/cnpj_input_field.dart';
import '../components/Inputs/date_picker_rectangle.dart';
import '../components/Inputs/email_input_field.dart';
import '../components/Inputs/name_input_field.dart';
import '../components/Inputs/password_input_field.dart';
import '../components/Inputs/phone_input_field.dart';
import '../components/Inputs/time_picker_rectangle.dart';

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
            TimePickerRectangle(
              initialTime: TimeOfDay.now(),
            ),
            DatePickerRectangle(
              initialDate: DateTime.now(),
            ),
            SizedBox(height: 30),
            CheckInputField(
              label: 'Agree to Terms',
              initialValue: false,
              onChanged: (isChecked) {
                print('Checkbox is: $isChecked');
              },
            ),
            SizedBox(height: 20),
            PasswordInputField(
              placeholder: 'Password',
              onPasswordChanged: (password) {
                print('Password is: $password');
              },
            ),
            SizedBox(height: 20),
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
