import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import '../intro/introduction_screen.dart';

class RegisterScreen extends StatelessWidget {
  RegisterScreen({super.key});

  BusinessRegisterRequest createBusinessRegisterRequest() {
    //The function i told you about
    return BusinessRegisterRequest(
      name: companyNameController.text,
      email: emailController.text,
      cnpj: cnpjController.text,
      phoneNumber: phoneController.text,
    );
  }

  final TextEditingController companyNameController = TextEditingController();
  final TextEditingController phoneController = TextEditingController();
  final TextEditingController emailController = TextEditingController();
  final TextEditingController cnpjController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Registrar'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            TextField(
              controller: companyNameController,
              decoration: const InputDecoration(labelText: 'Nome da empresa'),
            ),
            TextField(
              controller: phoneController,
              decoration: const InputDecoration(labelText: 'Telefone'),
              keyboardType: TextInputType.phone,
              inputFormatters: <TextInputFormatter>[
                FilteringTextInputFormatter.digitsOnly,
                _PhoneInputFormatter(),
              ],
            ),
            TextField(
              controller: emailController,
              decoration: const InputDecoration(labelText: 'Email'),
            ),
            TextField(
              controller: cnpjController,
              decoration: const InputDecoration(labelText: 'CNPJ'),
              keyboardType: TextInputType.number,
              inputFormatters: <TextInputFormatter>[
                FilteringTextInputFormatter.digitsOnly,
              ],
            ),
            const SizedBox(height: 20),
            ElevatedButton(
              onPressed: () {
                // TODO: Validate this data
                Navigator.pushAndRemoveUntil(
                  context,
                  MaterialPageRoute<void>(
                      builder: (BuildContext context) =>
                          const IntroductionScreenPage()),
                  (Route<dynamic> route) => false,
                );
              },
              child: const Text('Registrar'),
            ),
          ],
        ),
      ),
    );
  }
}

class _PhoneInputFormatter extends TextInputFormatter {
  @override
  TextEditingValue formatEditUpdate(
    TextEditingValue oldValue,
    TextEditingValue newValue,
  ) {
    if (newValue.text.length > 14) {
      return oldValue;
    }
    final String digits = newValue.text.replaceAll(RegExp(r'\D'), '');
    String formatted = '';
    for (int i = 0; i < digits.length; i++) {
      if (i == 0) formatted += '(';
      if (i == 2) formatted += ') ';
      if (i == 7) formatted += '-';
      formatted += digits[i];
    }
    return newValue.copyWith(
      text: formatted,
      selection: TextSelection.collapsed(offset: formatted.length),
    );
  }
}
