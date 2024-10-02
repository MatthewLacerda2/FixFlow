//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:openapi/src/model/client.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'apt_schedule.g.dart';

/// AptSchedule
///
/// Properties:
/// * [id] 
/// * [clientId] - The Id of the Client who made the Schedule
/// * [client] 
/// * [businessId] - The Id of the Business who owns this Contact
/// * [wasContacted] 
/// * [dateTime] - The scheduled DateTime of the Appointment
/// * [service] 
/// * [observation] 
/// * [price] 
@BuiltValue()
abstract class AptSchedule implements Built<AptSchedule, AptScheduleBuilder> {
  @BuiltValueField(wireName: r'id')
  String get id;

  /// The Id of the Client who made the Schedule
  @BuiltValueField(wireName: r'clientId')
  String get clientId;

  @BuiltValueField(wireName: r'client')
  Client? get client;

  /// The Id of the Business who owns this Contact
  @BuiltValueField(wireName: r'businessId')
  String get businessId;

  @BuiltValueField(wireName: r'wasContacted')
  bool? get wasContacted;

  /// The scheduled DateTime of the Appointment
  @BuiltValueField(wireName: r'dateTime')
  DateTime? get dateTime;

  @BuiltValueField(wireName: r'service')
  String? get service;

  @BuiltValueField(wireName: r'observation')
  String? get observation;

  @BuiltValueField(wireName: r'price')
  double? get price;

  AptSchedule._();

  factory AptSchedule([void updates(AptScheduleBuilder b)]) = _$AptSchedule;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(AptScheduleBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<AptSchedule> get serializer => _$AptScheduleSerializer();
}

class _$AptScheduleSerializer implements PrimitiveSerializer<AptSchedule> {
  @override
  final Iterable<Type> types = const [AptSchedule, _$AptSchedule];

  @override
  final String wireName = r'AptSchedule';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    AptSchedule object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'id';
    yield serializers.serialize(
      object.id,
      specifiedType: const FullType(String),
    );
    yield r'clientId';
    yield serializers.serialize(
      object.clientId,
      specifiedType: const FullType(String),
    );
    if (object.client != null) {
      yield r'client';
      yield serializers.serialize(
        object.client,
        specifiedType: const FullType(Client),
      );
    }
    yield r'businessId';
    yield serializers.serialize(
      object.businessId,
      specifiedType: const FullType(String),
    );
    if (object.wasContacted != null) {
      yield r'wasContacted';
      yield serializers.serialize(
        object.wasContacted,
        specifiedType: const FullType(bool),
      );
    }
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
    AptSchedule object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required AptScheduleBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'id':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(String),
          ) as String;
          result.id = valueDes;
          break;
        case r'clientId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(String),
          ) as String;
          result.clientId = valueDes;
          break;
        case r'client':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(Client),
          ) as Client;
          result.client.replace(valueDes);
          break;
        case r'businessId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(String),
          ) as String;
          result.businessId = valueDes;
          break;
        case r'wasContacted':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(bool),
          ) as bool;
          result.wasContacted = valueDes;
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
  AptSchedule deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = AptScheduleBuilder();
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

