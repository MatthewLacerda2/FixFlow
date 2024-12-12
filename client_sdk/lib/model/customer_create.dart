//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class CustomerCreate {
  /// Returns a new [CustomerCreate] instance.
  CustomerCreate({
    required this.phoneNumber,
    this.email,
    required this.businessId,
    required this.fullName,
    this.cpf,
    this.additionalNote,
  });

  /// Phone Number. Must contain only numbers
  String phoneNumber;

  String? email;

  /// The business from which the Customer is a customer
  String businessId;

  String fullName;

  /// CPF. Must be on format XXX.XXX.XXX-XX
  String? cpf;

  /// Special information about the Customer, if applicable
  String? additionalNote;

  @override
  bool operator ==(Object other) => identical(this, other) || other is CustomerCreate &&
    other.phoneNumber == phoneNumber &&
    other.email == email &&
    other.businessId == businessId &&
    other.fullName == fullName &&
    other.cpf == cpf &&
    other.additionalNote == additionalNote;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (phoneNumber.hashCode) +
    (email == null ? 0 : email!.hashCode) +
    (businessId.hashCode) +
    (fullName.hashCode) +
    (cpf == null ? 0 : cpf!.hashCode) +
    (additionalNote == null ? 0 : additionalNote!.hashCode);

  @override
  String toString() => 'CustomerCreate[phoneNumber=$phoneNumber, email=$email, businessId=$businessId, fullName=$fullName, cpf=$cpf, additionalNote=$additionalNote]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
      json[r'phoneNumber'] = this.phoneNumber;
    if (this.email != null) {
      json[r'email'] = this.email;
    } else {
      json[r'email'] = null;
    }
      json[r'businessId'] = this.businessId;
      json[r'fullName'] = this.fullName;
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
    return json;
  }

  /// Returns a new [CustomerCreate] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static CustomerCreate? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "CustomerCreate[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "CustomerCreate[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return CustomerCreate(
        phoneNumber: mapValueOfType<String>(json, r'phoneNumber')!,
        email: mapValueOfType<String>(json, r'email'),
        businessId: mapValueOfType<String>(json, r'businessId')!,
        fullName: mapValueOfType<String>(json, r'fullName')!,
        cpf: mapValueOfType<String>(json, r'cpf'),
        additionalNote: mapValueOfType<String>(json, r'additionalNote'),
      );
    }
    return null;
  }

  static List<CustomerCreate> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <CustomerCreate>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = CustomerCreate.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, CustomerCreate> mapFromJson(dynamic json) {
    final map = <String, CustomerCreate>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = CustomerCreate.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of CustomerCreate-objects as value to a dart map
  static Map<String, List<CustomerCreate>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<CustomerCreate>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = CustomerCreate.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
    'phoneNumber',
    'businessId',
    'fullName',
  };
}

