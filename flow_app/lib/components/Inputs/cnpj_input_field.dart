import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import 'custom_input_field.dart';

class CNPJInputField extends StatelessWidget {
  final String placeholder;
  final Function(String) onCNPJChanged;

  CNPJInputField({
    required this.placeholder,
    required this.onCNPJChanged,
  });

  @override
  Widget build(BuildContext context) {
    return CustomInputField(
      placeholder: placeholder,
      onTextChanged: onCNPJChanged,
      inputType: TextInputType.number,
      inputFormatters: [
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
    final digits = newValue.text.replaceAll(RegExp(r'\D'), '');
    var formatted = '';
    for (var i = 0; i < digits.length; i++) {
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
