import 'package:flutter/material.dart';

import '../../components/account_option.dart';

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
              style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
            ),
            SizedBox(height: 8),
            Text(
              'email@example.com',
              style: TextStyle(fontSize: 16, color: Colors.grey[600]),
            ),
            Text(
              '(98) 99934-4788',
              style: TextStyle(fontSize: 16, color: Colors.grey[600]),
            ),
            Text(
              'CNPJ: 00.000.000/0001-00',
              style: TextStyle(fontSize: 16, color: Colors.grey[600]),
            ),
            SizedBox(height: 20),
            Divider(),
            AccountOption(
              title: 'Notificações',
              onTap: () {
                // TODO: Navigate to Notifications settings
              },
            ),
            AccountOption(
              title: 'Configurações da Conta',
              onTap: () {
                // TODO: Navigate to Account settings
              },
            ),
            AccountOption(
              title: 'Configurações do App',
              onTap: () {
                // TODO: Navigate to App settings
              },
            ),
            AccountOption(
              title: 'Som',
              onTap: () {
                // TODO: Navigate to Sound settings
              },
            ),
          ],
        ),
      ),
    );
  }
}
