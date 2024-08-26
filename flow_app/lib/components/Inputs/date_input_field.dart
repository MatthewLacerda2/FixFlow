import 'package:flutter/material.dart';

class DateInputField extends StatefulWidget {
  final String placeholder;
  final Function(DateTime) onDateSelected;

  DateInputField({
    required this.placeholder,
    required this.onDateSelected,
  });

  @override
  _DateInputFieldState createState() => _DateInputFieldState();
}

class _DateInputFieldState extends State<DateInputField> {
  TextEditingController _controller = TextEditingController();
  DateTime? _selectedDate;

  Future<void> _selectDate(BuildContext context) async {
    final DateTime? picked = await showDatePicker(
      context: context,
      initialDate: _selectedDate ?? DateTime.now(),
      firstDate: DateTime(2000),
      lastDate: DateTime(2101),
    );
    if (picked != null && picked != _selectedDate) {
      setState(() {
        _selectedDate = picked;
        _controller.text = "${picked.toLocal()}".split(' ')[0];
      });
      widget.onDateSelected(picked);
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
          icon: Icon(Icons.calendar_today, color: Colors.grey.shade600),
          onPressed: () => _selectDate(context),
        ),
      ),
      readOnly: true,
      onTap: () => _selectDate(context),
    );
  }
}
