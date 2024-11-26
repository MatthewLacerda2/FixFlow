import 'package:flutter/material.dart';

class ClientList extends StatelessWidget {
  const ClientList({
    super.key,
    required this.name,
    required this.phone,
    required this.email,
    required this.lastAppointment,
    required this.onTap,
  });
  final String name;
  final String phone;
  final String email;
  final DateTime lastAppointment;
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
          subtitle: Row(
            children: <Widget>[
              Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: <Widget>[
                  Text(
                    'Cliente: $name',
                    style: const TextStyle(
                        fontWeight: FontWeight.w600, fontSize: 18),
                  ),
                  Text(
                    "Último atendimento: ${lastAppointment.toString().split(' ')[0]}",
                    textAlign: TextAlign.right,
                  ),
                  Text(
                    "Telefone: $phone",
                    textAlign: TextAlign.left,
                  ),
                  Text(
                    'Email: $email',
                    textAlign: TextAlign.right,
                  ),
                ],
              ),
            ],
          ),
          onTap: onTap,
        ),
      ),
    );
  }
}
