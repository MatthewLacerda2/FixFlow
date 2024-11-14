import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../utils/flow_storage.dart';

class CustomerDropdown extends StatefulWidget {
  const CustomerDropdown({super.key});

  @override
  CustomerDropdownState createState() => CustomerDropdownState();
}

class CustomerDropdownState extends State<CustomerDropdown> {
  final TextEditingController _searchController = TextEditingController();
  List<CustomerDTO> _customerNames = <CustomerDTO>[];
  CustomerDTO? _selectedCustomer;
  bool _isDropdownVisible = false;

  @override
  void initState() {
    super.initState();
    _searchController.addListener(_onSearchChanged);
  }

  @override
  void dispose() {
    _searchController.removeListener(_onSearchChanged);
    _searchController.dispose();
    super.dispose();
  }

  void _onSearchChanged() {
    if (_searchController.text.isEmpty) {
      setState(() {
        _isDropdownVisible = false;
      });
    } else {
      _fetchCustomers(_searchController.text);
    }
  }

  Future<void> _fetchCustomers(String query) async {
    final BusinessDTO? dto = await FlowStorage.getBusinessDTO();
    final String id = dto!.id!;
    final List<CustomerDTO>? response = await CustomerApi()
        .apiV1CustomerGet(businessId: id, offset: 0, limit: 7, fullname: query);

    setState(() {
      _customerNames = response ?? <CustomerDTO>[];
      _isDropdownVisible = _customerNames.isNotEmpty;
    });
  }

  String customerDataText(String fullname, String phoneNumber) {
    if (fullname.length > 21) {
      fullname = '${fullname.substring(0, 21)}...';
    }
    phoneNumber = phoneNumber.substring(phoneNumber.length - 10);
    return "$fullname | $phoneNumber";
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        TextField(
          controller: _searchController,
          decoration: const InputDecoration(
            hintText: 'Digite o nome do customer',
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
            child: ListView(
              shrinkWrap: true,
              children: _customerNames.map((CustomerDTO customer) {
                return ListTile(
                  title: Text(customerDataText(
                      customer.fullName, customer.phoneNumber)),
                  onTap: () {
                    setState(() {
                      _selectedCustomer = customer;
                      _isDropdownVisible = false;
                    });
                    _searchController.text = customer.fullName;
                  },
                );
              }).toList(),
            ),
          ),
      ],
    );
  }
}
