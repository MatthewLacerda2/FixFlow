import 'package:flutter/material.dart';

class CustomButton extends StatelessWidget {
  const CustomButton({
    super.key,
    required this.text,
    required this.textSize,
    this.textColor,
    required this.backgroundColor,
    this.borderColor,
    this.borderRadius,
    this.borderWidth,
    this.padding,
    required this.onPressed,
  });
  final String text;
  final double textSize;
  final Color? textColor;
  final Color backgroundColor;
  final Color? borderColor;
  final double? borderRadius;
  final double? borderWidth;
  final EdgeInsets? padding;
  final VoidCallback onPressed;

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        color: backgroundColor,
        border: Border.all(
            color: borderColor ?? Colors.black, width: borderWidth ?? 1.6),
        borderRadius: BorderRadius.circular(borderRadius ?? 12),
      ),
      child: TextButton(
        onPressed: onPressed,
        style: TextButton.styleFrom(
          padding: padding ??
              const EdgeInsets.symmetric(vertical: 1, horizontal: 30),
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
