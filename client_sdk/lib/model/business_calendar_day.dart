//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class BusinessCalendarDay {
  /// Returns a new [BusinessCalendarDay] instance.
  BusinessCalendarDay({
    this.date,
    this.idlePeriods = const [],
    this.holiday = const [],
    this.schedules = const [],
    this.logs = const [],
  });

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  DateTime? date;

  List<IdlePeriod>? idlePeriods;

  List<String>? holiday;

  List<AptSchedule>? schedules;

  List<AptLog>? logs;

  @override
  bool operator ==(Object other) => identical(this, other) || other is BusinessCalendarDay &&
    other.date == date &&
    _deepEquality.equals(other.idlePeriods, idlePeriods) &&
    _deepEquality.equals(other.holiday, holiday) &&
    _deepEquality.equals(other.schedules, schedules) &&
    _deepEquality.equals(other.logs, logs);

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (date == null ? 0 : date!.hashCode) +
    (idlePeriods == null ? 0 : idlePeriods!.hashCode) +
    (holiday == null ? 0 : holiday!.hashCode) +
    (schedules == null ? 0 : schedules!.hashCode) +
    (logs == null ? 0 : logs!.hashCode);

  @override
  String toString() => 'BusinessCalendarDay[date=$date, idlePeriods=$idlePeriods, holiday=$holiday, schedules=$schedules, logs=$logs]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
    if (this.date != null) {
      json[r'date'] = this.date!.toUtc().toIso8601String();
    } else {
      json[r'date'] = null;
    }
    if (this.idlePeriods != null) {
      json[r'idlePeriods'] = this.idlePeriods;
    } else {
      json[r'idlePeriods'] = null;
    }
    if (this.holiday != null) {
      json[r'holiday'] = this.holiday;
    } else {
      json[r'holiday'] = null;
    }
    if (this.schedules != null) {
      json[r'schedules'] = this.schedules;
    } else {
      json[r'schedules'] = null;
    }
    if (this.logs != null) {
      json[r'logs'] = this.logs;
    } else {
      json[r'logs'] = null;
    }
    return json;
  }

  /// Returns a new [BusinessCalendarDay] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static BusinessCalendarDay? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "BusinessCalendarDay[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "BusinessCalendarDay[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return BusinessCalendarDay(
        date: mapDateTime(json, r'date', r''),
        idlePeriods: IdlePeriod.listFromJson(json[r'idlePeriods']),
        holiday: json[r'holiday'] is Iterable
            ? (json[r'holiday'] as Iterable).cast<String>().toList(growable: false)
            : const [],
        schedules: AptSchedule.listFromJson(json[r'schedules']),
        logs: AptLog.listFromJson(json[r'logs']),
      );
    }
    return null;
  }

  static List<BusinessCalendarDay> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <BusinessCalendarDay>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = BusinessCalendarDay.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, BusinessCalendarDay> mapFromJson(dynamic json) {
    final map = <String, BusinessCalendarDay>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = BusinessCalendarDay.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of BusinessCalendarDay-objects as value to a dart map
  static Map<String, List<BusinessCalendarDay>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<BusinessCalendarDay>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = BusinessCalendarDay.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
  };
}

