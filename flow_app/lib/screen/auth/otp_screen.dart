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

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Verificação OTP'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: List.generate(6, (index) {
                return SizedBox(
                  width: 40,
                  child: TextField(
                    controller: controllers[index],
                    maxLength: 1,
                    decoration: InputDecoration(counterText: ''),
                    textAlign: TextAlign.center,
                    keyboardType: TextInputType.number,
                    onChanged: (value) {
                      if (value.isNotEmpty && index < 5) {
                        FocusScope.of(context).nextFocus();
                      } else if (value.isEmpty && index > 0) {
                        FocusScope.of(context).previousFocus();
                      }
                    },
                  ),
                );
              }),
            ),
            SizedBox(height: 20),
            if (errorMessage.isNotEmpty)
              Text(
                errorMessage,
                style: TextStyle(color: Colors.red),
              ),
            SizedBox(height: 20),
            ElevatedButton(
              onPressed: () {
                String enteredCode = controllers.map((c) => c.text).join();
                if (enteredCode == '123456') {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                        builder: (context) => IntroductionScreenPage()),
                  );
                } else {
                  setState(() {
                    errorMessage = 'Código inválido!';
                  });
                }
              },
              child: Text('Verificar'),
            ),
            // TODO: Use actual OTP validation
          ],
        ),
      ),
    );
  }
}
