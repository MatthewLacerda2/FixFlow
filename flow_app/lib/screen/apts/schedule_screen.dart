import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart';

import '../../components/Buttons/custom_button.dart';
import '../../components/Buttons/rounded_iconed_button.dart';
import '../../components/Inputs/date_picker_rectangle.dart';
import '../../components/Inputs/limited_text_input_field.dart';
import '../../components/Inputs/price_input_field.dart';
import '../../components/Inputs/services_input_field.dart';
import '../../components/Inputs/time_picker_rectangle.dart';
import '../../components/warning_modal.dart';
import '../../utils/date_time_utils.dart';
import '../../utils/flow_snack.dart';
import '../../utils/flow_storage.dart';
import '../main/main_screen.dart';

class ScheduleScreen extends StatefulWidget {
  const ScheduleScreen({super.key, required this.schedule});
  final AptSchedule schedule;

  @override
  ScheduleScreenState createState() => ScheduleScreenState();
}

class ScheduleScreenState extends State<ScheduleScreen> {
  late TextEditingController _precoController;
  late TextEditingController _observacaoController;

  String? upService;
  int preco = 0;
  DateTime newDateTime = DateTime.now();

  bool _isEdited = false;

  @override
  void initState() {
    super.initState();
    _precoController = TextEditingController(
        text: (widget.schedule.price! / 100).toStringAsFixed(2));
    _observacaoController =
        TextEditingController(text: widget.schedule.description);
    preco = widget.schedule.price! * 100;
    newDateTime = widget.schedule.dateTime!;
  }

  void _toggleEdit() {
    setState(() {
      _isEdited = true;
    });
  }

  void _saveChanges() async {
    final String mytoken = await FlowStorage.getToken();
    final ApiClient apiClient = FlowStorage.getApiClient(mytoken);

    final AptSchedule patchedSchedule = widget.schedule;
    patchedSchedule.price = preco;
    patchedSchedule.dateTime = newDateTime;
    patchedSchedule.description = _observacaoController.text;
    patchedSchedule.service = upService;
    final Response response = await AptScheduleApi(apiClient)
        .apiV1SchedulesPatchWithHttpInfo(aptSchedule: patchedSchedule);
    snackbarResponse(response);
  }

  void snackbarResponse(Response response) {
    if (response.statusCode == 200) {
      FlowSnack.show(context, "Agendamento editado!");
      goToSchedulesScreen();
    } else {
      FlowSnack.show(context, response.body);
    }
  }

  void goToSchedulesScreen() {
    Navigator.pushAndRemoveUntil(
      context,
      MaterialPageRoute<void>(
          builder: (BuildContext context) => const MainScreen(
                initialIndex: 1,
              )),
      (Route<dynamic> route) => false,
    );
  }

  void _cancelChanges() {
    setState(() {
      _isEdited = false;
      _precoController.text = widget.schedule.price!.toStringAsFixed(2);
      _observacaoController.text = widget.schedule.description ?? "";
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Editar Agendamento'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(22.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Text(
              'Cliente: ${widget.schedule.customer!.fullName}',
              style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 24),
            ),
            const SizedBox(height: 30),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                const Text(
                  'Dia:',
                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
                ),
                DatePickerRectangle(
                  initialDate: widget.schedule.dateTime!,
                  onDateSelected: (DateTime date) {
                    newDateTime = DateTimeUtils.setDate(newDateTime, date);
                    _toggleEdit();
                  },
                ),
                const Text(
                  'Hora:',
                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
                ),
                TimePickerRectangle(
                  initialTime:
                      TimeOfDay.fromDateTime(widget.schedule.dateTime!),
                  onTimeSelected: (TimeOfDay time) {
                    newDateTime =
                        DateTimeUtils.setTime(time, widget.schedule.dateTime!);
                    _toggleEdit();
                  },
                ),
              ],
            ),
            const SizedBox(height: 24),
            ServicesInputField(
              initialService: widget.schedule.service,
              onServiceSelected: (String? selectedService) {
                upService = selectedService;
              },
            ),
            const SizedBox(height: 24),
            PriceInputField(
              controller: _precoController,
              onPriceValid: (String value) {
                preco = ((double.tryParse(value) ?? 0) * 100).toInt();
                _toggleEdit();
              },
            ),
            const SizedBox(height: 30),
            LimitedTextInputField(
              controller: _observacaoController,
              maxLength: 250,
              labelText: 'Observação',
              onChanged: (String value) => _toggleEdit(),
            ),
            const SizedBox(height: 26),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: <Widget>[
                ElevatedButton(
                  onPressed: () async {
                    _saveChanges();
                  },
                  style: ElevatedButton.styleFrom(
                    backgroundColor: _isEdited ? Colors.green : Colors.grey,
                  ),
                  child: const Text(
                    'Salvar',
                    style: TextStyle(color: Colors.white, fontSize: 16),
                  ),
                ),
                ElevatedButton(
                  onPressed: _isEdited ? _cancelChanges : null,
                  style: ElevatedButton.styleFrom(
                    backgroundColor: _isEdited ? Colors.blue : Colors.grey,
                    padding:
                        const EdgeInsets.symmetric(horizontal: 16, vertical: 5),
                  ),
                  child: const Text(
                    'Cancelar',
                    style: TextStyle(color: Colors.white, fontSize: 16),
                  ),
                ),
              ],
            ),
            const SizedBox(height: 52),
            RoundedIconedButton(
                icon: Icons.delete_forever_rounded,
                onPressed: () {
                  showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      return WarningModal(
                        title: "Você tem certeza?",
                        description:
                            "Deletar o agendamento? Esta ação não poderá ser desfeita\n",
                        optionOne: CustomButton(
                          text: "Sim",
                          textSize: 14,
                          backgroundColor: Colors.green,
                          textColor: Colors.black,
                          onPressed: () async {
                            final String mytoken = await FlowStorage.getToken();
                            final ApiClient apiClient =
                                FlowStorage.getApiClient(mytoken);
                            AptScheduleApi(apiClient)
                                .apiV1SchedulesDelete(body: widget.schedule.id);
                            goToSchedulesScreen();
                          },
                        ),
                        optionTwo: CustomButton(
                          text: "Não",
                          textSize: 14,
                          backgroundColor: Colors.blue,
                          textColor: Colors.black,
                          onPressed: () {
                            Navigator.of(context).pop();
                          },
                        ),
                      );
                    },
                  );
                },
                size: 54,
                bottom: 46,
                right: 46,
                color: Colors.red),
          ],
        ),
      ),
    );
  }
}
