import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import '../../components/Inputs/circular_button.dart';
import '../../components/logs_list.dart';
import '../../components/schedules_list.dart';
import '../../utils/date_time_utils.dart';
import '../../utils/flow_storage.dart';
import '../apts/log_screen.dart';
import '../apts/schedule_screen.dart';

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
                  const SizedBox(height: 6),
                  _buildWeekdayLabels(),
                  const SizedBox(height: 10),
                  _buildCalendarGrid(),
                  Stack(
                    alignment: Alignment.center,
                    children: <Widget>[
                      const Divider(color: Colors.grey, height: 50),
                      Positioned(
                        left: 16,
                        child: CircularButton(
                          icon: Icons.keyboard_arrow_left,
                          size: 50,
                          onPressed: () {
                            //TODO:
                          },
                        ),
                      ),
                      Positioned(
                        right: 16,
                        child: CircularButton(
                          icon: Icons.keyboard_arrow_right,
                          size: 50,
                          onPressed: () {
                            //TODO:
                          },
                        ),
                      ),
                    ],
                  ),
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
        indicators.add(Colors.green);
      }
      if (calendarDay.logs!.isNotEmpty) {
        indicators.add(Colors.blue);
      }
      if (calendarDay.holiday!.isNotEmpty) {
        indicators.add(Colors.red);
      }
      if (calendarDay.idlePeriods!.isNotEmpty) {
        indicators.add(const Color.fromARGB(255, 0, 100, 3));
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
        ...selectedDayData.idlePeriods!.map(
          (IdlePeriod idle) => ListTile(
            title: Text('Período ocioso: ${idle.name}'),
          ),
        ),
        ...selectedDayData.holiday!.map(
          (String holiday) => ListTile(
            title: Text('Feriado: $holiday'),
          ),
        ),
        if (selectedDayData.schedules!.isNotEmpty)
          const Padding(
            padding: EdgeInsets.symmetric(horizontal: 8.0),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: <Widget>[
                SizedBox(height: 10),
                Text(
                  'Agendamentos',
                  style: TextStyle(
                    fontWeight: FontWeight.bold,
                    fontSize: 22,
                    color: Colors.green,
                  ),
                ),
                SizedBox(height: 10),
              ],
            ),
          ),
        ...selectedDayData.schedules!.map(
          (AptSchedule item) => SchedulesList(
            clientName: item.customer!.fullName,
            price: item.price ?? 0,
            hour: TimeOfDay.fromDateTime(item.dateTime!).format(context),
            date: DateTimeUtils.dateOnlyString(item.dateTime!),
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute<void>(
                  builder: (BuildContext context) =>
                      ScheduleScreen(schedule: item),
                ),
              );
            },
          ),
        ),
        if (selectedDayData.schedules!.isNotEmpty)
          const Padding(
            padding: EdgeInsets.symmetric(horizontal: 8.0),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: <Widget>[
                SizedBox(height: 14),
                Text(
                  'Atendimentos',
                  style: TextStyle(
                    fontWeight: FontWeight.bold,
                    fontSize: 22,
                    color: Colors.blue,
                  ),
                ),
                SizedBox(height: 14),
              ],
            ),
          ),
        ...selectedDayData.logs!.map(
          (AptLog item) => LogsList(
            clientName: item.customer!.fullName,
            price: item.price ?? 4.2,
            hour: TimeOfDay.fromDateTime(item.dateTime!).format(context),
            date: DateTimeUtils.dateOnlyString(item.dateTime!),
            service: item.service,
            observation: item.description,
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute<void>(
                  builder: (BuildContext context) => LogScreen(
                    log: item,
                  ),
                ),
              );
            },
          ),
        ),
        const SizedBox(height: 40),
      ],
    );
  }
}
