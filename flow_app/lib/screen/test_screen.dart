import 'package:flutter/material.dart';

import '../components/Inputs/check_input_field.dart';
import '../components/Inputs/cnpj_input_field.dart';
import '../components/Inputs/cpf_input_field.dart';
import '../components/Inputs/date_picker_rectangle.dart';
import '../components/Inputs/email_input_field.dart';
import '../components/Inputs/name_input_field.dart';
import '../components/Inputs/password_input_field.dart';
import '../components/Inputs/phone_input_field.dart';
import '../components/Inputs/time_picker_rectangle.dart';

class TestScreen extends StatelessWidget {
  const TestScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Test Screen'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            TimePickerRectangle(
              initialTime: TimeOfDay.now(),
              onTimeSelected: (TimeOfDay date) {
                print(date);
              },
            ),
            DatePickerRectangle(
              initialDate: DateTime.now(),
              onDateSelected: (DateTime date) {
                print(date);
              },
            ),
            const SizedBox(height: 30),
            CheckInputField(
              label: 'Agree to Terms',
              initialValue: false,
              onChanged: (bool isChecked) {
                print('Checkbox is: $isChecked');
              },
            ),
            const SizedBox(height: 20),
            PasswordInputField(
              placeholder: 'Password',
              onPasswordChanged: (String password) {
                print('Password is: $password');
              },
            ),
            const SizedBox(height: 20),
            NameInputField(
              placeholder: 'Nome da Empresa',
              onNameChanged: (String name) {
                print('Name is: $name');
              },
            ),
            const SizedBox(height: 20),
            EmailInputField(
              placeholder: 'Email',
              onEmailValidated: (String email) {
                print('Validated Email: $email');
              },
            ),
            const SizedBox(height: 20),
            PhoneInputField(
              placeholder: 'Telefone',
              onPhoneChanged: (String phone) {
                print('Phone Number: $phone');
              },
            ),
            const SizedBox(height: 20),
            CNPJInputField(
              placeholder: 'CNPJ',
              onCNPJChanged: (String cnpj) {
                print('CNPJ is: $cnpj');
              },
            ),
            const SizedBox(height: 20),
            CPFInputField(
                placeholder: "CPF",
                onCPFChanged: (String cpf) {
                  print('CPF is: $cpf');
                })
          ],
        ),
      ),
    );
  }
}

void main() {
  runApp(const MaterialApp(
    home: TestScreen(),
  ));
}
