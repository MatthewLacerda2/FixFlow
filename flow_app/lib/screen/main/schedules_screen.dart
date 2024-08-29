import 'package:flutter/material.dart';

import '../../components/rounded_icon_button.dart';
import '../apts/schedule_screen.dart';
import '../details/create_schedule_screen.dart';

class SchedulesScreen extends StatelessWidget {
  const SchedulesScreen({super.key});

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
                    Icon(Icons.timer_outlined, size: 28),
                    SizedBox(width: 8),
                    Text(
                      'Agendamentos',
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
                            color: Colors.grey, thickness: 1, height: 0),
                    itemBuilder: (BuildContext context, int index) {
                      final String? descricao = 'Agendamento de numero $index';
                      return ListTile(
                        title: Text('Cliente: Nome do Cliente $index'),
                        subtitle: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: <Widget>[
                            const Text('Hora: 17h45m'),
                            if (descricao != null) Text(descricao),
                          ],
                        ),
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (BuildContext context) => ScheduleScreen(
                                cliente: 'Fulano $index',
                                contactado: true,
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
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (BuildContext context) => CreateScheduleScreen(
                      contactado: false,
                      horario: TimeOfDay.now(),
                      dia: DateTime(2024, 8, 27),
                      preco: 150.00,
                      observacao: "This is an observation",
                    ),
                  ),
                );
              },
            ),
          ],
        ),
      ),
    );
  }
}
