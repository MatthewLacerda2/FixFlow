import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../../components/Buttons/custom_button.dart';
import '../../../utils/flow_storage.dart';
import '../../auth/initial_screen.dart';
import '../../main/main_screen.dart';
import '../are_you_sure_screen.dart';

class DeleteAccountScreen extends StatelessWidget {
  const DeleteAccountScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(22.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            const Spacer(),
            const Center(
              child: Text(
                'Desativar sua conta',
                style: TextStyle(
                  fontWeight: FontWeight.bold,
                  fontSize: 32,
                ),
              ),
            ),
            const SizedBox(height: 36),
            const Text(
              '• Mensalidades serão suspensas',
              style: TextStyle(fontSize: 18),
            ),
            const SizedBox(height: 4),
            const Text(
              '• Dados serão mantidos',
              style: TextStyle(fontSize: 18),
            ),
            const SizedBox(height: 4),
            const Text(
              '• Re-ative sua conta quando quiser',
              style: TextStyle(fontSize: 18),
            ),
            const SizedBox(height: 64),
            Align(
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: <Widget>[
                  CustomButton(
                    text: "Cancelar",
                    textSize: 16,
                    backgroundColor: Colors.white,
                    borderRadius: 12,
                    borderWidth: 1.6,
                    padding:
                        const EdgeInsets.symmetric(vertical: 1, horizontal: 30),
                    onPressed: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                            builder: (BuildContext context) => const MainScreen(
                                  initialIndex: 4,
                                )),
                      );
                    },
                  ),
                  CustomButton(
                    text: "Próximo",
                    textSize: 16,
                    backgroundColor: Colors.grey[800]!,
                    textColor: Colors.white,
                    borderRadius: 12,
                    borderWidth: 1.6,
                    padding:
                        const EdgeInsets.symmetric(vertical: 1, horizontal: 30),
                    onPressed: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                          builder: (BuildContext context) => AreYouSureScreen(
                            title: "Confirmar desativação da conta?",
                            description:
                                "Digite sua senha para confirmar a desativação\n\nLembre-se que você pode re-ativar sua conta e retomar tudo, a qualquer momento",
                            changeSuccessfulScreenTitle: "Conta desativada",
                            changeSuccessfulScreenDescription:
                                "Sua conta foi desativada, mas não perdida. Você pode reativar-la e continuar daonde parou, com o mesmo e-mail e senha\n\nVolte logo!",
                            onConfirm: () async {
                              final String mytoken =
                                  await FlowStorage.getToken();
                              final ApiClient apiClient =
                                  FlowStorage.getApiClient(mytoken);
                              BusinessApi(apiClient)
                                  .apiV1BusinessDeactivatePatch();
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
                        ),
                      );
                    },
                  )
                ],
              ),
            ),
            const Spacer(),
          ],
        ),
      ),
    );
  }
}
