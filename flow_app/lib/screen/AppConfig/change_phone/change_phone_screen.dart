import 'package:flutter/material.dart';

import '../../../components/Buttons/custom_button.dart';
import '../../../components/Inputs/phone_input_field.dart';
import '../../auth/otp_screen.dart';
import '../../main/account/app_config_screen.dart';
import '../change_successful.dart';

class ChangePhoneScreen extends StatelessWidget {
  const ChangePhoneScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text(
          'Trocar telefone',
          style: TextStyle(fontWeight: FontWeight.bold),
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(26),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            const SizedBox(height: 8),
            const Text(
              'Digite o novo telefone e enviaremos um SMS com um código para confirmar a troca:',
              style: TextStyle(fontSize: 18),
            ),
            const SizedBox(height: 14),
            const Text(
              'Atual Telefone: (98) 99934 4788',
              style: TextStyle(fontSize: 18, fontWeight: FontWeight.w300),
            ),
            // TODO: fetch data later
            const SizedBox(height: 24),
            PhoneInputField(
              placeholder: 'Novo Telefone',
              onPhoneChanged: (String phone) {
                print('Phone Number: $phone');
              },
            ),
            const SizedBox(height: 120),
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
                      Navigator.pushReplacement(
                        context,
                        MaterialPageRoute<void>(
                          builder: (BuildContext context) =>
                              const AppConfigScreen(),
                        ),
                      );
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
                      Navigator.pushReplacement(
                        context,
                        MaterialPageRoute<void>(
                          builder: (BuildContext context) => const OtpScreen(
                              message:
                                  "Enviamos um SMS com um código para o telefone (98) 99934-4788. Insira-o aqui para confirmar:",
                              phoneNumber: "98999344788",
                              nextScreen: ChangeSuccessfulScreen(
                                  title: "Telefone atualizado!",
                                  description:
                                      "Seu número de telefone foi trocado com sucesso")),
                        ),
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
