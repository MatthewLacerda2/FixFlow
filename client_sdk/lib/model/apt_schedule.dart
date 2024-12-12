//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class AptSchedule {
  /// Returns a new [AptSchedule] instance.
  AptSchedule({
    required this.id,
    required this.customerId,
    this.customer,
    required this.businessId,
    this.wasContacted,
    this.dateTime,
    this.service,
    this.description,
    this.price,
  });

  String id;

  String customerId;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  Customer? customer;

  String businessId;

  /// Was the Customer contacted to make this Schedule?
  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  bool? wasContacted;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  DateTime? dateTime;

  String? service;

  String? description;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  int? price;

  @override
  bool operator ==(Object other) => identical(this, other) || other is AptSchedule &&
    other.id == id &&
    other.customerId == customerId &&
    other.customer == customer &&
    other.businessId == businessId &&
    other.wasContacted == wasContacted &&
    other.dateTime == dateTime &&
    other.service == service &&
    other.description == description &&
    other.price == price;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (id.hashCode) +
    (customerId.hashCode) +
    (customer == null ? 0 : customer!.hashCode) +
    (businessId.hashCode) +
    (wasContacted == null ? 0 : wasContacted!.hashCode) +
    (dateTime == null ? 0 : dateTime!.hashCode) +
    (service == null ? 0 : service!.hashCode) +
    (description == null ? 0 : description!.hashCode) +
    (price == null ? 0 : price!.hashCode);

  @override
  String toString() => 'AptSchedule[id=$id, customerId=$customerId, customer=$customer, businessId=$businessId, wasContacted=$wasContacted, dateTime=$dateTime, service=$service, description=$description, price=$price]';

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
    if (this.wasContacted != null) {
      json[r'wasContacted'] = this.wasContacted;
    } else {
      json[r'wasContacted'] = null;
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
    if (this.description != null) {
      json[r'description'] = this.description;
    } else {
      json[r'description'] = null;
    }
    if (this.price != null) {
      json[r'price'] = this.price;
    } else {
      json[r'price'] = null;
    }
    return json;
  }

  /// Returns a new [AptSchedule] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static AptSchedule? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "AptSchedule[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "AptSchedule[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return AptSchedule(
        id: mapValueOfType<String>(json, r'id')!,
        customerId: mapValueOfType<String>(json, r'customerId')!,
        customer: Customer.fromJson(json[r'customer']),
        businessId: mapValueOfType<String>(json, r'businessId')!,
        wasContacted: mapValueOfType<bool>(json, r'wasContacted'),
        dateTime: mapDateTime(json, r'dateTime', r''),
        service: mapValueOfType<String>(json, r'service'),
        description: mapValueOfType<String>(json, r'description'),
        price: mapValueOfType<int>(json, r'price'),
      );
    }
    return null;
  }

  static List<AptSchedule> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <AptSchedule>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = AptSchedule.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, AptSchedule> mapFromJson(dynamic json) {
    final map = <String, AptSchedule>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = AptSchedule.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of AptSchedule-objects as value to a dart map
  static Map<String, List<AptSchedule>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<AptSchedule>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = AptSchedule.listFromJson(entry.value, growable: growable,);
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

