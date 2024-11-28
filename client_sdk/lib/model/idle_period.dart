//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class IdlePeriod {
  /// Returns a new [IdlePeriod] instance.
  IdlePeriod({
    this.id,
    this.name,
    this.businessId,
    this.start,
    this.finish,
  });

  String? id;

  String? name;

  String? businessId;

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

  @override
  bool operator ==(Object other) => identical(this, other) || other is IdlePeriod &&
    other.id == id &&
    other.name == name &&
    other.businessId == businessId &&
    other.start == start &&
    other.finish == finish;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (id == null ? 0 : id!.hashCode) +
    (name == null ? 0 : name!.hashCode) +
    (businessId == null ? 0 : businessId!.hashCode) +
    (start == null ? 0 : start!.hashCode) +
    (finish == null ? 0 : finish!.hashCode);

  @override
  String toString() => 'IdlePeriod[id=$id, name=$name, businessId=$businessId, start=$start, finish=$finish]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
    if (this.id != null) {
      json[r'id'] = this.id;
    } else {
      json[r'id'] = null;
    }
    if (this.name != null) {
      json[r'name'] = this.name;
    } else {
      json[r'name'] = null;
    }
    if (this.businessId != null) {
      json[r'businessId'] = this.businessId;
    } else {
      json[r'businessId'] = null;
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
    return json;
  }

  /// Returns a new [IdlePeriod] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static IdlePeriod? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "IdlePeriod[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "IdlePeriod[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return IdlePeriod(
        id: mapValueOfType<String>(json, r'id'),
        name: mapValueOfType<String>(json, r'name'),
        businessId: mapValueOfType<String>(json, r'businessId'),
        start: mapDateTime(json, r'start', r''),
        finish: mapDateTime(json, r'finish', r''),
      );
    }
    return null;
  }

  static List<IdlePeriod> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <IdlePeriod>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = IdlePeriod.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, IdlePeriod> mapFromJson(dynamic json) {
    final map = <String, IdlePeriod>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = IdlePeriod.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of IdlePeriod-objects as value to a dart map
  static Map<String, List<IdlePeriod>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<IdlePeriod>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = IdlePeriod.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
  };
}

