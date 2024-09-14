import 'package:flutter/material.dart';

import 'rounded_iconed_button.dart';

class OrderButton extends StatelessWidget {
  const OrderButton({
    super.key,
    required this.icon,
    required this.iconSize,
    required this.iconColor,
    this.isUp,
  });
  final IconData icon;
  final double iconSize;
  final Color iconColor;
  final bool? isUp;

  @override
  Widget build(BuildContext context) {
    return Row(
      children: <Widget>[
        RoundedIconedButton(
          icon: icon,
          onPressed: () {},
          size: iconSize,
          bottom: 0,
          right: 0,
          color: iconColor,
        ),
        isUp == null
            ? SizedBox(
                width: (isUp == null) ? iconSize - 10 : iconSize,
                height: iconSize,
              )
            : Icon(
                isUp == true ? Icons.arrow_upward : Icons.arrow_downward,
                size: iconSize,
                color: Colors.black,
              ),
        SizedBox(height: iconSize, width: 10)
      ],
    );
  }
}
