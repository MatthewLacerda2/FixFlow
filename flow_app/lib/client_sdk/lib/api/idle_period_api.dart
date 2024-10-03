//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;


class IdlePeriodApi {
  IdlePeriodApi([ApiClient? apiClient]) : apiClient = apiClient ?? defaultApiClient;

  final ApiClient apiClient;

  /// Removes Idle days
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [String] body:
  Future<Response> apiV1IdlePeriodDeleteWithHttpInfo({ String? body, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/IdlePeriod';

    // ignore: prefer_final_locals
    Object? postBody = body;

    final queryParams = <QueryParam>[];
    final headerParams = <String, String>{};
    final formParams = <String, String>{};

    const contentTypes = <String>['application/json', 'text/json', 'application/*+json'];


    return apiClient.invokeAPI(
      path,
      'DELETE',
      queryParams,
      postBody,
      headerParams,
      formParams,
      contentTypes.isEmpty ? null : contentTypes.first,
    );
  }

  /// Removes Idle days
  ///
  /// Parameters:
  ///
  /// * [String] body:
  Future<void> apiV1IdlePeriodDelete({ String? body, }) async {
    final response = await apiV1IdlePeriodDeleteWithHttpInfo( body: body, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
  }

  /// Returns all Idle Periods that contain the given date
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [BusinessIdlePeriodsRequest] businessIdlePeriodsRequest:
  Future<Response> apiV1IdlePeriodGetWithHttpInfo({ BusinessIdlePeriodsRequest? businessIdlePeriodsRequest, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/IdlePeriod';

    // ignore: prefer_final_locals
    Object? postBody = businessIdlePeriodsRequest;

    final queryParams = <QueryParam>[];
    final headerParams = <String, String>{};
    final formParams = <String, String>{};

    const contentTypes = <String>['application/json', 'text/json', 'application/*+json'];


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

  /// Returns all Idle Periods that contain the given date
  ///
  /// Parameters:
  ///
  /// * [BusinessIdlePeriodsRequest] businessIdlePeriodsRequest:
  Future<List<IdlePeriod>?> apiV1IdlePeriodGet({ BusinessIdlePeriodsRequest? businessIdlePeriodsRequest, }) async {
    final response = await apiV1IdlePeriodGetWithHttpInfo( businessIdlePeriodsRequest: businessIdlePeriodsRequest, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty && response.statusCode != HttpStatus.noContent) {
      final responseBody = await _decodeBodyBytes(response);
      return (await apiClient.deserializeAsync(responseBody, 'List<IdlePeriod>') as List)
        .cast<IdlePeriod>()
        .toList(growable: false);

    }
    return null;
  }

  /// Creates an Idle period
  ///
  /// Idle Periods are allowed to overlap
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [IdlePeriod] idlePeriod:
  Future<Response> apiV1IdlePeriodPostWithHttpInfo({ IdlePeriod? idlePeriod, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/IdlePeriod';

    // ignore: prefer_final_locals
    Object? postBody = idlePeriod;

    final queryParams = <QueryParam>[];
    final headerParams = <String, String>{};
    final formParams = <String, String>{};

    const contentTypes = <String>['application/json', 'text/json', 'application/*+json'];


    return apiClient.invokeAPI(
      path,
      'POST',
      queryParams,
      postBody,
      headerParams,
      formParams,
      contentTypes.isEmpty ? null : contentTypes.first,
    );
  }

  /// Creates an Idle period
  ///
  /// Idle Periods are allowed to overlap
  ///
  /// Parameters:
  ///
  /// * [IdlePeriod] idlePeriod:
  Future<IdlePeriod?> apiV1IdlePeriodPost({ IdlePeriod? idlePeriod, }) async {
    final response = await apiV1IdlePeriodPostWithHttpInfo( idlePeriod: idlePeriod, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty && response.statusCode != HttpStatus.noContent) {
      return await apiClient.deserializeAsync(await _decodeBodyBytes(response), 'IdlePeriod',) as IdlePeriod;
    
    }
    return null;
  }
}
