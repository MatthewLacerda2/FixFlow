import 'package:flutter/material.dart';

class HourInputField extends StatefulWidget {
  final String placeholder;
  final Function(TimeOfDay) onTimeSelected;

  HourInputField({
    required this.placeholder,
    required this.onTimeSelected,
  });

  @override
  _HourInputFieldState createState() => _HourInputFieldState();
}

class _HourInputFieldState extends State<HourInputField> {
  TextEditingController _controller = TextEditingController();
  TimeOfDay? _selectedTime;

  Future<void> _selectTime(BuildContext context) async {
    final TimeOfDay? picked = await showTimePicker(
      context: context,
      initialTime: _selectedTime ?? TimeOfDay.now(),
    );
    if (picked != null && picked != _selectedTime) {
      setState(() {
        _selectedTime = picked;
        _controller.text = picked.format(context);
      });
      widget.onTimeSelected(picked);
    }
  }

  @override
  Widget build(BuildContext context) {
    return TextField(
      controller: _controller,
      decoration: InputDecoration(
        hintText: widget.placeholder,
        filled: true,
        fillColor: Colors.grey.shade300,
        border: InputBorder.none,
        enabledBorder: UnderlineInputBorder(
          borderSide: BorderSide(color: Colors.grey.shade600),
        ),
        focusedBorder: UnderlineInputBorder(
          borderSide: BorderSide(color: Colors.grey.shade600),
        ),
        suffixIcon: IconButton(
          icon: Icon(Icons.access_time, color: Colors.grey.shade600),
          onPressed: () => _selectTime(context),
        ),
      ),
      readOnly: true,
      onTap: () => _selectTime(context),
    );
  }
}
