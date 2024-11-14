import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart';

import '../components/Buttons/custom_button.dart';
import '../components/Inputs/CPF_input_field.dart';
import '../components/Inputs/email_input_field.dart';
import '../components/Inputs/name_input_field.dart';
import '../components/Inputs/phone_input_field.dart';
import '../utils/flow_storage.dart';

class CreateClientScreen extends StatelessWidget {
  CreateClientScreen({super.key, required this.nextScreen});

  final Widget nextScreen;

  String _fullname = "";
  String _phoneNumber = "";
  String? _cpf;
  String? _email;
  String? _note;

  void sendCreateRequest(BuildContext context) async {
    final BusinessDTO? bd = await FlowStorage.getBusinessDTO();

    final CustomerCreate customer = CustomerCreate(
        businessId: bd!.id!,
        fullName: _fullname,
        phoneNumber: _phoneNumber,
        cpf: _cpf,
        email: _email,
        additionalNote: _note);

    final Response response = await CustomerApi()
        .apiV1CustomerPostWithHttpInfo(customerCreate: customer);

    if (response.statusCode != 201) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text(response.body),
        ),
      );
      return;
    }

    Navigator.push(
      context,
      MaterialPageRoute<void>(builder: (BuildContext context) => nextScreen),
    );
  }

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
            const SizedBox(height: 20),
            NameInputField(
              placeholder: 'Nome do(a) Cliente',
              onNameChanged: (String name) {
                _fullname = name;
              },
            ),
            const SizedBox(height: 20),
            PhoneInputField(
              placeholder: 'Telefone',
              onPhoneChanged: (String phone) {
                _phoneNumber = phone;
              },
            ),
            const SizedBox(height: 20),
            EmailInputField(
              placeholder: 'Email',
              onEmailValidated: (String email) {
                _email = email;
              },
            ),
            const SizedBox(height: 20),
            CpfInputField(
              placeholder: "CPF",
              onCPFChanged: (String cpf) {
                _cpf = cpf;
              },
            ),
            const SizedBox(height: 20),
            NameInputField(
              placeholder: 'Observação:',
              onNameChanged: (String note) {
                _note = note;
              },
            ),
            const SizedBox(height: 44),
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
                    text: "PróximoX",
                    textSize: 16,
                    backgroundColor: Colors.grey[300]!,
                    borderRadius: 12,
                    padding:
                        const EdgeInsets.symmetric(vertical: 1, horizontal: 30),
                    onPressed: () {
                      sendCreateRequest(context);
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
