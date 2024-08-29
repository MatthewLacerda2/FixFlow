import 'package:flutter/material.dart';

class TimePickerRectangle extends StatefulWidget {
  final TimeOfDay initialTime;
  final Function(TimeOfDay) onTimeSelected;

  TimePickerRectangle(
      {required this.initialTime, required this.onTimeSelected});

  @override
  _TimePickerRectangleState createState() => _TimePickerRectangleState();
}

class _TimePickerRectangleState extends State<TimePickerRectangle> {
  TimeOfDay? _selectedTime;

  @override
  void initState() {
    super.initState();
    _selectedTime = widget.initialTime;
  }

  Future<void> _selectTime(BuildContext context) async {
    final TimeOfDay? picked = await showTimePicker(
      context: context,
      initialTime: _selectedTime ?? TimeOfDay.now(),
    );
    if (picked != null && picked != _selectedTime) {
      setState(() {
        _selectedTime = picked;
        widget.onTimeSelected(_selectedTime!);
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Center(
      child: ElevatedButton(
        onPressed: () => _selectTime(context),
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
          '${_selectedTime!.hourOfPeriod.toString().padLeft(2, '0')}:${_selectedTime!.minute.toString().padLeft(2, '0')} ${_selectedTime!.period == DayPeriod.am ? "| AM" : "| PM"}',
          style: const TextStyle(
              fontSize: 24, color: Colors.black, fontWeight: FontWeight.normal),
        ),
      ),
    );
  }
}
