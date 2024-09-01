import 'package:flutter/material.dart';

import '../main/account_screen.dart';

class ChangeSuccessfulScreen extends StatelessWidget {
  final String title;
  final String description;

  const ChangeSuccessfulScreen({
    Key? key,
    required this.title,
    required this.description,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(32.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            const Icon(
              Icons.check_circle,
              color: Colors.black,
              size: 120,
            ),
            const SizedBox(height: 48),
            Text(
              title,
              style: const TextStyle(
                fontWeight: FontWeight.bold,
                fontSize: 30,
              ),
            ),
            const SizedBox(height: 18),
            Text(
              description,
              style: const TextStyle(
                fontSize: 18,
              ),
            ),
            const SizedBox(height: 80),
            ElevatedButton(
              onPressed: () {
                Navigator.pushAndRemoveUntil(
                  context,
                  MaterialPageRoute<void>(
                      builder: (BuildContext context) => const AccountScreen()),
                  (Route<dynamic> route) => false,
                );
              },
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.white,
                side: const BorderSide(width: 2),
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 6),
              ),
              child: const Text(
                'Finalizar',
                style: TextStyle(
                  color: Colors.black,
                  fontSize: 18,
                  fontWeight: FontWeight.bold,
                ),
              ),
            )
          ],
        ),
      ),
    );
  }
}
