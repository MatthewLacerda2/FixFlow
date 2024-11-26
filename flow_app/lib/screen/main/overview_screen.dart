import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../components/Buttons/colored_border_text_button.dart';
import '../../utils/date_time_utils.dart';
import '../../utils/flow_storage.dart';
import '../Overview/Clients/clients_screen.dart';
import '../Overview/calendar_screen.dart';

class OverviewScreen extends StatefulWidget {
  const OverviewScreen({super.key});

  @override
  _OverviewScreenState createState() => _OverviewScreenState();
}

class _OverviewScreenState extends State<OverviewScreen> {
  bool _isLoading = true;
  int _todayCount = 0;
  int _weekCount = 0;

  @override
  void initState() {
    super.initState();
    _loadData();
  }

  Future<void> _loadData() async {
    setState(() {
      _isLoading = true;
    });

    final BusinessDTO? bd = await FlowStorage.getBusinessDTO();
    final String businessId = bd!.id!;

    final List<AptSchedule>? todaySchedules =
        await AptScheduleApi().apiV1SchedulesGet(
      businessId: businessId,
      minPrice: 0,
      maxPrice: 999,
      minDateTime: DateTime.now(),
      maxDateTime:
          DateTimeUtils.getNextDayAtMidnight(DateTime.now().weekday + 1),
      limit: 100,
      offset: 0,
    );

    final List<AptSchedule>? weekSchedules =
        await AptScheduleApi().apiV1SchedulesGet(
      businessId: businessId,
      minPrice: 0,
      maxPrice: 999,
      minDateTime: DateTime.now(),
      maxDateTime: DateTimeUtils.getNextDayAtMidnight(DateTime.sunday),
      limit: 100,
      offset: 0,
    );

    setState(() {
      _todayCount = todaySchedules?.length ?? 0;
      _weekCount = weekSchedules?.length ?? 0;
      _isLoading = false;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: _isLoading
          ? const Center(
              child: CircularProgressIndicator(),
            )
          : Padding(
              padding: const EdgeInsets.all(16.0),
              child: Column(
                children: <Widget>[
                  Row(
                    children: <Widget>[
                      Row(
                        children: <Widget>[
                          Text(
                            '$_todayCount',
                            style: const TextStyle(
                                fontSize: 42, fontWeight: FontWeight.bold),
                          ),
                          const SizedBox(width: 6),
                          ConstrainedBox(
                            constraints: const BoxConstraints(maxWidth: 100),
                            child: Text(
                              'agendamentos para hoje',
                              style: TextStyle(
                                  fontSize: 12, color: Colors.grey[600]),
                            ),
                          ),
                        ],
                      ),
                      Row(
                        children: <Widget>[
                          Text(
                            '$_weekCount',
                            style: const TextStyle(
                                fontSize: 42, fontWeight: FontWeight.bold),
                          ),
                          const SizedBox(width: 8),
                          ConstrainedBox(
                            constraints: const BoxConstraints(maxWidth: 100),
                            child: Text(
                              'agendamentos para a semana',
                              style: TextStyle(
                                  fontSize: 12, color: Colors.grey[600]),
                            ),
                          ),
                        ],
                      ),
                    ],
                  ),
                  //TODO: add 'contatos para hoje'
                  const SizedBox(height: 9),
                  const Divider(
                    color: Colors.grey,
                    height: 1,
                  ),
                  const SizedBox(height: 24),
                  ColoredBorderTextButton(
                    text: 'Estatísticas',
                    textColor: Colors.white,
                    textSize: 16,
                    backgroundColor: Colors.orangeAccent,
                    width: 130,
                    onPressed: () {
                      //TODO: Make the Statistics Screen
                      ScaffoldMessenger.of(context).showSnackBar(
                        const SnackBar(
                          content: Text("Botão não-implementado !"),
                        ),
                      );
                    },
                  ),
                  const SizedBox(height: 24),
                  ColoredBorderTextButton(
                    text: 'Calendário',
                    textColor: Colors.white,
                    textSize: 16,
                    backgroundColor: Colors.blueAccent,
                    width: 130,
                    onPressed: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                          builder: (BuildContext context) =>
                              const CalendarScreen(),
                        ),
                      );
                    },
                  ),
                  const SizedBox(height: 24),
                  ColoredBorderTextButton(
                    text: 'Clientes',
                    textColor: Colors.white,
                    textSize: 16,
                    backgroundColor: Colors.green,
                    width: 130,
                    onPressed: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                          builder: (BuildContext context) =>
                              const ClientsScreen(),
                        ),
                      );
                    },
                  ),
                  const SizedBox(height: 24),
                  ColoredBorderTextButton(
                    text: 'Mensalidades',
                    textColor: Colors.black,
                    textSize: 16,
                    backgroundColor: Colors.grey[300]!,
                    width: 100,
                    onPressed: () {
                      // TODO: Create Mensalidades Screen
                      ScaffoldMessenger.of(context).showSnackBar(
                        const SnackBar(
                          content: Text("Botão não-implementado !"),
                        ),
                      );
                    },
                  ),
                ],
              ),
            ),
    );
  }
}
