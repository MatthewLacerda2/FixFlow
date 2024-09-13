import 'package:flutter/material.dart';

import 'edit_apt/create_schedule_screen.dart';

class ContactScreen extends StatefulWidget {
  const ContactScreen(
      {super.key,
      required this.cliente,
      required this.dia,
      required this.previousHorario,
      required this.previousDia,
      required this.previousPrice,
      required this.previousObservacao});

  final String cliente;
  final DateTime dia;
  final TimeOfDay previousHorario;
  final DateTime previousDia;
  final double previousPrice;
  final String previousObservacao;

  @override
  ContactScreenState createState() => ContactScreenState();
}

class ContactScreenState extends State<ContactScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Contato'),
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
                Text(
                  '${widget.dia.day.toString().padLeft(2, '0')} / ${widget.dia.month.toString().padLeft(2, '0')} / ${widget.dia.year}',
                  style: const TextStyle(
                      fontWeight: FontWeight.normal, fontSize: 18),
                ),
              ],
            ),
            const SizedBox(height: 23),
            const Text(
              'Atendimento anterior:',
              style: TextStyle(fontWeight: FontWeight.w500, fontSize: 20),
            ),
            const SizedBox(height: 16),
            Text(
              'Horário: ${widget.previousHorario.format(context)}',
              style: const TextStyle(fontSize: 16),
            ),
            const SizedBox(height: 6),
            Text(
              'Dia: ${widget.previousDia.day.toString().padLeft(2, '0')}/${widget.previousDia.month.toString().padLeft(2, '0')}/${widget.previousDia.year}',
              style: const TextStyle(fontSize: 16),
            ),
            const SizedBox(height: 6),
            Text(
              'Preço: R\$ ${widget.previousPrice.toStringAsFixed(2)}',
              style: const TextStyle(fontSize: 16),
            ),
            const SizedBox(height: 6),
            Text(
              'Observação: ${widget.previousObservacao}',
              style: const TextStyle(fontSize: 16),
            ),
            const SizedBox(height: 34),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: <Widget>[
                ElevatedButton(
                  onPressed: () {
                    print("debug is on the table");
                    Navigator.push(
                      context,
                      MaterialPageRoute<void>(
                        builder: (BuildContext context) => CreateScheduleScreen(
                          cliente: widget.cliente,
                          contactado: true,
                          horario: TimeOfDay.now(),
                          dia: DateTime(2024, 8, 27),
                          preco: 150.00,
                          observacao: "This is an observation",
                        ),
                      ),
                    );
                  },
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.blue,
                  ),
                  child: const Text(
                    'Criar agendamento',
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
