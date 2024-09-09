import 'package:flutter/material.dart';

class NotificationsScreen extends StatelessWidget {
  const NotificationsScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: <Widget>[
              const Row(
                children: <Widget>[
                  Icon(Icons.notifications_active_outlined, size: 28),
                  SizedBox(width: 8),
                  Text(
                    'Notificações',
                    style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
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
                    height: 5,
                  ),
                  itemBuilder: (BuildContext context, int index) {
                    return ListTile(
                      leading:
                          const Icon(Icons.error_outline, color: Colors.grey),
                      title: Text('Something $index'),
                      subtitle:
                          const Text('Agendamento para daqui 10 minutos!'),
                    );
                  },
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
