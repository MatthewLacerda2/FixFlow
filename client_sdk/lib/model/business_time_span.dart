//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class BusinessTimeSpan {
  /// Returns a new [BusinessTimeSpan] instance.
  BusinessTimeSpan({
    this.id,
    this.isActive,
    this.start,
    this.finish,
  });

  String? id;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  bool? isActive;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  String? start;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  String? finish;

  @override
  bool operator ==(Object other) => identical(this, other) || other is BusinessTimeSpan &&
    other.id == id &&
    other.isActive == isActive &&
    other.start == start &&
    other.finish == finish;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (id == null ? 0 : id!.hashCode) +
    (isActive == null ? 0 : isActive!.hashCode) +
    (start == null ? 0 : start!.hashCode) +
    (finish == null ? 0 : finish!.hashCode);

  @override
  String toString() => 'BusinessTimeSpan[id=$id, isActive=$isActive, start=$start, finish=$finish]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
    if (this.id != null) {
      json[r'id'] = this.id;
    } else {
      json[r'id'] = null;
    }
    if (this.isActive != null) {
      json[r'isActive'] = this.isActive;
    } else {
      json[r'isActive'] = null;
    }
    if (this.start != null) {
      json[r'start'] = this.start;
    } else {
      json[r'start'] = null;
    }
    if (this.finish != null) {
      json[r'finish'] = this.finish;
    } else {
      json[r'finish'] = null;
    }
    return json;
  }

  /// Returns a new [BusinessTimeSpan] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static BusinessTimeSpan? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "BusinessTimeSpan[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "BusinessTimeSpan[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return BusinessTimeSpan(
        id: mapValueOfType<String>(json, r'id'),
        isActive: mapValueOfType<bool>(json, r'isActive'),
        start: mapValueOfType<String>(json, r'start'),
        finish: mapValueOfType<String>(json, r'finish'),
      );
    }
    return null;
  }

  static List<BusinessTimeSpan> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <BusinessTimeSpan>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = BusinessTimeSpan.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, BusinessTimeSpan> mapFromJson(dynamic json) {
    final map = <String, BusinessTimeSpan>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = BusinessTimeSpan.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of BusinessTimeSpan-objects as value to a dart map
  static Map<String, List<BusinessTimeSpan>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<BusinessTimeSpan>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = BusinessTimeSpan.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
  };
}

