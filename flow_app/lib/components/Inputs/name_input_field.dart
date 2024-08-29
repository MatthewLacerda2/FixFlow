import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import 'custom_input_field.dart';

/// Input field for first-name or full-name
class NameInputField extends StatelessWidget {
  const NameInputField({
    super.key,
    required this.placeholder,
    required this.onNameChanged,
  });

  final String placeholder;

  final Function(String) onNameChanged;

  @override
  Widget build(BuildContext context) {
    return CustomInputField(
      placeholder: placeholder,
      onTextChanged: onNameChanged,
      inputType: TextInputType.name,
      inputFormatters: <TextInputFormatter>[
        FilteringTextInputFormatter.allow(RegExp(r'[a-zA-Z\s]')),
      ],
    );
  }
}
