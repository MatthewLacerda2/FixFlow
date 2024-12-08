import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../components/Buttons/colored_border_text_button.dart';
import '../../components/Buttons/order_button.dart';
import '../../components/Buttons/rounded_iconed_button.dart';
import '../../components/schedules_list.dart';
import '../../utils/apt_filters.dart';
import '../../utils/flow_storage.dart';
import '../apt_filters_screen.dart';
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
    print("umubuga fei di tal");
    print("\n");
    print(widget.aptFilters);
    print("\n");
    print("umubuga fei di tal");
  }

  Future<List<AptSchedule>> _fetchSchedules() async {
    final BusinessDTO? bd = await FlowStorage.getBusinessDTO();
    final String businessId = bd!.id!;

    final AptFilters f = widget.aptFilters;

    final List<AptSchedule>? response = await AptScheduleApi()
        .apiV1SchedulesGet(
            businessId: businessId,
            offset: f.offset,
            limit: f.limit,
            minPrice: f.minPrice,
            maxPrice: f.maxPrice,
            minDateTime: f.minDateTime,
            maxDateTime: f.maxDateTime);
    return response ?? <AptSchedule>[]; // Handle null safety
  }

  String getNormalizedString(String? string) {
    return (string == null || string.isEmpty) ? '-' : string;
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
                  child: const Row(children: <Widget>[
                    Icon(Icons.timer_outlined, size: 28),
                    SizedBox(width: 8),
                    Text(
                      'Agendamentos',
                      style:
                          TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                    ),
                  ]),
                ),
                Container(color: Colors.black, height: 1),
                const SizedBox(height: 8),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 10),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                      const OrderButton(
                        icon: Icons.perm_contact_cal,
                        isUp: true,
                        iconSize: 40,
                        iconColor: Colors.greenAccent,
                      ),
                      const OrderButton(
                        icon: Icons.attach_money,
                        iconSize: 40,
                        iconColor: Colors.greenAccent,
                      ),
                      const OrderButton(
                        icon: Icons.calendar_today,
                        iconSize: 40,
                        iconColor: Colors.greenAccent,
                      ),
                      ColoredBorderTextButton(
                        text: "Filtros",
                        onPressed: () {
                          Navigator.push<AptFilters>(
                            context,
                            MaterialPageRoute(
                              builder: (context) => AptFiltersScreen(
                                  aptFilters: widget.aptFilters),
                            ),
                          );
                        },
                        backgroundColor: Colors.greenAccent,
                        borderColor: Colors.grey,
                        textColor: Colors.white,
                      )
                    ],
                  ),
                ),
                const SizedBox(height: 10),
                Expanded(
                  child: FutureBuilder<List<AptSchedule>>(
                    future: _schedulesFuture,
                    builder: (BuildContext context,
                        AsyncSnapshot<List<AptSchedule>> snapshot) {
                      if (snapshot.connectionState == ConnectionState.waiting) {
                        return const Center(child: CircularProgressIndicator());
                      } else if (snapshot.hasError) {
                        return Center(
                          child: Text('Error: ${snapshot.error}'),
                        );
                      } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
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
                                thickness: 1,
                                height: 14),
                        itemBuilder: (BuildContext context, int index) {
                          final AptSchedule schedule = schedules[index];
                          return SchedulesList(
                            clientName: schedule.customer!.fullName,
                            price: schedule.price ?? 0.0,
                            hour: TimeOfDay.fromDateTime(schedule.dateTime!)
                                .format(context),
                            date:
                                '${schedule.dateTime!.day}/${schedule.dateTime!.month}/${schedule.dateTime!.year}',
                            service: getNormalizedString(schedule.service),
                            observation:
                                getNormalizedString(schedule.observation),
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
