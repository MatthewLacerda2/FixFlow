//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'client.g.dart';

/// Client
///
/// Properties:
/// * [id] 
/// * [userName] 
/// * [normalizedUserName] 
/// * [email] 
/// * [normalizedEmail] 
/// * [emailConfirmed] 
/// * [passwordHash] 
/// * [securityStamp] 
/// * [concurrencyStamp] 
/// * [phoneNumber] 
/// * [phoneNumberConfirmed] 
/// * [twoFactorEnabled] 
/// * [lockoutEnd] 
/// * [lockoutEnabled] 
/// * [accessFailedCount] 
/// * [businessId] - The business from which the Client is a customer
/// * [fullName] 
/// * [cpf] - CPF. Must be on format XXX.XXX.XXX-XX
/// * [additionalNote] 
@BuiltValue()
abstract class Client implements Built<Client, ClientBuilder> {
  @BuiltValueField(wireName: r'id')
  String? get id;

  @BuiltValueField(wireName: r'userName')
  String? get userName;

  @BuiltValueField(wireName: r'normalizedUserName')
  String? get normalizedUserName;

  @BuiltValueField(wireName: r'email')
  String? get email;

  @BuiltValueField(wireName: r'normalizedEmail')
  String? get normalizedEmail;

  @BuiltValueField(wireName: r'emailConfirmed')
  bool? get emailConfirmed;

  @BuiltValueField(wireName: r'passwordHash')
  String? get passwordHash;

  @BuiltValueField(wireName: r'securityStamp')
  String? get securityStamp;

  @BuiltValueField(wireName: r'concurrencyStamp')
  String? get concurrencyStamp;

  @BuiltValueField(wireName: r'phoneNumber')
  String? get phoneNumber;

  @BuiltValueField(wireName: r'phoneNumberConfirmed')
  bool? get phoneNumberConfirmed;

  @BuiltValueField(wireName: r'twoFactorEnabled')
  bool? get twoFactorEnabled;

  @BuiltValueField(wireName: r'lockoutEnd')
  DateTime? get lockoutEnd;

  @BuiltValueField(wireName: r'lockoutEnabled')
  bool? get lockoutEnabled;

  @BuiltValueField(wireName: r'accessFailedCount')
  int? get accessFailedCount;

  /// The business from which the Client is a customer
  @BuiltValueField(wireName: r'businessId')
  String get businessId;

  @BuiltValueField(wireName: r'fullName')
  String get fullName;

  /// CPF. Must be on format XXX.XXX.XXX-XX
  @BuiltValueField(wireName: r'cpf')
  String? get cpf;

  @BuiltValueField(wireName: r'additionalNote')
  String? get additionalNote;

  Client._();

  factory Client([void updates(ClientBuilder b)]) = _$Client;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(ClientBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<Client> get serializer => _$ClientSerializer();
}

class _$ClientSerializer implements PrimitiveSerializer<Client> {
  @override
  final Iterable<Type> types = const [Client, _$Client];

  @override
  final String wireName = r'Client';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    Client object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    if (object.id != null) {
      yield r'id';
      yield serializers.serialize(
        object.id,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.userName != null) {
      yield r'userName';
      yield serializers.serialize(
        object.userName,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.normalizedUserName != null) {
      yield r'normalizedUserName';
      yield serializers.serialize(
        object.normalizedUserName,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.email != null) {
      yield r'email';
      yield serializers.serialize(
        object.email,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.normalizedEmail != null) {
      yield r'normalizedEmail';
      yield serializers.serialize(
        object.normalizedEmail,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.emailConfirmed != null) {
      yield r'emailConfirmed';
      yield serializers.serialize(
        object.emailConfirmed,
        specifiedType: const FullType(bool),
      );
    }
    if (object.passwordHash != null) {
      yield r'passwordHash';
      yield serializers.serialize(
        object.passwordHash,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.securityStamp != null) {
      yield r'securityStamp';
      yield serializers.serialize(
        object.securityStamp,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.concurrencyStamp != null) {
      yield r'concurrencyStamp';
      yield serializers.serialize(
        object.concurrencyStamp,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.phoneNumber != null) {
      yield r'phoneNumber';
      yield serializers.serialize(
        object.phoneNumber,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.phoneNumberConfirmed != null) {
      yield r'phoneNumberConfirmed';
      yield serializers.serialize(
        object.phoneNumberConfirmed,
        specifiedType: const FullType(bool),
      );
    }
    if (object.twoFactorEnabled != null) {
      yield r'twoFactorEnabled';
      yield serializers.serialize(
        object.twoFactorEnabled,
        specifiedType: const FullType(bool),
      );
    }
    if (object.lockoutEnd != null) {
      yield r'lockoutEnd';
      yield serializers.serialize(
        object.lockoutEnd,
        specifiedType: const FullType.nullable(DateTime),
      );
    }
    if (object.lockoutEnabled != null) {
      yield r'lockoutEnabled';
      yield serializers.serialize(
        object.lockoutEnabled,
        specifiedType: const FullType(bool),
      );
    }
    if (object.accessFailedCount != null) {
      yield r'accessFailedCount';
      yield serializers.serialize(
        object.accessFailedCount,
        specifiedType: const FullType(int),
      );
    }
    yield r'businessId';
    yield serializers.serialize(
      object.businessId,
      specifiedType: const FullType(String),
    );
    yield r'fullName';
    yield serializers.serialize(
      object.fullName,
      specifiedType: const FullType(String),
    );
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
  }

  @override
  Object serialize(
    Serializers serializers,
    Client object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required ClientBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'id':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.id = valueDes;
          break;
        case r'userName':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.userName = valueDes;
          break;
        case r'normalizedUserName':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.normalizedUserName = valueDes;
          break;
        case r'email':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.email = valueDes;
          break;
        case r'normalizedEmail':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.normalizedEmail = valueDes;
          break;
        case r'emailConfirmed':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(bool),
          ) as bool;
          result.emailConfirmed = valueDes;
          break;
        case r'passwordHash':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.passwordHash = valueDes;
          break;
        case r'securityStamp':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.securityStamp = valueDes;
          break;
        case r'concurrencyStamp':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.concurrencyStamp = valueDes;
          break;
        case r'phoneNumber':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.phoneNumber = valueDes;
          break;
        case r'phoneNumberConfirmed':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(bool),
          ) as bool;
          result.phoneNumberConfirmed = valueDes;
          break;
        case r'twoFactorEnabled':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(bool),
          ) as bool;
          result.twoFactorEnabled = valueDes;
          break;
        case r'lockoutEnd':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(DateTime),
          ) as DateTime?;
          if (valueDes == null) continue;
          result.lockoutEnd = valueDes;
          break;
        case r'lockoutEnabled':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(bool),
          ) as bool;
          result.lockoutEnabled = valueDes;
          break;
        case r'accessFailedCount':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.accessFailedCount = valueDes;
          break;
        case r'businessId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(String),
          ) as String;
          result.businessId = valueDes;
          break;
        case r'fullName':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(String),
          ) as String;
          result.fullName = valueDes;
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
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  Client deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = ClientBuilder();
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

