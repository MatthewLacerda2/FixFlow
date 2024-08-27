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
          image: Center(child: Icon(Icons.schedule, size: 90)),
        ),
        PageViewModel(
          title: "Gerencie seus agendamentos",
          body:
              "Crie e acompanhe agendamentos de serviço com calma, sem se perder.",
          image: Center(child: Icon(Icons.list, size: 90)),
        ),
        PageViewModel(
          title: "Acompanhe seus registros",
          body:
              "Veja todos os seus atendimentos já feitos, organizados com calma e todos os detalhes",
          image: Center(child: Icon(Icons.notifications, size: 90)),
        ),
        PageViewModel(
          title: "Consistência",
          body:
              "Seu serviço deve ser contratado pelo Cliente de tempos em tempos, correto? É aí que o Flow entra!",
          image: Center(child: Icon(Icons.notifications, size: 90)),
        ),
        PageViewModel(
          title: '"Alô Fulano! Bora agendar?"',
          body:
              "O App vai lhe lembrar quando o Cliente tal, que veio dia tal, fazer tal coisa, deveria agendar outro atendimento.",
          image: Center(child: Icon(Icons.notifications, size: 90)),
        ),
        PageViewModel(
          title: "Automatizado e organizado",
          body:
              "O App lembra de tudo pra você, incluindo a hora de contatar o cliente sugerindo um agendamento",
          image: Center(child: Icon(Icons.notifications, size: 90)),
        ),
        PageViewModel(
          title: "Tudo pronto para começar",
          body: "Hora de agendar e atender!",
          image: Center(child: Icon(Icons.notifications, size: 90)),
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
