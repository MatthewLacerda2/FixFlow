//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;


class AptScheduleApi {
  AptScheduleApi([ApiClient? apiClient]) : apiClient = apiClient ?? defaultApiClient;

  final ApiClient apiClient;

  /// Deletes the Appointment Schedule with the given Id
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [String] body:
  Future<Response> apiV1SchedulesDeleteWithHttpInfo({ String? body, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/schedules';

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

  /// Deletes the Appointment Schedule with the given Id
  ///
  /// Parameters:
  ///
  /// * [String] body:
  Future<void> apiV1SchedulesDelete({ String? body, }) async {
    final response = await apiV1SchedulesDeleteWithHttpInfo( body: body, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
  }

  /// Gets a number of filtered Schedules
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [String] client:
  ///
  /// * [String] service:
  ///
  /// * [double] minPrice:
  ///
  /// * [double] maxPrice:
  ///
  /// * [DateTime] minDateTime:
  ///
  /// * [DateTime] maxDateTime:
  ///
  /// * [int] offset:
  ///
  /// * [int] limit:
  Future<Response> apiV1SchedulesGetWithHttpInfo({ String? client, String? service, double? minPrice, double? maxPrice, DateTime? minDateTime, DateTime? maxDateTime, int? offset, int? limit, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/schedules';

    // ignore: prefer_final_locals
    Object? postBody;

    final queryParams = <QueryParam>[];
    final headerParams = <String, String>{};
    final formParams = <String, String>{};

    if (client != null) {
      queryParams.addAll(_queryParams('', 'client', client));
    }
    if (service != null) {
      queryParams.addAll(_queryParams('', 'service', service));
    }
    if (minPrice != null) {
      queryParams.addAll(_queryParams('', 'minPrice', minPrice));
    }
    if (maxPrice != null) {
      queryParams.addAll(_queryParams('', 'maxPrice', maxPrice));
    }
    if (minDateTime != null) {
      queryParams.addAll(_queryParams('', 'minDateTime', minDateTime));
    }
    if (maxDateTime != null) {
      queryParams.addAll(_queryParams('', 'maxDateTime', maxDateTime));
    }
    if (offset != null) {
      queryParams.addAll(_queryParams('', 'offset', offset));
    }
    if (limit != null) {
      queryParams.addAll(_queryParams('', 'limit', limit));
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

  /// Gets a number of filtered Schedules
  ///
  /// Parameters:
  ///
  /// * [String] client:
  ///
  /// * [String] service:
  ///
  /// * [double] minPrice:
  ///
  /// * [double] maxPrice:
  ///
  /// * [DateTime] minDateTime:
  ///
  /// * [DateTime] maxDateTime:
  ///
  /// * [int] offset:
  ///
  /// * [int] limit:
  Future<List<AptSchedule>?> apiV1SchedulesGet({ String? client, String? service, double? minPrice, double? maxPrice, DateTime? minDateTime, DateTime? maxDateTime, int? offset, int? limit, }) async {
    final response = await apiV1SchedulesGetWithHttpInfo( client: client, service: service, minPrice: minPrice, maxPrice: maxPrice, minDateTime: minDateTime, maxDateTime: maxDateTime, offset: offset, limit: limit, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty && response.statusCode != HttpStatus.noContent) {
      final responseBody = await _decodeBodyBytes(response);
      return (await apiClient.deserializeAsync(responseBody, 'List<AptSchedule>') as List)
        .cast<AptSchedule>()
        .toList(growable: false);

    }
    return null;
  }

  /// Update the Appointment Schedule of the given Id
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [AptSchedule] aptSchedule:
  Future<Response> apiV1SchedulesPatchWithHttpInfo({ AptSchedule? aptSchedule, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/schedules';

    // ignore: prefer_final_locals
    Object? postBody = aptSchedule;

    final queryParams = <QueryParam>[];
    final headerParams = <String, String>{};
    final formParams = <String, String>{};

    const contentTypes = <String>['application/json', 'text/json', 'application/*+json'];


    return apiClient.invokeAPI(
      path,
      'PATCH',
      queryParams,
      postBody,
      headerParams,
      formParams,
      contentTypes.isEmpty ? null : contentTypes.first,
    );
  }

  /// Update the Appointment Schedule of the given Id
  ///
  /// Parameters:
  ///
  /// * [AptSchedule] aptSchedule:
  Future<AptSchedule?> apiV1SchedulesPatch({ AptSchedule? aptSchedule, }) async {
    final response = await apiV1SchedulesPatchWithHttpInfo( aptSchedule: aptSchedule, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty && response.statusCode != HttpStatus.noContent) {
      return await apiClient.deserializeAsync(await _decodeBodyBytes(response), 'AptSchedule',) as AptSchedule;
    
    }
    return null;
  }

  /// Creates an Appointment Schedule
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [CreateAptSchedule] createAptSchedule:
  Future<Response> apiV1SchedulesPostWithHttpInfo({ CreateAptSchedule? createAptSchedule, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/schedules';

    // ignore: prefer_final_locals
    Object? postBody = createAptSchedule;

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

  /// Creates an Appointment Schedule
  ///
  /// Parameters:
  ///
  /// * [CreateAptSchedule] createAptSchedule:
  Future<AptSchedule?> apiV1SchedulesPost({ CreateAptSchedule? createAptSchedule, }) async {
    final response = await apiV1SchedulesPostWithHttpInfo( createAptSchedule: createAptSchedule, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty && response.statusCode != HttpStatus.noContent) {
      return await apiClient.deserializeAsync(await _decodeBodyBytes(response), 'AptSchedule',) as AptSchedule;
    
    }
    return null;
  }
}
