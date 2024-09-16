import 'package:flutter/material.dart';

class TimePickerRectangle extends StatefulWidget {
  const TimePickerRectangle(
      {super.key, required this.initialTime, required this.onTimeSelected});

  final TimeOfDay initialTime;

  final Function(TimeOfDay) onTimeSelected;

  @override
  TimePickerRectangleState createState() => TimePickerRectangleState();
}

class TimePickerRectangleState extends State<TimePickerRectangle> {
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
          backgroundColor: Colors.white,
          padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
          shape: const RoundedRectangleBorder(
            side: BorderSide(color: Colors.grey),
          ),
        ),
        child: Text(
          '${_selectedTime!.hour.toString().padLeft(2, '0')}:${_selectedTime!.minute.toString().padLeft(2, '0')}h',
          style: TextStyle(
              fontSize: 18,
              color: Colors.grey[900],
              fontWeight: FontWeight.normal),
        ),
      ),
    );
  }
}
