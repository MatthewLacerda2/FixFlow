import 'package:flutter/material.dart';

//TODO:Transformar o circulo em caixa, e mover ele pra esquerda
class CheckInputField extends StatefulWidget {
  const CheckInputField({
    super.key,
    required this.label,
    required this.initialValue,
    required this.onChanged,
  });
  final String label;
  final bool initialValue;
  final Function(bool) onChanged;

  @override
  CheckInputFieldState createState() => CheckInputFieldState();
}

class CheckInputFieldState extends State<CheckInputField> {
  bool _isChecked = false;

  @override
  void initState() {
    super.initState();
    _isChecked = widget.initialValue;
  }

  @override
  Widget build(BuildContext context) {
    return Row(
      children: <Widget>[
        GestureDetector(
          onTap: () {
            setState(() {
              _isChecked = !_isChecked;
            });
            widget.onChanged(_isChecked);
          },
          child: Container(
            width: 22,
            height: 22,
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(3),
              border: Border.all(
                color: Colors.grey.shade600,
                width: 2.0,
              ),
              color: Colors.white,
            ),
            child: _isChecked
                ? Container(
                    margin: const EdgeInsets.all(1.8),
                    decoration: BoxDecoration(
                      color: Colors.blue,
                      borderRadius: BorderRadius.circular(3),
                    ),
                  )
                : null,
          ),
        ),
        const SizedBox(width: 10),
        Text(
          widget.label,
          style: const TextStyle(
            fontSize: 18,
          ),
        ),
      ],
    );
  }
}
