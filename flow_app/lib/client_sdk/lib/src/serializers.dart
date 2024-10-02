//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_import

import 'package:one_of_serializer/any_of_serializer.dart';
import 'package:one_of_serializer/one_of_serializer.dart';
import 'package:built_collection/built_collection.dart';
import 'package:built_value/json_object.dart';
import 'package:built_value/serializer.dart';
import 'package:built_value/standard_json_plugin.dart';
import 'package:built_value/iso_8601_date_time_serializer.dart';
import 'package:openapi/src/date_serializer.dart';
import 'package:openapi/src/model/date.dart';

import 'package:openapi/src/model/apt_log.dart';
import 'package:openapi/src/model/apt_schedule.dart';
import 'package:openapi/src/model/business.dart';
import 'package:openapi/src/model/business_calendar_day.dart';
import 'package:openapi/src/model/business_day.dart';
import 'package:openapi/src/model/business_idle_periods_request.dart';
import 'package:openapi/src/model/business_register_request.dart';
import 'package:openapi/src/model/client.dart';
import 'package:openapi/src/model/client_create.dart';
import 'package:openapi/src/model/client_dto.dart';
import 'package:openapi/src/model/client_record.dart';
import 'package:openapi/src/model/create_apt_log.dart';
import 'package:openapi/src/model/create_apt_schedule.dart';
import 'package:openapi/src/model/flow_login_request.dart';
import 'package:openapi/src/model/idle_period.dart';
import 'package:openapi/src/model/problem_details.dart';
import 'package:openapi/src/model/update_apt_log.dart';

part 'serializers.g.dart';

@SerializersFor([
  AptLog,
  AptSchedule,
  Business,
  BusinessCalendarDay,
  BusinessDay,
  BusinessIdlePeriodsRequest,
  BusinessRegisterRequest,
  Client,
  ClientCreate,
  ClientDTO,
  ClientRecord,
  CreateAptLog,
  CreateAptSchedule,
  FlowLoginRequest,
  IdlePeriod,
  ProblemDetails,
  UpdateAptLog,
])
Serializers serializers = (_$serializers.toBuilder()
      ..addBuilderFactory(
        const FullType(BuiltList, [FullType(IdlePeriod)]),
        () => ListBuilder<IdlePeriod>(),
      )
      ..addBuilderFactory(
        const FullType(BuiltList, [FullType(BusinessCalendarDay)]),
        () => ListBuilder<BusinessCalendarDay>(),
      )
      ..addBuilderFactory(
        const FullType(BuiltList, [FullType(BuiltList)]),
        () => ListBuilder<BuiltList>(),
      )
      ..add(const OneOfSerializer())
      ..add(const AnyOfSerializer())
      ..add(const DateSerializer())
      ..add(Iso8601DateTimeSerializer()))
    .build();

Serializers standardSerializers =
    (serializers.toBuilder()..addPlugin(StandardJsonPlugin())).build();
