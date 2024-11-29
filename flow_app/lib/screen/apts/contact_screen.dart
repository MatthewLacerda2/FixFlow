import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart';

import '../../components/Buttons/custom_button.dart';
import '../../components/Buttons/rounded_iconed_button.dart';
import '../../components/Inputs/date_picker_rectangle.dart';
import '../../components/Inputs/time_picker_rectangle.dart';
import '../../components/copyable_text.dart';
import '../../components/warning_modal.dart';
import '../../utils/date_time_utils.dart';
import '../../utils/flow_storage.dart';
import '../../utils/string_utils.dart';
import '../main/main_screen.dart';

class ContactScreen extends StatefulWidget {
  const ContactScreen({
    super.key,
    required this.contact,
  });

  final AptContact contact;

  @override
  ContactScreenState createState() => ContactScreenState();
}

class ContactScreenState extends State<ContactScreen> {
  UpdateAptContact upCont =
      UpdateAptContact(id: "id", dateTime: DateTime(2024), beenDone: false);

  String suggestedMessage = "";

  bool _isEdited = false;

  @override
  void initState() {
    super.initState();

    upCont = UpdateAptContact(
        id: widget.contact.id,
        dateTime: widget.contact.dateTime!,
        beenDone: false);

    _initializeSuggestedMessage();
  }

  Future<void> _initializeSuggestedMessage() async {
    final String message = await getSuggestedText();
    setState(() {
      suggestedMessage = message;
    });
  }

  Future<String> getSuggestedText() async {
    final BusinessDTO? dto = await FlowStorage.getBusinessDTO();
    String? service = widget.contact.aptLog!.service;
    if (service != null) {
      service += " ";
    }
    final String dia =
        DateTimeUtils.dateOnlyString(widget.contact.aptLog!.dateTime);
    return 'Olá, aqui é da ${dto!.name}. Você contratou um serviço conosco ${service}dia $dia, gostaria de agendar novamente?';
  }

  void _toggleEdit() {
    setState(() {
      _isEdited = true;
    });
  }

  void _saveChanges() async {
    final Response response = await AptContactApi()
        .apiV1ContactsPatchWithHttpInfo(updateAptContact: upCont);
    snackbarResponse(response);
  }

  void snackbarResponse(Response response) {
    if (response.statusCode == 200) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text("Lembrete editado!"),
        ),
      );
      goToContactScreen();
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
    });
  }

  void goToContactScreen() {
    Navigator.pushAndRemoveUntil(
      context,
      MaterialPageRoute<void>(
          builder: (BuildContext context) => const MainScreen(
                initialIndex: 2,
              )),
      (Route<dynamic> route) => false,
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Editar Lembrete'),
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
                  'Cliente: ${widget.contact.customer!.fullName}',
                  style: const TextStyle(
                      fontWeight: FontWeight.bold, fontSize: 24),
                ),
              ],
            ),
            const SizedBox(height: 16),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                Text(
                  'Dia: ${DateTimeUtils.dateOnlyString(widget.contact.dateTime)}',
                  style: const TextStyle(
                      fontWeight: FontWeight.bold, fontSize: 20),
                ),
                Text(
                  'Hora: ${TimeOfDay.fromDateTime(widget.contact.dateTime!).format(context)}',
                  style: const TextStyle(
                      fontWeight: FontWeight.bold, fontSize: 20),
                ),
              ],
            ),
            const SizedBox(height: 16),
            Text(
              'Serviço: ${StringUtils.normalIfBlank(widget.contact.aptLog!.service)}',
              style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
            ),
            const SizedBox(height: 16),
            Text(
              'Observação: ${StringUtils.normalIfBlank(widget.contact.aptLog!.description)}',
              style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
            ),
            const SizedBox(height: 32),
            const Text(
              'Alterar data e horário:',
              style: TextStyle(fontSize: 12),
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                const Text(
                  'Dia:',
                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
                ),
                DatePickerRectangle(
                  initialDate: widget.contact.dateTime!,
                  onDateSelected: (DateTime date) {
                    upCont.dateTime =
                        DateTimeUtils.setDate(upCont.dateTime, date);
                    _toggleEdit();
                  },
                ),
                const Text(
                  'Hora:',
                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
                ),
                TimePickerRectangle(
                  initialTime: TimeOfDay.fromDateTime(widget.contact.dateTime!),
                  onTimeSelected: (TimeOfDay time) {
                    upCont.dateTime =
                        DateTimeUtils.setTime(time, upCont.dateTime);
                    _toggleEdit();
                  },
                ),
              ],
            ),
            const SizedBox(height: 34),
            CopyableText(text: suggestedMessage),
            //WhatsAppButton(
            //    phoneNumber: widget.contact.customer!.phoneNumber!,
            //    message: "um dois tres trestando"),
            const SizedBox(height: 42),
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
            ),
            const SizedBox(height: 64),
            RoundedIconedButton(
                icon: Icons.delete_forever_rounded,
                onPressed: () {
                  showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      return WarningModal(
                        title: "Você tem certeza?",
                        description:
                            "Deletar o lembrete? Esta ação não poderá ser desfeita\n",
                        optionOne: CustomButton(
                          text: "Sim",
                          textSize: 14,
                          backgroundColor: Colors.green,
                          textColor: Colors.black,
                          onPressed: () {
                            AptContactApi()
                                .apiV1ContactsDelete(body: widget.contact.id);
                            goToContactScreen();
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
