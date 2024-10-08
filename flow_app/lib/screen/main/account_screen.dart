import 'package:flutter/material.dart';

import '../AppConfig/create_idle_period_screen.dart';
import '../AppConfig/option_item.dart';
import '../intro/introduction_screen.dart';
import 'account/about_screen.dart';
import 'account/app_config_screen.dart';

class AccountScreen extends StatelessWidget {
  const AccountScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            const SizedBox(height: 10),
            //TODO: fetch data, get from local storage, whatever
            const Text(
              'Nome da Empresa',
              style: TextStyle(fontSize: 32, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 12),
            Text(
              'CNPJ: 00.000.000/0001-00',
              style: TextStyle(fontSize: 16, color: Colors.grey[600]),
            ),
            const SizedBox(height: 6),
            Text(
              '(98) 99934-4788',
              style: TextStyle(fontSize: 16, color: Colors.grey[600]),
            ),
            const SizedBox(height: 6),
            Text(
              'email@example.com',
              style: TextStyle(fontSize: 16, color: Colors.grey[600]),
            ),
            const SizedBox(height: 14),
            OptionItem(
              title: 'Configurações da Conta',
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute<void>(
                      builder: (BuildContext context) =>
                          const AppConfigScreen()),
                );
              },
            ),
            OptionItem(
              title: 'Instruções do app',
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute<void>(
                      builder: (BuildContext context) =>
                          const IntroductionScreenPage()),
                );
              },
            ),
            OptionItem(
              title: 'Contato',
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute<void>(
                      builder: (BuildContext context) => const AboutScreen()),
                );
              },
            ),
            OptionItem(
              title: 'Criar período ocioso',
              titleStyle: const TextStyle(
                  color: Colors.green, fontWeight: FontWeight.bold),
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute<void>(
                      builder: (BuildContext context) =>
                          const CreateIdlePeriodScreen()),
                );
              },
            ),
          ],
        ),
      ),
    );
  }
}
