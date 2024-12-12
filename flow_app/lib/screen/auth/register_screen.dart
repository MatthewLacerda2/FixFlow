import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart';

import '../../components/Inputs/cnpj_input_field.dart';
import '../../components/Inputs/password_input_field.dart';
import '../../components/Inputs/phone_input_field.dart';
import '../../utils/flow_snack.dart';
import '../../utils/login_utils.dart';
import '../intro/introduction_screen.dart';

class RegisterScreen extends StatelessWidget {
  RegisterScreen({super.key});

  BusinessRegisterRequest createBusinessRegisterRequest() {
    return BusinessRegisterRequest(
        name: companyNameController.text.trim(),
        email: emailController.text.trim(),
        phoneNumber: _phoneNumber,
        cnpj: _cnpj,
        password: registerPassword,
        confirmPassword: registerPasswordConfirmation);
  }

  final TextEditingController companyNameController = TextEditingController();
  final TextEditingController emailController = TextEditingController();

  String? registerPassword, registerPasswordConfirmation;

  String _phoneNumber = "";
  String _cnpj = "";

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
            PhoneInputField(
              placeholder: 'Telefone',
              onPhoneChanged: (String phone) {
                _phoneNumber = phone;
              },
            ),
            TextField(
              controller: emailController,
              decoration: const InputDecoration(labelText: 'Email'),
            ),
            CNPJInputField(
                placeholder: "CNPJ",
                onCNPJChanged: (String cnpj) {
                  _cnpj = cnpj;
                }),
            const SizedBox(height: 20),
            PasswordInputField(
              placeholder: 'Password',
              onPasswordChanged: (String password) {
                registerPassword = password;
              },
            ),
            const SizedBox(height: 20),
            PasswordInputField(
              placeholder: 'Current password',
              onPasswordChanged: (String password) {
                registerPasswordConfirmation = password;
              },
            ),
            const SizedBox(height: 20),
            ElevatedButton(
              onPressed: () async {
                final BusinessRegisterRequest brr =
                    createBusinessRegisterRequest();

                final Response response = await BusinessApi()
                    .apiV1BusinessPostWithHttpInfo(
                        businessRegisterRequest: brr);

                if (response.statusCode != 201) {
                  FlowSnack.show(context, response.body);
                } else {
                  LoginUtils.login(brr.email!, brr.password!, context,
                      const IntroductionScreenPage());
                }
              },
              child: const Text('Registrar'),
            ),
          ],
        ),
      ),
    );
  }
}
