//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;


class BusinessApi {
  BusinessApi([ApiClient? apiClient]) : apiClient = apiClient ?? defaultApiClient;

  final ApiClient apiClient;

  /// Deactivates the Business Account with the given Id.  That freezes the subscription, and stops notifications
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [String] body:
  Future<Response> apiV1BusinessDeactivatePatchWithHttpInfo({ String? body, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/Business/deactivate';

    // ignore: prefer_final_locals
    Object? postBody = body;

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

  /// Deactivates the Business Account with the given Id.  That freezes the subscription, and stops notifications
  ///
  /// Parameters:
  ///
  /// * [String] body:
  Future<void> apiV1BusinessDeactivatePatch({ String? body, }) async {
    final response = await apiV1BusinessDeactivatePatchWithHttpInfo( body: body, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
  }

  /// Deletes the Business with the given Id and all it's data owned by it
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [String] body:
  Future<Response> apiV1BusinessDeleteWithHttpInfo({ String? body, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/Business';

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

  /// Deletes the Business with the given Id and all it's data owned by it
  ///
  /// Parameters:
  ///
  /// * [String] body:
  Future<void> apiV1BusinessDelete({ String? body, }) async {
    final response = await apiV1BusinessDeleteWithHttpInfo( body: body, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
  }

  /// Gets the Business with the given Id.  Used when the User logs-in or opens the app
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [String] businessId:
  Future<Response> apiV1BusinessGetWithHttpInfo({ String? businessId, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/Business';

    // ignore: prefer_final_locals
    Object? postBody;

    final queryParams = <QueryParam>[];
    final headerParams = <String, String>{};
    final formParams = <String, String>{};

    if (businessId != null) {
      queryParams.addAll(_queryParams('', 'businessId', businessId));
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

  /// Gets the Business with the given Id.  Used when the User logs-in or opens the app
  ///
  /// Parameters:
  ///
  /// * [String] businessId:
  Future<BusinessDTO?> apiV1BusinessGet({ String? businessId, }) async {
    final response = await apiV1BusinessGetWithHttpInfo( businessId: businessId, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty && response.statusCode != HttpStatus.noContent) {
      return await apiClient.deserializeAsync(await _decodeBodyBytes(response), 'BusinessDTO',) as BusinessDTO;
    
    }
    return null;
  }

  /// Updates the Business with the given Id
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [BusinessDTO] businessDTO:
  Future<Response> apiV1BusinessPatchWithHttpInfo({ BusinessDTO? businessDTO, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/Business';

    // ignore: prefer_final_locals
    Object? postBody = businessDTO;

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

  /// Updates the Business with the given Id
  ///
  /// Parameters:
  ///
  /// * [BusinessDTO] businessDTO:
  Future<BusinessDTO?> apiV1BusinessPatch({ BusinessDTO? businessDTO, }) async {
    final response = await apiV1BusinessPatchWithHttpInfo( businessDTO: businessDTO, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty && response.statusCode != HttpStatus.noContent) {
      return await apiClient.deserializeAsync(await _decodeBodyBytes(response), 'BusinessDTO',) as BusinessDTO;
    
    }
    return null;
  }

  /// Creates a Business User
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [BusinessRegisterRequest] businessRegisterRequest:
  Future<Response> apiV1BusinessPostWithHttpInfo({ BusinessRegisterRequest? businessRegisterRequest, }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/Business';

    // ignore: prefer_final_locals
    Object? postBody = businessRegisterRequest;

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

  /// Creates a Business User
  ///
  /// Parameters:
  ///
  /// * [BusinessRegisterRequest] businessRegisterRequest:
  Future<Business?> apiV1BusinessPost({ BusinessRegisterRequest? businessRegisterRequest, }) async {
    final response = await apiV1BusinessPostWithHttpInfo( businessRegisterRequest: businessRegisterRequest, );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty && response.statusCode != HttpStatus.noContent) {
      return await apiClient.deserializeAsync(await _decodeBodyBytes(response), 'Business',) as Business;
    
    }
    return null;
  }
}
