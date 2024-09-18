import 'package:flutter/material.dart';

import '../auth/initial_screen.dart';

class LeaveSuccessfulScreen extends StatelessWidget {
  const LeaveSuccessfulScreen({
    super.key,
    required this.title,
    required this.description,
  });
  final String title;
  final String description;

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
            const SizedBox(height: 60),
            Text(
              title,
              textAlign: TextAlign.center,
              style: const TextStyle(
                fontWeight: FontWeight.bold,
                fontSize: 30,
              ),
            ),
            const SizedBox(height: 23),
            Text(
              description,
              textAlign: TextAlign.justify,
              style: const TextStyle(
                fontSize: 16,
              ),
            ),
            const SizedBox(height: 64),
            ElevatedButton(
              onPressed: () {
                Navigator.pushReplacement(
                  context,
                  MaterialPageRoute<void>(
                      builder: (BuildContext context) => const InitialScreen()),
                  //TODO: make it drop your data before leaving
                );
              },
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.white,
                side: const BorderSide(width: 1.7),
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
