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
            Text(
              'Notificações',
              style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
            ),
            SizedBox(height: 20),
            Expanded(
              child: ListView.separated(
                itemCount: 10, // Example: 10 notifications
                separatorBuilder: (context, index) => Divider(
                  color: Colors.grey,
                  thickness: 1,
                ),
                itemBuilder: (context, index) {
                  return ListTile(
                    leading: Icon(Icons.error_outline,
                        color: Colors.grey), // Exclamation mark icon
                    title: Text('Something $index'),
                    subtitle: Text('Agendamento para daqui 10 minutos!'),
                    onTap: () {
                      // TODO: Handle notification tap
                    },
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
