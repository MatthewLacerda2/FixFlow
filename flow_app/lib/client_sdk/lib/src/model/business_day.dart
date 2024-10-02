//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'business_day.g.dart';

/// BusinessDay
///
/// Properties:
/// * [id] 
/// * [start] 
/// * [finish] 
/// * [isOpen] 
@BuiltValue()
abstract class BusinessDay implements Built<BusinessDay, BusinessDayBuilder> {
  @BuiltValueField(wireName: r'id')
  String? get id;

  @BuiltValueField(wireName: r'start')
  DateTime? get start;

  @BuiltValueField(wireName: r'finish')
  DateTime? get finish;

  @BuiltValueField(wireName: r'isOpen')
  bool? get isOpen;

  BusinessDay._();

  factory BusinessDay([void updates(BusinessDayBuilder b)]) = _$BusinessDay;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(BusinessDayBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<BusinessDay> get serializer => _$BusinessDaySerializer();
}

class _$BusinessDaySerializer implements PrimitiveSerializer<BusinessDay> {
  @override
  final Iterable<Type> types = const [BusinessDay, _$BusinessDay];

  @override
  final String wireName = r'BusinessDay';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    BusinessDay object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    if (object.id != null) {
      yield r'id';
      yield serializers.serialize(
        object.id,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.start != null) {
      yield r'start';
      yield serializers.serialize(
        object.start,
        specifiedType: const FullType(DateTime),
      );
    }
    if (object.finish != null) {
      yield r'finish';
      yield serializers.serialize(
        object.finish,
        specifiedType: const FullType(DateTime),
      );
    }
    if (object.isOpen != null) {
      yield r'isOpen';
      yield serializers.serialize(
        object.isOpen,
        specifiedType: const FullType(bool),
      );
    }
  }

  @override
  Object serialize(
    Serializers serializers,
    BusinessDay object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required BusinessDayBuilder result,
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
        case r'start':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(DateTime),
          ) as DateTime;
          result.start = valueDes;
          break;
        case r'finish':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(DateTime),
          ) as DateTime;
          result.finish = valueDes;
          break;
        case r'isOpen':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(bool),
          ) as bool;
          result.isOpen = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  BusinessDay deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = BusinessDayBuilder();
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

