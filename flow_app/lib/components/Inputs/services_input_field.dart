import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../utils/flow_storage.dart';

class ServicesInputField extends StatefulWidget {
  const ServicesInputField({
    super.key,
    this.initialService,
    required this.onServiceSelected,
  });

  final String? initialService;
  final ValueChanged<String?> onServiceSelected;

  @override
  ServicesInputFieldState createState() => ServicesInputFieldState();
}

class ServicesInputFieldState extends State<ServicesInputField> {
  final TextEditingController _textController = TextEditingController();
  List<String> _availableServices = <String>[];
  List<String> _filteredServices = <String>[];
  bool _allowNewServices = false;
  String? _selectedService;
  bool _isDropdownVisible = false;

  Future<void> loadBusinessOptions() async {
    final BusinessDTO? businessData = await FlowStorage.getBusinessDTO();

    setState(() {
      _availableServices = businessData!.services ?? <String>[];
      _allowNewServices = !(businessData.allowListedServicesOnly ?? true);
    });
  }

  @override
  void initState() {
    super.initState();
    _textController.text = widget.initialService ?? "";
    _selectedService = widget.initialService;
    _textController.addListener(_onSearchChanged);
    loadBusinessOptions();
    print(_availableServices);
  }

  @override
  void dispose() {
    _textController.removeListener(_onSearchChanged);
    _textController.dispose();
    super.dispose();
  }

  void _onSearchChanged() {
    final String query = _textController.text.toLowerCase();

    if (query.isEmpty) {
      setState(() {
        _isDropdownVisible = false;
      });
      return;
    }

    setState(() {
      _filteredServices = _availableServices
          .where((String service) => service.toLowerCase().contains(query))
          .toList();

      if (_allowNewServices &&
          !_filteredServices
              .any((String service) => service.toLowerCase() == query)) {
        _filteredServices.insert(0, 'Add Service');
      }

      _isDropdownVisible = _filteredServices.isNotEmpty;
    });
  }

  void _selectService(String service) {
    if (service == 'Add Service') {
      setState(() {
        _selectedService = _textController.text;
        _isDropdownVisible = false;
      });
    } else {
      setState(() {
        _selectedService = service;
        _textController.text = service;
        _isDropdownVisible = false;
      });
    }

    widget.onServiceSelected(_selectedService);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        TextField(
          controller: _textController,
          decoration: const InputDecoration(
            hintText: 'Digite um serviço',
            border: OutlineInputBorder(),
          ),
        ),
        if (_isDropdownVisible)
          Container(
            margin: const EdgeInsets.only(top: 8.0),
            decoration: BoxDecoration(
              border: Border.all(color: Colors.grey),
              borderRadius: BorderRadius.circular(4.0),
            ),
            child: ListView.builder(
              shrinkWrap: true,
              itemCount: _filteredServices.length,
              itemBuilder: (BuildContext context, int index) {
                final String service = _filteredServices[index];
                return ListTile(
                  title: Text(service),
                  onTap: () => _selectService(service),
                );
              },
            ),
          ),
      ],
    );
  }
}
