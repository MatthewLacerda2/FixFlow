//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class CreateAptLog {
  /// Returns a new [CreateAptLog] instance.
  CreateAptLog({
    required this.customerId,
    required this.businessId,
    this.scheduleId,
    this.dateTime,
    this.service,
    this.observation,
    this.price,
    this.whenShouldCustomerComeBack,
  });

  /// The Id of the Customer who took the Appointment
  String customerId;

  /// The Id of the Business who owns this Contact
  String businessId;

  /// The Id of the Schedule that precedes this Log, if any
  String? scheduleId;

  /// The DateTime when the Log was registered
  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  DateTime? dateTime;

  String? service;

  String? observation;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  double? price;

  /// The Date when we expect the Customer to schedule another appointment.  We are leaving as DateTime for simplicity but we only need the Date from this class
  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  DateTime? whenShouldCustomerComeBack;

  @override
  bool operator ==(Object other) => identical(this, other) || other is CreateAptLog &&
    other.customerId == customerId &&
    other.businessId == businessId &&
    other.scheduleId == scheduleId &&
    other.dateTime == dateTime &&
    other.service == service &&
    other.observation == observation &&
    other.price == price &&
    other.whenShouldCustomerComeBack == whenShouldCustomerComeBack;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (customerId.hashCode) +
    (businessId.hashCode) +
    (scheduleId == null ? 0 : scheduleId!.hashCode) +
    (dateTime == null ? 0 : dateTime!.hashCode) +
    (service == null ? 0 : service!.hashCode) +
    (observation == null ? 0 : observation!.hashCode) +
    (price == null ? 0 : price!.hashCode) +
    (whenShouldCustomerComeBack == null ? 0 : whenShouldCustomerComeBack!.hashCode);

  @override
  String toString() => 'CreateAptLog[customerId=$customerId, businessId=$businessId, scheduleId=$scheduleId, dateTime=$dateTime, service=$service, observation=$observation, price=$price, whenShouldCustomerComeBack=$whenShouldCustomerComeBack]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
      json[r'customerId'] = this.customerId;
      json[r'businessId'] = this.businessId;
    if (this.scheduleId != null) {
      json[r'scheduleId'] = this.scheduleId;
    } else {
      json[r'scheduleId'] = null;
    }
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
    if (this.observation != null) {
      json[r'observation'] = this.observation;
    } else {
      json[r'observation'] = null;
    }
    if (this.price != null) {
      json[r'price'] = this.price;
    } else {
      json[r'price'] = null;
    }
    if (this.whenShouldCustomerComeBack != null) {
      json[r'whenShouldCustomerComeBack'] = this.whenShouldCustomerComeBack!.toUtc().toIso8601String();
    } else {
      json[r'whenShouldCustomerComeBack'] = null;
    }
    return json;
  }

  /// Returns a new [CreateAptLog] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static CreateAptLog? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "CreateAptLog[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "CreateAptLog[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return CreateAptLog(
        customerId: mapValueOfType<String>(json, r'customerId')!,
        businessId: mapValueOfType<String>(json, r'businessId')!,
        scheduleId: mapValueOfType<String>(json, r'scheduleId'),
        dateTime: mapDateTime(json, r'dateTime', r''),
        service: mapValueOfType<String>(json, r'service'),
        observation: mapValueOfType<String>(json, r'observation'),
        price: mapValueOfType<double>(json, r'price'),
        whenShouldCustomerComeBack: mapDateTime(json, r'whenShouldCustomerComeBack', r''),
      );
    }
    return null;
  }

  static List<CreateAptLog> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <CreateAptLog>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = CreateAptLog.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, CreateAptLog> mapFromJson(dynamic json) {
    final map = <String, CreateAptLog>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = CreateAptLog.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of CreateAptLog-objects as value to a dart map
  static Map<String, List<CreateAptLog>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<CreateAptLog>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = CreateAptLog.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
    'customerId',
    'businessId',
  };
}

