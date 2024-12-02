import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../../components/Buttons/custom_button.dart';
import '../../../components/Inputs/check_input_field.dart';
import '../../../components/Inputs/enum_field.dart';
import '../../../components/warning_modal.dart';
import '../../../utils/flow_storage.dart';
import '../../AppConfig/change_phone/change_phone_screen.dart';
import '../../AppConfig/deactivate_account/deactivate_account_screen.dart';
import '../../AppConfig/delete_account/delete_warning_screen.dart';
import '../../AppConfig/option_item.dart';
import '../../auth/initial_screen.dart';

//TODO: gotta load the account configs
class AppConfigScreen extends StatelessWidget {
  const AppConfigScreen({super.key, required this.businessDTO});

  final BusinessDTO businessDTO;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Configurações da Conta'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: <Widget>[
              const SizedBox(height: 18),
              Container(
                height: 10,
                color: Colors.grey.shade800,
              ),
              const SizedBox(height: 32),
              const Text(
                'Opções de Serviços',
                style: TextStyle(fontWeight: FontWeight.bold, fontSize: 22),
              ),
              const SizedBox(height: 12),
              EnumField(
                  description: "Serviço...",
                  options: businessDTO.services ?? <String>[],
                  characterLimit: 20),
              const SizedBox(height: 3),
              const Text(
                'Opções mais comuns de serviços prestados',
                style: TextStyle(color: Colors.grey, fontSize: 12),
              ),
              const SizedBox(height: 10),
              CheckInputField(
                label: 'Permitir apenas serviços listados?',
                initialValue: businessDTO.allowListedServicesOnly!,
                onChanged: (bool isChecked) {
                  print('Outros: $isChecked');
                },
              ),
              const SizedBox(height: 18),
              CheckInputField(
                label: 'Atende aos feriados?',
                initialValue: businessDTO.openOnHolidays!,
                onChanged: (bool isChecked) {
                  print('Atende aos feriados: $isChecked');
                },
              ),
              const SizedBox(height: 30),
              Container(
                height: 10,
                color: Colors.grey.shade800,
              ),
              OptionItem(
                title: 'Trocar telefone',
                titleStyle: const TextStyle(
                    color: Colors.green,
                    fontWeight: FontWeight.bold,
                    fontSize: 16),
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute<void>(
                        builder: (BuildContext context) =>
                            const ChangePhoneScreen()),
                  );
                },
              ),
              OptionItem(
                title: 'Desativar conta',
                titleStyle: TextStyle(
                    color: Colors.grey[850]!,
                    fontWeight: FontWeight.bold,
                    fontSize: 16),
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute<void>(
                        builder: (BuildContext context) =>
                            const DeactivateAccountScreen()),
                  );
                },
              ),
              OptionItem(
                title: 'Deletar conta',
                titleStyle: TextStyle(
                    color: Colors.red[900]!,
                    fontWeight: FontWeight.bold,
                    fontSize: 16),
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute<void>(
                        builder: (BuildContext context) =>
                            const DeleteWarningScreen()),
                  );
                },
              ),
              OptionItem(
                title: 'Sair',
                titleStyle: TextStyle(
                    color: Colors.red[900]!,
                    fontWeight: FontWeight.bold,
                    fontSize: 16),
                onTap: () {
                  showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      return WarningModal(
                        title: "Você quer sair da conta?",
                        optionOne: CustomButton(
                          text: "Sim",
                          textSize: 16,
                          backgroundColor: Colors.green,
                          textColor: Colors.black,
                          onPressed: () {
                            FlowStorage.clear();
                            Navigator.push(
                              context,
                              MaterialPageRoute<void>(
                                builder: (BuildContext context) =>
                                    const InitialScreen(),
                              ),
                            );
                          },
                        ),
                        optionTwo: CustomButton(
                          text: "Não",
                          textSize: 16,
                          backgroundColor: Colors.blue,
                          textColor: Colors.black,
                          onPressed: () {
                            Navigator.of(context).pop();
                          },
                        ),
                      );
                    },
                  );
                },
              ),
              const SizedBox(height: 48),
            ],
          ),
        ),
      ),
    );
  }
}
