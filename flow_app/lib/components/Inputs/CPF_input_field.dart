import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import 'custom_input_field.dart';

class CpfInputField extends StatelessWidget {
  const CpfInputField({
    super.key,
    required this.placeholder,
    required this.onCPFChanged,
  });
  final String placeholder;
  final Function(String) onCPFChanged;

  @override
  Widget build(BuildContext context) {
    return CustomInputField(
      placeholder: placeholder,
      onTextChanged: onCPFChanged,
      inputType: TextInputType.number,
      inputFormatters: <TextInputFormatter>[
        FilteringTextInputFormatter.digitsOnly,
        _CpfInputFormatter(),
      ],
    );
  }
}

class _CpfInputFormatter extends TextInputFormatter {
  @override
  TextEditingValue formatEditUpdate(
    TextEditingValue oldValue,
    TextEditingValue newValue,
  ) {
    if (newValue.text.length > 14) {
      return oldValue;
    }
    final String digits = newValue.text.replaceAll(RegExp(r'\D'), '');
    String formatted = '';
    for (int i = 0; i < digits.length; i++) {
      if (i == 3 || i == 6) formatted += '.';
      if (i == 9) formatted += '-';
      formatted += digits[i];
    }
    return newValue.copyWith(
      text: formatted,
      selection: TextSelection.collapsed(offset: formatted.length),
    );
  }
}
