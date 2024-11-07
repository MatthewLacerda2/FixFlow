import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:http/src/response.dart';

import '../../components/Inputs/password_input_field.dart';
import '../intro/introduction_screen.dart';

class RegisterScreen extends StatelessWidget {
  RegisterScreen({super.key});

  BusinessRegisterRequest createBusinessRegisterRequest() {
    return BusinessRegisterRequest(
        name: companyNameController.text,
        email: emailController.text,
        cnpj: cnpjController.text,
        phoneNumber: phoneController.text,
        password: Bpassword,
        confirmPassword: BconfirmPassword);
  }

  final TextEditingController companyNameController = TextEditingController();
  final TextEditingController phoneController = TextEditingController();
  final TextEditingController emailController = TextEditingController();
  final TextEditingController cnpjController = TextEditingController();

  String? Bpassword, BconfirmPassword;

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
                _CNPJInputFormatter(),
              ],
            ),
            const SizedBox(height: 20),
            PasswordInputField(
              placeholder: 'Password',
              onPasswordChanged: (String password) {
                print('Password is: $password');
                Bpassword = password;
              },
            ),
            const SizedBox(height: 20),
            PasswordInputField(
              placeholder: 'Current password',
              onPasswordChanged: (String password) {
                print('Password is: $password');
                BconfirmPassword = password;
              },
            ),
            const SizedBox(height: 20),
            ElevatedButton(
              onPressed: () async {
                final BusinessRegisterRequest businessRegisterRequest =
                    createBusinessRegisterRequest();
                print(businessRegisterRequest);
                try {
                  // Step 2: Call the API to register the business and wait for the response
                  final Response response =
                      await BusinessApi().apiV1BusinessPostWithHttpInfo(
                    businessRegisterRequest: businessRegisterRequest,
                  );

                  // Check if the response status code is 201 (Created)
                  if (response.statusCode == 201) {
                    // Navigate to the IntroductionScreenPage on success
                    Navigator.pushAndRemoveUntil(
                      context,
                      MaterialPageRoute<void>(
                        builder: (BuildContext context) =>
                            const IntroductionScreenPage(),
                      ),
                      (Route<dynamic> route) => false,
                    );
                  } else {
                    // Show SnackBar if the response is not 201
                    ScaffoldMessenger.of(context).showSnackBar(
                      const SnackBar(
                        content: Text(
                            'Registration failed. Please check your input.'),
                      ),
                    );
                  }
                } catch (e) {
                  // Handle errors
                  print("Failed to register business: $e");
                  ScaffoldMessenger.of(context).showSnackBar(
                    const SnackBar(
                      content: Text('Registration failed. Please try again.'),
                    ),
                  );
                }
              },
              child: const Text('Registrare'),
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

class _CNPJInputFormatter extends TextInputFormatter {
  @override
  TextEditingValue formatEditUpdate(
    TextEditingValue oldValue,
    TextEditingValue newValue,
  ) {
    // Remove all non-digit characters
    final String digits = newValue.text.replaceAll(RegExp(r'\D'), '');

    String formatted = '';
    for (int i = 0; i < digits.length; i++) {
      if (i == 2) formatted += '.';
      if (i == 5) formatted += '.';
      if (i == 8) formatted += '/';
      if (i == 12) formatted += '-';
      formatted += digits[i];
    }

    return newValue.copyWith(
      text: formatted,
      selection: TextSelection.collapsed(offset: formatted.length),
    );
  }
}
