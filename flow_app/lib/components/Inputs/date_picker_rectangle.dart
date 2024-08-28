import 'package:flutter/material.dart';

class DatePickerRectangle extends StatefulWidget {
  final DateTime initialDate;

  DatePickerRectangle({required this.initialDate});

  @override
  _DatePickerRectangleState createState() => _DatePickerRectangleState();
}

class _DatePickerRectangleState extends State<DatePickerRectangle> {
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
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Center(
      child: ElevatedButton(
        onPressed: () => _selectDate(context),
        style: ElevatedButton.styleFrom(
          backgroundColor: const Color.fromARGB(255, 230, 230, 230),
          shadowColor: Colors.transparent,
          elevation: 0,
          padding: EdgeInsets.symmetric(horizontal: 15, vertical: 5),
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.zero, // Remove rounded corners
          ),
        ),
        child: Text(
          '${_selectedDate!.day.toString().padLeft(2, '0')}/${_selectedDate!.month.toString().padLeft(2, '0')}/${_selectedDate!.year}',
          style: const TextStyle(
              fontSize: 24, color: Colors.black, fontWeight: FontWeight.normal),
        ),
      ),
    );
  }
}