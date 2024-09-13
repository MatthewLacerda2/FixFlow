import 'package:flutter/material.dart';

class OtpScreen extends StatefulWidget {
  const OtpScreen(
      {super.key,
      required this.message,
      required this.phoneNumber,
      required this.nextScreen});

  final String message;
  final String phoneNumber;
  final Widget nextScreen;

  @override
  OtpScreenState createState() => OtpScreenState();
}

class OtpScreenState extends State<OtpScreen> {
  final List<TextEditingController> controllers =
      List<TextEditingController>.generate(
          6, (int index) => TextEditingController());
  String errorMessage = '';

  void _checkOtp() {
    final String enteredCode =
        controllers.map((TextEditingController c) => c.text).join();
    if (enteredCode == '123456') {
      Navigator.pushReplacement(
        context,
        MaterialPageRoute<void>(
            builder: (BuildContext context) => widget.nextScreen),
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
        title: const Text(
          'Verificação OTP',
          style: TextStyle(fontWeight: FontWeight.bold),
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 30.0, vertical: 140.0),
        child: Column(
          children: <Widget>[
            Align(
              alignment: Alignment.centerLeft,
              child: Padding(
                padding: const EdgeInsets.only(bottom: 75.0),
                child: Text(
                  widget.message,
                  textAlign: TextAlign.left,
                  style: const TextStyle(fontSize: 16),
                ),
              ),
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: List<Widget>.generate(6, (int index) {
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
