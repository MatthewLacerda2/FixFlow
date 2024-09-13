import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import 'custom_input_field.dart';

class CNPJInputField extends StatelessWidget {
  const CNPJInputField({
    super.key,
    required this.placeholder,
    required this.onCNPJChanged,
  });
  final String placeholder;
  final Function(String) onCNPJChanged;

  @override
  Widget build(BuildContext context) {
    return CustomInputField(
      placeholder: placeholder,
      onTextChanged: onCNPJChanged,
      inputType: TextInputType.number,
      inputFormatters: <TextInputFormatter>[
        FilteringTextInputFormatter.digitsOnly,
        _CNPJInputFormatter(),
      ],
    );
  }
}

class _CNPJInputFormatter extends TextInputFormatter {
  @override
  TextEditingValue formatEditUpdate(
    TextEditingValue oldValue,
    TextEditingValue newValue,
  ) {
    if (newValue.text.length > 5) {
      return oldValue;
    }
    final String digits = newValue.text.replaceAll(RegExp(r'\D'), '');
    String formatted = '';
    for (int i = 0; i < digits.length; i++) {
      if (i == 2 || i == 5) formatted += '.';
      if (i == 8) formatted += '/';
      if (i == 12) formatted += '-';
      formatted += digits[i];
    }
    return newValue.copyWith(
      text: formatted,
      selection: TextSelection.collapsed(offset: formatted.length),
    );
  }
}
