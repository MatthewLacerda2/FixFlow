import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

class CalendarScreen extends StatefulWidget {
  const CalendarScreen({super.key});

  @override
  _CalendarScreenState createState() => _CalendarScreenState();
}

class _CalendarScreenState extends State<CalendarScreen> {
  // Mock of calendar events
  Map<int, List<String>> schedules = <int, List<String>>{
    3: <String>['9:00 AM - Schedule 1', '1:00 PM - Schedule 2'],
    7: <String>['Appointment at 10:00 AM'],
    10: <String>['Idle period'],
    15: <String>['Holiday: Independence Day'],
  };

  DateTime currentDate = DateTime.now();
  int selectedDate = DateTime.now().day;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Calendar'),
      ),
      body: Column(
        children: <Widget>[
          // Month header
          Padding(
            padding: const EdgeInsets.symmetric(vertical: 8.0),
            child: Text(
              DateFormat('MMMM y').format(currentDate),
              style: const TextStyle(
                fontWeight: FontWeight.bold,
                fontSize: 24,
              ),
            ),
          ),
          // Day of the week labels
          _buildWeekDayLabels(),
          // Calendar Header (Month view)
          Expanded(
            child: PageView.builder(
              itemCount: 12, // 12 months
              controller: PageController(initialPage: currentDate.month - 1),
              onPageChanged: (int index) {
                setState(() {
                  currentDate = DateTime(currentDate.year, index + 1);
                });
              },
              itemBuilder: (BuildContext context, int index) {
                return _buildMonthView(index + 1);
              },
            ),
          ),
          // Selected Date Details
          _buildDetailsSection(),
        ],
      ),
    );
  }

  // Builds the labels for the days of the week
  Widget _buildWeekDayLabels() {
    const List<String> daysOfWeek = <String>['S', 'M', 'T', 'W', 'T', 'F', 'S'];
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceAround,
      children: daysOfWeek
          .map((String day) => Expanded(
                child: Center(
                  child: Text(
                    day,
                    style: const TextStyle(
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
              ))
          .toList(),
    );
  }

  // Builds the calendar view for the month
  Widget _buildMonthView(int month) {
    final DateTime firstDayOfMonth = DateTime(currentDate.year, month, 1);
    final int daysInMonth = DateUtils.getDaysInMonth(currentDate.year, month);
    final int firstWeekday =
        firstDayOfMonth.weekday; // Monday is 1, Sunday is 7

    return GridView.builder(
      padding: const EdgeInsets.symmetric(horizontal: 16), // Reduced padding
      gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
        crossAxisCount: 7,
        crossAxisSpacing: 4,
        mainAxisSpacing: 4,
      ),
      itemCount: daysInMonth +
          (firstWeekday - 1), // Fill with empty spaces for days before 1st
      itemBuilder: (BuildContext context, int index) {
        if (index < firstWeekday - 1) {
          return const SizedBox(); // Empty slots for the first week
        } else {
          final int day = index - (firstWeekday - 2);
          return _buildDayCell(day);
        }
      },
    );
  }

  // Builds each date cell
  Widget _buildDayCell(int day) {
    final List<String> dayEvents = schedules[day] ?? <String>[];

    final bool isIdle = dayEvents.contains('Idle period');
    final bool isHoliday =
        dayEvents.any((String event) => event.contains('Holiday'));
    final int scheduleCount =
        dayEvents.where((String event) => event.contains('Schedule')).length;
    final int appointmentCount =
        dayEvents.where((String event) => event.contains('Appointment')).length;

    Color dayColor = Colors.black;
    if (isIdle) {
      dayColor = Colors.red;
    } else if (isHoliday) {
      dayColor = Colors.orange;
    }

    return GestureDetector(
      onTap: () {
        setState(() {
          selectedDate = day;
        });
      },
      child: Container(
        decoration: BoxDecoration(
          border: Border.all(color: Colors.grey),
          borderRadius: BorderRadius.circular(8),
        ),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Text(
              '$day',
              style: TextStyle(
                fontWeight: FontWeight.bold,
                color: dayColor,
              ),
            ),
            if (scheduleCount > 0)
              Text(
                '$scheduleCount',
                style: const TextStyle(color: Colors.green),
              ),
            if (appointmentCount > 0)
              Text(
                '$appointmentCount',
                style: const TextStyle(color: Colors.blue),
              ),
          ],
        ),
      ),
    );
  }

  // Builds the section below showing what's up for the selected date
  Widget _buildDetailsSection() {
    return schedules.containsKey(selectedDate)
        ? Container(
            padding: const EdgeInsets.symmetric(
                horizontal: 16, vertical: 1), // Reduced padding
            decoration: const BoxDecoration(
              color: Colors.white,
              borderRadius: BorderRadius.vertical(top: Radius.circular(16)),
            ),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: <Widget>[
                Text(
                  'Events for Day $selectedDate:',
                  style: const TextStyle(
                      fontWeight: FontWeight.bold, fontSize: 16),
                ),
                const SizedBox(height: 8),
                ...schedules[selectedDate]!.map((String event) {
                  return Padding(
                    padding: const EdgeInsets.symmetric(vertical: 4),
                    child: Text(event),
                  );
                }),
              ],
            ),
          )
        : const SizedBox();
  }
}
