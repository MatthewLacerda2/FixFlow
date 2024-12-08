import 'dart:convert';

import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart';

import '../utils/flow_snack.dart';
import '../utils/flow_storage.dart';
import 'Inputs/check_input_field.dart';
import 'Inputs/enum_field.dart';

class BusinessConfig extends StatelessWidget {
  const BusinessConfig({
    super.key,
    required this.businessDTO,
  });

  final BusinessDTO businessDTO;

  Future<void> _patchBusinessDTO(BuildContext context) async {
    final String token = await FlowStorage.getToken();
    final ApiClient client = FlowStorage.getApiClient(token);
    final Response resp = await BusinessApi(client)
        .apiV1BusinessPatchWithHttpInfo(businessDTO: businessDTO);

    if (resp.statusCode == 200) {
      final BusinessDTO bDTO = BusinessDTO.fromJson(jsonDecode(resp.body))!;
      FlowStorage.saveBusinessDTO(bDTO);
    } else {
      FlowSnack.show(context, resp.body);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        const Text(
          'Opções de Serviços',
          style: TextStyle(fontWeight: FontWeight.bold, fontSize: 22),
        ),
        const SizedBox(height: 18),
        EnumField(
          description: "Serviço...",
          options: businessDTO.services ?? <String>[],
          characterLimit: 32,
          onItemsChanged: (List<String> updatedServices) async {
            businessDTO.services = updatedServices;
            _patchBusinessDTO(context);
          },
        ),
        const SizedBox(height: 12),
        CheckInputField(
          label: 'Permitir apenas serviços listados?',
          initialValue: businessDTO.allowListedServicesOnly ?? false,
          onChanged: (bool isChecked) async {
            businessDTO.allowListedServicesOnly = isChecked;
            _patchBusinessDTO(context);
          },
        ),
        const SizedBox(height: 18),
        CheckInputField(
          label: 'Atende aos feriados?',
          initialValue: businessDTO.openOnHolidays ?? false,
          onChanged: (bool isChecked) async {
            businessDTO.openOnHolidays = isChecked;
            _patchBusinessDTO(context);
          },
        ),
        const SizedBox(height: 34),
        Container(
          height: 10,
          color: Colors.grey.shade800,
        ),
      ],
    );
  }
}
