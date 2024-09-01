import 'package:flutter/material.dart';

import '../../main/app_config_screen.dart';

class DeactivateAccountScreen extends StatelessWidget {
  const DeactivateAccountScreen({super.key});

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
            const SizedBox(height: 32),
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
            const SizedBox(height: 60),
            Center(
              child: ElevatedButton(
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute<void>(
                        builder: (BuildContext context) =>
                            const AppConfigScreen()),
                  );
                },
                child: const Text('Voltar'),
              ),
            ),
            const Spacer(),
          ],
        ),
      ),
    );
  }
}
