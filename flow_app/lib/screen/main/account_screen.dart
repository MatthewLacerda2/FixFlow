import 'package:flutter/material.dart';
import 'package:introduction_screen/introduction_screen.dart';

import '../../components/account_option.dart';
import '../test_screen.dart';

//TODO: make opção de sair (com confirmação)

class AccountScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            //TODO: fetch data, get from local storage, whatever
            Text(
              'Nome da Empresa',
              style: TextStyle(fontSize: 30, fontWeight: FontWeight.bold),
            ),
            SizedBox(height: 20),
            Text(
              'CNPJ: 00.000.000/0001-00',
              style: TextStyle(fontSize: 16, color: Colors.grey[600]),
            ),
            Text(
              '(98) 99934-4788',
              style: TextStyle(fontSize: 16, color: Colors.grey[600]),
            ),
            SizedBox(height: 8),
            Text(
              'email@example.com',
              style: TextStyle(fontSize: 16, color: Colors.grey[600]),
            ),
            SizedBox(height: 20),
            Divider(),
            SizedBox(height: 20),
            AccountOption(
              title: 'Configurações de Notificação',
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => TestScreen()),
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
              title: 'Aúdio e som',
              onTap: () {
                // TODO: Navigate to Audio e som
              },
            ),
            AccountOption(
              title: 'Instruções do app',
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                      builder: (context) => IntroductionScreen()), //TODO: bug
                );
              },
            ),
            AccountOption(
              title: 'Sobre o App',
              onTap: () {
                // TODO: Navigate to Sobre o App
              },
            ),
          ],
        ),
      ),
    );
  }
}
