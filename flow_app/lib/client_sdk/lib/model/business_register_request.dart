//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class BusinessRegisterRequest {
  /// Returns a new [BusinessRegisterRequest] instance.
  BusinessRegisterRequest({
    required this.name,
    this.email,
    this.cnpj,
    required this.phoneNumber,
    this.password,
    this.confirmPassword,
  });

  /// The Name of the Business or Business owner
  String name;

  String? email;

  /// CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
  String? cnpj;

  /// Phone Number. Must contain only numbers and/or a '+'
  String phoneNumber;

  String? password;

  /// Must be identical to 'password'
  String? confirmPassword;

  @override
  bool operator ==(Object other) => identical(this, other) || other is BusinessRegisterRequest &&
    other.name == name &&
    other.email == email &&
    other.cnpj == cnpj &&
    other.phoneNumber == phoneNumber &&
    other.password == password &&
    other.confirmPassword == confirmPassword;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (name.hashCode) +
    (email == null ? 0 : email!.hashCode) +
    (cnpj == null ? 0 : cnpj!.hashCode) +
    (phoneNumber.hashCode) +
    (password == null ? 0 : password!.hashCode) +
    (confirmPassword == null ? 0 : confirmPassword!.hashCode);

  @override
  String toString() => 'BusinessRegisterRequest[name=$name, email=$email, cnpj=$cnpj, phoneNumber=$phoneNumber, password=$password, confirmPassword=$confirmPassword]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
      json[r'name'] = this.name;
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
      json[r'phoneNumber'] = this.phoneNumber;
    if (this.password != null) {
      json[r'password'] = this.password;
    } else {
      json[r'password'] = null;
    }
    if (this.confirmPassword != null) {
      json[r'confirmPassword'] = this.confirmPassword;
    } else {
      json[r'confirmPassword'] = null;
    }
    return json;
  }

  /// Returns a new [BusinessRegisterRequest] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static BusinessRegisterRequest? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "BusinessRegisterRequest[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "BusinessRegisterRequest[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return BusinessRegisterRequest(
        name: mapValueOfType<String>(json, r'name')!,
        email: mapValueOfType<String>(json, r'email'),
        cnpj: mapValueOfType<String>(json, r'cnpj'),
        phoneNumber: mapValueOfType<String>(json, r'phoneNumber')!,
        password: mapValueOfType<String>(json, r'password'),
        confirmPassword: mapValueOfType<String>(json, r'confirmPassword'),
      );
    }
    return null;
  }

  static List<BusinessRegisterRequest> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <BusinessRegisterRequest>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = BusinessRegisterRequest.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, BusinessRegisterRequest> mapFromJson(dynamic json) {
    final map = <String, BusinessRegisterRequest>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = BusinessRegisterRequest.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of BusinessRegisterRequest-objects as value to a dart map
  static Map<String, List<BusinessRegisterRequest>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<BusinessRegisterRequest>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = BusinessRegisterRequest.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
    'name',
    'phoneNumber',
  };
}

