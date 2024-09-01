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
        Expanded(
          child: GestureDetector(
            onTap: () {
              setState(() {
                _isChecked = !_isChecked;
              });
              widget.onChanged(_isChecked);
            },
            child: Text(
              widget.label,
              style: const TextStyle(
                fontSize: 17,
              ),
            ),
          ),
        ),
        GestureDetector(
          onTap: () {
            setState(() {
              _isChecked = !_isChecked;
            });
            widget.onChanged(_isChecked);
          },
          child: Container(
            width: 24,
            height: 24,
            decoration: BoxDecoration(
              shape: BoxShape.circle,
              border: Border.all(
                color: Colors.grey.shade600,
                width: 2.0,
              ),
              color: Colors.white,
            ),
            child: _isChecked
                ? Container(
                    margin: const EdgeInsets.all(3.0),
                    decoration: const BoxDecoration(
                      shape: BoxShape.circle,
                      color: Colors.blue,
                    ),
                  )
                : null,
          ),
        ),
      ],
    );
  }
}
