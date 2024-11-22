import 'package:flutter/material.dart';

class LimitedTextInputField extends StatefulWidget {
  const LimitedTextInputField({
    super.key,
    required this.controller,
    required this.maxLength,
    this.labelText,
    required this.onChanged,
  });

  final TextEditingController controller;

  final int maxLength;

  final String? labelText;

  final Function(String) onChanged;

  @override
  LimitedTextInputFieldState createState() => LimitedTextInputFieldState();
}

class LimitedTextInputFieldState extends State<LimitedTextInputField> {
  @override
  Widget build(BuildContext context) {
    return TextField(
      controller: widget.controller,
      maxLength: widget.maxLength,
      decoration: InputDecoration(
        labelText: widget.labelText,
      ),
      onChanged: (String value) {
        widget.onChanged(value);
      },
    );
  }
}
