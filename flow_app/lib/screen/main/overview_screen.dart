import 'package:flutter/material.dart';

import '../../components/Buttons/colored_border_text_button.dart';
import '../../components/Buttons/rounded_iconed_button.dart';
import '../Overview/Clients/clients_screen.dart';
import 'notifications_screen.dart';

class OverviewScreen extends StatelessWidget {
  const OverviewScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Stack(
        children: <Widget>[
          Padding(
            padding: const EdgeInsets.all(16.0),
            child: Column(
              children: <Widget>[
                Row(
                  children: <Widget>[
                    Row(
                      children: <Widget>[
                        const Text(
                          '12',
                          style: TextStyle(
                              fontSize: 42, fontWeight: FontWeight.bold),
                        ),
                        const SizedBox(width: 6),
                        ConstrainedBox(
                          constraints: const BoxConstraints(maxWidth: 100),
                          child: Text(
                            'agendamentos para hoje',
                            style: TextStyle(
                                fontSize: 12, color: Colors.grey[600]),
                          ),
                        ),
                      ],
                    ),
                    Row(
                      children: <Widget>[
                        const Text(
                          '28',
                          style: TextStyle(
                              fontSize: 42, fontWeight: FontWeight.bold),
                        ),
                        const SizedBox(width: 8),
                        ConstrainedBox(
                          constraints: const BoxConstraints(maxWidth: 100),
                          child: Text(
                            'agendamentos para a semana',
                            style: TextStyle(
                                fontSize: 12, color: Colors.grey[600]),
                          ),
                        ),
                      ],
                    ),
                    GestureDetector(
                      onTap: () {
                        Navigator.push(
                          context,
                          MaterialPageRoute<void>(
                            builder: (BuildContext context) =>
                                const NotificationsScreen(),
                          ),
                        );
                      },
                      child: Container(
                        decoration: BoxDecoration(
                          shape: BoxShape.circle,
                          color: Colors.white,
                          boxShadow: <BoxShadow>[
                            BoxShadow(
                              color: Colors.grey.withOpacity(0.5),
                              spreadRadius: 2,
                              blurRadius: 5,
                              offset: const Offset(0, 3),
                            ),
                          ],
                        ),
                        padding: const EdgeInsets.all(8),
                        child: const Icon(
                          Icons.notifications,
                          color: Colors.black,
                          size: 24,
                        ),
                      ),
                    ),
                  ],
                ),
                const SizedBox(height: 9),
                const Divider(
                  color: Colors.grey,
                  height: 1,
                ),
                const SizedBox(height: 26),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: <Widget>[
                    ColoredBorderTextButton(
                      text: 'Finanças',
                      textColor: Colors.white,
                      textSize: 16,
                      backgroundColor: Colors.blueAccent,
                      width: 50,
                      onPressed: () {
                        // TODO: Navigate to Finanças Screen
                      },
                    ),
                    ColoredBorderTextButton(
                      text: 'Estatísticas',
                      textColor: Colors.white,
                      textSize: 16,
                      backgroundColor: Colors.blueAccent,
                      width: 42,
                      onPressed: () {
                        // TODO: Navigate to Calendário Screen
                      },
                    ),
                  ],
                ),
                const SizedBox(height: 24),
                ColoredBorderTextButton(
                  text: 'Calendário',
                  textColor: Colors.white,
                  textSize: 16,
                  backgroundColor: Colors.blueAccent,
                  width: 130,
                  onPressed: () {
                    // TODO: Navigate to Estatísticas Screen
                  },
                ),
                const SizedBox(height: 22),
                const Text('Ver todos',
                    style:
                        TextStyle(fontSize: 22, fontWeight: FontWeight.bold)),
                const SizedBox(height: 20),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: <Widget>[
                    RoundedIconedButton(
                      icon: Icons.person,
                      text: 'Clientes',
                      size: 76,
                      bottom: 18,
                      right: 16,
                      color: Colors.blueAccent,
                      onPressed: () {
                        Navigator.push(
                          context,
                          MaterialPageRoute<void>(
                            builder: (BuildContext context) =>
                                const ClientsScreen(),
                          ),
                        );
                      },
                    ),
                    RoundedIconedButton(
                      icon: Icons.edit_calendar_outlined,
                      text: 'Lembretes',
                      size: 76,
                      bottom: 18,
                      right: 16,
                      color: Colors.blueAccent,
                      onPressed: () {
                        // TODO: Navigate to Contatos Screen
                      },
                    ),
                    RoundedIconedButton(
                      icon: Icons.schedule,
                      text: 'Agenda',
                      size: 76,
                      bottom: 18,
                      right: 16,
                      color: Colors.blueAccent,
                      onPressed: () {
                        // TODO: Navigate to Agendamentos Screen
                      },
                    ),
                    RoundedIconedButton(
                      icon: Icons.note_alt_rounded,
                      text: 'Atendimento',
                      size: 76,
                      bottom: 18,
                      right: 16,
                      color: Colors.blueAccent,
                      onPressed: () {
                        // TODO: Navigate to Atendimentos Screen
                      },
                    ),
                  ],
                ),
                const SizedBox(height: 28),
                ColoredBorderTextButton(
                  text: 'Mensalidades',
                  textColor: Colors.black,
                  textSize: 16,
                  backgroundColor: Colors.grey[300]!,
                  width: 100,
                  onPressed: () {
                    // TODO: Navigate to Mensalidades Screen
                  },
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
