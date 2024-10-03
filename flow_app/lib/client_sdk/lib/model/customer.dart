//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class Customer {
  /// Returns a new [Customer] instance.
  Customer({
    this.id,
    this.userName,
    this.normalizedUserName,
    this.email,
    this.normalizedEmail,
    this.emailConfirmed,
    this.passwordHash,
    this.securityStamp,
    this.concurrencyStamp,
    this.phoneNumber,
    this.phoneNumberConfirmed,
    this.twoFactorEnabled,
    this.lockoutEnd,
    this.lockoutEnabled,
    this.accessFailedCount,
    required this.businessId,
    required this.fullName,
    this.cpf,
    this.additionalNote,
  });

  String? id;

  String? userName;

  String? normalizedUserName;

  String? email;

  String? normalizedEmail;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  bool? emailConfirmed;

  String? passwordHash;

  String? securityStamp;

  String? concurrencyStamp;

  String? phoneNumber;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  bool? phoneNumberConfirmed;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  bool? twoFactorEnabled;

  DateTime? lockoutEnd;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  bool? lockoutEnabled;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  int? accessFailedCount;

  /// The business from which the Customer is a customer
  String businessId;

  String fullName;

  /// CPF. Must be on format XXX.XXX.XXX-XX
  String? cpf;

  String? additionalNote;

  @override
  bool operator ==(Object other) => identical(this, other) || other is Customer &&
    other.id == id &&
    other.userName == userName &&
    other.normalizedUserName == normalizedUserName &&
    other.email == email &&
    other.normalizedEmail == normalizedEmail &&
    other.emailConfirmed == emailConfirmed &&
    other.passwordHash == passwordHash &&
    other.securityStamp == securityStamp &&
    other.concurrencyStamp == concurrencyStamp &&
    other.phoneNumber == phoneNumber &&
    other.phoneNumberConfirmed == phoneNumberConfirmed &&
    other.twoFactorEnabled == twoFactorEnabled &&
    other.lockoutEnd == lockoutEnd &&
    other.lockoutEnabled == lockoutEnabled &&
    other.accessFailedCount == accessFailedCount &&
    other.businessId == businessId &&
    other.fullName == fullName &&
    other.cpf == cpf &&
    other.additionalNote == additionalNote;

  @override
  int get hashCode =>
    // ignore: unnecessary_parenthesis
    (id == null ? 0 : id!.hashCode) +
    (userName == null ? 0 : userName!.hashCode) +
    (normalizedUserName == null ? 0 : normalizedUserName!.hashCode) +
    (email == null ? 0 : email!.hashCode) +
    (normalizedEmail == null ? 0 : normalizedEmail!.hashCode) +
    (emailConfirmed == null ? 0 : emailConfirmed!.hashCode) +
    (passwordHash == null ? 0 : passwordHash!.hashCode) +
    (securityStamp == null ? 0 : securityStamp!.hashCode) +
    (concurrencyStamp == null ? 0 : concurrencyStamp!.hashCode) +
    (phoneNumber == null ? 0 : phoneNumber!.hashCode) +
    (phoneNumberConfirmed == null ? 0 : phoneNumberConfirmed!.hashCode) +
    (twoFactorEnabled == null ? 0 : twoFactorEnabled!.hashCode) +
    (lockoutEnd == null ? 0 : lockoutEnd!.hashCode) +
    (lockoutEnabled == null ? 0 : lockoutEnabled!.hashCode) +
    (accessFailedCount == null ? 0 : accessFailedCount!.hashCode) +
    (businessId.hashCode) +
    (fullName.hashCode) +
    (cpf == null ? 0 : cpf!.hashCode) +
    (additionalNote == null ? 0 : additionalNote!.hashCode);

  @override
  String toString() => 'Customer[id=$id, userName=$userName, normalizedUserName=$normalizedUserName, email=$email, normalizedEmail=$normalizedEmail, emailConfirmed=$emailConfirmed, passwordHash=$passwordHash, securityStamp=$securityStamp, concurrencyStamp=$concurrencyStamp, phoneNumber=$phoneNumber, phoneNumberConfirmed=$phoneNumberConfirmed, twoFactorEnabled=$twoFactorEnabled, lockoutEnd=$lockoutEnd, lockoutEnabled=$lockoutEnabled, accessFailedCount=$accessFailedCount, businessId=$businessId, fullName=$fullName, cpf=$cpf, additionalNote=$additionalNote]';

  Map<String, dynamic> toJson() {
    final json = <String, dynamic>{};
    if (this.id != null) {
      json[r'id'] = this.id;
    } else {
      json[r'id'] = null;
    }
    if (this.userName != null) {
      json[r'userName'] = this.userName;
    } else {
      json[r'userName'] = null;
    }
    if (this.normalizedUserName != null) {
      json[r'normalizedUserName'] = this.normalizedUserName;
    } else {
      json[r'normalizedUserName'] = null;
    }
    if (this.email != null) {
      json[r'email'] = this.email;
    } else {
      json[r'email'] = null;
    }
    if (this.normalizedEmail != null) {
      json[r'normalizedEmail'] = this.normalizedEmail;
    } else {
      json[r'normalizedEmail'] = null;
    }
    if (this.emailConfirmed != null) {
      json[r'emailConfirmed'] = this.emailConfirmed;
    } else {
      json[r'emailConfirmed'] = null;
    }
    if (this.passwordHash != null) {
      json[r'passwordHash'] = this.passwordHash;
    } else {
      json[r'passwordHash'] = null;
    }
    if (this.securityStamp != null) {
      json[r'securityStamp'] = this.securityStamp;
    } else {
      json[r'securityStamp'] = null;
    }
    if (this.concurrencyStamp != null) {
      json[r'concurrencyStamp'] = this.concurrencyStamp;
    } else {
      json[r'concurrencyStamp'] = null;
    }
    if (this.phoneNumber != null) {
      json[r'phoneNumber'] = this.phoneNumber;
    } else {
      json[r'phoneNumber'] = null;
    }
    if (this.phoneNumberConfirmed != null) {
      json[r'phoneNumberConfirmed'] = this.phoneNumberConfirmed;
    } else {
      json[r'phoneNumberConfirmed'] = null;
    }
    if (this.twoFactorEnabled != null) {
      json[r'twoFactorEnabled'] = this.twoFactorEnabled;
    } else {
      json[r'twoFactorEnabled'] = null;
    }
    if (this.lockoutEnd != null) {
      json[r'lockoutEnd'] = this.lockoutEnd!.toUtc().toIso8601String();
    } else {
      json[r'lockoutEnd'] = null;
    }
    if (this.lockoutEnabled != null) {
      json[r'lockoutEnabled'] = this.lockoutEnabled;
    } else {
      json[r'lockoutEnabled'] = null;
    }
    if (this.accessFailedCount != null) {
      json[r'accessFailedCount'] = this.accessFailedCount;
    } else {
      json[r'accessFailedCount'] = null;
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

  /// Returns a new [Customer] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static Customer? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "Customer[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "Customer[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return Customer(
        id: mapValueOfType<String>(json, r'id'),
        userName: mapValueOfType<String>(json, r'userName'),
        normalizedUserName: mapValueOfType<String>(json, r'normalizedUserName'),
        email: mapValueOfType<String>(json, r'email'),
        normalizedEmail: mapValueOfType<String>(json, r'normalizedEmail'),
        emailConfirmed: mapValueOfType<bool>(json, r'emailConfirmed'),
        passwordHash: mapValueOfType<String>(json, r'passwordHash'),
        securityStamp: mapValueOfType<String>(json, r'securityStamp'),
        concurrencyStamp: mapValueOfType<String>(json, r'concurrencyStamp'),
        phoneNumber: mapValueOfType<String>(json, r'phoneNumber'),
        phoneNumberConfirmed: mapValueOfType<bool>(json, r'phoneNumberConfirmed'),
        twoFactorEnabled: mapValueOfType<bool>(json, r'twoFactorEnabled'),
        lockoutEnd: mapDateTime(json, r'lockoutEnd', r''),
        lockoutEnabled: mapValueOfType<bool>(json, r'lockoutEnabled'),
        accessFailedCount: mapValueOfType<int>(json, r'accessFailedCount'),
        businessId: mapValueOfType<String>(json, r'businessId')!,
        fullName: mapValueOfType<String>(json, r'fullName')!,
        cpf: mapValueOfType<String>(json, r'cpf'),
        additionalNote: mapValueOfType<String>(json, r'additionalNote'),
      );
    }
    return null;
  }

  static List<Customer> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <Customer>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = Customer.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, Customer> mapFromJson(dynamic json) {
    final map = <String, Customer>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = Customer.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of Customer-objects as value to a dart map
  static Map<String, List<Customer>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<Customer>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = Customer.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
    'businessId',
    'fullName',
  };
}

