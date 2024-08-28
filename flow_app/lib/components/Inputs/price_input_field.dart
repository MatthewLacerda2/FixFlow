import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class PriceInputField extends StatefulWidget {
  final TextEditingController controller;
  final Function(String) onPriceValid;

  PriceInputField({
    required this.controller,
    required this.onPriceValid,
  });

  @override
  _PriceInputFieldState createState() => _PriceInputFieldState();
}

class _PriceInputFieldState extends State<PriceInputField> {
  @override
  Widget build(BuildContext context) {
    return TextField(
      controller: widget.controller,
      keyboardType: TextInputType.numberWithOptions(decimal: true),
      decoration: InputDecoration(
        labelText: 'Preço: R\$',
      ),
      inputFormatters: [
        FilteringTextInputFormatter.allow(RegExp(r'^\d+\.?\d{0,2}')),
      ],
      onChanged: (value) {
        if (double.tryParse(value) != null) {
          widget.onPriceValid(value);
        }
      },
    );
  }
}
