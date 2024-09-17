import 'package:flutter/material.dart';

class RoundedInfoBox extends StatelessWidget {
  const RoundedInfoBox({
    super.key,
    this.firstIcon,
    required this.secondIcon,
    required this.text,
    required this.height,
    this.width,
  });
  final IconData? firstIcon;
  final IconData secondIcon;
  final String text;
  final double height;
  final double? width;

  @override
  Widget build(BuildContext context) {
    final double boxWidth = width ?? height;

    return Container(
      width: boxWidth,
      height: height,
      padding: const EdgeInsets.all(8.0),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(12.0),
        boxShadow: <BoxShadow>[
          BoxShadow(
            color: Colors.grey.withOpacity(0.33),
            spreadRadius: 2,
            blurRadius: 5,
            offset: const Offset(0, 3),
          ),
        ],
      ),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: <Widget>[
          Row(
            mainAxisAlignment: width != null
                ? MainAxisAlignment.start
                : MainAxisAlignment.center,
            children: <Widget>[
              if (firstIcon != null) Icon(firstIcon, size: height / 3),
              Icon(secondIcon, size: height / 3),
            ],
          ),
          const SizedBox(height: 10),
          Text(
            text,
            style: const TextStyle(
              color: Colors.grey,
            ),
            textAlign: width != null ? TextAlign.start : TextAlign.center,
          ),
        ],
      ),
    );
  }
}
