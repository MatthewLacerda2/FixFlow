import 'package:test/test.dart';
import 'package:openapi/openapi.dart';


/// tests for IdlePeriodApi
void main() {
  final instance = Openapi().getIdlePeriodApi();

  group(IdlePeriodApi, () {
    // Removes Idle days
    //
    //Future apiV1IdlePeriodDelete({ String body }) async
    test('test apiV1IdlePeriodDelete', () async {
      // TODO
    });

    // Returns all Idle Periods that contain the given date
    //
    //Future<BuiltList<IdlePeriod>> apiV1IdlePeriodGet({ BusinessIdlePeriodsRequest businessIdlePeriodsRequest }) async
    test('test apiV1IdlePeriodGet', () async {
      // TODO
    });

    // Creates an Idle period
    //
    // Idle Periods are allowed to overlap
    //
    //Future<IdlePeriod> apiV1IdlePeriodPost({ IdlePeriod idlePeriod }) async
    test('test apiV1IdlePeriodPost', () async {
      // TODO
    });

  });
}
