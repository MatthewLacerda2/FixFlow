import 'package:email_validator/email_validator.dart';
import 'package:flutter/material.dart';

import 'custom_input_field.dart';

class EmailInputField extends StatefulWidget {
  final String placeholder;
  final Function(String) onEmailValidated;

  EmailInputField({
    required this.placeholder,
    required this.onEmailValidated,
  });

  @override
  _EmailInputFieldState createState() => _EmailInputFieldState();
}

class _EmailInputFieldState extends State<EmailInputField> {
  bool isValid = true;

  void validateEmail(String email) {
    setState(() {
      isValid = EmailValidator.validate(email);
    });

    if (isValid) {
      widget.onEmailValidated(email);
    }
  }

  @override
  Widget build(BuildContext context) {
    return CustomInputField(
      placeholder: widget.placeholder,
      onTextChanged: (text) {
        validateEmail(text);
      },
      inputType: TextInputType.emailAddress,
      inputFormatters: [],
    );
  }
}
