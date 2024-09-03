import 'package:flutter/material.dart';

class ColoredBorderTextButton extends StatelessWidget {
  const ColoredBorderTextButton({
    super.key,
    required this.text,
    required this.onPressed,
    required this.textColor,
    this.width,
  });

  final String text;
  final double? width;
  final VoidCallback onPressed;
  final Color textColor;

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        border: Border.all(color: textColor, width: 2),
        borderRadius: BorderRadius.circular(8),
      ),
      child: TextButton(
        onPressed: onPressed,
        style: TextButton.styleFrom(
          padding: EdgeInsets.symmetric(horizontal: width ?? 10, vertical: 1),
        ),
        child: Text(
          text,
          style: TextStyle(
            color: textColor,
            fontWeight: FontWeight.bold,
            fontSize: 15,
          ),
        ),
      ),
    );
  }
}
