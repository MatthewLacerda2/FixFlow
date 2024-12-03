import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../utils/flow_storage.dart';

class CustomerDropdown extends StatefulWidget {
  const CustomerDropdown({super.key, required this.onCustomerIdChanged});

  final ValueChanged<String> onCustomerIdChanged;

  @override
  CustomerDropdownState createState() => CustomerDropdownState();
}

class CustomerDropdownState extends State<CustomerDropdown> {
  final TextEditingController _searchController = TextEditingController();
  List<CustomerDTO> _customerNames = <CustomerDTO>[];
  String customerId = "";
  bool _isDropdownVisible = false;

  @override
  void initState() {
    super.initState();
    _searchController.addListener(_onSearchChanged);
  }

  void _updateCustomerId(String id) {
    setState(() {
      customerId = id;
    });
    _isDropdownVisible = false;
    widget.onCustomerIdChanged(id);
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
    final String mytoken = await FlowStorage.getToken();
    final ApiClient apiClient = FlowStorage.getApiClient(mytoken);
    final List<CustomerDTO>? response = await CustomerApi(apiClient)
        .apiV1CustomerGet(offset: 0, limit: 7, fullname: query);

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
            hintText: 'Digite o nome do Cliente',
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
                      _updateCustomerId(customer.id);
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
