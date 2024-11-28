import 'package:flutter/material.dart';

import '../components/Inputs/customer_dropdown.dart';
import '../components/Inputs/date_picker_rectangle.dart';
import '../components/Inputs/time_picker_rectangle.dart';
import '../utils/apt_filters.dart';
import '../utils/date_time_utils.dart';

class AptFiltersScreen extends StatefulWidget {
  const AptFiltersScreen({super.key, required this.aptFilters});

  final AptFilters aptFilters;

  @override
  State<AptFiltersScreen> createState() => _AppFiltersScreenState();
}

class _AppFiltersScreenState extends State<AptFiltersScreen> {
  late AptFilters auxAptFilters;

  @override
  void initState() {
    super.initState();
    auxAptFilters = widget.aptFilters;
  }

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
              CustomerDropdown(
                onCustomerIdChanged: (String id) {
                  auxAptFilters.clientId = id;
                },
              ),
              const SizedBox(height: 18),
              TextField(
                decoration: const InputDecoration(
                  labelText: 'Serviço',
                  border: OutlineInputBorder(),
                ),
                onChanged: (String value) {
                  auxAptFilters.service = value;
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
                        auxAptFilters.minPrice = double.parse(value);
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
                        auxAptFilters.maxPrice = double.parse(value);
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
                        auxAptFilters.minDateTime = DateTimeUtils.setTime(
                            time, auxAptFilters.minDateTime);
                      },
                    ),
                  ),
                  const Text('às', style: TextStyle(fontSize: 16)),
                  Expanded(
                    child: TimePickerRectangle(
                      initialTime: TimeOfDay.now(),
                      onTimeSelected: (TimeOfDay time) {
                        auxAptFilters.maxDateTime = DateTimeUtils.setTime(
                            time, auxAptFilters.maxDateTime);
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
                        auxAptFilters.minDateTime = DateTimeUtils.setDate(
                            date, auxAptFilters.minDateTime);
                      },
                    ),
                  ),
                  const Text('a', style: TextStyle(fontSize: 16)),
                  Expanded(
                    child: DatePickerRectangle(
                      initialDate: DateTime.now(),
                      onDateSelected: (DateTime date) {
                        auxAptFilters.maxDateTime = DateTimeUtils.setDate(
                            date, auxAptFilters.maxDateTime);
                      },
                    ),
                  ),
                ],
              ),
              const SizedBox(height: 24),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: <Widget>[
                  ElevatedButton(
                    onPressed: () async {
                      ScaffoldMessenger.of(context).showSnackBar(
                        SnackBar(
                          content: Text(auxAptFilters.toString()),
                        ),
                      );
                      Navigator.pop(context, auxAptFilters);
                    },
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.green,
                    ),
                    child: const Text(
                      'Salvar',
                      style: TextStyle(color: Colors.white, fontSize: 16),
                    ),
                  ),
                  ElevatedButton(
                    onPressed: () {
                      Navigator.pop(context);
                    },
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.blue,
                      padding: const EdgeInsets.symmetric(
                          horizontal: 16, vertical: 5),
                    ),
                    child: const Text(
                      'Cancelar',
                      style: TextStyle(color: Colors.white, fontSize: 16),
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
