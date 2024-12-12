//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class UpdateAptLog {
  /// Returns a new [UpdateAptLog] instance.
  UpdateAptLog({
    required this.id,
    this.dateTime,
    this.service,
    required this.price,
    this.description,
  });

  String id;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  DateTime? dateTime;

  String? service;

  int price;

  String? description;

  @override
  bool operator ==(Object other) => identical(this, other) || other is UpdateAptLog &&
    other.id == id &&
    other.dateTime == dateTime &&
    other.service == service &&
    other.price == price &&
    other.description == description;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (id.hashCode) +
    (dateTime == null ? 0 : dateTime!.hashCode) +
    (service == null ? 0 : service!.hashCode) +
    (price.hashCode) +
    (description == null ? 0 : description!.hashCode);

  @override
  String toString() => 'UpdateAptLog[id=$id, dateTime=$dateTime, service=$service, price=$price, description=$description]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
      json[r'id'] = this.id;
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
      json[r'price'] = this.price;
    if (this.description != null) {
      json[r'description'] = this.description;
    } else {
      json[r'description'] = null;
    }
    return json;
  }

  /// Returns a new [UpdateAptLog] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static UpdateAptLog? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "UpdateAptLog[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "UpdateAptLog[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return UpdateAptLog(
        id: mapValueOfType<String>(json, r'id')!,
        dateTime: mapDateTime(json, r'dateTime', r''),
        service: mapValueOfType<String>(json, r'service'),
        price: mapValueOfType<int>(json, r'price')!,
        description: mapValueOfType<String>(json, r'description'),
      );
    }
    return null;
  }

  static List<UpdateAptLog> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <UpdateAptLog>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = UpdateAptLog.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, UpdateAptLog> mapFromJson(dynamic json) {
    final map = <String, UpdateAptLog>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = UpdateAptLog.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of UpdateAptLog-objects as value to a dart map
  static Map<String, List<UpdateAptLog>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<UpdateAptLog>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = UpdateAptLog.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
    'id',
    'price',
  };
}

