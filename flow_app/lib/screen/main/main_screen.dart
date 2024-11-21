import 'package:flutter/material.dart';

import 'account_screen.dart';
import 'contacts_screen.dart';
import 'logs_screen.dart';
import 'overview_screen.dart';
import 'schedules_screen.dart';

class MainScreen extends StatefulWidget {
  const MainScreen({super.key});

  @override
  MainScreenState createState() => MainScreenState();
}

class MainScreenState extends State<MainScreen> {
  int _selectedIndex = 0;

  final List<Widget> _screens = <Widget>[
    const OverviewScreen(),
    const SchedulesScreen(),
    const ContactsScreen(),
    const LogsScreen(),
    const AccountScreen(),
  ];

  void _onItemTapped(int index) {
    setState(() {
      _selectedIndex = index;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: IndexedStack(
          index: _selectedIndex,
          children: _screens,
        ),
      ),
      //TODO: make the apt buttons 'pop' when there is something new
      bottomNavigationBar: BottomNavigationBar(
        items: const <BottomNavigationBarItem>[
          BottomNavigationBarItem(icon: Icon(Icons.dashboard), label: 'Main'),
          BottomNavigationBarItem(
              icon: Icon(Icons.edit_calendar_outlined), label: 'Agenda'),
          BottomNavigationBarItem(
              icon: Icon(Icons.punch_clock), label: 'Lembretes'),
          BottomNavigationBarItem(
              icon: Icon(Icons.note_alt_rounded), label: 'Atendimento'),
          BottomNavigationBarItem(icon: Icon(Icons.menu), label: 'Config'),
        ],
        backgroundColor: Colors.white,
        selectedItemColor: Colors.black,
        unselectedItemColor: Colors.grey[600],
        showSelectedLabels: true,
        showUnselectedLabels: true,
        type: BottomNavigationBarType.fixed,
        currentIndex: _selectedIndex,
        onTap: _onItemTapped,
      ),
    );
  }
}
