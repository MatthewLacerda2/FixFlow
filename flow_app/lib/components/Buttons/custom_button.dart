import 'package:flutter/material.dart';

class CustomButton extends StatelessWidget {
  const CustomButton({
    super.key,
    required this.text,
    required this.textSize,
    this.textColor,
    required this.backgroundColor,
    this.borderColor,
    required this.borderRadius,
    this.borderWidth,
    required this.padding,
    required this.onPressed,
  });
  final String text;
  final double textSize;
  final Color? textColor;
  final Color backgroundColor;
  final Color? borderColor;
  final double borderRadius;
  final double? borderWidth;
  final EdgeInsets padding;
  final VoidCallback onPressed;

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        color: backgroundColor,
        border: Border.all(
            color: borderColor ?? Colors.black, width: borderWidth ?? 1.6),
        borderRadius: BorderRadius.circular(borderRadius),
      ),
      child: TextButton(
        onPressed: onPressed,
        style: TextButton.styleFrom(
          padding: padding,
        ),
        child: Text(
          text,
          style: TextStyle(
            color: textColor ?? Colors.black,
            fontSize: textSize,
            fontWeight: FontWeight.bold,
          ),
        ),
      ),
    );
  }
}
