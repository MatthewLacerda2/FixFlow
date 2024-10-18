//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

library openapi.api;

import 'dart:async';
import 'dart:convert';
import 'dart:io';

import 'package:collection/collection.dart';
import 'package:http/http.dart';
import 'package:intl/intl.dart';
import 'package:meta/meta.dart';

part 'api_client.dart';
part 'api_helper.dart';
part 'api_exception.dart';
part 'auth/authentication.dart';
part 'auth/api_key_auth.dart';
part 'auth/oauth.dart';
part 'auth/http_basic_auth.dart';
part 'auth/http_bearer_auth.dart';

part 'api/accounts_api.dart';
part 'api/apt_log_api.dart';
part 'api/apt_schedule_api.dart';
part 'api/business_api.dart';
part 'api/business_calendar_day_api.dart';
part 'api/customer_api.dart';
part 'api/idle_period_api.dart';
part 'api/otp_api.dart';

part 'model/apt_log.dart';
part 'model/apt_schedule.dart';
part 'model/business.dart';
part 'model/business_calendar_day.dart';
part 'model/business_day.dart';
part 'model/business_idle_periods_request.dart';
part 'model/business_register_request.dart';
part 'model/create_apt_log.dart';
part 'model/create_apt_schedule.dart';
part 'model/customer.dart';
part 'model/customer_create.dart';
part 'model/customer_dto.dart';
part 'model/customer_record.dart';
part 'model/flow_login_request.dart';
part 'model/idle_period.dart';
part 'model/problem_details.dart';
part 'model/update_apt_log.dart';


/// An [ApiClient] instance that uses the default values obtained from
/// the OpenAPI specification file.
var defaultApiClient = ApiClient();

const _delimiters = {'csv': ',', 'ssv': ' ', 'tsv': '\t', 'pipes': '|'};
const _dateEpochMarker = 'epoch';
const _deepEquality = DeepCollectionEquality();
final _dateFormatter = DateFormat('yyyy-MM-dd');
final _regList = RegExp(r'^List<(.*)>$');
final _regSet = RegExp(r'^Set<(.*)>$');
final _regMap = RegExp(r'^Map<String,(.*)>$');

bool _isEpochMarker(String? pattern) => pattern == _dateEpochMarker || pattern == '/$_dateEpochMarker/';