//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'client_dto.g.dart';

/// Brief information about the Client
///
/// Properties:
/// * [id] 
/// * [fullName] 
/// * [cpf] - CPF. Must be on format XXX.XXX.XXX-XX
/// * [additionalNote] - Special information about the Client, if applicable
/// * [phoneNumber] - Phone Number. Must contain only numbers
/// * [email] 
@BuiltValue()
abstract class ClientDTO implements Built<ClientDTO, ClientDTOBuilder> {
  @BuiltValueField(wireName: r'id')
  String get id;

  @BuiltValueField(wireName: r'fullName')
  String get fullName;

  /// CPF. Must be on format XXX.XXX.XXX-XX
  @BuiltValueField(wireName: r'cpf')
  String? get cpf;

  /// Special information about the Client, if applicable
  @BuiltValueField(wireName: r'additionalNote')
  String? get additionalNote;

  /// Phone Number. Must contain only numbers
  @BuiltValueField(wireName: r'phoneNumber')
  String get phoneNumber;

  @BuiltValueField(wireName: r'email')
  String? get email;

  ClientDTO._();

  factory ClientDTO([void updates(ClientDTOBuilder b)]) = _$ClientDTO;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(ClientDTOBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<ClientDTO> get serializer => _$ClientDTOSerializer();
}

class _$ClientDTOSerializer implements PrimitiveSerializer<ClientDTO> {
  @override
  final Iterable<Type> types = const [ClientDTO, _$ClientDTO];

  @override
  final String wireName = r'ClientDTO';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    ClientDTO object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'id';
    yield serializers.serialize(
      object.id,
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
  }

  @override
  Object serialize(
    Serializers serializers,
    ClientDTO object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required ClientDTOBuilder result,
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
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  ClientDTO deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = ClientDTOBuilder();
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

