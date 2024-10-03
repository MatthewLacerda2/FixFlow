//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class BusinessDay {
  /// Returns a new [BusinessDay] instance.
  BusinessDay({
    this.id,
    this.start,
    this.finish,
    this.isOpen,
  });

  String? id;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  DateTime? start;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  DateTime? finish;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  bool? isOpen;

  @override
  bool operator ==(Object other) => identical(this, other) || other is BusinessDay &&
    other.id == id &&
    other.start == start &&
    other.finish == finish &&
    other.isOpen == isOpen;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (id == null ? 0 : id!.hashCode) +
    (start == null ? 0 : start!.hashCode) +
    (finish == null ? 0 : finish!.hashCode) +
    (isOpen == null ? 0 : isOpen!.hashCode);

  @override
  String toString() => 'BusinessDay[id=$id, start=$start, finish=$finish, isOpen=$isOpen]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
    if (this.id != null) {
      json[r'id'] = this.id;
    } else {
      json[r'id'] = null;
    }
    if (this.start != null) {
      json[r'start'] = this.start!.toUtc().toIso8601String();
    } else {
      json[r'start'] = null;
    }
    if (this.finish != null) {
      json[r'finish'] = this.finish!.toUtc().toIso8601String();
    } else {
      json[r'finish'] = null;
    }
    if (this.isOpen != null) {
      json[r'isOpen'] = this.isOpen;
    } else {
      json[r'isOpen'] = null;
    }
    return json;
  }

  /// Returns a new [BusinessDay] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static BusinessDay? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "BusinessDay[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "BusinessDay[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return BusinessDay(
        id: mapValueOfType<String>(json, r'id'),
        start: mapDateTime(json, r'start', r''),
        finish: mapDateTime(json, r'finish', r''),
        isOpen: mapValueOfType<bool>(json, r'isOpen'),
      );
    }
    return null;
  }

  static List<BusinessDay> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <BusinessDay>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = BusinessDay.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, BusinessDay> mapFromJson(dynamic json) {
    final map = <String, BusinessDay>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = BusinessDay.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of BusinessDay-objects as value to a dart map
  static Map<String, List<BusinessDay>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<BusinessDay>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = BusinessDay.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
  };
}

