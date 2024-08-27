import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:url_launcher/url_launcher.dart';

class AboutScreen extends StatelessWidget {
  final String linkedInUrl = 'https://www.linkedin.com/in/matheus-lacerda96/';
  final String whatsappUrl = 'https://wa.me/98999344788';

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Padding(
                padding: const EdgeInsets.only(top: 40.0),
                child: Center(
                  child: Text(
                    'Sobre',
                    style: TextStyle(
                      fontSize: 28,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
              ),
              SizedBox(height: 25),
              Row(
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  Container(
                    width: 120,
                    height: 120,
                    decoration: BoxDecoration(
                      color: Colors.grey[300],
                      borderRadius: BorderRadius.circular(8),
                      image: DecorationImage(
                        image: NetworkImage('https://i.imgur.com/cXWK4wf.png'),
                        fit: BoxFit.cover,
                      ),
                    ),
                  ),
                  SizedBox(width: 10),
                  Expanded(
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text(
                          'Matheus Lacerda',
                          style: TextStyle(
                            fontSize: 24,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                        SizedBox(height: 10),
                        Text(
                          'Desenvolvedor de Software',
                          style: TextStyle(
                            fontSize: 18,
                            color: Colors.grey[600],
                          ),
                        ),
                      ],
                    ),
                  ),
                ],
              ),
              SizedBox(height: 25),
              Text(
                'Sou um desenvolvedor Full Stack, criando websites, aplicativos, servidores, e até videogames. ' +
                    "Sempre busco novos desafios para entender o modelo de negócio e desenvolver com qualidade",
                style: TextStyle(
                  fontSize: 16,
                  color: Colors.grey[700],
                ),
              ),
              SizedBox(height: 14),
              Text(
                'Se você tem uma idéia e quer fazer acontecer, venha falar comigo',
                style: TextStyle(
                  fontSize: 16,
                  color: Colors.grey[700],
                ),
              ),
              SizedBox(height: 40),
              Row(
                children: [
                  FaIcon(FontAwesomeIcons.linkedin,
                      color: Colors.blue, size: 28),
                  SizedBox(width: 10),
                  GestureDetector(
                    onTap: () async {
                      if (await canLaunch(linkedInUrl)) {
                        await launch(linkedInUrl);
                      } else {
                        throw 'Could not launch $linkedInUrl';
                      }
                    },
                    child: Text(
                      'Conectar no LinkedIn',
                      style: TextStyle(
                        fontSize: 18,
                      ),
                    ),
                  ),
                ],
              ),
              SizedBox(height: 20),
              Row(
                children: [
                  FaIcon(FontAwesomeIcons.whatsapp,
                      color: Colors.green, size: 28),
                  SizedBox(width: 10),
                  GestureDetector(
                    onTap: () async {
                      if (await canLaunch(whatsappUrl)) {
                        await launch(whatsappUrl);
                      } else {
                        throw 'Could not launch $whatsappUrl';
                      }
                    },
                    child: Text(
                      'Chamar no WhatsApp',
                      style: TextStyle(
                        fontSize: 18,
                      ),
                    ),
                  ),
                ],
              ),
              SizedBox(height: 90),
              Align(
                alignment: Alignment.centerLeft,
                child: ElevatedButton.icon(
                  onPressed: () {
                    Navigator.pop(context);
                  },
                  icon: Icon(Icons.arrow_back, color: Colors.black),
                  label: Text(
                    'Voltar',
                    style: TextStyle(color: Colors.black, fontSize: 18),
                  ),
                  style: ElevatedButton.styleFrom(
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(40),
                      side: BorderSide(
                        color: Colors.grey[800]!,
                        width: 2.0,
                      ),
                    ),
                    padding: EdgeInsets.symmetric(horizontal: 28, vertical: 9),
                    elevation: 0,
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
