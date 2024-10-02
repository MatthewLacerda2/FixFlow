import 'package:test/test.dart';
import 'package:openapi/openapi.dart';

// tests for ClientCreate
void main() {
  final instance = ClientCreateBuilder();
  // TODO add properties to the builder and call build()

  group(ClientCreate, () {
    // The business from which the Client is a customer
    // String businessId
    test('to test the property `businessId`', () async {
      // TODO
    });

    // String fullName
    test('to test the property `fullName`', () async {
      // TODO
    });

    // CPF. Must be on format XXX.XXX.XXX-XX
    // String cpf
    test('to test the property `cpf`', () async {
      // TODO
    });

    // Special information about the Client, if applicable
    // String additionalNote
    test('to test the property `additionalNote`', () async {
      // TODO
    });

    // Phone Number. Must contain only numbers
    // String phoneNumber
    test('to test the property `phoneNumber`', () async {
      // TODO
    });

    // String email
    test('to test the property `email`', () async {
      // TODO
    });

  });
}
