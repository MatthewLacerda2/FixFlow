import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../../components/Inputs/name_input_field.dart';
import '../../../components/Screens/Overview/Clients/client_list.dart';
import '../../../utils/flow_storage.dart';
import '../../../utils/string_utils.dart';
import 'client_screen.dart';

class ClientsScreen extends StatefulWidget {
  const ClientsScreen({super.key});

  @override
  _ClientsScreenState createState() => _ClientsScreenState();
}

class _ClientsScreenState extends State<ClientsScreen> {
  late Future<List<CustomerDTO>> _customersFuture;
  List<CustomerDTO> _allCustomers = <CustomerDTO>[];
  List<CustomerDTO> _filteredCustomers = <CustomerDTO>[];

  @override
  void initState() {
    super.initState();
    _customersFuture = _fetchCustomers();
  }

  Future<List<CustomerDTO>> _fetchCustomers() async {
    final String mytoken = await FlowStorage.getToken();
    final ApiClient apiClient = FlowStorage.getApiClient(mytoken);

    final List<CustomerDTO>? response =
        await CustomerApi(apiClient).apiV1CustomerGet(offset: 0, limit: 100);
    setState(() {
      _allCustomers = response!;
      _filteredCustomers = response;
    });
    return response!;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Clientes'),
      ),
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: <Widget>[
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
                  final List<CustomerDTO> customers = _filteredCustomers;
                  return ListView.separated(
                    itemCount: customers.length + 1,
                    separatorBuilder: (BuildContext context, int index) =>
                        const Divider(
                      color: Colors.transparent,
                      thickness: 0,
                      height: 12,
                    ),
                    itemBuilder: (BuildContext context, int index) {
                      if (index == 0) {
                        return NameInputField(
                          placeholder: 'Filtrar por nome',
                          onNameChanged: (String name) {
                            setState(() {
                              if (name.isEmpty) {
                                _filteredCustomers = _allCustomers;
                              } else {
                                _filteredCustomers = _allCustomers
                                    .where((CustomerDTO customer) => customer
                                        .fullName
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
                        email: StringUtils.normalIfBlank(customer.email),
                        onTap: () async {
                          final String mytoken = await FlowStorage.getToken();
                          final ApiClient apiClient =
                              FlowStorage.getApiClient(mytoken);
                          final CustomerRecord? record =
                              await CustomerApi(apiClient)
                                  .apiV1CustomerRecordGet(
                                      customerId: customer.id);

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
