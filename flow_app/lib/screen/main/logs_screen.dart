import 'package:flutter/material.dart';

import '../../components/rounded_icon_button.dart';

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
                    Icon(Icons.note_alt, size: 28),
                    SizedBox(width: 8),
                    Text(
                      'Atendimentos',
                      style:
                          TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                    ),
                  ],
                ),
                SizedBox(height: 20),
                Expanded(
                  child: ListView.separated(
                    itemCount: 10,
                    separatorBuilder: (context, index) => Divider(
                      color: Colors.grey,
                      thickness: 1,
                    ),
                    itemBuilder: (context, index) {
                      return ListTile(
                        title: Text('Cliente: Nome do Cliente $index'),
                        subtitle: Text('Hora: 17h45m'),
                        onTap: () {
                          // TODO: Navigate to logView screen
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
