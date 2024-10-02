import 'package:test/test.dart';
import 'package:openapi/openapi.dart';


/// tests for BusinessApi
void main() {
  final instance = Openapi().getBusinessApi();

  group(BusinessApi, () {
    // Deactivates the Business Account with the given Id.  That freezes the subscription, and stops notifications
    //
    //Future apiV1BusinessDeactivatePatch({ String body }) async
    test('test apiV1BusinessDeactivatePatch', () async {
      // TODO
    });

    // Deletes the Business with the given Id and all it's data owned by it
    //
    //Future apiV1BusinessDelete({ String body }) async
    test('test apiV1BusinessDelete', () async {
      // TODO
    });

    // Gets the Business with the given Id.  Used when the User logs-in or opens the app
    //
    //Future<Business> apiV1BusinessGet({ String body }) async
    test('test apiV1BusinessGet', () async {
      // TODO
    });

    // Updates the Business with the given Id
    //
    //Future<Business> apiV1BusinessPatch({ Business business }) async
    test('test apiV1BusinessPatch', () async {
      // TODO
    });

    // Creates a Business User
    //
    //Future<Business> apiV1BusinessPost({ BusinessRegisterRequest businessRegisterRequest }) async
    test('test apiV1BusinessPost', () async {
      // TODO
    });

  });
}
