import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class CustomInputField extends StatelessWidget {
  const CustomInputField({
    super.key,
    required this.placeholder,
    required this.onTextChanged,
    this.inputType = TextInputType.text,
    this.inputFormatters,
  });
  final String placeholder;
  final Function(String) onTextChanged;
  final TextInputType inputType;
  final List<TextInputFormatter>? inputFormatters;

  @override
  Widget build(BuildContext context) {
    return TextField(
      keyboardType: inputType,
      inputFormatters: inputFormatters,
      decoration: InputDecoration(
        hintText: placeholder,
        filled: true,
        fillColor: Colors.grey.shade300,
        border: InputBorder.none,
        enabledBorder: UnderlineInputBorder(
          borderSide: BorderSide(color: Colors.grey.shade600),
        ),
        focusedBorder: UnderlineInputBorder(
          borderSide: BorderSide(color: Colors.grey.shade600),
        ),
      ),
      onChanged: onTextChanged,
    );
  }
}
