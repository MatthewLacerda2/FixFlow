import 'package:flutter/material.dart';

class LimitedTextInputField extends StatefulWidget {
  final TextEditingController controller;
  final int maxLength;
  final String labelText;
  final Function(String) onChanged;

  LimitedTextInputField({
    required this.controller,
    required this.maxLength,
    required this.labelText,
    required this.onChanged,
  });

  @override
  _LimitedTextInputFieldState createState() => _LimitedTextInputFieldState();
}

class _LimitedTextInputFieldState extends State<LimitedTextInputField> {
  @override
  Widget build(BuildContext context) {
    return TextField(
      controller: widget.controller,
      maxLength: widget.maxLength,
      decoration: InputDecoration(
        labelText: widget.labelText,
      ),
      onChanged: (value) {
        widget.onChanged(value);
      },
    );
  }
}
