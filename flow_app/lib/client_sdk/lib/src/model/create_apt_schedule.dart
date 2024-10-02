//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'create_apt_schedule.g.dart';

/// CreateAptSchedule
///
/// Properties:
/// * [clientId] - The Id of the Client who made the Schedule
/// * [dateTime] - The scheduled DateTime of the Appointment
/// * [service] 
/// * [observation] 
/// * [price] 
@BuiltValue()
abstract class CreateAptSchedule implements Built<CreateAptSchedule, CreateAptScheduleBuilder> {
  /// The Id of the Client who made the Schedule
  @BuiltValueField(wireName: r'clientId')
  String get clientId;

  /// The scheduled DateTime of the Appointment
  @BuiltValueField(wireName: r'dateTime')
  DateTime? get dateTime;

  @BuiltValueField(wireName: r'service')
  String? get service;

  @BuiltValueField(wireName: r'observation')
  String? get observation;

  @BuiltValueField(wireName: r'price')
  double? get price;

  CreateAptSchedule._();

  factory CreateAptSchedule([void updates(CreateAptScheduleBuilder b)]) = _$CreateAptSchedule;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(CreateAptScheduleBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<CreateAptSchedule> get serializer => _$CreateAptScheduleSerializer();
}

class _$CreateAptScheduleSerializer implements PrimitiveSerializer<CreateAptSchedule> {
  @override
  final Iterable<Type> types = const [CreateAptSchedule, _$CreateAptSchedule];

  @override
  final String wireName = r'CreateAptSchedule';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    CreateAptSchedule object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'clientId';
    yield serializers.serialize(
      object.clientId,
      specifiedType: const FullType(String),
    );
    if (object.dateTime != null) {
      yield r'dateTime';
      yield serializers.serialize(
        object.dateTime,
        specifiedType: const FullType(DateTime),
      );
    }
    if (object.service != null) {
      yield r'service';
      yield serializers.serialize(
        object.service,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.observation != null) {
      yield r'observation';
      yield serializers.serialize(
        object.observation,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.price != null) {
      yield r'price';
      yield serializers.serialize(
        object.price,
        specifiedType: const FullType(double),
      );
    }
  }

  @override
  Object serialize(
    Serializers serializers,
    CreateAptSchedule object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required CreateAptScheduleBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'clientId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(String),
          ) as String;
          result.clientId = valueDes;
          break;
        case r'dateTime':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(DateTime),
          ) as DateTime;
          result.dateTime = valueDes;
          break;
        case r'service':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.service = valueDes;
          break;
        case r'observation':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.observation = valueDes;
          break;
        case r'price':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(double),
          ) as double;
          result.price = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  CreateAptSchedule deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = CreateAptScheduleBuilder();
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

