import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import 'custom_input_field.dart';

//TODO: make the cursor jump once we type a section
class PhoneInputField extends StatelessWidget {
  const PhoneInputField({
    super.key,
    required this.placeholder,
    required this.onPhoneChanged,
  });
  final String placeholder;
  final Function(String) onPhoneChanged;

  @override
  Widget build(BuildContext context) {
    return CustomInputField(
      placeholder: placeholder,
      onTextChanged: onPhoneChanged,
      inputType: TextInputType.phone,
      inputFormatters: <TextInputFormatter>[
        FilteringTextInputFormatter.digitsOnly,
        _PhoneInputFormatter(),
      ],
    );
  }
}

class _PhoneInputFormatter extends TextInputFormatter {
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
      if (i == 0) formatted += '(';
      if (i == 2) formatted += ') ';
      if (i == 7) formatted += '-';
      formatted += digits[i];
    }
    return newValue.copyWith(
      text: formatted,
      selection: TextSelection.collapsed(offset: formatted.length),
    );
  }
}
