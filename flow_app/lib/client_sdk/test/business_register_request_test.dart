import 'package:test/test.dart';
import 'package:openapi/openapi.dart';

// tests for BusinessRegisterRequest
void main() {
  final instance = BusinessRegisterRequestBuilder();
  // TODO add properties to the builder and call build()

  group(BusinessRegisterRequest, () {
    // The Name of the Business or Business owner
    // String name
    test('to test the property `name`', () async {
      // TODO
    });

    // String email
    test('to test the property `email`', () async {
      // TODO
    });

    // CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
    // String cnpj
    test('to test the property `cnpj`', () async {
      // TODO
    });

    // Phone Number. Must contain only numbers and/or a '+'
    // String phoneNumber
    test('to test the property `phoneNumber`', () async {
      // TODO
    });

    // String otpCode
    test('to test the property `otpCode`', () async {
      // TODO
    });

    // Must be identical to 'password'
    // String confirmPassword
    test('to test the property `confirmPassword`', () async {
      // TODO
    });

  });
}
