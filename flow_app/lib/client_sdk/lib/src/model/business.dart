//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_collection/built_collection.dart';
import 'package:openapi/src/model/business_day.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'business.g.dart';

/// Business
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
/// * [createdDate] 
/// * [lastLogin] 
/// * [isActive] 
/// * [name] - The Name of the Business or Business owner
/// * [cnpj] - CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
/// * [businessDays] - The DateTimes of the week where the business is open
/// * [allowListedServicesOnly] 
/// * [openOnHolidays] 
@BuiltValue()
abstract class Business implements Built<Business, BusinessBuilder> {
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

  @BuiltValueField(wireName: r'createdDate')
  DateTime? get createdDate;

  @BuiltValueField(wireName: r'lastLogin')
  DateTime? get lastLogin;

  @BuiltValueField(wireName: r'isActive')
  bool? get isActive;

  /// The Name of the Business or Business owner
  @BuiltValueField(wireName: r'name')
  String? get name;

  /// CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
  @BuiltValueField(wireName: r'cnpj')
  String? get cnpj;

  /// The DateTimes of the week where the business is open
  @BuiltValueField(wireName: r'businessDays')
  BuiltList<BusinessDay>? get businessDays;

  @BuiltValueField(wireName: r'allowListedServicesOnly')
  bool? get allowListedServicesOnly;

  @BuiltValueField(wireName: r'openOnHolidays')
  bool? get openOnHolidays;

  Business._();

  factory Business([void updates(BusinessBuilder b)]) = _$Business;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(BusinessBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<Business> get serializer => _$BusinessSerializer();
}

class _$BusinessSerializer implements PrimitiveSerializer<Business> {
  @override
  final Iterable<Type> types = const [Business, _$Business];

  @override
  final String wireName = r'Business';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    Business object, {
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
    if (object.createdDate != null) {
      yield r'createdDate';
      yield serializers.serialize(
        object.createdDate,
        specifiedType: const FullType(DateTime),
      );
    }
    if (object.lastLogin != null) {
      yield r'lastLogin';
      yield serializers.serialize(
        object.lastLogin,
        specifiedType: const FullType(DateTime),
      );
    }
    if (object.isActive != null) {
      yield r'isActive';
      yield serializers.serialize(
        object.isActive,
        specifiedType: const FullType(bool),
      );
    }
    if (object.name != null) {
      yield r'name';
      yield serializers.serialize(
        object.name,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.cnpj != null) {
      yield r'cnpj';
      yield serializers.serialize(
        object.cnpj,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.businessDays != null) {
      yield r'businessDays';
      yield serializers.serialize(
        object.businessDays,
        specifiedType: const FullType.nullable(BuiltList, [FullType(BusinessDay)]),
      );
    }
    if (object.allowListedServicesOnly != null) {
      yield r'allowListedServicesOnly';
      yield serializers.serialize(
        object.allowListedServicesOnly,
        specifiedType: const FullType(bool),
      );
    }
    if (object.openOnHolidays != null) {
      yield r'openOnHolidays';
      yield serializers.serialize(
        object.openOnHolidays,
        specifiedType: const FullType(bool),
      );
    }
  }

  @override
  Object serialize(
    Serializers serializers,
    Business object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required BusinessBuilder result,
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
        case r'createdDate':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(DateTime),
          ) as DateTime;
          result.createdDate = valueDes;
          break;
        case r'lastLogin':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(DateTime),
          ) as DateTime;
          result.lastLogin = valueDes;
          break;
        case r'isActive':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(bool),
          ) as bool;
          result.isActive = valueDes;
          break;
        case r'name':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.name = valueDes;
          break;
        case r'cnpj':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.cnpj = valueDes;
          break;
        case r'businessDays':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(BuiltList, [FullType(BusinessDay)]),
          ) as BuiltList<BusinessDay>?;
          if (valueDes == null) continue;
          result.businessDays.replace(valueDes);
          break;
        case r'allowListedServicesOnly':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(bool),
          ) as bool;
          result.allowListedServicesOnly = valueDes;
          break;
        case r'openOnHolidays':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(bool),
          ) as bool;
          result.openOnHolidays = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  Business deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = BusinessBuilder();
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

