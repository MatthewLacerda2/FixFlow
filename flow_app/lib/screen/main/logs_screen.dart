import 'package:flutter/material.dart';

import '../../components/rounded_icon_button.dart';
import '../apts/log_screen.dart';

class LogsScreen extends StatelessWidget {
  const LogsScreen({super.key});

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
                    Icon(Icons.note_alt_outlined, size: 28),
                    SizedBox(width: 8),
                    Text(
                      'Atendimentos',
                      style:
                          TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                    ),
                  ],
                ),
                const SizedBox(height: 14),
                const Divider(color: Colors.black),
                const SizedBox(height: 12),
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
                              builder: (BuildContext context) => LogScreen(
                                cliente: 'Fulano $index',
                                marcouHorario: true,
                                horario: const TimeOfDay(hour: 14, minute: 30),
                                dia: DateTime(2024, 8, 27),
                                preco: 150.00,
                                observacao: "This is an observation",
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
            RoundedButton(
              icon: Icons.add,
              onPressed: () {
                // TODO: Navigate to log creation screen
              },
            ),
          ],
        ),
      ),
    );
  }
}
