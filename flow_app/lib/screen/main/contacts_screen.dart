import 'package:flutter/material.dart';

import '../apts/contact_screen.dart';

class ContactsScreen extends StatelessWidget {
  const ContactsScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Stack(
          children: <Widget>[
            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: <Widget>[
                const Row(
                  children: <Widget>[
                    Icon(Icons.calendar_month, size: 28),
                    SizedBox(width: 8),
                    Text(
                      'Contatos',
                      style:
                          TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                    ),
                  ],
                ),
                const SizedBox(height: 14),
                const Divider(),
                Expanded(
                  child: ListView.separated(
                    itemCount: 10,
                    separatorBuilder: (BuildContext context, int index) =>
                        const Divider(
                      color: Colors.grey,
                      thickness: 1,
                      height: 0,
                    ),
                    itemBuilder: (BuildContext context, int index) {
                      return ListTile(
                        title: Text('Cliente: Nome do Cliente $index'),
                        subtitle: const Text('Hora: 17h45m'),
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (BuildContext context) => ContactScreen(
                                cliente: 'Fulano $index',
                                dia: DateTime(2024, 8, 27),
                                previousHorario:
                                    const TimeOfDay(hour: 14, minute: 30),
                                previousDia: DateTime(2024, 8, 27),
                                previousPrice: 150.0,
                                previousObservacao: "Something",
                              ),
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
