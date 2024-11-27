import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../utils/apt_filters.dart';
import '../../utils/flow_storage.dart';
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
  bool _isLoading = true;

  late final AptFilters scheduleFilters;
  late final AptFilters logFilters;
  late List<Widget> _screens = <Widget>[];

  @override
  void initState() {
    super.initState();
    _selectedIndex = widget.initialIndex;
    _initialize();
  }

  Future<void> _initialize() async {
    await _loadData();
    setState(() {
      _isLoading = false;
    });
  }

  Future<void> _loadData() async {
    final BusinessDTO? bd = await FlowStorage.getBusinessDTO();
    final String businessId = bd!.id!;

    scheduleFilters = AptFilters(
        businessId: businessId,
        maxPrice: 9999,
        minDateTime: DateTime.now(),
        maxDateTime: DateTime.now().add(const Duration(days: 30)));

    logFilters = AptFilters(
        businessId: businessId,
        maxPrice: 9999,
        minDateTime: DateTime(2023),
        maxDateTime: DateTime.now());

    _screens = <Widget>[
      const OverviewScreen(),
      SchedulesScreen(aptFilters: scheduleFilters),
      ContactsScreen(aptFilters: scheduleFilters),
      LogsScreen(aptFilters: logFilters),
      const AccountScreen(),
    ];
  }

  void _onItemTapped(int index) {
    setState(() {
      _selectedIndex = index;
    });
  }

  @override
  Widget build(BuildContext context) {
    if (_isLoading) {
      return const Center(child: CircularProgressIndicator());
    }

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
