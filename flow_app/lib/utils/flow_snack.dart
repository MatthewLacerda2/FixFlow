import 'package:flutter/material.dart';

class FlowSnack extends StatelessWidget {
  const FlowSnack({super.key, required this.message});

  final String message;

  static void show(BuildContext context, String message) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text(
          message,
          textAlign: TextAlign.center,
        ),
        behavior: SnackBarBehavior.floating,
        margin: const EdgeInsets.symmetric(horizontal: 12, vertical: 12),
        duration: const Duration(seconds: 3),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    show(context, message);
    return const SizedBox.shrink();
  }
}
