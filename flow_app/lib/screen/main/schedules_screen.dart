import 'package:flutter/material.dart';

import '../../components/rounded_icon_button.dart';

class SchedulesScreen extends StatelessWidget {
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
                    Icon(Icons.timer_outlined, size: 28),
                    SizedBox(width: 8),
                    Text(
                      'Agendamentos',
                      style:
                          TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                    ),
                  ],
                ),
                SizedBox(height: 20),
                Expanded(
                  child: ListView.separated(
                    itemCount: 10, // Example: 10 schedules
                    separatorBuilder: (context, index) => Divider(
                      color: Colors.grey,
                      thickness: 1,
                    ),
                    itemBuilder: (context, index) {
                      final String descricao =
                          index % 2 == 0 ? 'Descrição do agendamento' : '';
                      return ListTile(
                        title: Text('Cliente: Nome do Cliente $index'),
                        subtitle: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Text('Hora: 17h45m'),
                            if (descricao.isNotEmpty) Text(descricao),
                          ],
                        ),
                        onTap: () {
                          // TODO: Navigate to scheduleView screen
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
                // TODO: Navigate to schedule creation screen
              },
            ),
          ],
        ),
      ),
    );
  }
}
