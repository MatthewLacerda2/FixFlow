import 'package:email_validator/email_validator.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import 'custom_input_field.dart';

class EmailInputField extends StatefulWidget {
  const EmailInputField({
    super.key,
    required this.placeholder,
    required this.onEmailValidated,
  });

  final String placeholder;

  final Function(String) onEmailValidated;

  @override
  EmailInputFieldState createState() => EmailInputFieldState();
}

class EmailInputFieldState extends State<EmailInputField> {
  bool isValidEmail = true;

  void validateEmail(String email) {
    setState(() {
      isValidEmail = EmailValidator.validate(email);
    });

    if (isValidEmail) {
      widget.onEmailValidated(email);
    }
  }

  @override
  Widget build(BuildContext context) {
    return CustomInputField(
      placeholder: widget.placeholder,
      onTextChanged: (String text) {
        validateEmail(text);
      },
      inputType: TextInputType.emailAddress,
      inputFormatters: const <TextInputFormatter>[],
    );
  }
}
