//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;


class AptContactApi {
  AptContactApi([ApiClient? apiClient]) : apiClient = apiClient ?? defaultApiClient;

  final ApiClient apiClient;

  /// Deletes the Appointment Contact of the given Id
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [String] body:
  Future<Response> apiV1ContactsDeleteWithHttpInfo({ String? body, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/contacts';

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

  /// Deletes the Appointment Contact of the given Id
  ///
  /// Parameters:
  ///
  /// * [String] body:
  Future<void> apiV1ContactsDelete({ String? body, }) async {
    final response = await apiV1ContactsDeleteWithHttpInfo( body: body, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
  }

  /// Get a number of Contacts
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [String] clientName:
  ///
  /// * [DateTime] minDateTime:
  ///
  /// * [DateTime] maxDateTime:
  ///
  /// * [int] offset:
  ///
  /// * [int] limit:
  Future<Response> apiV1ContactsGetWithHttpInfo({ String? clientName, DateTime? minDateTime, DateTime? maxDateTime, int? offset, int? limit, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/contacts';

    // ignore: prefer_final_locals
    Object? postBody;

    final queryParams = <QueryParam>[];
    final headerParams = <String, String>{};
    final formParams = <String, String>{};

    if (clientName != null) {
      queryParams.addAll(_queryParams('', 'clientName', clientName));
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

  /// Get a number of Contacts
  ///
  /// Parameters:
  ///
  /// * [String] clientName:
  ///
  /// * [DateTime] minDateTime:
  ///
  /// * [DateTime] maxDateTime:
  ///
  /// * [int] offset:
  ///
  /// * [int] limit:
  Future<List<AptContact>?> apiV1ContactsGet({ String? clientName, DateTime? minDateTime, DateTime? maxDateTime, int? offset, int? limit, }) async {
    final response = await apiV1ContactsGetWithHttpInfo( clientName: clientName, minDateTime: minDateTime, maxDateTime: maxDateTime, offset: offset, limit: limit, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty && response.statusCode != HttpStatus.noContent) {
      final responseBody = await _decodeBodyBytes(response);
      return (await apiClient.deserializeAsync(responseBody, 'List<AptContact>') as List)
        .cast<AptContact>()
        .toList(growable: false);

    }
    return null;
  }

  /// Update the Appointment Contact's of the given Id
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [UpdateAptContact] updateAptContact:
  Future<Response> apiV1ContactsPatchWithHttpInfo({ UpdateAptContact? updateAptContact, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/contacts';

    // ignore: prefer_final_locals
    Object? postBody = updateAptContact;

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

  /// Update the Appointment Contact's of the given Id
  ///
  /// Parameters:
  ///
  /// * [UpdateAptContact] updateAptContact:
  Future<AptContact?> apiV1ContactsPatch({ UpdateAptContact? updateAptContact, }) async {
    final response = await apiV1ContactsPatchWithHttpInfo( updateAptContact: updateAptContact, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty && response.statusCode != HttpStatus.noContent) {
      return await apiClient.deserializeAsync(await _decodeBodyBytes(response), 'AptContact',) as AptContact;
    
    }
    return null;
  }
}
