import 'package:test/test.dart';
import 'package:openapi/openapi.dart';


/// tests for OTPApi
void main() {
  final instance = Openapi().getOTPApi();

  group(OTPApi, () {
    // Creates an OTP for when creating a Business
    //
    //Future apiV1OTPPost({ String body }) async
    test('test apiV1OTPPost', () async {
      // TODO
    });

  });
}
