import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../../components/Buttons/custom_button.dart';
import '../../../components/business_config.dart';
import '../../../components/warning_modal.dart';
import '../../../utils/flow_storage.dart';
import '../../AppConfig/deactivate_account/deactivate_account_screen.dart';
import '../../AppConfig/delete_account/delete_warning_screen.dart';
import '../../AppConfig/option_item.dart';
import '../../auth/initial_screen.dart';

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
              BusinessConfig(
                businessDTO: businessDTO,
              ),
              Container(
                height: 10,
                color: Colors.grey.shade800,
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
