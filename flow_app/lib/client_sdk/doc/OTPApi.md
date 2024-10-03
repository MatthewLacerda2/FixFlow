# openapi.api.OTPApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiV1OTPPost**](OTPApi.md#apiv1otppost) | **POST** /api/v1/OTP | Creates an OTP for when creating a Business


# **apiV1OTPPost**
> apiV1OTPPost(body)

Creates an OTP for when creating a Business

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = OTPApi();
final body = String(); // String | 

try {
    api_instance.apiV1OTPPost(body);
} catch (e) {
    print('Exception when calling OTPApi->apiV1OTPPost: $e\n');
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

