# openapi.api.AccountsApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiV1AccountsLogoutPost**](AccountsApi.md#apiv1accountslogoutpost) | **POST** /api/v1/accounts/logout | 
[**apiV1AccountsPost**](AccountsApi.md#apiv1accountspost) | **POST** /api/v1/accounts | Login with an email and password


# **apiV1AccountsLogoutPost**
> apiV1AccountsLogoutPost()



### Example
```dart
import 'package:openapi/api.dart';

final api = Openapi().getAccountsApi();

try {
    api.apiV1AccountsLogoutPost();
} catch on DioException (e) {
    print('Exception when calling AccountsApi->apiV1AccountsLogoutPost: $e\n');
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
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1AccountsPost**
> String apiV1AccountsPost(flowLoginRequest)

Login with an email and password

### Example
```dart
import 'package:openapi/api.dart';

final api = Openapi().getAccountsApi();
final FlowLoginRequest flowLoginRequest = ; // FlowLoginRequest | 

try {
    final response = api.apiV1AccountsPost(flowLoginRequest);
    print(response);
} catch on DioException (e) {
    print('Exception when calling AccountsApi->apiV1AccountsPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **flowLoginRequest** | [**FlowLoginRequest**](FlowLoginRequest.md)|  | [optional] 

### Return type

**String**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

