import 'package:flutter/material.dart';

import 'rounded_iconed_button.dart';

class OrderButton extends StatelessWidget {
  const OrderButton({
    super.key,
    required this.icon,
    required this.iconSize,
    required this.iconColor,
    required this.onToggle,
    required this.sort,
    this.isUp,
  });

  final IconData icon;
  final double iconSize;
  final Color iconColor;
  final bool? isUp;
  final String sort;
  final Function(String key, bool isUp) onToggle;

  @override
  Widget build(BuildContext context) {
    return Row(
      children: <Widget>[
        RoundedIconedButton(
          icon: icon,
          onPressed: () {
            final bool newState = (isUp == null) ? true : !isUp!;
            onToggle(sort, newState);
          },
          size: iconSize,
          bottom: 0,
          right: 0,
          color: iconColor,
        ),
        isUp == null
            ? SizedBox(
                width: iconSize,
                height: iconSize,
              )
            : Icon(
                isUp == true ? Icons.arrow_upward : Icons.arrow_downward,
                size: iconSize,
                color: Colors.black,
              ),
        SizedBox(height: iconSize, width: 10),
      ],
    );
  }
}
