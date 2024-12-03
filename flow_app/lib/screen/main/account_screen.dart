import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../utils/flow_storage.dart';
import '../AppConfig/option_item.dart';
import '../intro/introduction_screen.dart';
import 'account/about_screen.dart';
import 'account/app_config_screen.dart';

class AccountScreen extends StatelessWidget {
  const AccountScreen({super.key});
  //TODO: see if ya can delete this
  Future<BusinessDTO?> _fetchBusinessData() async {
    return await FlowStorage.getBusinessDTO();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: FutureBuilder<BusinessDTO?>(
        future: _fetchBusinessData(),
        builder: (BuildContext context, AsyncSnapshot<BusinessDTO?> snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          } else if (snapshot.hasError) {
            return Center(
              child: Text(
                'Failed to load business data: ${snapshot.error}',
                style: const TextStyle(color: Colors.red),
                textAlign: TextAlign.center,
              ),
            );
          } else if (!snapshot.hasData || snapshot.data == null) {
            return const Center(child: Text('No business data found.'));
          } else {
            final BusinessDTO business = snapshot.data!;
            return Padding(
              padding: const EdgeInsets.all(16),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: <Widget>[
                  const SizedBox(height: 10),
                  Text(
                    business.name!,
                    style: const TextStyle(
                        fontSize: 32, fontWeight: FontWeight.bold),
                  ),
                  const SizedBox(height: 12),
                  Text(
                    'CNPJ: ${business.cnpj}',
                    style: TextStyle(fontSize: 16, color: Colors.grey[600]),
                  ),
                  const SizedBox(height: 6),
                  Text(
                    business.phoneNumber!,
                    style: TextStyle(fontSize: 16, color: Colors.grey[600]),
                  ),
                  const SizedBox(height: 6),
                  Text(
                    business.email ?? 'Email não informado',
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
                                AppConfigScreen(businessDTO: business)),
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
                            builder: (BuildContext context) =>
                                const AboutScreen()),
                      );
                    },
                  ), /*
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
                  ),*/
                ],
              ),
            );
          }
        },
      ),
    );
  }
}
