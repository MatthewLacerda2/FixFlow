import 'package:flutter/material.dart';

class RoundedIconedButton extends StatelessWidget {
  const RoundedIconedButton({
    super.key,
    required this.icon,
    required this.onPressed,
    required this.size,
    required this.bottom,
    required this.right,
    required this.color,
    this.text,
  });

  final IconData icon;
  final VoidCallback onPressed;
  final double size;
  final double bottom;
  final double right;
  final Color color;
  final String? text;

  @override
  Widget build(BuildContext context) {
    return Positioned(
      bottom: bottom,
      right: right,
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: <Widget>[
          SizedBox(
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
          if (text != null) ...<Widget>[
            const SizedBox(height: 6),
            Text(
              text!,
              textAlign: TextAlign.center,
              style: TextStyle(
                color: Colors.black,
                fontSize: size / 5.5,
              ),
            ),
          ],
        ],
      ),
    );
  }
}
