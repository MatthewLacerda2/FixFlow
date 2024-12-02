//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class CreateAptSchedule {
  /// Returns a new [CreateAptSchedule] instance.
  CreateAptSchedule({
    required this.customerId,
    this.dateTime,
    this.service,
    this.description,
    required this.price,
  });

  String customerId;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  DateTime? dateTime;

  String? service;

  String? description;

  double price;

  @override
  bool operator ==(Object other) => identical(this, other) || other is CreateAptSchedule &&
    other.customerId == customerId &&
    other.dateTime == dateTime &&
    other.service == service &&
    other.description == description &&
    other.price == price;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (customerId.hashCode) +
    (dateTime == null ? 0 : dateTime!.hashCode) +
    (service == null ? 0 : service!.hashCode) +
    (description == null ? 0 : description!.hashCode) +
    (price.hashCode);

  @override
  String toString() => 'CreateAptSchedule[customerId=$customerId, dateTime=$dateTime, service=$service, description=$description, price=$price]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
      json[r'customerId'] = this.customerId;
    if (this.dateTime != null) {
      json[r'dateTime'] = this.dateTime!.toUtc().toIso8601String();
    } else {
      json[r'dateTime'] = null;
    }
    if (this.service != null) {
      json[r'service'] = this.service;
    } else {
      json[r'service'] = null;
    }
    if (this.description != null) {
      json[r'description'] = this.description;
    } else {
      json[r'description'] = null;
    }
      json[r'price'] = this.price;
    return json;
  }

  /// Returns a new [CreateAptSchedule] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static CreateAptSchedule? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "CreateAptSchedule[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "CreateAptSchedule[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return CreateAptSchedule(
        customerId: mapValueOfType<String>(json, r'customerId')!,
        dateTime: mapDateTime(json, r'dateTime', r''),
        service: mapValueOfType<String>(json, r'service'),
        description: mapValueOfType<String>(json, r'description'),
        price: mapValueOfType<double>(json, r'price')!,
      );
    }
    return null;
  }

  static List<CreateAptSchedule> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <CreateAptSchedule>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = CreateAptSchedule.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, CreateAptSchedule> mapFromJson(dynamic json) {
    final map = <String, CreateAptSchedule>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = CreateAptSchedule.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of CreateAptSchedule-objects as value to a dart map
  static Map<String, List<CreateAptSchedule>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<CreateAptSchedule>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = CreateAptSchedule.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
    'customerId',
    'price',
  };
}

