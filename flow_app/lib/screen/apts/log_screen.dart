import 'package:flutter/material.dart';
import 'package:snackbar/snackbar.dart';

import '../../components/Inputs/date_picker_rectangle.dart';
import '../../components/Inputs/time_picker_rectangle.dart';

class LogScreen extends StatefulWidget {
  final String cliente;
  final bool marcouHorario;
  final TimeOfDay horario;
  final DateTime dia;
  final double preco;
  final String observacao;

  const LogScreen({
    Key? key,
    required this.cliente,
    required this.marcouHorario,
    required this.horario,
    required this.dia,
    required this.preco,
    required this.observacao,
  }) : super(key: key);

  @override
  _LogScreenState createState() => _LogScreenState();
}

class _LogScreenState extends State<LogScreen> {
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
        Navigator.pop(context); // Go back to the logs screen
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
        title: Text('Atendimento'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Text(
                  'Cliente: ${widget.cliente}',
                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 18),
                ),
                if (widget.marcouHorario)
                  Row(
                    children: [
                      Icon(Icons.check, color: Colors.green),
                      SizedBox(width: 5),
                      Text(
                        'Agendado',
                        style: TextStyle(
                            fontWeight: FontWeight.bold, fontSize: 15),
                      ),
                    ],
                  ),
              ],
            ),
            SizedBox(height: 20),
            Row(
              children: [
                Expanded(
                  child: DatePickerRectangle(
                    initialDate: DateTime.now(),
                  ),
                ),
                SizedBox(width: 10),
                Expanded(
                  child: TimePickerRectangle(
                    initialTime: TimeOfDay.now(),
                  ),
                ),
              ],
            ),
            SizedBox(height: 20),
            TextField(
              controller: _precoController,
              keyboardType: TextInputType.numberWithOptions(decimal: true),
              decoration: InputDecoration(
                labelText: 'Preço: R\$',
                suffixText: 'R\$',
              ),
              onChanged: (value) {
                if (double.tryParse(value) != null) {
                  _toggleEdit();
                }
              },
            ),
            SizedBox(height: 20),
            TextField(
              controller: _observacaoController,
              maxLength: 250,
              decoration: InputDecoration(
                labelText: 'Observação',
              ),
              onChanged: (value) => _toggleEdit(),
            ),
            SizedBox(height: 20),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: [
                ElevatedButton(
                  onPressed: _isEdited ? _cancelChanges : null,
                  child: _isEdited ? Text('Cancelar') : Text('Voltar'),
                  style: ElevatedButton.styleFrom(
                    backgroundColor: _isEdited ? Colors.blue : Colors.grey,
                  ),
                ),
                ElevatedButton(
                  onPressed: _isEdited ? _saveChanges : null,
                  child: Text('Salvar'),
                  style: ElevatedButton.styleFrom(
                    backgroundColor: _isEdited ? Colors.green : Colors.grey,
                  ),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
