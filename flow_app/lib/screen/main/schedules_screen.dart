import 'package:flutter/material.dart';

import '../../components/Buttons/colored_border_text_button.dart';
import '../../components/Buttons/order_button.dart';
import '../../components/Buttons/rounded_iconed_button.dart';
import '../../components/schedules_list.dart';
import '../apt_filters_screen.dart';
import '../apts/edit_apt/create_schedule_screen.dart';
import '../apts/schedule_screen.dart';
import '../create_client_screen.dart';

class SchedulesScreen extends StatelessWidget {
  const SchedulesScreen({super.key});

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
                  color: Colors.greenAccent,
                  padding: const EdgeInsets.all(8),
                  height: 60,
                  child: const Row(children: <Widget>[
                    Icon(Icons.timer_outlined, size: 28),
                    SizedBox(width: 8),
                    Text(
                      'Agendamentos',
                      style:
                          TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                    ),
                  ]),
                ),
                Container(
                  color: Colors.black,
                  height: 1,
                ),
                const SizedBox(height: 8),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 10),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                      const OrderButton(
                        icon: Icons.perm_contact_cal,
                        isUp: true,
                        iconSize: 40,
                        iconColor: Colors.greenAccent,
                      ),
                      const OrderButton(
                        icon: Icons.attach_money,
                        iconSize: 40,
                        iconColor: Colors.greenAccent,
                      ),
                      const OrderButton(
                        icon: Icons.calendar_today,
                        iconSize: 40,
                        iconColor: Colors.greenAccent,
                      ),
                      ColoredBorderTextButton(
                        text: "Filtros",
                        onPressed: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute<void>(
                              builder: (BuildContext context) =>
                                  const AptFiltersScreen(),
                            ),
                          );
                        },
                        backgroundColor: Colors.greenAccent,
                        borderColor: Colors.grey,
                        textColor: Colors.white,
                      )
                    ],
                  ),
                ),
                const SizedBox(height: 10),
                Expanded(
                  child: ListView.separated(
                    itemCount: 10,
                    separatorBuilder: (BuildContext context, int index) =>
                        const Divider(
                            color: Colors.transparent, thickness: 1, height: 9),
                    itemBuilder: (BuildContext context, int index) {
                      return SchedulesList(
                        clientName: 'nomeDoCliente',
                        price: 150.00,
                        hour: '17h45m',
                        date: '21/12/24',
                        service: 'troca de peças',
                        observation: 'Agendamento de numero $index',
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute<void>(
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
            RoundedIconedButton(
              icon: Icons.person_add_alt_1_sharp,
              size: 38,
              bottom: 100,
              right: 16,
              color: const Color.fromARGB(255, 0, 175, 0),
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute<void>(
                    builder: (BuildContext context) => CreateClientScreen(
                      nextScreen: CreateScheduleScreen(
                        contactado: false,
                        horario: TimeOfDay.now(),
                        dia: DateTime(2024, 8, 27),
                        preco: 150.00,
                        observacao: "This is an observation",
                      ),
                    ),
                  ),
                );
              },
            ),
            RoundedIconedButton(
              icon: Icons.add,
              size: 68,
              bottom: 18,
              right: 16,
              color: Colors.purple,
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute<void>(
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
