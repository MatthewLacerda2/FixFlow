import 'package:flutter/material.dart';

class PasswordInputField extends StatefulWidget {
  final String placeholder;
  final Function(String) onPasswordChanged;

  PasswordInputField({
    required this.placeholder,
    required this.onPasswordChanged,
  });

  @override
  _PasswordInputFieldState createState() => _PasswordInputFieldState();
}

class _PasswordInputFieldState extends State<PasswordInputField> {
  bool _obscureText = true;

  void _toggleVisibility() {
    setState(() {
      _obscureText = !_obscureText;
    });
  }

  @override
  Widget build(BuildContext context) {
    return TextField(
      obscureText: _obscureText,
      decoration: InputDecoration(
        hintText: widget.placeholder,
        filled: true,
        fillColor: Colors.grey.shade300,
        border: InputBorder.none,
        enabledBorder: UnderlineInputBorder(
          borderSide: BorderSide(color: Colors.grey.shade600),
        ),
        focusedBorder: UnderlineInputBorder(
          borderSide: BorderSide(color: Colors.grey.shade600),
        ),
        suffixIcon: IconButton(
          icon: Icon(
            _obscureText ? Icons.visibility : Icons.visibility_off,
            color: Colors.grey.shade600,
          ),
          onPressed: _toggleVisibility,
        ),
      ),
      onChanged: widget.onPasswordChanged,
    );
  }
}
