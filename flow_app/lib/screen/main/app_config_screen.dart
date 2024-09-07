import 'package:flutter/material.dart';

import '../../components/Buttons/colored_border_text_button.dart';
import '../../components/Buttons/custom_button.dart';
import '../../components/Inputs/check_input_field.dart';
import '../../components/Inputs/enum_field.dart';
import '../../components/Inputs/time_picker_rectangle.dart';
import '../../components/warning_modal.dart';
import '../AppConfig/change_password/change_password_screen.dart';
import '../AppConfig/change_phone/change_phone_screen.dart';
import '../AppConfig/deactivate_account/deactivate_account_screen.dart';
import '../AppConfig/delete_account/delete_warning_screen.dart';
import '../auth/initial_screen.dart';

//TODO: the preload options are not loading
//TODO: the vertical size of the input area is too big
//TODO: the background is not white
//TODO: the counter is outside the input area
class AppConfigScreen extends StatelessWidget {
  const AppConfigScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Configurações da Conta'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: <Widget>[
              const Text(
                'Horário Comercial',
                style: TextStyle(fontWeight: FontWeight.bold, fontSize: 22),
              ),
              const SizedBox(height: 15),
              ...List<Widget>.generate(7, (int index) {
                final List<String> daysOfWeek = <String>[
                  'Domingo',
                  'Segunda',
                  'Terça',
                  'Quarta',
                  'Quinta',
                  'Sexta',
                  'Sábado'
                ];
                return _CommercialHoursRow(day: daysOfWeek[index]);
              }),
              const SizedBox(height: 18),
              Container(
                height: 10,
                color: Colors.grey.shade800,
              ),
              const SizedBox(height: 32),
              const Text(
                'Opções de Serviços',
                style: TextStyle(fontWeight: FontWeight.bold, fontSize: 22),
              ),
              const SizedBox(height: 12),
              const EnumField(
                  description: "Serviço...",
                  options: <String>['Item 1', 'Item 2', 'Item 3'],
                  characterLimit: 20),
              const SizedBox(height: 3),
              const Text(
                'Opções mais comuns de serviços prestados',
                style: TextStyle(color: Colors.grey, fontSize: 12),
              ),
              const SizedBox(height: 10),
              CheckInputField(
                label: 'Permitir apenas serviços listados?',
                initialValue: false,
                onChanged: (bool isChecked) {
                  print('Outros: $isChecked');
                },
              ),
              const SizedBox(height: 18),
              CheckInputField(
                label: 'Atende aos feriados?',
                initialValue: false,
                onChanged: (bool isChecked) {
                  print('Atende aos feriados: $isChecked');
                },
              ),
              const SizedBox(height: 16),
              CheckInputField(
                label: 'Atende a domicílio?',
                initialValue: false,
                onChanged: (bool isChecked) {
                  print('Atende a domicílio: $isChecked');
                },
              ),
              // TODO: handle google calendar, simultaneous appointments
              const SizedBox(height: 28),
              Container(
                height: 10,
                color: Colors.grey.shade800,
              ),
              const SizedBox(height: 28),
              const Text(
                'Notificações',
                style: TextStyle(fontWeight: FontWeight.bold, fontSize: 22),
              ),
              const SizedBox(height: 15),
              CheckInputField(
                label: 'Aviso de "agendamento próximo"',
                initialValue: true,
                onChanged: (bool isChecked) {
                  print('Aviso de agendamento próximo: $isChecked');
                },
              ),
              const SizedBox(height: 12),
              CheckInputField(
                label: 'Notificar novos agendamentos',
                initialValue: true,
                onChanged: (bool isChecked) {
                  print('Notificar novos agendamentos: $isChecked');
                },
              ),
              const SizedBox(height: 12),
              CheckInputField(
                label: 'Notificar novos atendimentos',
                initialValue: true,
                onChanged: (bool isChecked) {
                  print('Notificar novos atendimentos: $isChecked');
                },
              ),
              const SizedBox(height: 24),
              ColoredBorderTextButton(
                text: 'Trocar telefone',
                onPressed: () {
                  Navigator.push(
                      context,
                      MaterialPageRoute<void>(
                          builder: (BuildContext context) =>
                              const ChangePhoneScreen()));
                },
                textColor: Colors.green,
              ),
              const SizedBox(height: 16),
              ColoredBorderTextButton(
                text: 'Trocar senha',
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute<void>(
                        builder: (BuildContext context) =>
                            const ChangePasswordScreen()),
                  );
                },
                textColor: Colors.blue,
              ),
              const SizedBox(height: 16),
              ColoredBorderTextButton(
                text: 'Desativar conta',
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute<void>(
                        builder: (BuildContext context) =>
                            const DeactivateAccountScreen()),
                  );
                },
                textColor: Colors.grey[850]!,
              ),
              const SizedBox(height: 16),
              ColoredBorderTextButton(
                text: 'Deletar conta',
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute<void>(
                      builder: (BuildContext context) =>
                          const DeleteWarningScreen(),
                    ),
                  );
                },
                textColor: Colors.red[900]!,
              ),
              const SizedBox(height: 16),
              ColoredBorderTextButton(
                text: 'Sair',
                onPressed: () {
                  showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      return WarningModal(
                        title: "Você quer sair da conta?",
                        optionOne: CustomButton(
                          text: "Sim",
                          textSize: 16,
                          backgroundColor: Colors.green,
                          textColor: Colors.black,
                          onPressed: () {
                            Navigator.push(
                              context,
                              MaterialPageRoute<void>(
                                builder: (BuildContext context) =>
                                    const InitialScreen(),
                              ),
                            );
                          },
                        ),
                        optionTwo: CustomButton(
                          text: "Não",
                          textSize: 16,
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
                textColor: Colors.red[500]!,
              ),
              const SizedBox(height: 60),
            ],
          ),
        ),
      ),
    );
  }
}

class _CommercialHoursRow extends StatefulWidget {
  const _CommercialHoursRow({required this.day});
  final String day;

  @override
  __CommercialHoursRowState createState() => __CommercialHoursRowState();
}

class __CommercialHoursRowState extends State<_CommercialHoursRow> {
  bool isActive = true;

  @override
  Widget build(BuildContext context) {
    return Row(
      children: <Widget>[
        Text(
          '${widget.day}:',
          style: const TextStyle(fontSize: 16),
        ),
        const Spacer(),
        if (isActive)
          Row(
            children: <Widget>[
              TimePickerRectangle(
                initialTime: const TimeOfDay(hour: 8, minute: 0),
                onTimeSelected: (TimeOfDay time) {
                  print('${widget.day} Start Time: $time');
                },
              ),
              const SizedBox(width: 8),
              TimePickerRectangle(
                initialTime: const TimeOfDay(hour: 17, minute: 0),
                onTimeSelected: (TimeOfDay time) {
                  print('${widget.day} End Time: $time');
                },
              ),
            ],
          )
        else
          const Text(
            'Sem horário',
            style: TextStyle(
                color: Colors.grey, fontSize: 14, fontWeight: FontWeight.bold),
          ),
        IconButton(
          icon: Icon(isActive ? Icons.close : Icons.add,
              color: isActive ? Colors.red : Colors.green, size: 22),
          onPressed: () {
            setState(() {
              isActive = !isActive;
            });
          },
        ),
      ],
    );
  }
}
