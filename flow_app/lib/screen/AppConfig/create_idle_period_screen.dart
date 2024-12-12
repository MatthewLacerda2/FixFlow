import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart';

import '../../components/Buttons/custom_button.dart';
import '../../components/Inputs/date_picker_rectangle.dart';
import '../../components/Inputs/name_input_field.dart';
import '../../components/Inputs/time_picker_rectangle.dart';
import '../../components/apt_list.dart';
import '../../utils/date_time_utils.dart';
import '../../utils/flow_snack.dart';
import '../../utils/flow_storage.dart';
import '../../utils/string_utils.dart';
import '../apts/schedule_screen.dart';
import 'change_successful.dart';

class CreateIdlePeriodScreen extends StatefulWidget {
  const CreateIdlePeriodScreen({super.key});

  @override
  State<CreateIdlePeriodScreen> createState() => _CreateIdlePeriodScreenState();
}

class _CreateIdlePeriodScreenState extends State<CreateIdlePeriodScreen> {
  late DateTime _startDateTime;
  late DateTime _finishDateTime;
  late Future<List<AptSchedule>> _schedulesFuture;
  String periodName = "";

  @override
  void initState() {
    super.initState();
    _startDateTime = DateTime.now();
    _finishDateTime = DateTime.now().add(const Duration(days: 3));
    _schedulesFuture = _fetchSchedules();
  }

  Future<List<AptSchedule>> _fetchSchedules() async {
    final String mytoken = await FlowStorage.getToken();
    final ApiClient apiClient = FlowStorage.getApiClient(mytoken);

    final List<AptSchedule>? response =
        await AptScheduleApi(apiClient).apiV1SchedulesGet(
      minPrice: 0,
      minDateTime: _startDateTime,
      maxDateTime: _finishDateTime,
      offset: 0,
      limit: 20,
    );

    return response ?? <AptSchedule>[];
  }

  void _updateSchedules() {
    setState(() {
      _schedulesFuture = _fetchSchedules();
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("Criar Período Ocioso")),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: <Widget>[
            const SizedBox(height: 6),
            const Text(
              "Não será possível criar agendamentos para um período ocioso. Isso pode ser útil durante feriados ou manutenções.",
              textAlign: TextAlign.justify,
              style: TextStyle(fontSize: 16, color: Colors.black),
            ),
            const SizedBox(height: 18),
            NameInputField(
              placeholder: 'Nome do período:',
              onNameChanged: (String note) {
                periodName = note;
              },
            ),
            const SizedBox(height: 12),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                const Text(
                  'Início:',
                  style: TextStyle(fontSize: 18, fontWeight: FontWeight.w500),
                ),
                DatePickerRectangle(
                  initialDate: _startDateTime,
                  onDateSelected: (DateTime date) {
                    setState(() {
                      _startDateTime = DateTime(
                        date.year,
                        date.month,
                        date.day,
                        _startDateTime.hour,
                        _startDateTime.minute,
                      );
                      _updateSchedules();
                    });
                  },
                ),
                TimePickerRectangle(
                  initialTime: TimeOfDay.fromDateTime(_startDateTime),
                  onTimeSelected: (TimeOfDay time) {
                    setState(() {
                      _startDateTime = DateTime(
                        _startDateTime.year,
                        _startDateTime.month,
                        _startDateTime.day,
                        time.hour,
                        time.minute,
                      );
                      _updateSchedules();
                    });
                  },
                ),
              ],
            ),
            const SizedBox(height: 8),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                const Text(
                  'Término:',
                  style: TextStyle(fontSize: 18, fontWeight: FontWeight.w500),
                ),
                DatePickerRectangle(
                  initialDate: _finishDateTime,
                  onDateSelected: (DateTime date) {
                    setState(() {
                      _finishDateTime = DateTime(
                        date.year,
                        date.month,
                        date.day,
                        _finishDateTime.hour,
                        _finishDateTime.minute,
                      );
                      _updateSchedules();
                    });
                  },
                ),
                TimePickerRectangle(
                  initialTime: TimeOfDay.fromDateTime(_finishDateTime),
                  onTimeSelected: (TimeOfDay time) {
                    setState(() {
                      _finishDateTime = DateTime(
                        _finishDateTime.year,
                        _finishDateTime.month,
                        _finishDateTime.day,
                        time.hour,
                        time.minute,
                      );
                      _updateSchedules();
                    });
                  },
                ),
              ],
            ),
            const SizedBox(height: 24),
            const Text(
                "Os agendamentos abaixo podem ser editados ou deletados individualmente"),
            Expanded(
              child: RefreshIndicator(
                onRefresh: () async => _updateSchedules(),
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
                        child: Text('Não há agendamentos para este período.'),
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
                          clientName: schedule.customer?.fullName ?? 'N/A',
                          price: (schedule.price ?? 0) / 100,
                          hour: TimeOfDay.fromDateTime(schedule.dateTime!)
                              .format(context),
                          date: DateTimeUtils.dateOnlyString(
                              schedule.dateTime ?? DateTime.now()),
                          service:
                              StringUtils.normalIfBlank(schedule.service ?? ''),
                          observation: StringUtils.normalIfBlank(
                              schedule.description ?? ''),
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
            ),
            Align(
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: <Widget>[
                  CustomButton(
                    text: "Voltar",
                    textSize: 18,
                    backgroundColor: Colors.white,
                    onPressed: () {
                      Navigator.pop(context);
                    },
                  ),
                  CustomButton(
                    text: "Confirmar",
                    textSize: 18,
                    backgroundColor: Colors.green,
                    textColor: Colors.white,
                    onPressed: () async {
                      final String mytoken = await FlowStorage.getToken();
                      final BusinessDTO? businessData =
                          await FlowStorage.getBusinessDTO();

                      final ApiClient apiClient =
                          FlowStorage.getApiClient(mytoken);

                      final IdlePeriod newIdlePeriod = IdlePeriod(
                          id: "id",
                          name: periodName != ""
                              ? periodName
                              : DateTimeUtils.niceFormattedDateTime(
                                  DateTime.now(), context),
                          businessId: businessData!.id!,
                          start: _startDateTime,
                          finish: _finishDateTime);

                      final Response resp = await IdlePeriodApi(apiClient)
                          .apiV1IdlePeriodPostWithHttpInfo(
                              idlePeriod: newIdlePeriod);

                      if (resp.statusCode != 201) {
                        FlowSnack.show(context, resp.body);
                        print(resp.body);
                        print(resp.statusCode);
                        return;
                      }

                      Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                          builder: (BuildContext context) =>
                              const ChangeSuccessfulScreen(
                            title: "Período Ocioso criado!",
                            description:
                                "Não será possível criar agendamentos para este período\n\nVocê pode deletar este período na tela de Calendário",
                          ),
                        ),
                      );
                    },
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
