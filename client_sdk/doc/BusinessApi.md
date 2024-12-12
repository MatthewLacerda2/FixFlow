# openapi.api.BusinessApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiV1BusinessDeactivatePatch**](BusinessApi.md#apiv1businessdeactivatepatch) | **PATCH** /api/v1/Business/deactivate | Deactivates the Business Account of the given Id.  That freezes subscription and stops notifications
[**apiV1BusinessDelete**](BusinessApi.md#apiv1businessdelete) | **DELETE** /api/v1/Business | Deletes the Business
[**apiV1BusinessGet**](BusinessApi.md#apiv1businessget) | **GET** /api/v1/Business | Gets the Business' Data of the given Id.  Used mostly when the User logs-in or opens the app
[**apiV1BusinessPatch**](BusinessApi.md#apiv1businesspatch) | **PATCH** /api/v1/Business | Updates the Business of the given Id
[**apiV1BusinessPost**](BusinessApi.md#apiv1businesspost) | **POST** /api/v1/Business | Registers a Business User


# **apiV1BusinessDeactivatePatch**
> apiV1BusinessDeactivatePatch()

Deactivates the Business Account of the given Id.  That freezes subscription and stops notifications

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = BusinessApi();

try {
    api_instance.apiV1BusinessDeactivatePatch();
} catch (e) {
    print('Exception when calling BusinessApi->apiV1BusinessDeactivatePatch: $e\n');
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1BusinessDelete**
> apiV1BusinessDelete()

Deletes the Business

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = BusinessApi();

try {
    api_instance.apiV1BusinessDelete();
} catch (e) {
    print('Exception when calling BusinessApi->apiV1BusinessDelete: $e\n');
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1BusinessGet**
> BusinessDTO apiV1BusinessGet(businessId)

Gets the Business' Data of the given Id.  Used mostly when the User logs-in or opens the app

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = BusinessApi();
final businessId = businessId_example; // String | 

try {
    final result = api_instance.apiV1BusinessGet(businessId);
    print(result);
} catch (e) {
    print('Exception when calling BusinessApi->apiV1BusinessGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **businessId** | **String**|  | [optional] 

### Return type

[**BusinessDTO**](BusinessDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1BusinessPatch**
> BusinessDTO apiV1BusinessPatch(businessDTO)

Updates the Business of the given Id

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = BusinessApi();
final businessDTO = BusinessDTO(); // BusinessDTO | 

try {
    final result = api_instance.apiV1BusinessPatch(businessDTO);
    print(result);
} catch (e) {
    print('Exception when calling BusinessApi->apiV1BusinessPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **businessDTO** | [**BusinessDTO**](BusinessDTO.md)|  | [optional] 

### Return type

[**BusinessDTO**](BusinessDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1BusinessPost**
> Business apiV1BusinessPost(businessRegisterRequest)

Registers a Business User

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = BusinessApi();
final businessRegisterRequest = BusinessRegisterRequest(); // BusinessRegisterRequest | 

try {
    final result = api_instance.apiV1BusinessPost(businessRegisterRequest);
    print(result);
} catch (e) {
    print('Exception when calling BusinessApi->apiV1BusinessPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **businessRegisterRequest** | [**BusinessRegisterRequest**](BusinessRegisterRequest.md)|  | [optional] 

### Return type

[**Business**](Business.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

