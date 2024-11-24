import 'package:flutter/material.dart';

import 'account_screen.dart';
import 'contacts_screen.dart';
import 'logs_screen.dart';
import 'overview_screen.dart';
import 'schedules_screen.dart';

class MainScreen extends StatefulWidget {
  const MainScreen({super.key, required this.initialIndex});

  final int initialIndex;

  @override
  MainScreenState createState() => MainScreenState();
}

class MainScreenState extends State<MainScreen> {
  int _selectedIndex = 0;

  @override
  void initState() {
    super.initState();
    // Use the initialIndex from the widget
    _selectedIndex = widget.initialIndex;
  }

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
