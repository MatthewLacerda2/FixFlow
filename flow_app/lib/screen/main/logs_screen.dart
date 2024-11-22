import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../components/Buttons/colored_border_text_button.dart';
import '../../components/Buttons/order_button.dart';
import '../../components/Buttons/rounded_iconed_button.dart';
import '../../components/logs_list.dart';
import '../../utils/date_time_utils.dart';
import '../../utils/flow_storage.dart';
import '../apt_filters_screen.dart';
import '../apts/edit_apt/create_log_screen.dart';
import '../apts/log_screen.dart';
import '../create_client_screen.dart';

class LogsScreen extends StatefulWidget {
  const LogsScreen({super.key});

  AptLog getLog() {
    Customer cu = Customer(businessId: "businessId", fullName: "full Name");
    return AptLog(
        id: "id",
        customer: cu,
        customerId: "cId",
        businessId: "bId",
        dateTime: DateTime.now(),
        price: 100,
        description: "something",
        scheduleId: "sId",
        service: "facial");
  }

  @override
  _LogsScreenState createState() => _LogsScreenState();
}

class _LogsScreenState extends State<LogsScreen> {
  late List<AptLog> _logsFuture;

  @override
  void initState() {
    super.initState();
    _fetchLogs();
  }

  void _fetchLogs() async {
    final BusinessDTO? bd = await FlowStorage.getBusinessDTO();
    final String businessId = bd!.id!;

    final List<AptLog>? response = await AptLogApi().apiV1LogsGet(
        limit: 100,
        offset: 0,
        maxPrice: 9999,
        minPrice: 0,
        minDateTime: DateTime(2023),
        maxDateTime: DateTime(2025));
    _logsFuture = response ?? <AptLog>[]; // Handle null safety
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
                  color: Colors.blueAccent,
                  padding: const EdgeInsets.all(8),
                  height: 60,
                  child: const Row(
                    children: <Widget>[
                      Icon(Icons.note_alt_outlined, size: 28),
                      SizedBox(width: 8),
                      Text(
                        'Atendimentos',
                        style: TextStyle(
                            fontSize: 24, fontWeight: FontWeight.bold),
                      ),
                    ],
                  ),
                ),
                Container(
                  color: Colors.black,
                  height: 1,
                ),
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
                        iconColor: Colors.blue,
                      ),
                      const OrderButton(
                        icon: Icons.attach_money,
                        iconSize: 40,
                        iconColor: Colors.blue,
                      ),
                      const OrderButton(
                        icon: Icons.calendar_today,
                        iconSize: 40,
                        iconColor: Colors.blue,
                      ),
                      ColoredBorderTextButton(
                        text: "Filtros",
                        onPressed: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute<void>(
                              builder: (BuildContext context) =>
                                  const AptFiltersScreen(),
                            ),
                          );
                        },
                        backgroundColor: Colors.blue,
                        borderColor: Colors.black,
                        textColor: Colors.white,
                      )
                    ],
                  ),
                ),
                const SizedBox(height: 10),
                Expanded(
                  child: ListView.separated(
                    itemCount: _logsFuture.length,
                    separatorBuilder: (BuildContext context, int index) =>
                        const Divider(
                      color: Colors.transparent,
                      thickness: 0,
                      height: 9,
                    ),
                    itemBuilder: (BuildContext context, int index) {
                      AptLog log = _logsFuture[index];
                      TimeOfDay hora = TimeOfDay.fromDateTime(log.dateTime!);
                      return LogsList(
                        clientName: log.customer!.fullName,
                        price: log.price ?? 0,
                        hour: hora.toString(),
                        date: DateTimeUtils.dateOnlyString(log.dateTime!),
                        service: log.service,
                        observation: log.description,
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute<void>(
                              builder: (BuildContext context) => LogScreen(
                                log: log,
                              ),
                            ),
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
                      nextScreen: CreateLogScreen(
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
                    builder: (BuildContext context) => CreateLogScreen(
                      contactado: false,
                      horario: TimeOfDay.now(),
                      dia: DateTime(2024, 8, 27),
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
