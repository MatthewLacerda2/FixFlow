//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_collection/built_collection.dart';
import 'package:openapi/src/model/apt_log.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'client_record.g.dart';

/// Client history in the business
///
/// Properties:
/// * [fullName] 
/// * [phoneNumber] - Phone Number. Must contain only numbers
/// * [email] 
/// * [cpf] 
/// * [additionalNote] - Special information about the Client, if applicable
/// * [firstLog] 
/// * [lastLog] 
/// * [logs] 
/// * [numSchedules] 
/// * [avgTimeBetweenSchedules] 
@BuiltValue()
abstract class ClientRecord implements Built<ClientRecord, ClientRecordBuilder> {
  @BuiltValueField(wireName: r'fullName')
  String get fullName;

  /// Phone Number. Must contain only numbers
  @BuiltValueField(wireName: r'phoneNumber')
  String get phoneNumber;

  @BuiltValueField(wireName: r'email')
  String? get email;

  @BuiltValueField(wireName: r'cpf')
  String? get cpf;

  /// Special information about the Client, if applicable
  @BuiltValueField(wireName: r'additionalNote')
  String? get additionalNote;

  @BuiltValueField(wireName: r'firstLog')
  DateTime? get firstLog;

  @BuiltValueField(wireName: r'lastLog')
  DateTime? get lastLog;

  @BuiltValueField(wireName: r'logs')
  BuiltList<AptLog>? get logs;

  @BuiltValueField(wireName: r'numSchedules')
  int? get numSchedules;

  @BuiltValueField(wireName: r'avgTimeBetweenSchedules')
  int? get avgTimeBetweenSchedules;

  ClientRecord._();

  factory ClientRecord([void updates(ClientRecordBuilder b)]) = _$ClientRecord;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(ClientRecordBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<ClientRecord> get serializer => _$ClientRecordSerializer();
}

class _$ClientRecordSerializer implements PrimitiveSerializer<ClientRecord> {
  @override
  final Iterable<Type> types = const [ClientRecord, _$ClientRecord];

  @override
  final String wireName = r'ClientRecord';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    ClientRecord object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'fullName';
    yield serializers.serialize(
      object.fullName,
      specifiedType: const FullType(String),
    );
    yield r'phoneNumber';
    yield serializers.serialize(
      object.phoneNumber,
      specifiedType: const FullType(String),
    );
    if (object.email != null) {
      yield r'email';
      yield serializers.serialize(
        object.email,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.cpf != null) {
      yield r'cpf';
      yield serializers.serialize(
        object.cpf,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.additionalNote != null) {
      yield r'additionalNote';
      yield serializers.serialize(
        object.additionalNote,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.firstLog != null) {
      yield r'firstLog';
      yield serializers.serialize(
        object.firstLog,
        specifiedType: const FullType.nullable(DateTime),
      );
    }
    if (object.lastLog != null) {
      yield r'lastLog';
      yield serializers.serialize(
        object.lastLog,
        specifiedType: const FullType.nullable(DateTime),
      );
    }
    if (object.logs != null) {
      yield r'logs';
      yield serializers.serialize(
        object.logs,
        specifiedType: const FullType.nullable(BuiltList, [FullType(AptLog)]),
      );
    }
    if (object.numSchedules != null) {
      yield r'numSchedules';
      yield serializers.serialize(
        object.numSchedules,
        specifiedType: const FullType(int),
      );
    }
    if (object.avgTimeBetweenSchedules != null) {
      yield r'avgTimeBetweenSchedules';
      yield serializers.serialize(
        object.avgTimeBetweenSchedules,
        specifiedType: const FullType(int),
      );
    }
  }

  @override
  Object serialize(
    Serializers serializers,
    ClientRecord object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required ClientRecordBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'fullName':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(String),
          ) as String;
          result.fullName = valueDes;
          break;
        case r'phoneNumber':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(String),
          ) as String;
          result.phoneNumber = valueDes;
          break;
        case r'email':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.email = valueDes;
          break;
        case r'cpf':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.cpf = valueDes;
          break;
        case r'additionalNote':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.additionalNote = valueDes;
          break;
        case r'firstLog':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(DateTime),
          ) as DateTime?;
          if (valueDes == null) continue;
          result.firstLog = valueDes;
          break;
        case r'lastLog':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(DateTime),
          ) as DateTime?;
          if (valueDes == null) continue;
          result.lastLog = valueDes;
          break;
        case r'logs':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(BuiltList, [FullType(AptLog)]),
          ) as BuiltList<AptLog>?;
          if (valueDes == null) continue;
          result.logs.replace(valueDes);
          break;
        case r'numSchedules':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.numSchedules = valueDes;
          break;
        case r'avgTimeBetweenSchedules':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.avgTimeBetweenSchedules = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  ClientRecord deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = ClientRecordBuilder();
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

