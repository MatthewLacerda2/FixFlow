import 'package:flutter/material.dart';

import '../apts/contact_screen.dart';

class ContactsScreen extends StatelessWidget {
  const ContactsScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(0),
        child: Stack(
          children: <Widget>[
            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: <Widget>[
                Container(
                  color: Colors.blueAccent,
                  padding: const EdgeInsets.all(8.0),
                  height: 60,
                  child: const Row(
                    children: <Widget>[
                      Icon(Icons.calendar_month, size: 28),
                      SizedBox(width: 8),
                      Text(
                        'Contatos',
                        style: TextStyle(
                            fontSize: 24, fontWeight: FontWeight.bold),
                      ),
                    ],
                  ),
                ),
                Container(color: Colors.black, height: 1),
                const SizedBox(height: 14),
                Expanded(
                  child: ListView.separated(
                    itemCount: 10,
                    separatorBuilder: (BuildContext context, int index) =>
                        const Divider(
                      color: Colors.transparent,
                      thickness: 1,
                      height: 9,
                    ),
                    itemBuilder: (BuildContext context, int index) {
                      return Padding(
                          padding: const EdgeInsets.symmetric(horizontal: 8.5),
                          child: Container(
                            decoration: BoxDecoration(
                              color: Colors.white,
                              borderRadius: BorderRadius.circular(8),
                              border: Border.all(color: Colors.grey.shade300),
                              boxShadow: <BoxShadow>[
                                BoxShadow(
                                  color: Colors.grey.withOpacity(0.25),
                                  spreadRadius: 1.5,
                                  blurRadius: 4,
                                  offset: const Offset(0, 3),
                                ),
                              ],
                            ),
                            child: ListTile(
                              title: Text('Cliente: Cliente $index'),
                              subtitle: const Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: <Widget>[
                                  Text('Hora: 17h45m'),
                                ],
                              ),
                              onTap: () {
                                Navigator.push(
                                  context,
                                  MaterialPageRoute<void>(
                                    builder: (BuildContext context) =>
                                        ContactScreen(
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
                            ),
                          ));
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
