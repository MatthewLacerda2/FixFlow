//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class BusinessIdlePeriodsRequest {
  /// Returns a new [BusinessIdlePeriodsRequest] instance.
  BusinessIdlePeriodsRequest({
    this.businessId,
    this.date,
  });

  String? businessId;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  DateTime? date;

  @override
  bool operator ==(Object other) => identical(this, other) || other is BusinessIdlePeriodsRequest &&
    other.businessId == businessId &&
    other.date == date;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (businessId == null ? 0 : businessId!.hashCode) +
    (date == null ? 0 : date!.hashCode);

  @override
  String toString() => 'BusinessIdlePeriodsRequest[businessId=$businessId, date=$date]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
    if (this.businessId != null) {
      json[r'businessId'] = this.businessId;
    } else {
      json[r'businessId'] = null;
    }
    if (this.date != null) {
      json[r'date'] = this.date!.toUtc().toIso8601String();
    } else {
      json[r'date'] = null;
    }
    return json;
  }

  /// Returns a new [BusinessIdlePeriodsRequest] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static BusinessIdlePeriodsRequest? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "BusinessIdlePeriodsRequest[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "BusinessIdlePeriodsRequest[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return BusinessIdlePeriodsRequest(
        businessId: mapValueOfType<String>(json, r'businessId'),
        date: mapDateTime(json, r'date', r''),
      );
    }
    return null;
  }

  static List<BusinessIdlePeriodsRequest> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <BusinessIdlePeriodsRequest>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = BusinessIdlePeriodsRequest.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, BusinessIdlePeriodsRequest> mapFromJson(dynamic json) {
    final map = <String, BusinessIdlePeriodsRequest>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = BusinessIdlePeriodsRequest.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of BusinessIdlePeriodsRequest-objects as value to a dart map
  static Map<String, List<BusinessIdlePeriodsRequest>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<BusinessIdlePeriodsRequest>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = BusinessIdlePeriodsRequest.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
  };
}

