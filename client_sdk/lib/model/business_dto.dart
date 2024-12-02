//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class BusinessDTO {
  /// Returns a new [BusinessDTO] instance.
  BusinessDTO({
    this.id,
    this.services = const [],
    this.allowListedServicesOnly,
    this.openOnHolidays,
    this.name,
    this.email,
    this.cnpj,
    this.phoneNumber,
  });

  String? id;

  List<String>? services;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  bool? allowListedServicesOnly;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  bool? openOnHolidays;

  String? name;

  String? email;

  String? cnpj;

  String? phoneNumber;

  @override
  bool operator ==(Object other) => identical(this, other) || other is BusinessDTO &&
    other.id == id &&
    _deepEquality.equals(other.services, services) &&
    other.allowListedServicesOnly == allowListedServicesOnly &&
    other.openOnHolidays == openOnHolidays &&
    other.name == name &&
    other.email == email &&
    other.cnpj == cnpj &&
    other.phoneNumber == phoneNumber;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (id == null ? 0 : id!.hashCode) +
    (services == null ? 0 : services!.hashCode) +
    (allowListedServicesOnly == null ? 0 : allowListedServicesOnly!.hashCode) +
    (openOnHolidays == null ? 0 : openOnHolidays!.hashCode) +
    (name == null ? 0 : name!.hashCode) +
    (email == null ? 0 : email!.hashCode) +
    (cnpj == null ? 0 : cnpj!.hashCode) +
    (phoneNumber == null ? 0 : phoneNumber!.hashCode);

  @override
  String toString() => 'BusinessDTO[id=$id, services=$services, allowListedServicesOnly=$allowListedServicesOnly, openOnHolidays=$openOnHolidays, name=$name, email=$email, cnpj=$cnpj, phoneNumber=$phoneNumber]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
    if (this.id != null) {
      json[r'id'] = this.id;
    } else {
      json[r'id'] = null;
    }
    if (this.services != null) {
      json[r'services'] = this.services;
    } else {
      json[r'services'] = null;
    }
    if (this.allowListedServicesOnly != null) {
      json[r'allowListedServicesOnly'] = this.allowListedServicesOnly;
    } else {
      json[r'allowListedServicesOnly'] = null;
    }
    if (this.openOnHolidays != null) {
      json[r'openOnHolidays'] = this.openOnHolidays;
    } else {
      json[r'openOnHolidays'] = null;
    }
    if (this.name != null) {
      json[r'name'] = this.name;
    } else {
      json[r'name'] = null;
    }
    if (this.email != null) {
      json[r'email'] = this.email;
    } else {
      json[r'email'] = null;
    }
    if (this.cnpj != null) {
      json[r'cnpj'] = this.cnpj;
    } else {
      json[r'cnpj'] = null;
    }
    if (this.phoneNumber != null) {
      json[r'phoneNumber'] = this.phoneNumber;
    } else {
      json[r'phoneNumber'] = null;
    }
    return json;
  }

  /// Returns a new [BusinessDTO] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static BusinessDTO? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "BusinessDTO[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "BusinessDTO[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return BusinessDTO(
        id: mapValueOfType<String>(json, r'id'),
        services: json[r'services'] is Iterable
            ? (json[r'services'] as Iterable).cast<String>().toList(growable: false)
            : const [],
        allowListedServicesOnly: mapValueOfType<bool>(json, r'allowListedServicesOnly'),
        openOnHolidays: mapValueOfType<bool>(json, r'openOnHolidays'),
        name: mapValueOfType<String>(json, r'name'),
        email: mapValueOfType<String>(json, r'email'),
        cnpj: mapValueOfType<String>(json, r'cnpj'),
        phoneNumber: mapValueOfType<String>(json, r'phoneNumber'),
      );
    }
    return null;
  }

  static List<BusinessDTO> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <BusinessDTO>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = BusinessDTO.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, BusinessDTO> mapFromJson(dynamic json) {
    final map = <String, BusinessDTO>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = BusinessDTO.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of BusinessDTO-objects as value to a dart map
  static Map<String, List<BusinessDTO>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<BusinessDTO>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = BusinessDTO.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
  };
}

