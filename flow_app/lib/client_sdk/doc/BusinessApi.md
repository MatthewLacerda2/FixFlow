# openapi.api.BusinessApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiV1BusinessDeactivatePatch**](BusinessApi.md#apiv1businessdeactivatepatch) | **PATCH** /api/v1/Business/deactivate | Deactivates the Business Account with the given Id.  That freezes the subscription, and stops notifications
[**apiV1BusinessDelete**](BusinessApi.md#apiv1businessdelete) | **DELETE** /api/v1/Business | Deletes the Business with the given Id and all it's data owned by it
[**apiV1BusinessGet**](BusinessApi.md#apiv1businessget) | **GET** /api/v1/Business | Gets the Business with the given Id.  Used when the User logs-in or opens the app
[**apiV1BusinessPatch**](BusinessApi.md#apiv1businesspatch) | **PATCH** /api/v1/Business | Updates the Business with the given Id
[**apiV1BusinessPost**](BusinessApi.md#apiv1businesspost) | **POST** /api/v1/Business | Creates a Business User


# **apiV1BusinessDeactivatePatch**
> apiV1BusinessDeactivatePatch(body)

Deactivates the Business Account with the given Id.  That freezes the subscription, and stops notifications

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = BusinessApi();
final body = String(); // String | 

try {
    api_instance.apiV1BusinessDeactivatePatch(body);
} catch (e) {
    print('Exception when calling BusinessApi->apiV1BusinessDeactivatePatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | **String**|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1BusinessDelete**
> apiV1BusinessDelete(body)

Deletes the Business with the given Id and all it's data owned by it

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = BusinessApi();
final body = String(); // String | 

try {
    api_instance.apiV1BusinessDelete(body);
} catch (e) {
    print('Exception when calling BusinessApi->apiV1BusinessDelete: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | **String**|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1BusinessGet**
> Business apiV1BusinessGet(body)

Gets the Business with the given Id.  Used when the User logs-in or opens the app

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = BusinessApi();
final body = String(); // String | 

try {
    final result = api_instance.apiV1BusinessGet(body);
    print(result);
} catch (e) {
    print('Exception when calling BusinessApi->apiV1BusinessGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | **String**|  | [optional] 

### Return type

[**Business**](Business.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1BusinessPatch**
> Business apiV1BusinessPatch(business)

Updates the Business with the given Id

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = BusinessApi();
final business = Business(); // Business | 

try {
    final result = api_instance.apiV1BusinessPatch(business);
    print(result);
} catch (e) {
    print('Exception when calling BusinessApi->apiV1BusinessPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **business** | [**Business**](Business.md)|  | [optional] 

### Return type

[**Business**](Business.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1BusinessPost**
> Business apiV1BusinessPost(businessRegisterRequest)

Creates a Business User

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

