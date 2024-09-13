import 'package:flutter/material.dart';
import 'package:introduction_screen/introduction_screen.dart';

import '../main/main_screen.dart';

class IntroductionScreenPage extends StatelessWidget {
  const IntroductionScreenPage({super.key});

  @override
  Widget build(BuildContext context) {
    return IntroductionScreen(
      pages: <PageViewModel>[
        PageViewModel(
          title: "Bem-vindo ao Flow!",
          body: "Automatize seus agendamentos de forma fácil e eficiente.",
          image: const Padding(
            padding: EdgeInsets.only(bottom: 24.0),
            child: Center(child: Icon(Icons.av_timer, size: 90)),
          ),
        ),
        PageViewModel(
          title: "Gerencie seus agendamentos",
          body:
              "Crie e acompanhe agendamentos de serviço, com calma e sem se perder.",
          image: const Padding(
            padding: EdgeInsets.only(bottom: 24.0),
            child: Center(child: Icon(Icons.event_note, size: 90)),
          ),
        ),
        PageViewModel(
          title: "Acompanhe seus registros",
          body:
              "Veja todos os seus atendimentos já feitos, organizados com calma e todos os detalhes",
          image: const Padding(
            padding: EdgeInsets.only(bottom: 24.0),
            child: Center(child: Icon(Icons.history, size: 90)),
          ),
        ),
        PageViewModel(
          title: "Consistência",
          body:
              "Seu serviço deve ser contratado pelo Cliente de tempos em tempos, correto? É aí que o Flow entra!",
          image: const Padding(
            padding: EdgeInsets.only(bottom: 24.0),
            child: Center(child: Icon(Icons.repeat, size: 90)),
          ),
        ),
        PageViewModel(
          title: '"Alô Fulano! Bora agendar?"',
          body:
              "O App vai lhe lembrar quando o Cliente tal, que veio dia tal, fazer tal coisa, deveria agendar outro atendimento.",
          image: const Padding(
            padding: EdgeInsets.only(bottom: 24.0),
            child: Center(child: Icon(Icons.message, size: 90)),
          ),
        ),
        PageViewModel(
          title: "Automatizado e organizado",
          body:
              "O App lembra de tudo pra você, incluindo a hora de contatar o cliente sugerindo um agendamento",
          image: const Padding(
            padding: EdgeInsets.only(bottom: 24.0),
            child: Center(child: Icon(Icons.notifications, size: 90)),
          ),
        ),
        PageViewModel(
          title: "Tudo pronto para começar",
          body: "Hora de agendar e atender!",
          image: const Padding(
            padding: EdgeInsets.only(bottom: 24.0),
            child: Center(child: Icon(Icons.assignment_turned_in, size: 90)),
          ),
        ),
      ],
      onDone: () {
        Navigator.pushReplacement(
          context,
          MaterialPageRoute<void>(
              builder: (BuildContext context) =>
                  const MainScreen()), //TODO: should be the configuration screen OR main screen
        );
      },
      showSkipButton: true,
      skip: const Text("Pular"),
      next: const Text("Próximo"),
      done: const Text("Pronto"),
      dotsDecorator: DotsDecorator(
        activeColor: Theme.of(context).primaryColor,
      ),
    );
  }
}
