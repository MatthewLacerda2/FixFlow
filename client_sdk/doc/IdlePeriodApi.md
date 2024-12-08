# openapi.api.IdlePeriodApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiV1IdlePeriodDelete**](IdlePeriodApi.md#apiv1idleperioddelete) | **DELETE** /api/v1/IdlePeriod | Deletes an Idle Period
[**apiV1IdlePeriodPost**](IdlePeriodApi.md#apiv1idleperiodpost) | **POST** /api/v1/IdlePeriod | Creates an Idle period


# **apiV1IdlePeriodDelete**
> apiV1IdlePeriodDelete(body)

Deletes an Idle Period

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = IdlePeriodApi();
final body = String(); // String | 

try {
    api_instance.apiV1IdlePeriodDelete(body);
} catch (e) {
    print('Exception when calling IdlePeriodApi->apiV1IdlePeriodDelete: $e\n');
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

# **apiV1IdlePeriodPost**
> IdlePeriod apiV1IdlePeriodPost(idlePeriod)

Creates an Idle period

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = IdlePeriodApi();
final idlePeriod = IdlePeriod(); // IdlePeriod | 

try {
    final result = api_instance.apiV1IdlePeriodPost(idlePeriod);
    print(result);
} catch (e) {
    print('Exception when calling IdlePeriodApi->apiV1IdlePeriodPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **idlePeriod** | [**IdlePeriod**](IdlePeriod.md)|  | [optional] 

### Return type

[**IdlePeriod**](IdlePeriod.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

