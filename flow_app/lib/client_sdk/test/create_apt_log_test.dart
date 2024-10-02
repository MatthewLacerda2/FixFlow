import 'package:test/test.dart';
import 'package:openapi/openapi.dart';

// tests for CreateAptLog
void main() {
  final instance = CreateAptLogBuilder();
  // TODO add properties to the builder and call build()

  group(CreateAptLog, () {
    // The Id of the Client who took the Appointment
    // String clientId
    test('to test the property `clientId`', () async {
      // TODO
    });

    // The Id of the Business who owns this Contact
    // String businessId
    test('to test the property `businessId`', () async {
      // TODO
    });

    // The Id of the Schedule that precedes this Log, if any
    // String scheduleId
    test('to test the property `scheduleId`', () async {
      // TODO
    });

    // The DateTime when the Log was registered
    // DateTime dateTime
    test('to test the property `dateTime`', () async {
      // TODO
    });

    // double price
    test('to test the property `price`', () async {
      // TODO
    });

    // String service
    test('to test the property `service`', () async {
      // TODO
    });

    // String description
    test('to test the property `description`', () async {
      // TODO
    });

    // The Date when we expect the Client to schedule another appointment.  We are leaving as DateTime for simplicity but we only need the Date from this class
    // DateTime whenShouldClientComeBack
    test('to test the property `whenShouldClientComeBack`', () async {
      // TODO
    });

  });
}
