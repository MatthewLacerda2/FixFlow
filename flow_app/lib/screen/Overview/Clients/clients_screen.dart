import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../../components/Inputs/name_input_field.dart';
import '../../../components/Screens/Overview/Clients/client_list.dart';
import '../../../utils/flow_storage.dart';
import 'client_screen.dart';

class ClientsScreen extends StatefulWidget {
  const ClientsScreen({super.key});

  @override
  _ClientsScreenState createState() => _ClientsScreenState();
}

class _ClientsScreenState extends State<ClientsScreen> {
  late Future<List<CustomerDTO>> _customersFuture;
  List<CustomerDTO> _allCustomers = [];
  List<CustomerDTO> _filteredCustomers = [];

  @override
  void initState() {
    super.initState();
    _customersFuture = _fetchCustomers();
  }

  Future<List<CustomerDTO>> _fetchCustomers() async {
    try {
      final BusinessDTO? bd = await FlowStorage.getBusinessDTO();
      final String businessId = bd!.id!;

      final List<CustomerDTO>? response = await CustomerApi().apiV1CustomerGet(
        businessId: businessId,
        offset: 0,
        limit: 100,
      );
      // Save the fetched customers and initialize filtered list
      setState(() {
        _allCustomers = response!;
        _filteredCustomers = response;
      });
      return response!;
    } catch (e) {
      print("Error fetching customers: $e");
      return <CustomerDTO>[];
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          Container(
            color: const Color.fromARGB(255, 43, 255, 36),
            padding: const EdgeInsets.all(8),
            height: 60,
            child: const Row(
              children: <Widget>[
                Icon(Icons.person, size: 28),
                SizedBox(width: 8),
                Text(
                  'Clientes',
                  style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                ),
              ],
            ),
          ),
          Container(
            color: Colors.black,
            height: 1,
          ),
          Expanded(
            child: FutureBuilder<List<CustomerDTO>>(
              future: _customersFuture,
              builder: (BuildContext context,
                  AsyncSnapshot<List<CustomerDTO>> snapshot) {
                if (snapshot.connectionState == ConnectionState.waiting) {
                  return const Center(child: CircularProgressIndicator());
                } else if (snapshot.hasError) {
                  return const Center(child: Text('Error loading customers'));
                } else if (snapshot.hasData && snapshot.data!.isEmpty) {
                  return const Center(child: Text('No customers found'));
                } else if (snapshot.hasData) {
                  // Use filtered customers instead of full list
                  final List<CustomerDTO> customers = _filteredCustomers;
                  return ListView.separated(
                    itemCount: customers.length + 1,
                    separatorBuilder: (BuildContext context, int index) =>
                        const Divider(
                      color: Colors.transparent,
                      thickness: 0,
                      height: 9,
                    ),
                    itemBuilder: (BuildContext context, int index) {
                      // Filter input field
                      if (index == 0) {
                        return NameInputField(
                          placeholder: 'Filtrar por nome',
                          onNameChanged: (String name) {
                            setState(() {
                              if (name.isEmpty) {
                                // If name is empty, show all customers
                                _filteredCustomers = _allCustomers;
                              } else {
                                // Filter customers by full name
                                _filteredCustomers = _allCustomers
                                    .where((customer) => customer.fullName
                                        .toLowerCase()
                                        .contains(name.toLowerCase()))
                                    .toList();
                              }
                            });
                          },
                        );
                      }

                      final CustomerDTO customer = customers[index - 1];
                      return ClientList(
                        name: customer.fullName,
                        lastAppointment: DateTime.now(),
                        phone: customer.phoneNumber,
                        email: customer.email ?? '-',
                        onTap: () async {
                          final CustomerRecord? record = await CustomerApi()
                              .apiV1CustomerRecordGet(customerId: customer.id);

                          Navigator.push(
                            context,
                            MaterialPageRoute<void>(
                              builder: (BuildContext context) =>
                                  ClientScreen(record: record!),
                            ),
                          );
                        },
                      );
                    },
                  );
                } else {
                  return const Center(child: Text('Unknown error occurred'));
                }
              },
            ),
          ),
        ],
      ),
    );
  }
}
