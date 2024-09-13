import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

//TODO:make it handle price by itself, and take an initial price
class PriceInputField extends StatefulWidget {
  const PriceInputField({
    super.key,
    required this.controller,
    required this.onPriceValid,
  });
  final TextEditingController controller;
  final Function(String) onPriceValid;

  @override
  PriceInputFieldState createState() => PriceInputFieldState();
}

class PriceInputFieldState extends State<PriceInputField> {
  @override
  Widget build(BuildContext context) {
    return TextField(
      controller: widget.controller,
      keyboardType: const TextInputType.numberWithOptions(decimal: true),
      decoration: const InputDecoration(
        labelText: 'Preço: R\$',
      ),
      inputFormatters: <TextInputFormatter>[
        FilteringTextInputFormatter.allow(RegExp(r'^\d+\.?\d{0,2}')),
      ],
      onChanged: (String value) {
        if (double.tryParse(value) != null) {
          widget.onPriceValid(value);
        }
      },
    );
  }
}
