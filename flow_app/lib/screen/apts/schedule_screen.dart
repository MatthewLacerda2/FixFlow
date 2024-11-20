import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:snackbar/snackbar.dart';

import '../../components/Buttons/custom_button.dart';
import '../../components/Buttons/rounded_iconed_button.dart';
import '../../components/Inputs/date_picker_rectangle.dart';
import '../../components/Inputs/limited_text_input_field.dart';
import '../../components/Inputs/price_input_field.dart';
import '../../components/Inputs/time_picker_rectangle.dart';
import '../../components/warning_modal.dart';
import '../main/schedules_screen.dart';

//TODO: buttons must ask for a confirmation
class ScheduleScreen extends StatefulWidget {
  const ScheduleScreen({
    super.key,
    required this.schedule,
  });
  final AptSchedule schedule;

  @override
  ScheduleScreenState createState() => ScheduleScreenState();
}

class ScheduleScreenState extends State<ScheduleScreen> {
  late TextEditingController _precoController;
  late TextEditingController _observacaoController;

  bool _isEdited = false;

  @override
  void initState() {
    super.initState();
    _precoController = TextEditingController(
        text: widget.schedule.price?.toStringAsFixed(2) ?? "");
    _observacaoController =
        TextEditingController(text: widget.schedule.observation);
  }

  void _toggleEdit() {
    setState(() {
      _isEdited = true;
    });
  }

  void _saveChanges() {
    setState(() {
      snackUndo("Confirmar?", () {
        // TODO: load animation for when we wait for the response of the server
        // TODO: Check if the response is 200OK or something else
        snack("Salvo!");
        Navigator.pop(context);
      }, undoText: "Confirmar");
    });
  }

  void _cancelChanges() {
    setState(() {
      _isEdited = false;
      _precoController.text = widget.schedule.price?.toStringAsFixed(2) ?? "";
      _observacaoController.text = widget.schedule.observation ?? "";
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
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                Text(
                  'Cliente: ${widget.schedule.customer!.fullName}',
                  style: const TextStyle(
                      fontWeight: FontWeight.bold, fontSize: 24),
                ),
                if (widget.schedule.wasContacted!)
                  const Row(
                    children: <Widget>[
                      Icon(Icons.check, color: Colors.blue, size: 22),
                      SizedBox(width: 8),
                      Text(
                        'Contactado',
                        style: TextStyle(
                            fontWeight: FontWeight.bold, fontSize: 18),
                      ),
                    ],
                  ),
              ],
            ),
            const SizedBox(height: 26),
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
                    _toggleEdit();
                  },
                ),
              ],
            ),
            const SizedBox(height: 22),
            PriceInputField(
              controller: _precoController,
              onPriceValid: (String value) {
                _toggleEdit();
              },
            ),
            const SizedBox(height: 28),
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
                            "Deletar o agendamento? Esta ação não poderá ser desfeita",
                        optionOne: CustomButton(
                          text: "Sim",
                          textSize: 14,
                          backgroundColor: Colors.green,
                          textColor: Colors.black,
                          onPressed: () {
                            AptScheduleApi()
                                .apiV1SchedulesDelete(body: widget.schedule.id);
                            Navigator.of(context).pop();
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
