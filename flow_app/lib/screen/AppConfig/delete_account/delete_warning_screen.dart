import 'package:flutter/material.dart';

import '../../../components/Buttons/custom_button.dart';
import '../are_you_sure_screen.dart';
import 'delete_account_screen.dart';

class DeleteWarningScreen extends StatelessWidget {
  const DeleteWarningScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text(
          'Aviso',
          style: TextStyle(fontWeight: FontWeight.bold, fontSize: 24),
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            // const SizedBox(height: 32),
            const Text(
              'Deletar sua conta apagará todos os seus dados',
              style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 18),
            const Text(
              'Você pode desativar sua conta, isso suspende suas mensalidades, e você poderá re-ativar sua conta quando quiser',
              style: TextStyle(fontSize: 18),
            ),
            const SizedBox(height: 42),
            const Text(
              'Gostaria de desativar sua conta ao invés de deletar?',
              style: TextStyle(fontSize: 20),
            ),
            const SizedBox(height: 42),
            Align(
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: <Widget>[
                  CustomButton(
                    text: "Deletar",
                    textSize: 18,
                    backgroundColor: Colors.red,
                    textColor: Colors.white,
                    borderRadius: 12,
                    borderWidth: 1.6,
                    padding:
                        const EdgeInsets.symmetric(vertical: 1, horizontal: 50),
                    onPressed: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                            builder: (BuildContext context) => AreYouSureScreen(
                                title: "Confirmação de Deleção",
                                description:
                                    "Uma pena te ver partir...\n\nDigite sua senha para confirmar a desativação\n",
                                changeSuccessfulScreenTitle: "Conta deletada",
                                changeSuccessfulScreenDescription:
                                    "Uma pena te ver partir. Volte sempre!",
                                onPressed: () {
                                  //TODO: http request to deactivate account
                                })),
                      );
                    },
                  ),
                  CustomButton(
                    text: "Desativar",
                    textColor: Colors.white,
                    textSize: 18,
                    backgroundColor: Colors.green,
                    borderRadius: 12,
                    borderWidth: 1.6,
                    padding:
                        const EdgeInsets.symmetric(vertical: 1, horizontal: 40),
                    onPressed: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                            builder: (BuildContext context) =>
                                const DeleteAccountScreen()),
                      );
                    },
                  )
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
