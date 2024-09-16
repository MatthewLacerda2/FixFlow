import 'package:flutter/material.dart';

class DatePickerRectangle extends StatefulWidget {
  const DatePickerRectangle(
      {super.key, required this.initialDate, required this.onDateSelected});
  final DateTime initialDate;
  final Function(DateTime) onDateSelected;

  @override
  DatePickerRectangleState createState() => DatePickerRectangleState();
}

class DatePickerRectangleState extends State<DatePickerRectangle> {
  DateTime? _selectedDate;

  @override
  void initState() {
    super.initState();
    _selectedDate = widget.initialDate;
  }

  Future<void> _selectDate(BuildContext context) async {
    final DateTime? picked = await showDatePicker(
      context: context,
      initialDate: _selectedDate ?? DateTime.now(),
      firstDate: DateTime(2000),
      lastDate: DateTime(2100),
    );
    if (picked != null && picked != _selectedDate) {
      setState(() {
        _selectedDate = picked;
        widget.onDateSelected(_selectedDate!);
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Center(
      child: ElevatedButton(
        onPressed: () => _selectDate(context),
        style: ElevatedButton.styleFrom(
          backgroundColor: Colors.white,
          padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
          shape: RoundedRectangleBorder(
            side: const BorderSide(color: Colors.grey),
          ),
        ),
        child: Text(
          '${_selectedDate!.day.toString().padLeft(2, '0')}/${_selectedDate!.month.toString().padLeft(2, '0')}/${_selectedDate!.year}',
          style: const TextStyle(
              fontSize: 18,
              color: Color.fromARGB(255, 70, 70, 70),
              fontWeight: FontWeight.normal),
        ),
      ),
    );
  }
}
