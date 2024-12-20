//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class Subscription {
  /// Returns a new [Subscription] instance.
  Subscription({
    this.id,
    this.businessId,
    this.businessName,
    this.businessCNPJ,
    this.dateTime,
    this.price,
    this.payed,
    this.additionalNote,
    this.timeSpentDeactivated,
  });

  String? id;

  String? businessId;

  /// The name of the Business or Business' owner.
  String? businessName;

  String? businessCNPJ;

  /// The date from when the service started being used
  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  DateTime? dateTime;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  int? price;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  bool? payed;

  /// Special information about that month's payment
  String? additionalNote;

  /// When we deactivate the account, we store the time the user spent, so if he comes back he'll pick up the time left
  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  String? timeSpentDeactivated;

  @override
  bool operator ==(Object other) => identical(this, other) || other is Subscription &&
    other.id == id &&
    other.businessId == businessId &&
    other.businessName == businessName &&
    other.businessCNPJ == businessCNPJ &&
    other.dateTime == dateTime &&
    other.price == price &&
    other.payed == payed &&
    other.additionalNote == additionalNote &&
    other.timeSpentDeactivated == timeSpentDeactivated;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (id == null ? 0 : id!.hashCode) +
    (businessId == null ? 0 : businessId!.hashCode) +
    (businessName == null ? 0 : businessName!.hashCode) +
    (businessCNPJ == null ? 0 : businessCNPJ!.hashCode) +
    (dateTime == null ? 0 : dateTime!.hashCode) +
    (price == null ? 0 : price!.hashCode) +
    (payed == null ? 0 : payed!.hashCode) +
    (additionalNote == null ? 0 : additionalNote!.hashCode) +
    (timeSpentDeactivated == null ? 0 : timeSpentDeactivated!.hashCode);

  @override
  String toString() => 'Subscription[id=$id, businessId=$businessId, businessName=$businessName, businessCNPJ=$businessCNPJ, dateTime=$dateTime, price=$price, payed=$payed, additionalNote=$additionalNote, timeSpentDeactivated=$timeSpentDeactivated]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
    if (this.id != null) {
      json[r'id'] = this.id;
    } else {
      json[r'id'] = null;
    }
    if (this.businessId != null) {
      json[r'businessId'] = this.businessId;
    } else {
      json[r'businessId'] = null;
    }
    if (this.businessName != null) {
      json[r'businessName'] = this.businessName;
    } else {
      json[r'businessName'] = null;
    }
    if (this.businessCNPJ != null) {
      json[r'businessCNPJ'] = this.businessCNPJ;
    } else {
      json[r'businessCNPJ'] = null;
    }
    if (this.dateTime != null) {
      json[r'dateTime'] = this.dateTime!.toUtc().toIso8601String();
    } else {
      json[r'dateTime'] = null;
    }
    if (this.price != null) {
      json[r'price'] = this.price;
    } else {
      json[r'price'] = null;
    }
    if (this.payed != null) {
      json[r'payed'] = this.payed;
    } else {
      json[r'payed'] = null;
    }
    if (this.additionalNote != null) {
      json[r'additionalNote'] = this.additionalNote;
    } else {
      json[r'additionalNote'] = null;
    }
    if (this.timeSpentDeactivated != null) {
      json[r'timeSpentDeactivated'] = this.timeSpentDeactivated;
    } else {
      json[r'timeSpentDeactivated'] = null;
    }
    return json;
  }

  /// Returns a new [Subscription] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static Subscription? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "Subscription[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "Subscription[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return Subscription(
        id: mapValueOfType<String>(json, r'id'),
        businessId: mapValueOfType<String>(json, r'businessId'),
        businessName: mapValueOfType<String>(json, r'businessName'),
        businessCNPJ: mapValueOfType<String>(json, r'businessCNPJ'),
        dateTime: mapDateTime(json, r'dateTime', r''),
        price: mapValueOfType<int>(json, r'price'),
        payed: mapValueOfType<bool>(json, r'payed'),
        additionalNote: mapValueOfType<String>(json, r'additionalNote'),
        timeSpentDeactivated: mapValueOfType<String>(json, r'timeSpentDeactivated'),
      );
    }
    return null;
  }

  static List<Subscription> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <Subscription>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = Subscription.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, Subscription> mapFromJson(dynamic json) {
    final map = <String, Subscription>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = Subscription.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of Subscription-objects as value to a dart map
  static Map<String, List<Subscription>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<Subscription>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = Subscription.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
  };
}

