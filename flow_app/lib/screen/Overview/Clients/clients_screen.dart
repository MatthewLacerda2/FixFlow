import 'package:flutter/material.dart';

import '../../../components/Inputs/date_picker_rectangle.dart';
import '../../../components/Inputs/time_picker_rectangle.dart';
import '../../../components/Screens/Overview/Clients/client_list.dart';
import '../../../components/warning_modal.dart';
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
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                      ElevatedButton(
                        onPressed: () {
                          showDialog(
                            context: context,
                            builder: (BuildContext context) {
                              return WarningModal(
                                title: "Faixa de horário",
                                backgroundColor: Colors.yellow,
                                optionOne: TimePickerRectangle(
                                    initialTime: TimeOfDay.now(),
                                    onTimeSelected:
                                        (TimeOfDay selectedTime) {}),
                                optionTwo: TimePickerRectangle(
                                    initialTime: TimeOfDay.now(),
                                    onTimeSelected:
                                        (TimeOfDay selectedTime) {}),
                              );
                            },
                          );
                        },
                        style: ElevatedButton.styleFrom(
                          backgroundColor: Colors.yellow,
                        ),
                        child: const Text(
                          'Hora',
                          style: TextStyle(color: Colors.white, fontSize: 16),
                        ),
                      ),
                      ElevatedButton(
                        onPressed: () {
                          showDialog(
                            context: context,
                            builder: (BuildContext context) {
                              return WarningModal(
                                title: "Entre os dias",
                                backgroundColor: Colors.yellow,
                                optionOne: DatePickerRectangle(
                                    initialDate: DateTime.now(),
                                    onDateSelected: (DateTime selectedTime) {}),
                                optionTwo: DatePickerRectangle(
                                    initialDate: DateTime.now(),
                                    onDateSelected: (DateTime selectedTime) {}),
                              );
                            },
                          );
                        },
                        style: ElevatedButton.styleFrom(
                          backgroundColor: Colors.yellow,
                        ),
                        child: const Text(
                          'Data',
                          style: TextStyle(color: Colors.white, fontSize: 16),
                        ),
                      ),
                      ElevatedButton(
                        onPressed: () {
                          showDialog(
                            context: context,
                            builder: (BuildContext context) {
                              return WarningModal(
                                title: "Nome do cliente",
                                backgroundColor: Colors.yellow,
                                optionOne: DatePickerRectangle(
                                    initialDate: DateTime.now(),
                                    onDateSelected: (DateTime selectedTime) {}),
                              );
                            },
                          );
                        },
                        style: ElevatedButton.styleFrom(
                          backgroundColor: Colors.yellow,
                        ),
                        child: const Text(
                          'Cliente',
                          style: TextStyle(color: Colors.white, fontSize: 16),
                        ),
                      ),
                    ],
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
