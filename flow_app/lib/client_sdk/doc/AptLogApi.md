# openapi.api.AptLogApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiV1LogsDelete**](AptLogApi.md#apiv1logsdelete) | **DELETE** /api/v1/logs | Deletes the Appointment Log with the given Id
[**apiV1LogsGet**](AptLogApi.md#apiv1logsget) | **GET** /api/v1/logs | Gets a number of filtered Logs
[**apiV1LogsPatch**](AptLogApi.md#apiv1logspatch) | **PATCH** /api/v1/logs | Update the Appointment Log with the given Id
[**apiV1LogsPost**](AptLogApi.md#apiv1logspost) | **POST** /api/v1/logs | Creates a Log


# **apiV1LogsDelete**
> apiV1LogsDelete(body)

Deletes the Appointment Log with the given Id

### Example
```dart
import 'package:openapi/api.dart';

final api = Openapi().getAptLogApi();
final String body = body_example; // String | 

try {
    api.apiV1LogsDelete(body);
} catch on DioException (e) {
    print('Exception when calling AptLogApi->apiV1LogsDelete: $e\n');
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

# **apiV1LogsGet**
> BuiltList<BuiltList<AptLog>> apiV1LogsGet(body)

Gets a number of filtered Logs

### Example
```dart
import 'package:openapi/api.dart';

final api = Openapi().getAptLogApi();
final JsonObject body = Object; // JsonObject | 

try {
    final response = api.apiV1LogsGet(body);
    print(response);
} catch on DioException (e) {
    print('Exception when calling AptLogApi->apiV1LogsGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | **JsonObject**|  | [optional] 

### Return type

[**BuiltList&lt;BuiltList&lt;AptLog&gt;&gt;**](BuiltList.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1LogsPatch**
> AptLog apiV1LogsPatch(updateAptLog)

Update the Appointment Log with the given Id

### Example
```dart
import 'package:openapi/api.dart';

final api = Openapi().getAptLogApi();
final UpdateAptLog updateAptLog = ; // UpdateAptLog | 

try {
    final response = api.apiV1LogsPatch(updateAptLog);
    print(response);
} catch on DioException (e) {
    print('Exception when calling AptLogApi->apiV1LogsPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **updateAptLog** | [**UpdateAptLog**](UpdateAptLog.md)|  | [optional] 

### Return type

[**AptLog**](AptLog.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1LogsPost**
> AptLog apiV1LogsPost(createAptLog)

Creates a Log

Generates a Contact

### Example
```dart
import 'package:openapi/api.dart';

final api = Openapi().getAptLogApi();
final CreateAptLog createAptLog = ; // CreateAptLog | 

try {
    final response = api.apiV1LogsPost(createAptLog);
    print(response);
} catch on DioException (e) {
    print('Exception when calling AptLogApi->apiV1LogsPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **createAptLog** | [**CreateAptLog**](CreateAptLog.md)|  | [optional] 

### Return type

[**AptLog**](AptLog.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

