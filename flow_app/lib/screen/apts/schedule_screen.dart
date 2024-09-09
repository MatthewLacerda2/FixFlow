import 'package:flutter/material.dart';
import 'package:snackbar/snackbar.dart';

import '../../components/Inputs/date_picker_rectangle.dart';
import '../../components/Inputs/limited_text_input_field.dart';
import '../../components/Inputs/price_input_field.dart';
import '../../components/Inputs/time_picker_rectangle.dart';

//TODO: buttons must ask for a confirmation
class ScheduleScreen extends StatefulWidget {
  const ScheduleScreen({
    super.key,
    required this.cliente,
    required this.contactado,
    required this.horario,
    required this.dia,
    required this.preco,
    required this.observacao,
  });
  final String cliente;
  final bool contactado;
  final TimeOfDay horario;
  final DateTime dia;
  final double preco;
  final String observacao;

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
    _precoController =
        TextEditingController(text: widget.preco.toStringAsFixed(2));
    _observacaoController = TextEditingController(text: widget.observacao);
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
      _precoController.text = widget.preco.toStringAsFixed(2);
      _observacaoController.text = widget.observacao;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Agendamento'),
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
                  'Cliente: ${widget.cliente}',
                  style: const TextStyle(
                      fontWeight: FontWeight.bold, fontSize: 22),
                ),
                if (widget.contactado)
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
            const SizedBox(height: 24),
            Row(
              children: <Widget>[
                Expanded(
                  child: DatePickerRectangle(
                    initialDate: widget.dia,
                    onDateSelected: (DateTime date) {
                      _toggleEdit();
                    },
                  ),
                ),
                const SizedBox(width: 16),
                Expanded(
                  child: TimePickerRectangle(
                    initialTime: widget.horario,
                    onTimeSelected: (TimeOfDay time) {
                      _toggleEdit();
                    },
                  ),
                ),
              ],
            ),
            const SizedBox(height: 20),
            PriceInputField(
              controller: _precoController,
              onPriceValid: (String value) {
                _toggleEdit();
              },
            ),
            const SizedBox(height: 26),
            LimitedTextInputField(
              controller: _observacaoController,
              maxLength: 250,
              labelText: 'Observação',
              onChanged: (String value) => _toggleEdit(),
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
            )
          ],
        ),
      ),
    );
  }
}
