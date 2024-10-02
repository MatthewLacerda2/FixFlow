# openapi.api.IdlePeriodApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiV1IdlePeriodDelete**](IdlePeriodApi.md#apiv1idleperioddelete) | **DELETE** /api/v1/IdlePeriod | Removes Idle days
[**apiV1IdlePeriodGet**](IdlePeriodApi.md#apiv1idleperiodget) | **GET** /api/v1/IdlePeriod | Returns all Idle Periods that contain the given date
[**apiV1IdlePeriodPost**](IdlePeriodApi.md#apiv1idleperiodpost) | **POST** /api/v1/IdlePeriod | Creates an Idle period


# **apiV1IdlePeriodDelete**
> apiV1IdlePeriodDelete(body)

Removes Idle days

### Example
```dart
import 'package:openapi/api.dart';

final api = Openapi().getIdlePeriodApi();
final String body = body_example; // String | 

try {
    api.apiV1IdlePeriodDelete(body);
} catch on DioException (e) {
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

# **apiV1IdlePeriodGet**
> BuiltList<IdlePeriod> apiV1IdlePeriodGet(businessIdlePeriodsRequest)

Returns all Idle Periods that contain the given date

### Example
```dart
import 'package:openapi/api.dart';

final api = Openapi().getIdlePeriodApi();
final BusinessIdlePeriodsRequest businessIdlePeriodsRequest = ; // BusinessIdlePeriodsRequest | 

try {
    final response = api.apiV1IdlePeriodGet(businessIdlePeriodsRequest);
    print(response);
} catch on DioException (e) {
    print('Exception when calling IdlePeriodApi->apiV1IdlePeriodGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **businessIdlePeriodsRequest** | [**BusinessIdlePeriodsRequest**](BusinessIdlePeriodsRequest.md)|  | [optional] 

### Return type

[**BuiltList&lt;IdlePeriod&gt;**](IdlePeriod.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1IdlePeriodPost**
> IdlePeriod apiV1IdlePeriodPost(idlePeriod)

Creates an Idle period

Idle Periods are allowed to overlap

### Example
```dart
import 'package:openapi/api.dart';

final api = Openapi().getIdlePeriodApi();
final IdlePeriod idlePeriod = ; // IdlePeriod | 

try {
    final response = api.apiV1IdlePeriodPost(idlePeriod);
    print(response);
} catch on DioException (e) {
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

