import 'package:flutter/material.dart';

import '../../components/rounded_icon_button.dart';
import '../apts/log_screen.dart';

class LogsScreen extends StatelessWidget {
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
                Row(
                  children: [
                    Icon(Icons.note_alt_outlined, size: 28),
                    SizedBox(width: 8),
                    Text(
                      'Atendimentos',
                      style:
                          TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                    ),
                  ],
                ),
                SizedBox(height: 14),
                Divider(),
                Expanded(
                  child: ListView.separated(
                    itemCount: 10,
                    separatorBuilder: (context, index) => Divider(
                      color: Colors.grey,
                      thickness: 1,
                      height: 0,
                    ),
                    itemBuilder: (context, index) {
                      return ListTile(
                        title: Text('Cliente: Nome do Cliente $index'),
                        subtitle: Text('Hora: 17h45m'),
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (context) => LogPage(
                                cliente: 'Fulano $index',
                                marcouHorario: true,
                                horario: TimeOfDay(hour: 14, minute: 30),
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
            RoundedIconButton(
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
