import 'package:flutter/material.dart';

import '../utils/string_utils.dart';

class AptList extends StatelessWidget {
  const AptList({
    super.key,
    required this.clientName,
    required this.price,
    required this.hour,
    required this.date,
    this.service,
    this.observation,
    required this.onTap,
  });
  final String clientName;
  final double price;
  final String hour;
  final String date;
  final String? service;
  final String? observation;
  final VoidCallback onTap;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 8),
      child: Container(
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(8),
          border: Border.all(color: Colors.grey.shade300),
          boxShadow: <BoxShadow>[
            BoxShadow(
              color: Colors.grey.withOpacity(0.25),
              spreadRadius: 1.5,
              blurRadius: 4,
              offset: const Offset(0, 3),
            ),
          ],
        ),
        child: ListTile(
          title: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: <Widget>[
              Text('Cliente: $clientName'),
              Text(
                'R\$ ${price.toStringAsFixed(2)}',
                style: const TextStyle(
                  color: Colors.blue,
                  fontWeight: FontWeight.bold,
                ),
              ),
            ],
          ),
          subtitle: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: <Widget>[
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: <Widget>[
                  Text('Data: $date'),
                  Text('Hora: $hour'),
                ],
              ),
              const SizedBox(height: 2),
              Text(StringUtils.normalIfBlank(service)),
              const SizedBox(height: 2),
              Text('Observação: ${StringUtils.normalIfBlank(observation)}')
            ],
          ),
          onTap: onTap,
        ),
      ),
    );
  }
}
