import 'package:flutter/material.dart';

class CheckInputField extends StatefulWidget {
  final String label;
  final bool initialValue;
  final Function(bool) onChanged;

  CheckInputField({
    required this.label,
    required this.initialValue,
    required this.onChanged,
  });

  @override
  _CheckInputFieldState createState() => _CheckInputFieldState();
}

class _CheckInputFieldState extends State<CheckInputField> {
  bool _isChecked = false;

  @override
  void initState() {
    super.initState();
    _isChecked = widget.initialValue;
  }

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisSize: MainAxisSize.max,
      children: [
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
              style: TextStyle(
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
                    margin: EdgeInsets.all(3.0),
                    decoration: BoxDecoration(
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
