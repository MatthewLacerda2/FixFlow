import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart';
import 'package:snackbar/snackbar.dart';

import '../../../components/Inputs/customer_dropdown.dart';
import '../../../components/Inputs/date_picker_rectangle.dart';
import '../../../components/Inputs/limited_text_input_field.dart';
import '../../../components/Inputs/price_input_field.dart';
import '../../../components/Inputs/time_picker_rectangle.dart';
import '../../../utils/date_time_utils.dart';
import '../../main/main_screen.dart';

class CreateScheduleScreen extends StatefulWidget {
  const CreateScheduleScreen({
    super.key,
    this.cliente,
    required this.contactado,
    required this.horario,
    required this.dia,
    required this.preco,
    required this.observacao,
  });

  final String? cliente;
  final bool contactado;
  final TimeOfDay horario;
  final DateTime dia;
  final double preco;
  final String observacao;

  @override
  CreateScheduleScreenState createState() => CreateScheduleScreenState();
}

class CreateScheduleScreenState extends State<CreateScheduleScreen> {
  late TextEditingController _precoController;
  late TextEditingController _observacaoController;

  String customerId = "";
  late DateTime dateTime = DateTime.now();

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

  void _saveChanges() async {
    setState(() {
      // Show loading indicator or disable buttons here
    });

    try {
      final CreateAptSchedule createAptSchedule = CreateAptSchedule(
        customerId: customerId,
        dateTime: dateTime,
        service: 'Service Name',
        observation: _observacaoController.text,
        price: double.tryParse(_precoController.text) ?? 0.0,
      );
      print("UHASUHASAS");
      print(createAptSchedule);
      print("UHASUHASAS");
      final Response response = await AptScheduleApi()
          .apiV1SchedulesPostWithHttpInfo(createAptSchedule: createAptSchedule);
      print("osfidjgsorgi");
      if (response.statusCode == 201) {
        print("11111111");
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text("Agendamento feito!"),
          ),
        );
        Navigator.pushAndRemoveUntil(
          context,
          MaterialPageRoute(
              builder: (BuildContext context) => const MainScreen()),
          (Route<dynamic> route) => false,
        );
      } else {
        print("2222222222");
        print(createAptSchedule);
        print(response);
        print(response.body);
        snack("Error: $response");
      }
    } catch (error) {
      snack("An error occurred: $error");
    }
    print("debug");
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
        title: const Text(
          'Criar Agendamento',
          style: TextStyle(fontWeight: FontWeight.bold),
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(24.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                Expanded(
                  child: CustomerDropdown(
                    onCustomerIdChanged: (String id) {
                      customerId = id;
                    },
                  ),
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
                      dateTime = DateTimeUtils.setDate(dateTime, date);
                      print("umubuga");
                      _toggleEdit();
                    },
                  ),
                ),
                const SizedBox(width: 16),
                Expanded(
                  child: TimePickerRectangle(
                    initialTime: widget.horario,
                    onTimeSelected: (TimeOfDay time) {
                      dateTime = DateTimeUtils.setTime(time, dateTime);
                      print("fei di tal");
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
            const SizedBox(height: 20),
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
                    padding: const EdgeInsets.symmetric(
                      horizontal: 16,
                      vertical: 5,
                    ),
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
