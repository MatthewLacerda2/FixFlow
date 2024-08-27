import 'package:flutter/material.dart';
import 'package:snackbar/snackbar.dart';

class LogPage extends StatefulWidget {
  final String cliente;
  final bool marcouHorario;
  final TimeOfDay horario;
  final DateTime dia;
  final double preco;
  final String observacao;

  const LogPage({
    Key? key,
    required this.cliente,
    required this.marcouHorario,
    required this.horario,
    required this.dia,
    required this.preco,
    required this.observacao,
  }) : super(key: key);

  @override
  _LogPageState createState() => _LogPageState();
}

class _LogPageState extends State<LogPage> {
  late TextEditingController _horarioController;
  late TextEditingController _diaController;
  late TextEditingController _precoController;
  late TextEditingController _observacaoController;

  bool _isEdited = false;

  @override
  void initState() {
    super.initState();
    _horarioController = TextEditingController();
    _diaController = TextEditingController();
    _precoController = TextEditingController();
    _observacaoController = TextEditingController();
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _horarioController.text = widget.horario.format(context);
    _diaController.text =
        "${widget.dia.day}/${widget.dia.month}/${widget.dia.year}";
    _precoController.text = widget.preco.toStringAsFixed(2);
    _observacaoController.text = widget.observacao;
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
      _horarioController.text = widget.horario.format(context);
      _diaController.text =
          "${widget.dia.day}/${widget.dia.month}/${widget.dia.year}";
      _precoController.text = widget.preco.toStringAsFixed(2);
      _observacaoController.text = widget.observacao;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Log'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Text(
              'Cliente: ${widget.cliente}',
              style: TextStyle(fontWeight: FontWeight.bold),
            ),
            SizedBox(height: 10),
            Text(
              'Marcou horário: ${widget.marcouHorario ? "Sim" : "Não"}',
              style: TextStyle(fontWeight: FontWeight.bold),
            ),
            SizedBox(height: 20),
            TextField(
              controller: _horarioController,
              decoration: InputDecoration(labelText: 'Horário'),
              onChanged: (value) => _toggleEdit(),
            ),
            TextField(
              controller: _diaController,
              decoration: InputDecoration(labelText: 'Dia'),
              onChanged: (value) => _toggleEdit(),
            ),
            TextField(
              controller: _precoController,
              decoration: InputDecoration(labelText: 'Preço'),
              keyboardType: TextInputType.number,
              onChanged: (value) => _toggleEdit(),
            ),
            TextField(
              controller: _observacaoController,
              maxLength: 250,
              decoration: InputDecoration(
                labelText: 'Observação',
                helperText: 'Apenas até 250 caracteres',
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
