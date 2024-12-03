import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';

import '../../components/Buttons/colored_border_text_button.dart';
import '../../components/Buttons/order_button.dart';
import '../../components/apt_list.dart';
import '../../utils/apt_filters.dart';
import '../../utils/date_time_utils.dart';
import '../../utils/flow_storage.dart';
import '../../utils/string_utils.dart';
import '../apt_filters_screen.dart';
import '../apts/contact_screen.dart';

class ContactsScreen extends StatefulWidget {
  const ContactsScreen({super.key, required this.aptFilters});

  final AptFilters aptFilters;

  @override
  _ContactsScreenState createState() => _ContactsScreenState();
}

class _ContactsScreenState extends State<ContactsScreen> {
  late Future<List<AptContact>> _contactsFuture;

  @override
  void initState() {
    super.initState();
    _contactsFuture = _fetchContacts();
  }

  Future<List<AptContact>> _fetchContacts() async {
    final String mytoken = await FlowStorage.getToken();
    final ApiClient apiClient = FlowStorage.getApiClient(mytoken);

    final AptFilters f = widget.aptFilters;

    final List<AptContact>? response = await AptContactApi(apiClient)
        .apiV1ContactsGet(
            minDateTime: f.minDateTime,
            maxDateTime: f.maxDateTime,
            offset: f.offset,
            limit: f.limit);
    return response ?? <AptContact>[]; // Handle null safety
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(0),
        child: Stack(
          children: <Widget>[
            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: <Widget>[
                Container(
                  color: Colors.blueGrey,
                  padding: const EdgeInsets.all(8),
                  height: 60,
                  child: const Row(
                    children: <Widget>[
                      Icon(Icons.timer_outlined, size: 28),
                      SizedBox(width: 8),
                      Text(
                        'Lembretes',
                        style: TextStyle(
                            fontSize: 24, fontWeight: FontWeight.bold),
                      ),
                    ],
                  ),
                ),
                Container(color: Colors.black, height: 1),
                const SizedBox(height: 8),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 10),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                      const OrderButton(
                        icon: Icons.perm_contact_cal,
                        isUp: true,
                        iconSize: 40,
                        iconColor: Colors.blueGrey,
                      ),
                      const OrderButton(
                        icon: Icons.attach_money,
                        iconSize: 40,
                        iconColor: Colors.blueGrey,
                      ),
                      const OrderButton(
                        icon: Icons.calendar_today,
                        iconSize: 40,
                        iconColor: Colors.blueGrey,
                      ),
                      ColoredBorderTextButton(
                        text: "Filtros",
                        onPressed: () {
                          Navigator.push<AptFilters>(
                            context,
                            MaterialPageRoute(
                              builder: (context) => AptFiltersScreen(
                                  aptFilters: widget.aptFilters),
                            ),
                          );
                        },
                        backgroundColor: Colors.blueGrey,
                        borderColor: Colors.grey,
                        textColor: Colors.white,
                      )
                    ],
                  ),
                ),
                const SizedBox(height: 10),
                Expanded(
                  child: FutureBuilder<List<AptContact>>(
                    future: _contactsFuture,
                    builder: (BuildContext context,
                        AsyncSnapshot<List<AptContact>> snapshot) {
                      if (snapshot.connectionState == ConnectionState.waiting) {
                        return const Center(child: CircularProgressIndicator());
                      } else if (snapshot.hasError) {
                        return Center(
                          child: Text('Error: ${snapshot.error}'),
                        );
                      } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
                        return const Center(
                          child: Text('Não há lembretes.'),
                        );
                      }

                      final List<AptContact> contacts = snapshot.data!;
                      return ListView.separated(
                        itemCount: contacts.length,
                        separatorBuilder: (BuildContext context, int index) =>
                            const Divider(
                                color: Colors.transparent,
                                thickness: 0,
                                height: 10),
                        itemBuilder: (BuildContext context, int index) {
                          final AptContact contact = contacts[index];
                          return AptList(
                            clientName: contact.customer!.fullName,
                            price: contact.aptLog!.price ?? 0,
                            hour: TimeOfDay.fromDateTime(contact.dateTime!)
                                .format(context),
                            date:
                                DateTimeUtils.dateOnlyString(contact.dateTime!),
                            service: StringUtils.normalIfBlank(
                                contact.aptLog!.service),
                            observation: StringUtils.normalIfBlank(
                                contact.aptLog!.description),
                            onTap: () {
                              Navigator.push(
                                context,
                                MaterialPageRoute<void>(
                                  builder: (BuildContext context) =>
                                      ContactScreen(contact: contact),
                                ),
                              );
                            },
                          );
                        },
                      );
                    },
                  ),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
