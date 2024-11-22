import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../../components/Buttons/colored_border_text_button.dart';
import '../../../components/logs_list.dart';
import '../../../utils/date_time_utils.dart';
import '../../apts/log_screen.dart';

class ClientScreen extends StatelessWidget {
  const ClientScreen({super.key, required this.record});

  final CustomerRecord record;

  double getTotalSpent(List<AptLog> aptLogs) {
    return aptLogs.fold(0.0, (double sum, AptLog log) => sum + log.price!);
  }

  String getFormattedDate(DateTime? date) {
    if (date == null) {
      return '-';
    }

    return DateTimeUtils.dateOnlyString(date);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Cliente'),
      ),
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 16),
        child: ListView(
          children: <Widget>[
            Text(
              record.fullName,
              style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 24),
            ),
            Text(
              'Início: ${getFormattedDate(record.firstLog)}',
              style: TextStyle(fontSize: 12, color: Colors.grey[700]),
            ),
            const SizedBox(height: 4),
            Text(
              'Último atendimento: ${getFormattedDate(record.lastLog)}',
              style: TextStyle(fontSize: 12, color: Colors.grey[700]),
            ),
            const SizedBox(height: 10),
            Text(
              'Phone: ${record.phoneNumber}',
              style: const TextStyle(fontSize: 16),
            ),
            Text(
              'Email: ${record.email ?? 'não informado'}',
              style: const TextStyle(fontSize: 16),
            ),
            Text(
              'CPF: ${record.cpf ?? 'não informado'}',
              style: const TextStyle(fontSize: 16),
            ),
            const SizedBox(height: 8),
            Text(
              'Anotação: ${record.additionalNote}',
              style: const TextStyle(fontSize: 14, fontStyle: FontStyle.italic),
            ),
            const SizedBox(height: 18),
            Container(
              height: 8,
              color: Colors.grey.shade800,
            ),
            const SizedBox(height: 18),
            Text(
              'Agendamentos: ${record.numSchedules}',
              style: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
            ),
            Text(
              'Atendimentos: ${record.logs?.length ?? " 0"}',
              style: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 2),
            const Row(
              children: <Widget>[
                Text(
                  'Total gasto: ',
                  style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                ),
                Text(
                  'R\$1510',
                  style: TextStyle(
                      fontSize: 18,
                      fontWeight: FontWeight.bold,
                      color: Colors.blue),
                )
              ],
            ),
            const SizedBox(height: 2),
            Text(
              'Tempo médio de retorno: ${record.avgTimeBetweenSchedules}',
              style: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 22),
            Container(
              height: 1,
              color: Colors.grey,
            ),
            const SizedBox(height: 6),
            Center(
              child: ColoredBorderTextButton(
                text: 'Atendimentos',
                onPressed: () {},
                textColor: Colors.grey[800]!,
                backgroundColor: Colors.grey[200]!,
              ),
            ),
            const SizedBox(height: 6),
            Container(
              height: 1,
              color: Colors.grey,
            ),
            const SizedBox(height: 10),
            ...record.logs!.map((AptLog log) {
              final String timeOfDayString =
                  TimeOfDay.fromDateTime(log.dateTime!).format(context);
              return LogsList(
                clientName: record.fullName,
                price: log.price ??
                    0, //TODO: prices are coming null, this is just a band-aid
                hour: timeOfDayString,
                date: DateTimeUtils.dateOnlyString(log.dateTime!),
                service: log.service ?? '-',
                observation: log.description ?? '-',
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute<void>(
                      builder: (BuildContext context) => LogScreen(
                        log: log,
                      ),
                    ),
                  );
                },
              );
            }),
          ],
        ),
      ),
    );
  }
}
