//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'business_idle_periods_request.g.dart';

/// BusinessIdlePeriodsRequest
///
/// Properties:
/// * [businessId] 
/// * [date] 
@BuiltValue()
abstract class BusinessIdlePeriodsRequest implements Built<BusinessIdlePeriodsRequest, BusinessIdlePeriodsRequestBuilder> {
  @BuiltValueField(wireName: r'businessId')
  String? get businessId;

  @BuiltValueField(wireName: r'date')
  DateTime? get date;

  BusinessIdlePeriodsRequest._();

  factory BusinessIdlePeriodsRequest([void updates(BusinessIdlePeriodsRequestBuilder b)]) = _$BusinessIdlePeriodsRequest;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(BusinessIdlePeriodsRequestBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<BusinessIdlePeriodsRequest> get serializer => _$BusinessIdlePeriodsRequestSerializer();
}

class _$BusinessIdlePeriodsRequestSerializer implements PrimitiveSerializer<BusinessIdlePeriodsRequest> {
  @override
  final Iterable<Type> types = const [BusinessIdlePeriodsRequest, _$BusinessIdlePeriodsRequest];

  @override
  final String wireName = r'BusinessIdlePeriodsRequest';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    BusinessIdlePeriodsRequest object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    if (object.businessId != null) {
      yield r'businessId';
      yield serializers.serialize(
        object.businessId,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.date != null) {
      yield r'date';
      yield serializers.serialize(
        object.date,
        specifiedType: const FullType(DateTime),
      );
    }
  }

  @override
  Object serialize(
    Serializers serializers,
    BusinessIdlePeriodsRequest object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required BusinessIdlePeriodsRequestBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'businessId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.businessId = valueDes;
          break;
        case r'date':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(DateTime),
          ) as DateTime;
          result.date = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  BusinessIdlePeriodsRequest deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = BusinessIdlePeriodsRequestBuilder();
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

