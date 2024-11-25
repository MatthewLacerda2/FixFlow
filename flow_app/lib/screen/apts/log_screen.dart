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
import '../main/main_screen.dart';

class LogScreen extends StatefulWidget {
  const LogScreen({
    super.key,
    required this.log,
  });

  final AptLog log;

  @override
  LogScreenState createState() => LogScreenState();
}

class LogScreenState extends State<LogScreen> {
  late TextEditingController _precoController;
  late TextEditingController _observacaoController;

  UpdateAptLog upLog = UpdateAptLog(id: "id");

  bool _isEdited = false;

  @override
  void initState() {
    super.initState();

    final AptLog log = widget.log;
    upLog = UpdateAptLog(
        id: log.id,
        dateTime: log.dateTime,
        description: log.description,
        price: log.price,
        scheduleId: log.scheduleId,
        service: log.service);
    print(upLog);

    _precoController = TextEditingController(
        text: widget.log.price?.toStringAsFixed(2) ?? "0.0");
    _observacaoController = TextEditingController(text: widget.log.description);
  }

  void _toggleEdit() {
    setState(() {
      _isEdited = true;
    });
  }

  void _saveChanges() async {
    final Response response =
        await AptLogApi().apiV1LogsPatchWithHttpInfo(updateAptLog: upLog);
    snackbarResponse(response);
  }

  void snackbarResponse(Response response) {
    if (response.statusCode == 200) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text("Agendamento editado!"),
        ),
      );
      goToLogsScreen();
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text(response.body),
        ),
      );
    }
  }

  void _cancelChanges() {
    setState(() {
      _isEdited = false;
      _precoController.text = widget.log.price?.toStringAsFixed(2) ?? "0.0";
      _observacaoController.text = widget.log.description ?? "";
    });
  }

  void goToLogsScreen() {
    Navigator.pushAndRemoveUntil(
      context,
      MaterialPageRoute<void>(
          builder: (BuildContext context) => const MainScreen(
                initialIndex: 3,
              )),
      (Route<dynamic> route) => false,
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Editar Atendimento'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(24.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                Text(
                  'Cliente: ${widget.log.customer!.fullName}',
                  style: const TextStyle(
                      fontWeight: FontWeight.bold, fontSize: 22),
                ),
                if (widget.log.scheduleId != null)
                  const Row(
                    children: <Widget>[
                      Icon(Icons.check, color: Colors.blue, size: 22),
                      SizedBox(width: 8),
                      Text(
                        'Agendado',
                        style: TextStyle(
                            fontWeight: FontWeight.bold, fontSize: 18),
                      ),
                    ],
                  ),
              ],
            ),
            const SizedBox(height: 24),
            ServicesInputField(
              initialService: widget.log.service,
              onServiceSelected: (String? selectedService) {
                upLog.service = selectedService;
              },
            ),
            const SizedBox(height: 24),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                const Text(
                  'Dia:',
                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 18),
                ),
                DatePickerRectangle(
                  initialDate: widget.log.dateTime!,
                  onDateSelected: (DateTime date) {
                    upLog.dateTime =
                        DateTimeUtils.setDate(upLog.dateTime!, date);
                    _toggleEdit();
                  },
                ),
                const Text(
                  'Hora:',
                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 18),
                ),
                TimePickerRectangle(
                  initialTime: TimeOfDay.fromDateTime(widget.log.dateTime!),
                  onTimeSelected: (TimeOfDay time) {
                    upLog.dateTime =
                        DateTimeUtils.setTime(time, upLog.dateTime!);
                    _toggleEdit();
                  },
                ),
              ],
            ),
            const SizedBox(height: 16),
            const SizedBox(height: 16),
            PriceInputField(
              controller: _precoController,
              onPriceValid: (String value) {
                upLog.price = double.parse(value);
                _toggleEdit();
              },
            ),
            const SizedBox(height: 24),
            LimitedTextInputField(
              controller: _observacaoController,
              maxLength: 250,
              labelText: 'Observação',
              onChanged: (String value) {
                upLog.description = value;
                _toggleEdit();
              },
            ),
            const SizedBox(height: 32),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: <Widget>[
                ElevatedButton(
                  onPressed: _isEdited ? _saveChanges : null,
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
                          onPressed: () {
                            AptLogApi().apiV1LogsDelete(body: widget.log.id);
                            goToLogsScreen();
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
