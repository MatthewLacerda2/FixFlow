import 'package:test/test.dart';
import 'package:openapi/openapi.dart';


/// tests for ClientApi
void main() {
  final instance = Openapi().getClientApi();

  group(ClientApi, () {
    // Gets a number of filtered Clients
    //
    //Future<BuiltList<BuiltList<ClientDTO>>> apiV1ClientGet({ String businessId, int offset, int limit, String fullname }) async
    test('test apiV1ClientGet', () async {
      // TODO
    });

    // Get Client Record in the Business.  Credentials, but also schedules and logs history
    //
    //Future<ClientRecord> apiV1ClientIdGet(String id, { String clientId }) async
    test('test apiV1ClientIdGet', () async {
      // TODO
    });

    // Updates the Client with the given Id
    //
    //Future<ClientDTO> apiV1ClientPatch({ ClientDTO clientDTO }) async
    test('test apiV1ClientPatch', () async {
      // TODO
    });

    // Create a Client Account
    //
    //Future<ClientDTO> apiV1ClientPost({ ClientCreate clientCreate }) async
    test('test apiV1ClientPost', () async {
      // TODO
    });

  });
}
