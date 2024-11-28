//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;


class ScheduleSort {
  /// Instantiate a new enum with the provided [value].
  const ScheduleSort._(this.value);

  /// The underlying value of this enum member.
  final int value;

  @override
  String toString() => value.toString();

  int toJson() => value;

  static const number0 = ScheduleSort._(0);
  static const number1 = ScheduleSort._(1);
  static const number2 = ScheduleSort._(2);

  /// List of all possible values in this [enum][ScheduleSort].
  static const values = <ScheduleSort>[
    number0,
    number1,
    number2,
  ];

  static ScheduleSort? fromJson(dynamic value) => ScheduleSortTypeTransformer().decode(value);

  static List<ScheduleSort> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <ScheduleSort>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = ScheduleSort.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }
}

/// Transformation class that can [encode] an instance of [ScheduleSort] to int,
/// and [decode] dynamic data back to [ScheduleSort].
class ScheduleSortTypeTransformer {
  factory ScheduleSortTypeTransformer() => _instance ??= const ScheduleSortTypeTransformer._();

  const ScheduleSortTypeTransformer._();

  int encode(ScheduleSort data) => data.value;

  /// Decodes a [dynamic value][data] to a ScheduleSort.
  ///
  /// If [allowNull] is true and the [dynamic value][data] cannot be decoded successfully,
  /// then null is returned. However, if [allowNull] is false and the [dynamic value][data]
  /// cannot be decoded successfully, then an [UnimplementedError] is thrown.
  ///
  /// The [allowNull] is very handy when an API changes and a new enum value is added or removed,
  /// and users are still using an old app with the old code.
  ScheduleSort? decode(dynamic data, {bool allowNull = true}) {
    if (data != null) {
      switch (data) {
        case 0: return ScheduleSort.number0;
        case 1: return ScheduleSort.number1;
        case 2: return ScheduleSort.number2;
        default:
          if (!allowNull) {
            throw ArgumentError('Unknown enum value to decode: $data');
          }
      }
    }
    return null;
  }

  /// Singleton [ScheduleSortTypeTransformer] instance.
  static ScheduleSortTypeTransformer? _instance;
}

