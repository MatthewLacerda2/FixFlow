import 'package:flutter/material.dart';

//TODO: password must contain uppercase letters, lowercase letters, numbers, special characters, hieroglyphics and the blood of a virgin
class PasswordInputField extends StatefulWidget {
  const PasswordInputField({
    super.key,
    required this.placeholder,
    this.onPasswordChanged,
  });
  final String placeholder;

  /// Function called when the value is changed
  final Function(String)? onPasswordChanged;

  @override
  PasswordInputFieldState createState() => PasswordInputFieldState();
}

class PasswordInputFieldState extends State<PasswordInputField> {
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
