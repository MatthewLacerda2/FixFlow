import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import '../../components/Buttons/rounded_iconed_button.dart';
import '../apts/edit_apt/create_log_screen.dart';
import '../apts/edit_apt/create_schedule_screen.dart';
//TODO: we got to think of a better way to let users do actions like creating, editing or deleting schedules, logs and idle periods
class CalendarScreen extends StatefulWidget {
  const CalendarScreen({super.key});

  @override
  CalendarScreenState createState() => CalendarScreenState();
}

class CalendarScreenState extends State<CalendarScreen> {
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
      body: Stack(
        children: <Widget>[
          Column(
        children: <Widget>[
          Padding(
            padding: const EdgeInsets.symmetric(vertical: 8.0),
            child: Text(
                  DateFormat('MMMM').format(currentDate),
              style: const TextStyle(
                fontWeight: FontWeight.bold,
                fontSize: 24,
              ),
            ),
          ),
          _buildWeekDayLabels(),
              const SizedBox(height: 10),
          SizedBox(
                height: MediaQuery.of(context).size.height * 0.385,
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
          Container(
            height: 1,
            color: Colors.grey,
          ),
          const SizedBox(height: 20),
          _buildDetailsSection(),
            ],
      ),
          RoundedIconedButton(
            icon: Icons.add,
            size: 60,
            bottom: 110,
            right: 18,
            color: Colors.greenAccent,
            onPressed: () {
              Navigator.push(
                context,
                MaterialPageRoute<void>(
                  builder: (BuildContext context) => CreateScheduleScreen(
                    contactado: false,
                    horario: TimeOfDay.now(),
                    dia: DateTime(2024, 8, 27),
                    preco: 150.00,
                    observacao: "This is an observation",
                  ),
                ),
              );
            },
          ),
          RoundedIconedButton(
            icon: Icons.add,
            size: 60,
            bottom: 30,
            right: 18,
            color: Colors.blueAccent,
            onPressed: () {
              Navigator.push(
                context,
                MaterialPageRoute<void>(
                  builder: (BuildContext context) => CreateLogScreen(
                    contactado: false,
                    horario: TimeOfDay.now(),
                    dia: DateTime(2024, 8, 27),
                    preco: 150.00,
                    observacao: "This is an observation",
                  ),
                ),
              );
            },
          ),
        ],
      ),
    );
  }

  Widget _buildWeekDayLabels() {
    const List<String> daysOfWeek = <String>['S', 'M', 'T', 'W', 'T', 'F', 'S'];
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 14),
      child: Row(
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
      ),
    );
  }

  Widget _buildMonthView(int month) {
    final DateTime firstDayOfMonth = DateTime(currentDate.year, month, 2);
    final int daysInMonth = DateUtils.getDaysInMonth(currentDate.year, month);
    final int firstWeekday =
        firstDayOfMonth.weekday;

    return GridView.builder(
      
      padding: const EdgeInsets.symmetric(horizontal: 16),
      gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
        crossAxisCount: 7,
        crossAxisSpacing: 4,
        mainAxisSpacing: 4,
      ),
      itemCount: daysInMonth +
          (firstWeekday - 1),
      itemBuilder: (BuildContext context, int index) {
        if (index < firstWeekday - 1) {
          return const SizedBox();
        } else {
          final int day = index - (firstWeekday - 2);
          return _buildDayCell(day);
        }
      },
    );
  }

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
          border: day == selectedDate ? Border.all(color: Colors.grey) : null,
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

  Widget _buildDetailsSection() {
    return schedules.containsKey(selectedDate)
        ? ListView.builder(
              shrinkWrap: true,
              physics: const NeverScrollableScrollPhysics(),
            padding: const EdgeInsets.symmetric(horizontal: 16),
              itemCount: schedules[selectedDate]!.length,
              itemBuilder: (BuildContext context, int index) {
              return Card(
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(8),
                ),
                margin: const EdgeInsets.symmetric(vertical: 8),
                elevation: 1,
                child: ListTile(
                  title: Text(schedules[selectedDate]![index]),
                  contentPadding: const EdgeInsets.symmetric(horizontal: 12),
                ),
                );
            },
          )
        : const Padding(
            padding: EdgeInsets.symmetric(horizontal: 20, vertical: 2),
            child: Row(
              children: <Widget>[
                Text(
                  'Sem eventos',
                  textAlign: TextAlign.start,
                  style: TextStyle(
                    fontSize: 16,
                    color: Colors.grey,
                  ),
                ),
              ],
            ),
          );
  }
}
