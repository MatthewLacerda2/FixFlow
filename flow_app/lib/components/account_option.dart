import 'package:flutter/material.dart';

class AccountOption extends StatelessWidget {
  final String title;
  final VoidCallback onTap;

  const AccountOption({required this.title, required this.onTap});

  @override
  Widget build(BuildContext context) {
    return ListTile(
      title: Text(title),
      trailing: Icon(Icons.arrow_forward_ios),
      onTap: onTap,
    );
  }
}
