//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class BusinessWeek {
  /// Returns a new [BusinessWeek] instance.
  BusinessWeek({
    this.id,
    this.sunday,
    this.monday,
    this.tuesday,
    this.wednesday,
    this.thursday,
    this.friday,
    this.saturday,
  });

  String? id;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  BusinessTimeSpan? sunday;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  BusinessTimeSpan? monday;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  BusinessTimeSpan? tuesday;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  BusinessTimeSpan? wednesday;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  BusinessTimeSpan? thursday;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  BusinessTimeSpan? friday;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  BusinessTimeSpan? saturday;

  @override
  bool operator ==(Object other) => identical(this, other) || other is BusinessWeek &&
    other.id == id &&
    other.sunday == sunday &&
    other.monday == monday &&
    other.tuesday == tuesday &&
    other.wednesday == wednesday &&
    other.thursday == thursday &&
    other.friday == friday &&
    other.saturday == saturday;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (id == null ? 0 : id!.hashCode) +
    (sunday == null ? 0 : sunday!.hashCode) +
    (monday == null ? 0 : monday!.hashCode) +
    (tuesday == null ? 0 : tuesday!.hashCode) +
    (wednesday == null ? 0 : wednesday!.hashCode) +
    (thursday == null ? 0 : thursday!.hashCode) +
    (friday == null ? 0 : friday!.hashCode) +
    (saturday == null ? 0 : saturday!.hashCode);

  @override
  String toString() => 'BusinessWeek[id=$id, sunday=$sunday, monday=$monday, tuesday=$tuesday, wednesday=$wednesday, thursday=$thursday, friday=$friday, saturday=$saturday]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
    if (this.id != null) {
      json[r'id'] = this.id;
    } else {
      json[r'id'] = null;
    }
    if (this.sunday != null) {
      json[r'sunday'] = this.sunday;
    } else {
      json[r'sunday'] = null;
    }
    if (this.monday != null) {
      json[r'monday'] = this.monday;
    } else {
      json[r'monday'] = null;
    }
    if (this.tuesday != null) {
      json[r'tuesday'] = this.tuesday;
    } else {
      json[r'tuesday'] = null;
    }
    if (this.wednesday != null) {
      json[r'wednesday'] = this.wednesday;
    } else {
      json[r'wednesday'] = null;
    }
    if (this.thursday != null) {
      json[r'thursday'] = this.thursday;
    } else {
      json[r'thursday'] = null;
    }
    if (this.friday != null) {
      json[r'friday'] = this.friday;
    } else {
      json[r'friday'] = null;
    }
    if (this.saturday != null) {
      json[r'saturday'] = this.saturday;
    } else {
      json[r'saturday'] = null;
    }
    return json;
  }

  /// Returns a new [BusinessWeek] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static BusinessWeek? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "BusinessWeek[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "BusinessWeek[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return BusinessWeek(
        id: mapValueOfType<String>(json, r'id'),
        sunday: BusinessTimeSpan.fromJson(json[r'sunday']),
        monday: BusinessTimeSpan.fromJson(json[r'monday']),
        tuesday: BusinessTimeSpan.fromJson(json[r'tuesday']),
        wednesday: BusinessTimeSpan.fromJson(json[r'wednesday']),
        thursday: BusinessTimeSpan.fromJson(json[r'thursday']),
        friday: BusinessTimeSpan.fromJson(json[r'friday']),
        saturday: BusinessTimeSpan.fromJson(json[r'saturday']),
      );
    }
    return null;
  }

  static List<BusinessWeek> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <BusinessWeek>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = BusinessWeek.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, BusinessWeek> mapFromJson(dynamic json) {
    final map = <String, BusinessWeek>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = BusinessWeek.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of BusinessWeek-objects as value to a dart map
  static Map<String, List<BusinessWeek>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<BusinessWeek>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = BusinessWeek.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
  };
}

