import 'package:flutter/material.dart';

class RoundedButton extends StatelessWidget {
  const RoundedButton(
      {super.key,
      required this.icon,
      required this.onPressed,
      required this.size,
      required this.bottom,
      required this.right,
      required this.color});
  final IconData icon;
  final VoidCallback onPressed;

  final double size;
  final double bottom;
  final double right;
  final Color color;

  @override
  Widget build(BuildContext context) {
    return Positioned(
      bottom: bottom,
      right: right,
      child: SizedBox(
        width: size,
        height: size,
        child: FloatingActionButton(
          onPressed: onPressed,
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(size / 6),
          ),
          elevation: 4,
          backgroundColor: color,
          child: Icon(
            icon,
            size: size / 1.4,
            color: Colors.white,
          ),
        ),
      ),
    );
  }
}
