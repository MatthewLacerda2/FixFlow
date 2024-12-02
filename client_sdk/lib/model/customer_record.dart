//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class CustomerRecord {
  /// Returns a new [CustomerRecord] instance.
  CustomerRecord({
    required this.fullName,
    required this.phoneNumber,
    this.email,
    required this.cpf,
    this.additionalNote,
    this.firstLog,
    this.lastLog,
    this.logs = const [],
    this.avgTimeBetweenSchedules,
  });

  String fullName;

  /// Phone Number. Must contain only numbers
  String phoneNumber;

  String? email;

  String? cpf;

  /// Special information about the Customer, if applicable
  String? additionalNote;

  DateTime? firstLog;

  DateTime? lastLog;

  List<AptLog>? logs;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  int? avgTimeBetweenSchedules;

  @override
  bool operator ==(Object other) => identical(this, other) || other is CustomerRecord &&
    other.fullName == fullName &&
    other.phoneNumber == phoneNumber &&
    other.email == email &&
    other.cpf == cpf &&
    other.additionalNote == additionalNote &&
    other.firstLog == firstLog &&
    other.lastLog == lastLog &&
    _deepEquality.equals(other.logs, logs) &&
    other.avgTimeBetweenSchedules == avgTimeBetweenSchedules;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (fullName.hashCode) +
    (phoneNumber.hashCode) +
    (email == null ? 0 : email!.hashCode) +
    (cpf == null ? 0 : cpf!.hashCode) +
    (additionalNote == null ? 0 : additionalNote!.hashCode) +
    (firstLog == null ? 0 : firstLog!.hashCode) +
    (lastLog == null ? 0 : lastLog!.hashCode) +
    (logs == null ? 0 : logs!.hashCode) +
    (avgTimeBetweenSchedules == null ? 0 : avgTimeBetweenSchedules!.hashCode);

  @override
  String toString() => 'CustomerRecord[fullName=$fullName, phoneNumber=$phoneNumber, email=$email, cpf=$cpf, additionalNote=$additionalNote, firstLog=$firstLog, lastLog=$lastLog, logs=$logs, avgTimeBetweenSchedules=$avgTimeBetweenSchedules]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
      json[r'fullName'] = this.fullName;
      json[r'phoneNumber'] = this.phoneNumber;
    if (this.email != null) {
      json[r'email'] = this.email;
    } else {
      json[r'email'] = null;
    }
    if (this.cpf != null) {
      json[r'cpf'] = this.cpf;
    } else {
      json[r'cpf'] = null;
    }
    if (this.additionalNote != null) {
      json[r'additionalNote'] = this.additionalNote;
    } else {
      json[r'additionalNote'] = null;
    }
    if (this.firstLog != null) {
      json[r'firstLog'] = this.firstLog!.toUtc().toIso8601String();
    } else {
      json[r'firstLog'] = null;
    }
    if (this.lastLog != null) {
      json[r'lastLog'] = this.lastLog!.toUtc().toIso8601String();
    } else {
      json[r'lastLog'] = null;
    }
    if (this.logs != null) {
      json[r'logs'] = this.logs;
    } else {
      json[r'logs'] = null;
    }
    if (this.avgTimeBetweenSchedules != null) {
      json[r'avgTimeBetweenSchedules'] = this.avgTimeBetweenSchedules;
    } else {
      json[r'avgTimeBetweenSchedules'] = null;
    }
    return json;
  }

  /// Returns a new [CustomerRecord] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static CustomerRecord? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "CustomerRecord[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "CustomerRecord[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return CustomerRecord(
        fullName: mapValueOfType<String>(json, r'fullName')!,
        phoneNumber: mapValueOfType<String>(json, r'phoneNumber')!,
        email: mapValueOfType<String>(json, r'email'),
        cpf: mapValueOfType<String>(json, r'cpf'),
        additionalNote: mapValueOfType<String>(json, r'additionalNote'),
        firstLog: mapDateTime(json, r'firstLog', r''),
        lastLog: mapDateTime(json, r'lastLog', r''),
        logs: AptLog.listFromJson(json[r'logs']),
        avgTimeBetweenSchedules: mapValueOfType<int>(json, r'avgTimeBetweenSchedules'),
      );
    }
    return null;
  }

  static List<CustomerRecord> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <CustomerRecord>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = CustomerRecord.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, CustomerRecord> mapFromJson(dynamic json) {
    final map = <String, CustomerRecord>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = CustomerRecord.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of CustomerRecord-objects as value to a dart map
  static Map<String, List<CustomerRecord>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<CustomerRecord>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = CustomerRecord.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
    'fullName',
    'phoneNumber',
    'cpf',
  };
}

