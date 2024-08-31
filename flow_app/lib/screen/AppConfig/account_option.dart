import 'package:flutter/material.dart';

class AccountOption extends StatelessWidget {
  const AccountOption({
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
    return ListTile(
      title: Text(
        title,
        style: titleStyle,
      ),
      trailing: const Icon(Icons.arrow_forward_ios, size: 16),
      onTap: onTap,
    );
  }
}
