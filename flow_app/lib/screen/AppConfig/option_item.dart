import 'package:flutter/material.dart';

class OptionItem extends StatelessWidget {
  const OptionItem({
    super.key,
    required this.title,
    required this.onTap,
    this.titleStyle,
  });
  final String title;
  final VoidCallback onTap;
  final TextStyle? titleStyle;

  @override
  Widget build(BuildContext context) {
    return Column(
      children: <Widget>[
        Container(
          height: 1,
          color: Colors.grey,
        ),
        ListTile(
          title: Text(
            title,
            style: titleStyle,
          ),
          trailing: const Icon(Icons.arrow_forward_ios, size: 16),
          onTap: onTap,
        ),
      ],
    );
  }
}
