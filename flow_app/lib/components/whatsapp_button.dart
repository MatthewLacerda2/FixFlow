import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';

import '../utils/flow_snack.dart';

class WhatsAppButton extends StatelessWidget {
  final String phoneNumber;
  final String message;

  const WhatsAppButton({
    super.key,
    required this.phoneNumber,
    required this.message,
  });

  void _openWhatsApp(BuildContext context) async {
    final Uri whatsappUri = Uri(
      scheme: 'https',
      host: 'api.whatsapp.com',
      path: 'send',
      queryParameters: {
        'phone': phoneNumber,
        'text': message,
      },
    );

    debugPrint('Generated WhatsApp URI: ${whatsappUri.toString()}');

    if (await canLaunchUrl(whatsappUri)) {
      await launchUrl(whatsappUri, mode: LaunchMode.externalApplication);
    } else {
      FlowSnack.show(context, "Falha ao abrir o WhatsApp");
    }
  }

  @override
  Widget build(BuildContext context) {
    return IconButton(
      icon: const Icon(Icons.phone, color: Colors.green),
      iconSize: 50,
      onPressed: () => _openWhatsApp(context),
    );
  }
}
