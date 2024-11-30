import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart';
import 'package:snackbar/snackbar.dart';

import '../../../components/Inputs/customer_dropdown.dart';
import '../../../components/Inputs/date_picker_rectangle.dart';
import '../../../components/Inputs/limited_text_input_field.dart';
import '../../../components/Inputs/price_input_field.dart';
import '../../../components/Inputs/services_input_field.dart';
import '../../../components/Inputs/time_picker_rectangle.dart';
import '../../../utils/flow_snack.dart';
import '../../../utils/flow_storage.dart';
import '../../main/main_screen.dart';

class CreateLogScreen extends StatefulWidget {
  const CreateLogScreen({
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
  CreateLogScreenState createState() => CreateLogScreenState();
}

class CreateLogScreenState extends State<CreateLogScreen> {
  late TextEditingController _precoController;
  late TextEditingController _observacaoController;

  String customerId = "";
  DateTime whenShouldComeBack = DateTime.now();
  DateTime registerDate = DateTime.now();

  String service = "";
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
    final BusinessDTO? bd = await FlowStorage.getBusinessDTO();
    final String mytoken = await FlowStorage.getToken();
    final ApiClient apiClient = FlowStorage.getApiClient(mytoken);
    final String businessId = bd!.id!;

    final CreateAptLog createLog = CreateAptLog(
        customerId: customerId,
        businessId: businessId,
        dateTime: registerDate,
        observation: _observacaoController.text,
        price: double.tryParse(_precoController.text) ?? 0.0,
        service: service,
        whenShouldCustomerComeBack: whenShouldComeBack);

    final Response response = await AptLogApi(apiClient)
        .apiV1LogsPostWithHttpInfo(createAptLog: createLog);

    if (response.statusCode == 201) {
      FlowSnack.show(context, "Atendimento registrado!");
      Navigator.pushAndRemoveUntil(
        context,
        MaterialPageRoute<void>(
            builder: (BuildContext context) => const MainScreen(
                  initialIndex: 3,
                )),
        (Route<dynamic> route) => false,
      );
    } else {
      print(createLog);
      print(response.body);
      snack("Error: $response");
    }
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
          'Registrar Atendimento',
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
                  child: widget.cliente != null
                      ? Text(
                          'Cliente: ${widget.cliente}',
                          style: const TextStyle(
                              fontWeight: FontWeight.bold, fontSize: 22),
                        )
                      : CustomerDropdown(
                          onCustomerIdChanged: (String id) {
                            customerId = id;
                          },
                        ),
                ),
                if (widget.cliente != null || widget.contactado)
                  const Row(
                    children: <Widget>[
                      Icon(Icons.check, color: Colors.blue, size: 22),
                      SizedBox(width: 8),
                      Text(
                        'Pré-Agendado',
                        style: TextStyle(
                            fontWeight: FontWeight.bold, fontSize: 18),
                      ),
                    ],
                  ),
              ],
            ),
            const SizedBox(height: 24),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                Row(
                  children: <Widget>[
                    const Text('Data:', style: TextStyle(fontSize: 18)),
                    const SizedBox(width: 6),
                    DatePickerRectangle(
                      initialDate: widget.dia,
                      onDateSelected: (DateTime date) {
                        registerDate = date;
                        _toggleEdit();
                      },
                    ),
                  ],
                ),
                Row(
                  children: <Widget>[
                    const Text('Hora:', style: TextStyle(fontSize: 18)),
                    const SizedBox(width: 6),
                    TimePickerRectangle(
                      initialTime: widget.horario,
                      onTimeSelected: (TimeOfDay time) {
                        _toggleEdit();
                      },
                    ),
                  ],
                ),
              ],
            ),
            const SizedBox(height: 20),
            ServicesInputField(
              onServiceSelected: (String? selectedService) {
                service = selectedService ?? "";
              },
            ),
            const SizedBox(height: 24),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: <Widget>[
                Row(
                  children: <Widget>[
                    const Text('Retornar em:', style: TextStyle(fontSize: 18)),
                    const SizedBox(width: 6),
                    DatePickerRectangle(
                      initialDate: widget.dia,
                      onDateSelected: (DateTime date) {
                        whenShouldComeBack = date;
                        _toggleEdit();
                      },
                    ),
                  ],
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
                  onPressed: () {
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
            )
          ],
        ),
      ),
    );
  }
}
