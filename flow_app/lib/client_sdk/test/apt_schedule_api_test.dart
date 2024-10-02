import 'package:test/test.dart';
import 'package:openapi/openapi.dart';


/// tests for AptScheduleApi
void main() {
  final instance = Openapi().getAptScheduleApi();

  group(AptScheduleApi, () {
    // Deletes the Appointment Schedule with the given Id
    //
    //Future apiV1SchedulesDelete({ String body }) async
    test('test apiV1SchedulesDelete', () async {
      // TODO
    });

    // Gets a number of filtered Schedules
    //
    //Future<BuiltList<BuiltList<AptSchedule>>> apiV1SchedulesGet({ JsonObject body }) async
    test('test apiV1SchedulesGet', () async {
      // TODO
    });

    // Update the Appointment Schedule with the given Id
    //
    //Future<AptSchedule> apiV1SchedulesPatch({ AptSchedule aptSchedule }) async
    test('test apiV1SchedulesPatch', () async {
      // TODO
    });

    // Creates an Appointment Schedule
    //
    //Future<AptSchedule> apiV1SchedulesPost({ CreateAptSchedule createAptSchedule }) async
    test('test apiV1SchedulesPost', () async {
      // TODO
    });

  });
}
