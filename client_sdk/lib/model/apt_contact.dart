//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class AptContact {
  /// Returns a new [AptContact] instance.
  AptContact({
    required this.id,
    required this.customerId,
    this.customer,
    required this.businessId,
    required this.aptLogId,
    this.aptLog,
    this.dateTime,
  });

  String id;

  /// The Id of the Customer to Contact
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

  /// The Id of the Log that precedes this Contact
  String aptLogId;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  AptLog? aptLog;

  /// The Date to Contact the Customer  The Time is used because, chances are, there is a better Time of the day to contact the Customer
  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  DateTime? dateTime;

  @override
  bool operator ==(Object other) => identical(this, other) || other is AptContact &&
    other.id == id &&
    other.customerId == customerId &&
    other.customer == customer &&
    other.businessId == businessId &&
    other.aptLogId == aptLogId &&
    other.aptLog == aptLog &&
    other.dateTime == dateTime;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (id.hashCode) +
    (customerId.hashCode) +
    (customer == null ? 0 : customer!.hashCode) +
    (businessId.hashCode) +
    (aptLogId.hashCode) +
    (aptLog == null ? 0 : aptLog!.hashCode) +
    (dateTime == null ? 0 : dateTime!.hashCode);

  @override
  String toString() => 'AptContact[id=$id, customerId=$customerId, customer=$customer, businessId=$businessId, aptLogId=$aptLogId, aptLog=$aptLog, dateTime=$dateTime]';

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
      json[r'aptLogId'] = this.aptLogId;
    if (this.aptLog != null) {
      json[r'aptLog'] = this.aptLog;
    } else {
      json[r'aptLog'] = null;
    }
    if (this.dateTime != null) {
      json[r'dateTime'] = this.dateTime!.toUtc().toIso8601String();
    } else {
      json[r'dateTime'] = null;
    }
    return json;
  }

  /// Returns a new [AptContact] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static AptContact? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "AptContact[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "AptContact[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return AptContact(
        id: mapValueOfType<String>(json, r'id')!,
        customerId: mapValueOfType<String>(json, r'customerId')!,
        customer: Customer.fromJson(json[r'customer']),
        businessId: mapValueOfType<String>(json, r'businessId')!,
        aptLogId: mapValueOfType<String>(json, r'aptLogId')!,
        aptLog: AptLog.fromJson(json[r'aptLog']),
        dateTime: mapDateTime(json, r'dateTime', r''),
      );
    }
    return null;
  }

  static List<AptContact> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <AptContact>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = AptContact.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, AptContact> mapFromJson(dynamic json) {
    final map = <String, AptContact>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = AptContact.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of AptContact-objects as value to a dart map
  static Map<String, List<AptContact>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<AptContact>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = AptContact.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
    'id',
    'customerId',
    'businessId',
    'aptLogId',
  };
}

