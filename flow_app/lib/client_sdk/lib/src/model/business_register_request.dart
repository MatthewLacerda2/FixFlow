//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'business_register_request.g.dart';

/// BusinessRegisterRequest
///
/// Properties:
/// * [name] - The Name of the Business or Business owner
/// * [email] 
/// * [cnpj] - CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
/// * [phoneNumber] - Phone Number. Must contain only numbers and/or a '+'
/// * [otpCode] 
/// * [confirmPassword] - Must be identical to 'password'
@BuiltValue()
abstract class BusinessRegisterRequest implements Built<BusinessRegisterRequest, BusinessRegisterRequestBuilder> {
  /// The Name of the Business or Business owner
  @BuiltValueField(wireName: r'name')
  String get name;

  @BuiltValueField(wireName: r'email')
  String? get email;

  /// CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
  @BuiltValueField(wireName: r'cnpj')
  String? get cnpj;

  /// Phone Number. Must contain only numbers and/or a '+'
  @BuiltValueField(wireName: r'phoneNumber')
  String get phoneNumber;

  @BuiltValueField(wireName: r'otpCode')
  String? get otpCode;

  /// Must be identical to 'password'
  @BuiltValueField(wireName: r'confirmPassword')
  String? get confirmPassword;

  BusinessRegisterRequest._();

  factory BusinessRegisterRequest([void updates(BusinessRegisterRequestBuilder b)]) = _$BusinessRegisterRequest;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(BusinessRegisterRequestBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<BusinessRegisterRequest> get serializer => _$BusinessRegisterRequestSerializer();
}

class _$BusinessRegisterRequestSerializer implements PrimitiveSerializer<BusinessRegisterRequest> {
  @override
  final Iterable<Type> types = const [BusinessRegisterRequest, _$BusinessRegisterRequest];

  @override
  final String wireName = r'BusinessRegisterRequest';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    BusinessRegisterRequest object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'name';
    yield serializers.serialize(
      object.name,
      specifiedType: const FullType(String),
    );
    if (object.email != null) {
      yield r'email';
      yield serializers.serialize(
        object.email,
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
    yield r'phoneNumber';
    yield serializers.serialize(
      object.phoneNumber,
      specifiedType: const FullType(String),
    );
    if (object.otpCode != null) {
      yield r'otpCode';
      yield serializers.serialize(
        object.otpCode,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.confirmPassword != null) {
      yield r'confirmPassword';
      yield serializers.serialize(
        object.confirmPassword,
        specifiedType: const FullType.nullable(String),
      );
    }
  }

  @override
  Object serialize(
    Serializers serializers,
    BusinessRegisterRequest object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required BusinessRegisterRequestBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'name':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(String),
          ) as String;
          result.name = valueDes;
          break;
        case r'email':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.email = valueDes;
          break;
        case r'cnpj':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.cnpj = valueDes;
          break;
        case r'phoneNumber':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(String),
          ) as String;
          result.phoneNumber = valueDes;
          break;
        case r'otpCode':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.otpCode = valueDes;
          break;
        case r'confirmPassword':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.confirmPassword = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  BusinessRegisterRequest deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = BusinessRegisterRequestBuilder();
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

