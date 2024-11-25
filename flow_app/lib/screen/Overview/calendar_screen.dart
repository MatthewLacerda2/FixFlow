import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import '../../utils/flow_storage.dart';

class CalendarScreen extends StatefulWidget {
  const CalendarScreen({super.key});

  @override
  CalendarScreenState createState() => CalendarScreenState();
}

class CalendarScreenState extends State<CalendarScreen> {
  List<BusinessCalendarDay> _calendarDays = <BusinessCalendarDay>[];
  final DateTime _currentDate = DateTime.now();
  int _selectedDay = DateTime.now().day;
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    _fetchCalendar();
  }

  Future<void> _fetchCalendar() async {
    try {
      final BusinessDTO? business = await FlowStorage.getBusinessDTO();
      final String? bId = business!.id;
      final List<BusinessCalendarDay>? calendar = await BusinessCalendarDayApi()
          .apiV1BusinessCalendarDayGet(businessId: bId);
      setState(() {
        _calendarDays = calendar ?? <BusinessCalendarDay>[];
        _isLoading = false;
      });
    } catch (e) {
      setState(() {
        _isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Calendar'),
      ),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : SingleChildScrollView(
              child: Column(
                children: <Widget>[
                  _buildMonthTitle(),
                  _buildWeekdayLabels(),
                  _buildCalendarGrid(),
                  const Divider(color: Colors.grey, height: 1),
                  _buildEventsSection(),
                ],
              ),
            ),
    );
  }

  Widget _buildMonthTitle() {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 8.0),
      child: Text(
        DateFormat('MMMM yyyy').format(_currentDate),
        style: const TextStyle(
          fontWeight: FontWeight.bold,
          fontSize: 24,
        ),
      ),
    );
  }

  Widget _buildWeekdayLabels() {
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

  Widget _buildCalendarGrid() {
    final int daysInMonth =
        DateUtils.getDaysInMonth(_currentDate.year, _currentDate.month);
    final int firstWeekday =
        DateTime(_currentDate.year, _currentDate.month).weekday;

    return GridView.builder(
      padding: const EdgeInsets.symmetric(horizontal: 16),
      shrinkWrap: true,
      physics: const NeverScrollableScrollPhysics(),
      gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
        crossAxisCount: 7,
        crossAxisSpacing: 4,
        mainAxisSpacing: 4,
      ),
      itemCount: daysInMonth + (firstWeekday - 1),
      itemBuilder: (BuildContext context, int index) {
        if (index < firstWeekday - 1) {
          return const SizedBox();
        } else {
          final int day = index - (firstWeekday - 2);
          final BusinessCalendarDay calendarDay = _calendarDays.firstWhere(
            (BusinessCalendarDay dayData) => dayData.date?.day == day,
          );

          return GestureDetector(
            onTap: () {
              setState(() {
                _selectedDay = day;
              });
            },
            child: _buildDayCell(day, calendarDay),
          );
        }
      },
    );
  }

  Widget _buildDayCell(int day, BusinessCalendarDay? calendarDay) {
    final List<Color> indicators = <Color>[];

    if (calendarDay != null) {
      if (calendarDay.schedules!.isNotEmpty) {
        indicators.add(Colors.orangeAccent);
      }
      if (calendarDay.logs!.isNotEmpty) {
        indicators.add(Colors.blue);
      }
      if (calendarDay.holiday!.isNotEmpty) {
        indicators.add(Colors.red);
      }
      if (calendarDay.idlePeriods!.isNotEmpty) {
        indicators.add(Colors.green);
      }
    }

    return Container(
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(8),
        border: day == _selectedDay
            ? Border.all(color: Colors.grey, width: 1.5)
            : null,
      ),
      child: Column(
        children: <Widget>[
          Text(
            '$day',
            style: const TextStyle(fontWeight: FontWeight.bold),
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: indicators
                .map((Color color) => Container(
                      width: 8,
                      height: 4,
                      margin: const EdgeInsets.symmetric(horizontal: 1),
                      color: color,
                    ))
                .toList(),
          ),
        ],
      ),
    );
  }

  Widget _buildEventsSection() {
    final BusinessCalendarDay selectedDayData = _calendarDays.firstWhere(
      (BusinessCalendarDay dayData) => dayData.date?.day == _selectedDay,
    );

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        ...selectedDayData.schedules!.map(
          (AptSchedule schedule) => ListTile(
            title: Text(schedule.service ?? schedule.customer!.fullName),
            onTap: () {
              // TODO: Navigate to Schedule screen
            },
          ),
        ),
        ...selectedDayData.idlePeriods!.map(
          (IdlePeriod idle) => ListTile(
            title: Text('Idle Period: ${idle.name}'),
          ),
        ),
        ...selectedDayData.holiday!.map(
          (String holiday) => ListTile(
            title: Text('Holiday: $holiday'),
          ),
        ),
        ...selectedDayData.logs!.map(
          (AptLog log) => ListTile(
            title: Text(log.service ?? log.customer!.fullName),
            onTap: () {
              // TODO: Navigate to Log screen
            },
          ),
        ),
      ],
    );
  }
}
