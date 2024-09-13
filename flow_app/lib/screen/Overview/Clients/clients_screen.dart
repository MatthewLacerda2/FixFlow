import 'package:flutter/material.dart';

import '../../../components/Inputs/name_input_field.dart';
import '../../../components/Screens/Overview/Clients/client_list.dart';
import 'client_screen.dart';

class ClientsScreen extends StatelessWidget {
  const ClientsScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Stack(
          children: <Widget>[
            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: <Widget>[
                Container(
                  color: const Color.fromARGB(255, 43, 255, 36),
                  padding: const EdgeInsets.all(8),
                  height: 60,
                  child: const Row(
                    children: <Widget>[
                      Icon(Icons.person, size: 28),
                      SizedBox(width: 8),
                      Text(
                        'Clientes',
                        style: TextStyle(
                            fontSize: 24, fontWeight: FontWeight.bold),
                      ),
                    ],
                  ),
                ),
                Container(
                  color: Colors.black,
                  height: 1,
                ),
                const SizedBox(height: 8),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 10),
                  child: NameInputField(
                    placeholder: 'Filtrar por nome',
                    onNameChanged: (String name) {
                      print('Name is: $name');
                    },
                  ),
                ),
                const SizedBox(height: 8),
                Expanded(
                  child: ListView.separated(
                    itemCount: 10,
                    separatorBuilder: (BuildContext context, int index) =>
                        const Divider(
                      color: Colors.transparent,
                      thickness: 0,
                      height: 9,
                    ),
                    itemBuilder: (BuildContext context, int index) {
                      return ClientList(
                        name: 'Nome do Cliente $index',
                        lastAppointment: DateTime.now(),
                        phone: '(98) 99934 - 4788',
                        email: 'math2.0@hotmail.com',
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute<void>(
                              builder: (BuildContext context) =>
                                  const ClientScreen(),
                            ),
                          );
                        },
                      );
                    },
                  ),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
