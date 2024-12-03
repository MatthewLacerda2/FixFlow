import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../components/Buttons/order_button.dart';
import '../../components/Buttons/rounded_iconed_button.dart';
import '../../components/apt_list.dart';
import '../../utils/apt_filters.dart';
import '../../utils/date_time_utils.dart';
import '../../utils/flow_storage.dart';
import '../../utils/string_utils.dart';
import '../apts/edit_apt/create_schedule_screen.dart';
import '../apts/schedule_screen.dart';
import '../create_client_screen.dart';

class SchedulesScreen extends StatefulWidget {
  const SchedulesScreen({super.key, required this.aptFilters});

  final AptFilters aptFilters;

  @override
  _SchedulesScreenState createState() => _SchedulesScreenState();
}

class _SchedulesScreenState extends State<SchedulesScreen> {
  late Future<List<AptSchedule>> _schedulesFuture;

  @override
  void initState() {
    super.initState();
    _schedulesFuture = _fetchSchedules();
  }

  Future<List<AptSchedule>> _fetchSchedules() async {
    final String mytoken = await FlowStorage.getToken();

    final ApiClient apiClient = FlowStorage.getApiClient(mytoken);

    final AptFilters f = widget.aptFilters;

    final List<AptSchedule>? response = await AptScheduleApi(apiClient)
        .apiV1SchedulesGet(
            minPrice: f.minPrice,
            maxPrice: f.maxPrice,
            minDateTime: f.minDateTime,
            maxDateTime: f.maxDateTime,
            offset: f.offset,
            limit: f.limit);

    return response ?? <AptSchedule>[]; // Handle null safety
  }

  Future<void> _refreshSchedules() async {
    setState(() {
      _schedulesFuture = _fetchSchedules();
    });
    await _schedulesFuture; // Wait for the new data to load.
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(0),
        child: Stack(
          children: <Widget>[
            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: <Widget>[
                Container(
                  color: Colors.greenAccent,
                  padding: const EdgeInsets.all(8),
                  height: 60,
                  child: const Row(
                    children: <Widget>[
                      Icon(Icons.timer_outlined, size: 28),
                      SizedBox(width: 8),
                      Text(
                        'Agendamentos',
                        style: TextStyle(
                            fontSize: 24, fontWeight: FontWeight.bold),
                      ),
                    ],
                  ),
                ),
                Container(color: Colors.black, height: 1),
                const SizedBox(height: 8),
                const Padding(
                  padding: EdgeInsets.symmetric(horizontal: 10),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                      OrderButton(
                        icon: Icons.perm_contact_cal,
                        isUp: true,
                        iconSize: 40,
                        iconColor: Colors.greenAccent,
                      ),
                      OrderButton(
                        icon: Icons.attach_money,
                        iconSize: 40,
                        iconColor: Colors.greenAccent,
                      ),
                      OrderButton(
                        icon: Icons.calendar_today,
                        iconSize: 40,
                        iconColor: Colors.greenAccent,
                      ),
                    ],
                  ),
                ),
                const SizedBox(height: 10),
                Expanded(
                  child: RefreshIndicator(
                    onRefresh: _refreshSchedules,
                    child: FutureBuilder<List<AptSchedule>>(
                      future: _schedulesFuture,
                      builder: (BuildContext context,
                          AsyncSnapshot<List<AptSchedule>> snapshot) {
                        if (snapshot.connectionState ==
                            ConnectionState.waiting) {
                          return const Center(
                              child: CircularProgressIndicator());
                        } else if (snapshot.hasError) {
                          return Center(
                            child: Text('Error: ${snapshot.error}'),
                          );
                        } else if (!snapshot.hasData ||
                            snapshot.data!.isEmpty) {
                          return const Center(
                            child: Text('Não há agendamentos.'),
                          );
                        }

                        final List<AptSchedule> schedules = snapshot.data!;
                        return ListView.separated(
                          itemCount: schedules.length,
                          separatorBuilder: (BuildContext context, int index) =>
                              const Divider(
                                  color: Colors.transparent,
                                  thickness: 0,
                                  height: 10),
                          itemBuilder: (BuildContext context, int index) {
                            final AptSchedule schedule = schedules[index];
                            return AptList(
                              clientName: schedule.customer!.fullName,
                              price: schedule.price ?? 0,
                              hour: TimeOfDay.fromDateTime(schedule.dateTime!)
                                  .format(context),
                              date: DateTimeUtils.dateOnlyString(
                                  schedule.dateTime),
                              service:
                                  StringUtils.normalIfBlank(schedule.service),
                              observation: StringUtils.normalIfBlank(
                                  schedule.description),
                              onTap: () {
                                Navigator.push(
                                  context,
                                  MaterialPageRoute<void>(
                                    builder: (BuildContext context) =>
                                        ScheduleScreen(schedule: schedule),
                                  ),
                                );
                              },
                            );
                          },
                        );
                      },
                    ),
                  ),
                )
              ],
            ),
            RoundedIconedButton(
              icon: Icons.person_add_alt_1_sharp,
              size: 38,
              bottom: 100,
              right: 16,
              color: const Color.fromARGB(255, 0, 175, 0),
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute<void>(
                    builder: (BuildContext context) => CreateClientScreen(
                      nextScreen: CreateScheduleScreen(
                        contactado: false,
                        horario: TimeOfDay.now(),
                        dia: DateTime.now(),
                        preco: 150.00,
                        observacao: "",
                      ),
                    ),
                  ),
                );
              },
            ),
            RoundedIconedButton(
              icon: Icons.add,
              size: 68,
              bottom: 18,
              right: 16,
              color: Colors.purple,
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute<void>(
                    builder: (BuildContext context) => CreateScheduleScreen(
                      contactado: false,
                      horario: TimeOfDay.now(),
                      dia: DateTime.now(),
                      preco: 150.00,
                      observacao: "",
                    ),
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
