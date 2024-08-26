import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import 'custom_input_field.dart';

class NameInputField extends StatelessWidget {
  final String placeholder;
  final Function(String) onNameChanged;

  NameInputField({
    required this.placeholder,
    required this.onNameChanged,
  });

  @override
  Widget build(BuildContext context) {
    return CustomInputField(
      placeholder: placeholder,
      onTextChanged: onNameChanged,
      inputType: TextInputType.name,
      inputFormatters: [
        FilteringTextInputFormatter.allow(RegExp(r'[a-zA-Z\s]')),
      ],
    );
  }
}
