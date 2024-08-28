import 'package:flutter/material.dart';

import '../intro/introduction_screen.dart';

class OtpScreen extends StatefulWidget {
  @override
  _OtpScreenState createState() => _OtpScreenState();
}

class _OtpScreenState extends State<OtpScreen> {
  final List<TextEditingController> controllers =
      List.generate(6, (index) => TextEditingController());
  String errorMessage = '';

  void _checkOtp() {
    String enteredCode = controllers.map((c) => c.text).join();
    if (enteredCode == '123456') {
      Navigator.push(
        context,
        MaterialPageRoute(builder: (context) => IntroductionScreenPage()),
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
        title: Text('Verificação OTP'),
      ),
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 30.0, vertical: 140.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.start,
          children: <Widget>[
            Align(
              alignment: Alignment.centerLeft,
              child: Padding(
                padding: const EdgeInsets.only(bottom: 75.0),
                child: Text(
                  'Enviamos um SMS com um código de seis dígitos para você. Insira o código aqui para confirmar o seu telefone:',
                  textAlign: TextAlign.left,
                  style: TextStyle(fontSize: 16),
                ),
              ),
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: List.generate(6, (index) {
                return Row(
                  children: [
                    SizedBox(
                      width: 43,
                      child: TextField(
                        controller: controllers[index],
                        maxLength: 1,
                        decoration: InputDecoration(
                          counterText: '',
                          border: OutlineInputBorder(),
                        ),
                        textAlign: TextAlign.center,
                        keyboardType: TextInputType.number,
                        onChanged: (value) {
                          if (value.isNotEmpty && index < 5) {
                            FocusScope.of(context).nextFocus();
                          } else if (value.isEmpty && index > 0) {
                            FocusScope.of(context).previousFocus();
                          }

                          if (controllers.every((c) => c.text.isNotEmpty)) {
                            _checkOtp();
                          }
                        },
                      ),
                    ),
                    if (index < 5) SizedBox(width: 10),
                  ],
                );
              }),
            ),

            SizedBox(height: 20),
            if (errorMessage.isNotEmpty)
              Text(
                errorMessage,
                style: TextStyle(color: Colors.red),
              ),
            // TODO: Use actual OTP validation
          ],
        ),
      ),
    );
  }
}
