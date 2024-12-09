import 'package:client_sdk/api.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart';

import '../utils/date_time_utils.dart';
import '../utils/flow_snack.dart';
import '../utils/flow_storage.dart';
import 'Buttons/colored_border_text_button.dart';

class IdlePeriodModal extends StatelessWidget {
  const IdlePeriodModal({
    super.key,
    required this.idlePeriod,
  });
  final IdlePeriod idlePeriod;

  @override
  Widget build(BuildContext context) {
    return Dialog(
      backgroundColor: Colors.white,
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(10),
      ),
      child: Container(
        padding: const EdgeInsets.all(16),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: <Widget>[
            Text(
              idlePeriod.name,
              style: const TextStyle(
                fontSize: 20,
                fontWeight: FontWeight.bold,
              ),
            ),
            const SizedBox(height: 20),
            Text(
              'Início: ${DateTimeUtils.niceFormattedDateTime(idlePeriod.start, context)}',
              style: TextStyle(
                fontSize: 20,
                color: Colors.grey[700],
              ),
            ),
            const SizedBox(height: 16),
            Text(
              'Fim: ${DateTimeUtils.niceFormattedDateTime(idlePeriod.finish, context)}',
              style: TextStyle(
                fontSize: 20,
                color: Colors.grey[700],
              ),
            ),
            const SizedBox(height: 30),
            ColoredBorderTextButton(
              text: "Deletar?",
              onPressed: () async {
                final String mytoken = await FlowStorage.getToken();
                final ApiClient apiClient = FlowStorage.getApiClient(mytoken);

                final Response resp = await IdlePeriodApi(apiClient)
                    .apiV1IdlePeriodDeleteWithHttpInfo(body: idlePeriod.id);

                if (resp.statusCode == 204) {
                  FlowSnack.show(context, "Período deletado!");
                } else {
                  FlowSnack.show(context, resp.body);
                }
              },
              textColor: Colors.black,
              textSize: 16,
              width: 16,
            ),
            const SizedBox(height: 2),
          ],
        ),
      ),
    );
  }
}
