import 'package:flutter/material.dart';

import '../components/Inputs/date_picker_rectangle.dart';
import '../components/Inputs/name_input_field.dart';
import '../components/Inputs/time_picker_rectangle.dart';

class AptFiltersScreen extends StatelessWidget {
  const AptFiltersScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Filtros'),
        titleTextStyle: const TextStyle(
            fontWeight: FontWeight.bold, color: Colors.black, fontSize: 22),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: SingleChildScrollView(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              NameInputField(
                placeholder: 'Cliente',
                onNameChanged: (String name) {
                  print('Cliente is: $name');
                },
              ),
              const SizedBox(height: 18),
              TextField(
                decoration: const InputDecoration(
                  labelText: 'Serviço',
                  border: OutlineInputBorder(),
                ),
                onChanged: (String value) {
                  print('Service is: $value');
                },
              ),
              const SizedBox(height: 18),
              Row(
                children: <Widget>[
                  Expanded(
                    child: TextField(
                      decoration: const InputDecoration(
                        labelText: 'Preço Mínimo',
                        border: OutlineInputBorder(),
                      ),
                      keyboardType: TextInputType.number,
                      onChanged: (String value) {
                        print('Min Price is: $value');
                      },
                    ),
                  ),
                  const SizedBox(width: 10),
                  Expanded(
                    child: TextField(
                      decoration: const InputDecoration(
                        labelText: 'Preço Máximo',
                        border: OutlineInputBorder(),
                      ),
                      keyboardType: TextInputType.number,
                      onChanged: (String value) {
                        print('Max Price is: $value');
                      },
                    ),
                  ),
                ],
              ),
              const SizedBox(height: 18),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: <Widget>[
                  const Text(
                    'Horários:',
                    style: TextStyle(fontSize: 16),
                  ),
                  Expanded(
                    child: TimePickerRectangle(
                      initialTime: TimeOfDay.now(),
                      onTimeSelected: (TimeOfDay time) {
                        print('Min Hour is: $time');
                      },
                    ),
                  ),
                  const Text('às', style: TextStyle(fontSize: 16)),
                  Expanded(
                    child: TimePickerRectangle(
                      initialTime: TimeOfDay.now(),
                      onTimeSelected: (TimeOfDay time) {
                        print('Max Hour is: $time');
                      },
                    ),
                  ),
                ],
              ),
              const SizedBox(height: 18),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: <Widget>[
                  const Text('Entre', style: TextStyle(fontSize: 16)),
                  Expanded(
                    child: DatePickerRectangle(
                      initialDate: DateTime.now(),
                      onDateSelected: (DateTime date) {
                        print('Min Date is: $date');
                      },
                    ),
                  ),
                  const Text('a', style: TextStyle(fontSize: 16)),
                  Expanded(
                    child: DatePickerRectangle(
                      initialDate: DateTime.now(),
                      onDateSelected: (DateTime date) {
                        print('Max Date is: $date');
                      },
                    ),
                  ),
                ],
              ),
            ],
          ),
        ),
      ),
    );
  }
}
