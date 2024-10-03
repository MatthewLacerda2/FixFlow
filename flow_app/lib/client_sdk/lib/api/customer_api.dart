//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//
// @dart=2.18

// ignore_for_file: unused_element, unused_import
// ignore_for_file: always_put_required_named_parameters_first
// ignore_for_file: constant_identifier_names
// ignore_for_file: lines_longer_than_80_chars

part of openapi.api;

class CustomerApi {
  CustomerApi([ApiClient? apiClient])
      : apiClient = apiClient ?? defaultApiClient;

  final ApiClient apiClient;

  /// Gets a number of filtered Customers
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [String] businessId:
  ///
  /// * [int] offset:
  ///
  /// * [int] limit:
  ///
  /// * [String] fullname:
  Future<Response> apiV1CustomerGetWithHttpInfo({
    String? businessId,
    int? offset,
    int? limit,
    String? fullname,
  }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/Customer';

    // ignore: prefer_final_locals
    Object? postBody;

    final queryParams = <QueryParam>[];
    final headerParams = <String, String>{};
    final formParams = <String, String>{};

    if (businessId != null) {
      queryParams.addAll(_queryParams('', 'businessId', businessId));
    }
    if (offset != null) {
      queryParams.addAll(_queryParams('', 'offset', offset));
    }
    if (limit != null) {
      queryParams.addAll(_queryParams('', 'limit', limit));
    }
    if (fullname != null) {
      queryParams.addAll(_queryParams('', 'fullname', fullname));
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

  /// Gets a number of filtered Customers
  ///
  /// Parameters:
  ///
  /// * [String] businessId:
  ///
  /// * [int] offset:
  ///
  /// * [int] limit:
  ///
  /// * [String] fullname:
  Future<List<List<CustomerDTO>>?> apiV1CustomerGet({
    String? businessId,
    int? offset,
    int? limit,
    String? fullname,
  }) async {
    final response = await apiV1CustomerGetWithHttpInfo(
      businessId: businessId,
      offset: offset,
      limit: limit,
      fullname: fullname,
    );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty &&
        response.statusCode != HttpStatus.noContent) {
      final responseBody = await _decodeBodyBytes(response);
      return (await apiClient.deserializeAsync(
              responseBody, 'List<List<CustomerDTO>>') as List)
          .map((e) => (e as List).cast<CustomerDTO>())
          .toList(growable: false);
    }
    return null;
  }

  /// Get Customer Record in the Business.  Credentials, but also schedules and logs history
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [String] id (required):
  ///
  /// * [String] customerId:
  Future<Response> apiV1CustomerIdGetWithHttpInfo(
    String id, {
    String? customerId,
  }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/Customer/{id}'.replaceAll('{id}', id);

    // ignore: prefer_final_locals
    Object? postBody;

    final queryParams = <QueryParam>[];
    final headerParams = <String, String>{};
    final formParams = <String, String>{};

    if (customerId != null) {
      queryParams.addAll(_queryParams('', 'customerId', customerId));
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

  /// Get Customer Record in the Business.  Credentials, but also schedules and logs history
  ///
  /// Parameters:
  ///
  /// * [String] id (required):
  ///
  /// * [String] customerId:
  Future<CustomerRecord?> apiV1CustomerIdGet(
    String id, {
    String? customerId,
  }) async {
    final response = await apiV1CustomerIdGetWithHttpInfo(
      id,
      customerId: customerId,
    );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty &&
        response.statusCode != HttpStatus.noContent) {
      return await apiClient.deserializeAsync(
        await _decodeBodyBytes(response),
        'CustomerRecord',
      ) as CustomerRecord;
    }
    return null;
  }

  /// Updates the Customer with the given Id
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [CustomerDTO] customerDTO:
  Future<Response> apiV1CustomerPatchWithHttpInfo({
    CustomerDTO? customerDTO,
  }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/Customer';

    // ignore: prefer_final_locals
    Object? postBody = customerDTO;

    final queryParams = <QueryParam>[];
    final headerParams = <String, String>{};
    final formParams = <String, String>{};

    const contentTypes = <String>[
      'application/json',
      'text/json',
      'application/*+json'
    ];

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

  /// Updates the Customer with the given Id
  ///
  /// Parameters:
  ///
  /// * [CustomerDTO] customerDTO:
  Future<CustomerDTO?> apiV1CustomerPatch({
    CustomerDTO? customerDTO,
  }) async {
    final response = await apiV1CustomerPatchWithHttpInfo(
      customerDTO: customerDTO,
    );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty &&
        response.statusCode != HttpStatus.noContent) {
      return await apiClient.deserializeAsync(
        await _decodeBodyBytes(response),
        'CustomerDTO',
      ) as CustomerDTO;
    }
    return null;
  }

  /// Create a Customer Account
  ///
  /// Note: This method returns the HTTP [Response].
  ///
  /// Parameters:
  ///
  /// * [CustomerCreate] customerCreate:
  Future<Response> apiV1CustomerPostWithHttpInfo({
    CustomerCreate? customerCreate,
  }) async {
    // ignore: prefer_const_declarations
    final path = r'/api/v1/Customer';

    // ignore: prefer_final_locals
    Object? postBody = customerCreate;

    final queryParams = <QueryParam>[];
    final headerParams = <String, String>{};
    final formParams = <String, String>{};

    const contentTypes = <String>[
      'application/json',
      'text/json',
      'application/*+json'
    ];

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

  /// Create a Customer Account
  ///
  /// Parameters:
  ///
  /// * [CustomerCreate] customerCreate:
  Future<CustomerDTO?> apiV1CustomerPost({
    CustomerCreate? customerCreate,
  }) async {
    final response = await apiV1CustomerPostWithHttpInfo(
      customerCreate: customerCreate,
    );
    if (response.statusCode >= HttpStatus.badRequest) {
      throw ApiException(response.statusCode, await _decodeBodyBytes(response));
    }
    // When a remote server returns no body with a status of 204, we shall not decode it.
    // At the time of writing this, `dart:convert` will throw an "Unexpected end of input"
    // FormatException when trying to decode an empty string.
    if (response.body.isNotEmpty &&
        response.statusCode != HttpStatus.noContent) {
      return await apiClient.deserializeAsync(
        await _decodeBodyBytes(response),
        'CustomerDTO',
      ) as CustomerDTO;
    }
    return null;
  }
}
