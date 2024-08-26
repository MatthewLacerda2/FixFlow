import 'package:flutter/material.dart';
import 'package:introduction_screen/introduction_screen.dart';

import '../main/main_screen.dart';

class IntroductionScreenPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return IntroductionScreen(
      pages: [
        PageViewModel(
          title: "Bem-vindo ao Flow!",
          body: "Automatize seus agendamentos de forma fácil e eficiente.",
          image: Center(child: Icon(Icons.schedule, size: 100.0)),
        ),
        PageViewModel(
          title: "Gerencie seus logs",
          body: "Acompanhe seus atendimentos e mantenha tudo organizado.",
          image: Center(child: Icon(Icons.list, size: 100.0)),
        ),
        PageViewModel(
          title: "Notificações Inteligentes",
          body: "Receba lembretes para nunca perder um agendamento.",
          image: Center(child: Icon(Icons.notifications, size: 100.0)),
        ),
        PageViewModel(
          title: "Tudo pronto para começar",
          body: "Hora de agendar e atender!",
          image: Center(child: Icon(Icons.notifications, size: 100.0)),
        ),
      ],
      onDone: () {
        Navigator.pushReplacement(
          context,
          MaterialPageRoute(builder: (context) => MainScreen()),
        );
      },
      showSkipButton: true,
      skip: Text("Pular"),
      next: Text("Próximo"),
      done: Text("Pronto"),
      dotsDecorator: DotsDecorator(
        activeColor: Theme.of(context).primaryColor,
      ),
    );
  }
}
