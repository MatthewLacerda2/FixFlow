import 'package:flutter/material.dart';

import '../intro/introduction_screen.dart';

class OtpScreen extends StatefulWidget {
  const OtpScreen({super.key});

  @override
  OtpScreenState createState() => OtpScreenState();
}

class OtpScreenState extends State<OtpScreen> {
  final List<TextEditingController> controllers =
      List.generate(6, (int index) => TextEditingController());
  String errorMessage = '';

  void _checkOtp() {
    final String enteredCode =
        controllers.map((TextEditingController c) => c.text).join();
    if (enteredCode == '123456') {
      Navigator.push(
        context,
        MaterialPageRoute(
            builder: (BuildContext context) => const IntroductionScreenPage()),
      );
    } else {
      setState(() {
        errorMessage = 'Código inválido!';
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Verificação OTP'),
      ),
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 30.0, vertical: 140.0),
        child: Column(
          children: <Widget>[
            const Align(
              alignment: Alignment.centerLeft,
              child: Padding(
                padding: EdgeInsets.only(bottom: 75.0),
                child: Text(
                  'Enviamos um SMS com um código de seis dígitos para você. Insira o código aqui para confirmar o seu telefone:',
                  textAlign: TextAlign.left,
                  style: TextStyle(fontSize: 16),
                ),
              ),
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: List.generate(6, (int index) {
                return Row(
                  children: <Widget>[
                    SizedBox(
                      width: 43,
                      child: TextField(
                        controller: controllers[index],
                        maxLength: 1,
                        decoration: const InputDecoration(
                          counterText: '',
                          border: OutlineInputBorder(),
                        ),
                        textAlign: TextAlign.center,
                        keyboardType: TextInputType.number,
                        onChanged: (String value) {
                          if (value.isNotEmpty && index < 5) {
                            FocusScope.of(context).nextFocus();
                          } else if (value.isEmpty && index > 0) {
                            FocusScope.of(context).previousFocus();
                          }

                          if (controllers.every(
                              (TextEditingController c) => c.text.isNotEmpty)) {
                            _checkOtp();
                          }
                        },
                      ),
                    ),
                    if (index < 5) const SizedBox(width: 10),
                  ],
                );
              }),
            ),

            const SizedBox(height: 20),
            if (errorMessage.isNotEmpty)
              Text(
                errorMessage,
                style: const TextStyle(color: Colors.red),
              ),
            // TODO: Use actual OTP validation
          ],
        ),
      ),
    );
  }
}
