//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;


class SubscriptionApi {
  SubscriptionApi([ApiClient? apiClient]) : apiClient = apiClient ?? defaultApiClient;

  final ApiClient apiClient;

  /// Gets Idle Periods owned by the Company that start and end within a given time-period
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [int] startMonth:
  ///
  /// * [int] startYear:
  ///
  /// * [int] endMonth:
  ///
  /// * [int] endYear:
  Future<Response> apiV1SubscriptionGetWithHttpInfo({ int? startMonth, int? startYear, int? endMonth, int? endYear, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/Subscription';

    // ignore: prefer_final_locals
    Object? postBody;

    final queryParams = <QueryParam>[];
    final headerParams = <String, String>{};
    final formParams = <String, String>{};

    if (startMonth != null) {
      queryParams.addAll(_queryParams('', 'startMonth', startMonth));
    }
    if (startYear != null) {
      queryParams.addAll(_queryParams('', 'startYear', startYear));
    }
    if (endMonth != null) {
      queryParams.addAll(_queryParams('', 'endMonth', endMonth));
    }
    if (endYear != null) {
      queryParams.addAll(_queryParams('', 'endYear', endYear));
    }

    const contentTypes = <String>[];


    return apiClient.invokeAPI(
      path,
      'GET',
      queryParams,
      postBody,
      headerParams,
      formParams,
      contentTypes.isEmpty ? null : contentTypes.first,
    );
  }

  /// Gets Idle Periods owned by the Company that start and end within a given time-period
  ///
  /// Parameters:
  ///
  /// * [int] startMonth:
  ///
  /// * [int] startYear:
  ///
  /// * [int] endMonth:
  ///
  /// * [int] endYear:
  Future<List<Subscription>?> apiV1SubscriptionGet({ int? startMonth, int? startYear, int? endMonth, int? endYear, }) async {
    final response = await apiV1SubscriptionGetWithHttpInfo( startMonth: startMonth, startYear: startYear, endMonth: endMonth, endYear: endYear, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty && response.statusCode != HttpStatus.noContent) {
      final responseBody = await _decodeBodyBytes(response);
      return (await apiClient.deserializeAsync(responseBody, 'List<Subscription>') as List)
        .cast<Subscription>()
        .toList(growable: false);

    }
    return null;
  }

  /// Deletes an Idle Period
  ///
  /// Note: This method returns the HTTP [Response].
  Future<Response> apiV1SubscriptionUnpayedGetWithHttpInfo() async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/Subscription/unpayed';

    // ignore: prefer_final_locals
    Object? postBody;

    final queryParams = <QueryParam>[];
    final headerParams = <String, String>{};
    final formParams = <String, String>{};

    const contentTypes = <String>[];


    return apiClient.invokeAPI(
      path,
      'GET',
      queryParams,
      postBody,
      headerParams,
      formParams,
      contentTypes.isEmpty ? null : contentTypes.first,
    );
  }

  /// Deletes an Idle Period
  Future<List<Subscription>?> apiV1SubscriptionUnpayedGet() async {
    final response = await apiV1SubscriptionUnpayedGetWithHttpInfo();
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty && response.statusCode != HttpStatus.noContent) {
      final responseBody = await _decodeBodyBytes(response);
      return (await apiClient.deserializeAsync(responseBody, 'List<Subscription>') as List)
        .cast<Subscription>()
        .toList(growable: false);

    }
    return null;
  }
}
