//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:openapi/src/model/idle_period.dart';
import 'package:built_collection/built_collection.dart';
import 'package:openapi/src/model/apt_log.dart';
import 'package:openapi/src/model/apt_schedule.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'business_calendar_day.g.dart';

/// BusinessCalendarDay
///
/// Properties:
/// * [date] 
/// * [description] 
/// * [idlePeriods] 
/// * [holiday] 
/// * [schedules] 
/// * [logs] 
@BuiltValue()
abstract class BusinessCalendarDay implements Built<BusinessCalendarDay, BusinessCalendarDayBuilder> {
  @BuiltValueField(wireName: r'date')
  DateTime? get date;

  @BuiltValueField(wireName: r'description')
  String? get description;

  @BuiltValueField(wireName: r'idlePeriods')
  BuiltList<IdlePeriod>? get idlePeriods;

  @BuiltValueField(wireName: r'holiday')
  BuiltList<String>? get holiday;

  @BuiltValueField(wireName: r'schedules')
  BuiltList<AptSchedule>? get schedules;

  @BuiltValueField(wireName: r'logs')
  BuiltList<AptLog>? get logs;

  BusinessCalendarDay._();

  factory BusinessCalendarDay([void updates(BusinessCalendarDayBuilder b)]) = _$BusinessCalendarDay;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(BusinessCalendarDayBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<BusinessCalendarDay> get serializer => _$BusinessCalendarDaySerializer();
}

class _$BusinessCalendarDaySerializer implements PrimitiveSerializer<BusinessCalendarDay> {
  @override
  final Iterable<Type> types = const [BusinessCalendarDay, _$BusinessCalendarDay];

  @override
  final String wireName = r'BusinessCalendarDay';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    BusinessCalendarDay object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    if (object.date != null) {
      yield r'date';
      yield serializers.serialize(
        object.date,
        specifiedType: const FullType(DateTime),
      );
    }
    if (object.description != null) {
      yield r'description';
      yield serializers.serialize(
        object.description,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.idlePeriods != null) {
      yield r'idlePeriods';
      yield serializers.serialize(
        object.idlePeriods,
        specifiedType: const FullType.nullable(BuiltList, [FullType(IdlePeriod)]),
      );
    }
    if (object.holiday != null) {
      yield r'holiday';
      yield serializers.serialize(
        object.holiday,
        specifiedType: const FullType.nullable(BuiltList, [FullType(String)]),
      );
    }
    if (object.schedules != null) {
      yield r'schedules';
      yield serializers.serialize(
        object.schedules,
        specifiedType: const FullType.nullable(BuiltList, [FullType(AptSchedule)]),
      );
    }
    if (object.logs != null) {
      yield r'logs';
      yield serializers.serialize(
        object.logs,
        specifiedType: const FullType.nullable(BuiltList, [FullType(AptLog)]),
      );
    }
  }

  @override
  Object serialize(
    Serializers serializers,
    BusinessCalendarDay object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required BusinessCalendarDayBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'date':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(DateTime),
          ) as DateTime;
          result.date = valueDes;
          break;
        case r'description':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.description = valueDes;
          break;
        case r'idlePeriods':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(BuiltList, [FullType(IdlePeriod)]),
          ) as BuiltList<IdlePeriod>?;
          if (valueDes == null) continue;
          result.idlePeriods.replace(valueDes);
          break;
        case r'holiday':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(BuiltList, [FullType(String)]),
          ) as BuiltList<String>?;
          if (valueDes == null) continue;
          result.holiday.replace(valueDes);
          break;
        case r'schedules':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(BuiltList, [FullType(AptSchedule)]),
          ) as BuiltList<AptSchedule>?;
          if (valueDes == null) continue;
          result.schedules.replace(valueDes);
          break;
        case r'logs':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(BuiltList, [FullType(AptLog)]),
          ) as BuiltList<AptLog>?;
          if (valueDes == null) continue;
          result.logs.replace(valueDes);
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  BusinessCalendarDay deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = BusinessCalendarDayBuilder();
    final serializedList = (serialized as Iterable<Object?>).toList();
    final unhandled = <Object?>[];
    _deserializeProperties(
      serializers,
      serialized,
      specifiedType: specifiedType,
      serializedList: serializedList,
      unhandled: unhandled,
      result: result,
    );
    return result.build();
  }
}

