import 'package:flutter/material.dart';

import '../../../components/Buttons/custom_button.dart';
import '../../components/Inputs/date_picker_rectangle.dart';
import '../../components/Inputs/time_picker_rectangle.dart';
import '../main/app_config_screen.dart';
import 'change_successful.dart';

//TODO: load the schedules which are in that period, let the user decide if he wants to delete them and tell the user, or maintain the schedule
class CreateIdlePeriodScreen extends StatelessWidget {
  const CreateIdlePeriodScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(24),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            const Spacer(),
            const Center(
              child: Text(
                "Criar período ocioso",
                style: TextStyle(
                  fontWeight: FontWeight.bold,
                  fontSize: 30,
                ),
              ),
            ),
            const SizedBox(height: 14),
            Text(
              "Isso pode ser útil para manutenções, feriados ou qualquer outra situação em que você não possa realizar atendimentos",
              textAlign: TextAlign.justify,
              style: TextStyle(fontSize: 16, color: Colors.grey[700]),
            ),
            const SizedBox(height: 16),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                const Text(
                  'Início: ',
                  style: TextStyle(fontSize: 18),
                ),
                DatePickerRectangle(
                  initialDate: DateTime.now(),
                  onDateSelected: (DateTime date) {
                    print("debug is on the table");
                  },
                ),
                TimePickerRectangle(
                  initialTime: TimeOfDay.now(),
                  onTimeSelected: (TimeOfDay time) {
                    print("debug is on the table");
                  },
                ),
              ],
            ),
            const SizedBox(height: 16),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                const Text(
                  'Fim:    ',
                  style: TextStyle(fontSize: 18),
                ),
                DatePickerRectangle(
                  initialDate: DateTime.now(),
                  onDateSelected: (DateTime date) {
                    print("debug is on the table");
                  },
                ),
                TimePickerRectangle(
                  initialTime: TimeOfDay.now(),
                  onTimeSelected: (TimeOfDay time) {
                    print("debug is on the table");
                  },
                ),
              ],
            ),
            const SizedBox(height: 66),
            Align(
                child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                  CustomButton(
                    text: "Voltar",
                    textSize: 18,
                    backgroundColor: Colors.white,
                    borderRadius: 12,
                    borderWidth: 1.6,
                    padding:
                        const EdgeInsets.symmetric(vertical: 1, horizontal: 44),
                    onPressed: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                            builder: (BuildContext context) =>
                                const AppConfigScreen()),
                      );
                    },
                  ),
                  CustomButton(
                    text: "Confirmar",
                    textSize: 18,
                    backgroundColor: Colors.green,
                    textColor: Colors.white,
                    borderRadius: 12,
                    borderWidth: 1.6,
                    padding:
                        const EdgeInsets.symmetric(vertical: 1, horizontal: 32),
                    onPressed: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                            builder: (BuildContext context) =>
                                const ChangeSuccessfulScreen(
                                  title: "Período criado com sucesso",
                                  description:
                                      "Seu período ocioso foi criado com sucesso\n\nNão será possível criar agendamentos para este período\n\nVocê pode cancelar esse período a qualquer momento, na tela de configurações",
                                )),
                      );
                    },
                  ),
                ])),
            const Spacer(),
          ],
        ),
      ),
    );
  }
}
