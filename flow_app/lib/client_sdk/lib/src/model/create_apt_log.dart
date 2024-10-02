//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'create_apt_log.g.dart';

/// CreateAptLog
///
/// Properties:
/// * [clientId] - The Id of the Client who took the Appointment
/// * [businessId] - The Id of the Business who owns this Contact
/// * [scheduleId] - The Id of the Schedule that precedes this Log, if any
/// * [dateTime] - The DateTime when the Log was registered
/// * [price] 
/// * [service] 
/// * [description] 
/// * [whenShouldClientComeBack] - The Date when we expect the Client to schedule another appointment.  We are leaving as DateTime for simplicity but we only need the Date from this class
@BuiltValue()
abstract class CreateAptLog implements Built<CreateAptLog, CreateAptLogBuilder> {
  /// The Id of the Client who took the Appointment
  @BuiltValueField(wireName: r'clientId')
  String get clientId;

  /// The Id of the Business who owns this Contact
  @BuiltValueField(wireName: r'businessId')
  String get businessId;

  /// The Id of the Schedule that precedes this Log, if any
  @BuiltValueField(wireName: r'scheduleId')
  String? get scheduleId;

  /// The DateTime when the Log was registered
  @BuiltValueField(wireName: r'dateTime')
  DateTime? get dateTime;

  @BuiltValueField(wireName: r'price')
  double? get price;

  @BuiltValueField(wireName: r'service')
  String? get service;

  @BuiltValueField(wireName: r'description')
  String? get description;

  /// The Date when we expect the Client to schedule another appointment.  We are leaving as DateTime for simplicity but we only need the Date from this class
  @BuiltValueField(wireName: r'whenShouldClientComeBack')
  DateTime? get whenShouldClientComeBack;

  CreateAptLog._();

  factory CreateAptLog([void updates(CreateAptLogBuilder b)]) = _$CreateAptLog;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(CreateAptLogBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<CreateAptLog> get serializer => _$CreateAptLogSerializer();
}

class _$CreateAptLogSerializer implements PrimitiveSerializer<CreateAptLog> {
  @override
  final Iterable<Type> types = const [CreateAptLog, _$CreateAptLog];

  @override
  final String wireName = r'CreateAptLog';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    CreateAptLog object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'clientId';
    yield serializers.serialize(
      object.clientId,
      specifiedType: const FullType(String),
    );
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
    if (object.price != null) {
      yield r'price';
      yield serializers.serialize(
        object.price,
        specifiedType: const FullType(double),
      );
    }
    if (object.service != null) {
      yield r'service';
      yield serializers.serialize(
        object.service,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.description != null) {
      yield r'description';
      yield serializers.serialize(
        object.description,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.whenShouldClientComeBack != null) {
      yield r'whenShouldClientComeBack';
      yield serializers.serialize(
        object.whenShouldClientComeBack,
        specifiedType: const FullType(DateTime),
      );
    }
  }

  @override
  Object serialize(
    Serializers serializers,
    CreateAptLog object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required CreateAptLogBuilder result,
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
        case r'price':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(double),
          ) as double;
          result.price = valueDes;
          break;
        case r'service':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.service = valueDes;
          break;
        case r'description':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.description = valueDes;
          break;
        case r'whenShouldClientComeBack':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(DateTime),
          ) as DateTime;
          result.whenShouldClientComeBack = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  CreateAptLog deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = CreateAptLogBuilder();
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

