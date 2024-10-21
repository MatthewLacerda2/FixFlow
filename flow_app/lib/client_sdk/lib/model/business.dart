//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class Business {
  /// Returns a new [Business] instance.
  Business({
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
    this.createdDate,
    this.isActive,
    this.name,
    this.cnpj,
    this.businessWeek,
    this.services = const [],
    this.allowListedServicesOnly,
    this.openOnHolidays,
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

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  DateTime? createdDate;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  bool? isActive;

  /// The Name of the Business or Business owner
  String? name;

  /// CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
  String? cnpj;

  ///
  /// Please note: This property should have been non-nullable! Since the specification file
  /// does not include a default value (using the "default:" property), however, the generated
  /// source code must fall back to having a nullable type.
  /// Consider adding a "default:" property in the specification file to hide this note.
  ///
  BusinessWeek? businessWeek;

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

  @override
  bool operator ==(Object other) => identical(this, other) || other is Business &&
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
    other.createdDate == createdDate &&
    other.isActive == isActive &&
    other.name == name &&
    other.cnpj == cnpj &&
    other.businessWeek == businessWeek &&
    _deepEquality.equals(other.services, services) &&
    other.allowListedServicesOnly == allowListedServicesOnly &&
    other.openOnHolidays == openOnHolidays;

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
    (createdDate == null ? 0 : createdDate!.hashCode) +
    (isActive == null ? 0 : isActive!.hashCode) +
    (name == null ? 0 : name!.hashCode) +
    (cnpj == null ? 0 : cnpj!.hashCode) +
    (businessWeek == null ? 0 : businessWeek!.hashCode) +
    (services == null ? 0 : services!.hashCode) +
    (allowListedServicesOnly == null ? 0 : allowListedServicesOnly!.hashCode) +
    (openOnHolidays == null ? 0 : openOnHolidays!.hashCode);

  @override
  String toString() => 'Business[id=$id, userName=$userName, normalizedUserName=$normalizedUserName, email=$email, normalizedEmail=$normalizedEmail, emailConfirmed=$emailConfirmed, passwordHash=$passwordHash, securityStamp=$securityStamp, concurrencyStamp=$concurrencyStamp, phoneNumber=$phoneNumber, phoneNumberConfirmed=$phoneNumberConfirmed, twoFactorEnabled=$twoFactorEnabled, lockoutEnd=$lockoutEnd, lockoutEnabled=$lockoutEnabled, accessFailedCount=$accessFailedCount, createdDate=$createdDate, isActive=$isActive, name=$name, cnpj=$cnpj, businessWeek=$businessWeek, services=$services, allowListedServicesOnly=$allowListedServicesOnly, openOnHolidays=$openOnHolidays]';

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
    if (this.createdDate != null) {
      json[r'createdDate'] = this.createdDate!.toUtc().toIso8601String();
    } else {
      json[r'createdDate'] = null;
    }
    if (this.isActive != null) {
      json[r'isActive'] = this.isActive;
    } else {
      json[r'isActive'] = null;
    }
    if (this.name != null) {
      json[r'name'] = this.name;
    } else {
      json[r'name'] = null;
    }
    if (this.cnpj != null) {
      json[r'cnpj'] = this.cnpj;
    } else {
      json[r'cnpj'] = null;
    }
    if (this.businessWeek != null) {
      json[r'businessWeek'] = this.businessWeek;
    } else {
      json[r'businessWeek'] = null;
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
    return json;
  }

  /// Returns a new [Business] instance and imports its values from
  /// [value] if it's a [Map], null otherwise.
  // ignore: prefer_constructors_over_static_methods
  static Business? fromJson(dynamic value) {
    if (value is Map) {
      final json = value.cast<String, dynamic>();

      // Ensure that the map contains the required keys.
      // Note 1: the values aren't checked for validity beyond being non-null.
      // Note 2: this code is stripped in release mode!
      assert(() {
        requiredKeys.forEach((key) {
          assert(json.containsKey(key), 'Required key "Business[$key]" is missing from JSON.');
          assert(json[key] != null, 'Required key "Business[$key]" has a null value in JSON.');
        });
        return true;
      }());

      return Business(
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
        createdDate: mapDateTime(json, r'createdDate', r''),
        isActive: mapValueOfType<bool>(json, r'isActive'),
        name: mapValueOfType<String>(json, r'name'),
        cnpj: mapValueOfType<String>(json, r'cnpj'),
        businessWeek: BusinessWeek.fromJson(json[r'businessWeek']),
        services: json[r'services'] is Iterable
            ? (json[r'services'] as Iterable).cast<String>().toList(growable: false)
            : const [],
        allowListedServicesOnly: mapValueOfType<bool>(json, r'allowListedServicesOnly'),
        openOnHolidays: mapValueOfType<bool>(json, r'openOnHolidays'),
      );
    }
    return null;
  }

  static List<Business> listFromJson(dynamic json, {bool growable = false,}) {
    final result = <Business>[];
    if (json is List && json.isNotEmpty) {
      for (final row in json) {
        final value = Business.fromJson(row);
        if (value != null) {
          result.add(value);
        }
      }
    }
    return result.toList(growable: growable);
  }

  static Map<String, Business> mapFromJson(dynamic json) {
    final map = <String, Business>{};
    if (json is Map && json.isNotEmpty) {
      json = json.cast<String, dynamic>(); // ignore: parameter_assignments
      for (final entry in json.entries) {
        final value = Business.fromJson(entry.value);
        if (value != null) {
          map[entry.key] = value;
        }
      }
    }
    return map;
  }

  // maps a json object with a list of Business-objects as value to a dart map
  static Map<String, List<Business>> mapListFromJson(dynamic json, {bool growable = false,}) {
    final map = <String, List<Business>>{};
    if (json is Map && json.isNotEmpty) {
      // ignore: parameter_assignments
      json = json.cast<String, dynamic>();
      for (final entry in json.entries) {
        map[entry.key] = Business.listFromJson(entry.value, growable: growable,);
      }
    }
    return map;
  }

  /// The list of required keys that must be present in a JSON.
  static const requiredKeys = <String>{
  };
}

