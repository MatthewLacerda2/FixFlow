//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class AptLog {
  /// Returns a new [AptLog] instance.
  AptLog({
    required this.id,
    required this.customerId,
    this.customer,
    required this.businessId,
    this.scheduleId,
    this.dateTime,
    this.service,
    this.price,
    this.description,
  });

  String id;

  /// The Id of the Customer who took the Appointment
  String customerId;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  Customer? customer;

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

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  double? price;

  String? description;

  @override
  bool operator ==(Object other) => identical(this, other) || other is AptLog &&
    other.id == id &&
    other.customerId == customerId &&
    other.customer == customer &&
    other.businessId == businessId &&
    other.scheduleId == scheduleId &&
    other.dateTime == dateTime &&
    other.service == service &&
    other.price == price &&
    other.description == description;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (id.hashCode) +
    (customerId.hashCode) +
    (customer == null ? 0 : customer!.hashCode) +
    (businessId.hashCode) +
    (scheduleId == null ? 0 : scheduleId!.hashCode) +
    (dateTime == null ? 0 : dateTime!.hashCode) +
    (service == null ? 0 : service!.hashCode) +
    (price == null ? 0 : price!.hashCode) +
    (description == null ? 0 : description!.hashCode);

  @override
  String toString() => 'AptLog[id=$id, customerId=$customerId, customer=$customer, businessId=$businessId, scheduleId=$scheduleId, dateTime=$dateTime, service=$service, price=$price, description=$description]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
      json[r'id'] = this.id;
      json[r'customerId'] = this.customerId;
    if (this.customer != null) {
      json[r'customer'] = this.customer;
    } else {
      json[r'customer'] = null;
    }
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
    if (this.price != null) {
      json[r'price'] = this.price;
    } else {
      json[r'price'] = null;
    }
    if (this.description != null) {
      json[r'description'] = this.description;
    } else {
      json[r'description'] = null;
    }
    return json;
  }

  /// Returns a new [AptLog] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static AptLog? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "AptLog[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "AptLog[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return AptLog(
        id: mapValueOfType<String>(json, r'id')!,
        customerId: mapValueOfType<String>(json, r'customerId')!,
        customer: Customer.fromJson(json[r'customer']),
        businessId: mapValueOfType<String>(json, r'businessId')!,
        scheduleId: mapValueOfType<String>(json, r'scheduleId'),
        dateTime: mapDateTime(json, r'dateTime', r''),
        service: mapValueOfType<String>(json, r'service'),
        price: mapValueOfType<double>(json, r'price'),
        description: mapValueOfType<String>(json, r'description'),
      );
    }
    return null;
  }

  static List<AptLog> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <AptLog>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = AptLog.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, AptLog> mapFromJson(dynamic json) {
    final map = <String, AptLog>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = AptLog.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of AptLog-objects as value to a dart map
  static Map<String, List<AptLog>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<AptLog>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = AptLog.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
    'id',
    'customerId',
    'businessId',
  };
}

