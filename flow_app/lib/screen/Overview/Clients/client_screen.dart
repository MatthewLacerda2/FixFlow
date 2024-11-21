import 'package:flutter/material.dart';

import '../../../components/Buttons/colored_border_text_button.dart';
import '../../../components/logs_list.dart';
import '../../apts/log_screen.dart';

class ClientScreen extends StatelessWidget {
  const ClientScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Client Screen'),
      ),
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            const Text(
              'Cliente: Name of Client',
              style: TextStyle(fontWeight: FontWeight.bold, fontSize: 24),
            ),
            Text(
              'Início: 01/01/2023',
              style: TextStyle(fontSize: 12, color: Colors.grey[700]),
            ),
            Text(
              'Último atendimento: ${DateTime.now().toString().split(' ')[0]}',
              style: TextStyle(fontSize: 12, color: Colors.grey[700]),
            ),
            const SizedBox(height: 16),
            const Text(
              'Phone: 123456789',
              style: TextStyle(fontSize: 16),
            ),
            const Text(
              'Email: example@example.com',
              style: TextStyle(fontSize: 16),
            ),
            const Text(
              'CPF: 123.456.789-00',
              style: TextStyle(fontSize: 16),
            ),
            const SizedBox(height: 6),
            const Text(
              'Anotação: Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
              style: TextStyle(fontSize: 14, fontStyle: FontStyle.italic),
            ),
            const SizedBox(height: 18),
            Container(
              height: 10,
              color: Colors.grey.shade800,
            ),
            const SizedBox(height: 20),
            const Text(
              'Agendamentos: 10',
              style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
            ),
            const Text(
              'Atendimentos: 10',
              style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
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
            const Text(
              'Tempo médio de retorno: 90 dias',
              style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 24),
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
            Expanded(
              child: ListView.separated(
                shrinkWrap: true,
                physics: const PageScrollPhysics(),
                itemCount: 10,
                separatorBuilder: (BuildContext context, int index) =>
                    const Divider(
                  color: Colors.transparent,
                  thickness: 0,
                  height: 9,
                ),
                itemBuilder: (BuildContext context, int index) {
                  return LogsList(
                    clientName: 'Nome do Cliente $index',
                    price: 150.00,
                    hour: '17h45m',
                    date: '21/12/24',
                    service: 'Serviço $index',
                    observation: "Observação $index",
                    onTap: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute<void>(
                          builder: (BuildContext context) => LogScreen(
                            cliente: 'Fulano $index',
                            marcouHorario: true,
                            horario: const TimeOfDay(hour: 14, minute: 30),
                            dia: DateTime(2024, 8, 27),
                            preco: 150.00,
                            observacao: "",
                          ),
                        ),
                      );
                    },
                  );
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}
