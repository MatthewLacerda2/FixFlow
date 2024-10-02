import 'package:test/test.dart';
import 'package:openapi/openapi.dart';


/// tests for AptLogApi
void main() {
  final instance = Openapi().getAptLogApi();

  group(AptLogApi, () {
    // Deletes the Appointment Log with the given Id
    //
    //Future apiV1LogsDelete({ String body }) async
    test('test apiV1LogsDelete', () async {
      // TODO
    });

    // Gets a number of filtered Logs
    //
    //Future<BuiltList<BuiltList<AptLog>>> apiV1LogsGet({ JsonObject body }) async
    test('test apiV1LogsGet', () async {
      // TODO
    });

    // Update the Appointment Log with the given Id
    //
    //Future<AptLog> apiV1LogsPatch({ UpdateAptLog updateAptLog }) async
    test('test apiV1LogsPatch', () async {
      // TODO
    });

    // Creates a Log
    //
    // Generates a Contact
    //
    //Future<AptLog> apiV1LogsPost({ CreateAptLog createAptLog }) async
    test('test apiV1LogsPost', () async {
      // TODO
    });

  });
}
