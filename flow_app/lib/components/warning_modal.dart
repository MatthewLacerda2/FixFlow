import 'package:flutter/material.dart';

class WarningModal extends StatelessWidget {
  const WarningModal({
    super.key,
    required this.title,
    this.optionOne,
    this.optionTwo,
    this.backgroundColor,
    this.description,
  });
  final String title;
  final String? description;
  final Widget? optionOne;
  final Widget? optionTwo;
  final Color? backgroundColor;

  @override
  Widget build(BuildContext context) {
    return Dialog(
      backgroundColor: backgroundColor ?? Colors.white,
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(10),
      ),
      child: Container(
        padding: const EdgeInsets.all(16),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: <Widget>[
            Text(
              title,
              style: const TextStyle(
                fontSize: 22,
                fontWeight: FontWeight.bold,
              ),
            ),
            const SizedBox(height: 10),
            if (description != null)
              Text(
                description!,
                style: TextStyle(
                  fontSize: 14,
                  color: Colors.grey[700],
                ),
              ),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                if (optionOne != null) optionOne!,
                if (optionTwo != null) optionTwo!,
              ],
            ),
            const SizedBox(height: 2),
          ],
        ),
      ),
    );
  }
}
