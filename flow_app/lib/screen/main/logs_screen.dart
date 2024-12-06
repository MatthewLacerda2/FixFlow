import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../components/Buttons/order_button.dart';
import '../../components/Buttons/rounded_iconed_button.dart';
import '../../components/apt_list.dart';
import '../../utils/apt_filters.dart';
import '../../utils/date_time_utils.dart';
import '../../utils/flow_storage.dart';
import '../../utils/string_utils.dart';
import '../apts/edit_apt/create_log_screen.dart';
import '../apts/log_screen.dart';
import '../create_client_screen.dart';

class LogsScreen extends StatefulWidget {
  const LogsScreen({super.key, required this.aptFilters});

  final AptFilters aptFilters;

  @override
  _LogsScreenState createState() => _LogsScreenState();
}

class _LogsScreenState extends State<LogsScreen> {
  late Future<List<AptLog>> _logsFuture;

  @override
  void initState() {
    super.initState();
    _logsFuture = _fetchLogs();
  }

  Future<List<AptLog>> _fetchLogs() async {
    final String mytoken = await FlowStorage.getToken();
    final ApiClient apiClient = FlowStorage.getApiClient(mytoken);

    final AptFilters f = widget.aptFilters;

    final List<AptLog>? response = await AptLogApi(apiClient).apiV1LogsGet(
        offset: f.offset,
        limit: f.limit,
        minPrice: f.minPrice,
        maxPrice: f.maxPrice,
        minDateTime: f.minDateTime,
        maxDateTime: f.maxDateTime);
    return response ?? <AptLog>[]; // Handle null safety
  }

  Future<void> _refreshLogs() async {
    setState(() {
      _logsFuture = _fetchLogs();
    });
    await _logsFuture;
  }

  List<String> order = <String>['date', 'client', 'price'];
  List<bool?> ups = <bool?>[true, null, null];

  void _handleToggle(String key, bool newIsUp) {
    setState(() {
      _logsFuture = _logsFuture.then((List<AptLog> contacts) {
        final List<AptLog> sortedLogs = List<AptLog>.from(contacts);

        switch (key) {
          case 'date':
            sortedLogs.sort((AptLog a, AptLog b) {
              final int comparison = a.dateTime!.compareTo(b.dateTime!);
              return newIsUp ? comparison : -comparison;
            });
            ups[0] = (ups[0] == null) ? true : !ups[0]!;
            ups[1] = null;
            ups[2] = null;
            break;
          case 'client':
            sortedLogs.sort((AptLog a, AptLog b) {
              final int comparison =
                  a.customer!.fullName.compareTo(b.customer!.fullName);
              return newIsUp ? comparison : -comparison;
            });
            ups[0] = null;
            ups[1] = (ups[1] == null) ? true : !ups[1]!;
            ups[2] = null;
            break;
          case 'price':
            sortedLogs.sort((AptLog a, AptLog b) {
              final int comparison = (a.price ?? 0).compareTo(b.price ?? 0);
              return newIsUp ? comparison : -comparison;
            });
            ups[0] = null;
            ups[1] = null;
            ups[2] = (ups[2] == null) ? true : !ups[2]!;
            break;
          default:
            break;
        }
        return sortedLogs;
      });
    });
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
                const SizedBox(height: 10),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 10),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                      OrderButton(
                        onToggle: _handleToggle,
                        sort: order[0],
                        isUp: ups[0],
                        icon: Icons.calendar_today,
                        iconSize: 40,
                        iconColor: Colors.blueGrey,
                      ),
                      OrderButton(
                        onToggle: _handleToggle,
                        sort: order[1],
                        isUp: ups[1],
                        icon: Icons.perm_contact_cal,
                        iconSize: 40,
                        iconColor: Colors.blueGrey,
                      ),
                      OrderButton(
                        onToggle: _handleToggle,
                        sort: order[2],
                        isUp: ups[2],
                        icon: Icons.attach_money,
                        iconSize: 40,
                        iconColor: Colors.blueGrey,
                      ),
                    ],
                  ),
                ),
                const SizedBox(height: 18),
                Expanded(
                  child: RefreshIndicator(
                    onRefresh: _refreshLogs,
                    child: FutureBuilder<List<AptLog>>(
                      future: _logsFuture,
                      builder: (BuildContext context,
                          AsyncSnapshot<List<AptLog>> snapshot) {
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

                        final List<AptLog> logs = snapshot.data!;
                        return ListView.separated(
                          itemCount: logs.length,
                          separatorBuilder: (BuildContext context, int index) =>
                              const Divider(
                            color: Colors.transparent,
                            thickness: 0,
                            height: 10,
                          ),
                          itemBuilder: (BuildContext context, int index) {
                            final AptLog log = logs[index];
                            return AptList(
                              clientName: log.customer!.fullName,
                              price: log.price! / 100,
                              hour: TimeOfDay.fromDateTime(log.dateTime!)
                                  .format(context),
                              date: DateTimeUtils.dateOnlyString(log.dateTime!),
                              service: StringUtils.normalIfBlank(log.service),
                              observation:
                                  StringUtils.normalIfBlank(log.description),
                              onTap: () {
                                Navigator.push(
                                  context,
                                  MaterialPageRoute<void>(
                                    builder: (BuildContext context) =>
                                        LogScreen(
                                      log: log,
                                    ),
                                  ),
                                );
                              },
                            );
                          },
                        );
                      },
                    ),
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
