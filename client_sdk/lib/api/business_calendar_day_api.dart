//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;


class BusinessCalendarDayApi {
  BusinessCalendarDayApi([ApiClient? apiClient]) : apiClient = apiClient ?? defaultApiClient;

  final ApiClient apiClient;

  /// Gets all the events for this month
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [String] businessId:
  ///
  /// * [int] year:
  ///
  /// * [int] month:
  Future<Response> apiV1BusinessCalendarDayGetWithHttpInfo({ String? businessId, int? year, int? month, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/BusinessCalendarDay';

    // ignore: prefer_final_locals
    Object? postBody;

    final queryParams = <QueryParam>[];
    final headerParams = <String, String>{};
    final formParams = <String, String>{};

    if (businessId != null) {
      queryParams.addAll(_queryParams('', 'businessId', businessId));
    }
    if (year != null) {
      queryParams.addAll(_queryParams('', 'year', year));
    }
    if (month != null) {
      queryParams.addAll(_queryParams('', 'month', month));
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

  /// Gets all the events for this month
  ///
  /// Parameters:
  ///
  /// * [String] businessId:
  ///
  /// * [int] year:
  ///
  /// * [int] month:
  Future<List<BusinessCalendarDay>?> apiV1BusinessCalendarDayGet({ String? businessId, int? year, int? month, }) async {
    final response = await apiV1BusinessCalendarDayGetWithHttpInfo( businessId: businessId, year: year, month: month, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty && response.statusCode != HttpStatus.noContent) {
      final responseBody = await _decodeBodyBytes(response);
      return (await apiClient.deserializeAsync(responseBody, 'List<BusinessCalendarDay>') as List)
        .cast<BusinessCalendarDay>()
        .toList(growable: false);

    }
    return null;
  }
}
