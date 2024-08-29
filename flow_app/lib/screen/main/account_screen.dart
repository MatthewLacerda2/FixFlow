import 'package:flutter/material.dart';

import '../../components/account_option.dart';
import '../about_screen.dart';
import '../intro/introduction_screen.dart';
import '../test_screen.dart';

//TODO: make opção de sair (com confirmação)

class AccountScreen extends StatelessWidget {
  const AccountScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            //TODO: fetch data, get from local storage, whatever
            const Text(
              'Nome da Empresa',
              style: TextStyle(fontSize: 30, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 10),
            Text(
              'CNPJ: 00.000.000/0001-00',
              style: TextStyle(fontSize: 16, color: Colors.grey[600]),
            ),
            const SizedBox(height: 4),
            Text(
              '(98) 99934-4788',
              style: TextStyle(fontSize: 16, color: Colors.grey[600]),
            ),
            const SizedBox(height: 4),
            Text(
              'email@example.com',
              style: TextStyle(fontSize: 16, color: Colors.grey[600]),
            ),
            const SizedBox(height: 15),
            const Divider(),
            const SizedBox(height: 15),
            AccountOption(
              title: 'Configurações de Notificação',
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                      builder: (BuildContext context) => const TestScreen()),
                );
              },
            ),
            AccountOption(
              title: 'Configurações da Conta',
              onTap: () {
                // TODO: Navigate to Account settings
              },
            ),
            AccountOption(
              title: 'Configurações do Aplicativo',
              onTap: () {
                // TODO: Navigate to App settings
              },
            ),
            AccountOption(
              title: 'Instruções do app',
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                      builder: (BuildContext context) =>
                          const IntroductionScreenPage()),
                );
              },
            ),
            AccountOption(
              title: 'Sobre',
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                      builder: (BuildContext context) => const AboutScreen()),
                );
              },
            ),
          ],
        ),
      ),
    );
  }
}
