//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:openapi/src/model/client.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'apt_log.g.dart';

/// AptLog
///
/// Properties:
/// * [id] 
/// * [clientId] - The Id of the Client who took the Appointment
/// * [client] 
/// * [businessId] - The Id of the Business who owns this Contact
/// * [scheduleId] - The Id of the Schedule that precedes this Log, if any
/// * [dateTime] - The DateTime when the Log was registered
/// * [service] 
/// * [price] 
/// * [description] 
@BuiltValue()
abstract class AptLog implements Built<AptLog, AptLogBuilder> {
  @BuiltValueField(wireName: r'id')
  String get id;

  /// The Id of the Client who took the Appointment
  @BuiltValueField(wireName: r'clientId')
  String get clientId;

  @BuiltValueField(wireName: r'client')
  Client? get client;

  /// The Id of the Business who owns this Contact
  @BuiltValueField(wireName: r'businessId')
  String get businessId;

  /// The Id of the Schedule that precedes this Log, if any
  @BuiltValueField(wireName: r'scheduleId')
  String? get scheduleId;

  /// The DateTime when the Log was registered
  @BuiltValueField(wireName: r'dateTime')
  DateTime? get dateTime;

  @BuiltValueField(wireName: r'service')
  String? get service;

  @BuiltValueField(wireName: r'price')
  double? get price;

  @BuiltValueField(wireName: r'description')
  String? get description;

  AptLog._();

  factory AptLog([void updates(AptLogBuilder b)]) = _$AptLog;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(AptLogBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<AptLog> get serializer => _$AptLogSerializer();
}

class _$AptLogSerializer implements PrimitiveSerializer<AptLog> {
  @override
  final Iterable<Type> types = const [AptLog, _$AptLog];

  @override
  final String wireName = r'AptLog';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    AptLog object, {
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
    if (object.scheduleId != null) {
      yield r'scheduleId';
      yield serializers.serialize(
        object.scheduleId,
        specifiedType: const FullType.nullable(String),
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
    if (object.price != null) {
      yield r'price';
      yield serializers.serialize(
        object.price,
        specifiedType: const FullType(double),
      );
    }
    if (object.description != null) {
      yield r'description';
      yield serializers.serialize(
        object.description,
        specifiedType: const FullType.nullable(String),
      );
    }
  }

  @override
  Object serialize(
    Serializers serializers,
    AptLog object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required AptLogBuilder result,
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
        case r'scheduleId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.scheduleId = valueDes;
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
        case r'price':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(double),
          ) as double;
          result.price = valueDes;
          break;
        case r'description':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.description = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  AptLog deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = AptLogBuilder();
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

