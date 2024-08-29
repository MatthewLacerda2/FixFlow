import 'package:flutter/material.dart';

/// A Text field with a set limit number of characters
class LimitedTextInputField extends StatefulWidget {
  const LimitedTextInputField({
    super.key,
    required this.controller,
    required this.maxLength,
    this.labelText,
    required this.onChanged,
  });

  final TextEditingController controller;

  /// The maximum number of characters
  final int maxLength;

  /// The text in the field. Can be used to setup a initial text
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
