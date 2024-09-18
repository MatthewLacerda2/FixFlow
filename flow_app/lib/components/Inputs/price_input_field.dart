import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class PriceInputField extends StatefulWidget {
  const PriceInputField({
    super.key,
    required this.controller,
    required this.onPriceValid,
    this.initialPrice,
  });

  final TextEditingController controller;
  final Function(String) onPriceValid;
  final double? initialPrice;

  @override
  PriceInputFieldState createState() => PriceInputFieldState();
}

class PriceInputFieldState extends State<PriceInputField> {

  @override
  void initState() {
    super.initState();
    if (widget.initialPrice != null) {
      widget.controller.text = widget.initialPrice!.toStringAsFixed(2);
    }
  }

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
