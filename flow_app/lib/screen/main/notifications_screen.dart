import 'package:flutter/material.dart';

class NotificationsScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Row(
              children: [
                Icon(Icons.notifications_active_outlined, size: 28),
                SizedBox(width: 8),
                Text(
                  'Notificações',
                  style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                ),
              ],
            ),
            SizedBox(height: 14),
            Divider(),
            SizedBox(height: 6),
            Expanded(
              child: ListView.separated(
                itemCount: 10,
                separatorBuilder: (context, index) => Divider(
                  color: Colors.grey,
                  thickness: 1,
                  height: 8,
                ),
                itemBuilder: (context, index) {
                  return ListTile(
                    leading: Icon(Icons.error_outline, color: Colors.grey),
                    title: Text('Something $index'),
                    subtitle: Text('Agendamento para daqui 10 minutos!'),
                  );
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}
