import 'package:flutter/material.dart';

class ColoredBorderTextButton extends StatelessWidget {
  const ColoredBorderTextButton({
    super.key,
    required this.text,
    required this.onPressed,
    required this.textColor,
    this.borderColor,
    this.backgroundColor,
    this.textSize,
    this.width,
  });

  final String text;
  final double? width;
  final VoidCallback onPressed;
  final Color textColor;
  final Color? borderColor;
  final double? textSize;
  final Color? backgroundColor;

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        border: Border.all(
            color: borderColor ?? backgroundColor ?? textColor, width: 1.8),
        color: backgroundColor ?? Colors.white,
        borderRadius: BorderRadius.circular(12),
      ),
      child: TextButton(
        onPressed: onPressed,
        style: TextButton.styleFrom(
          padding: EdgeInsets.symmetric(horizontal: width ?? 5),
        ),
        child: Text(
          text,
          style: TextStyle(
            color: textColor,
            fontWeight: FontWeight.bold,
            height: 0,
            fontSize: textSize ?? 15,
          ),
        ),
      ),
    );
  }
}
